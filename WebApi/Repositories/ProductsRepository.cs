using Dapper;
using System.Data.SqlClient;
using WebApi.Configurations;
using WebApi.Entities;

namespace WebApi.Repositories
{
    public class ListProductsFilters
    {
        public required int? CategoryID;
    }

    public class ProductsRepository
    {
        private string _connectionString;

        public ProductsRepository(DatabaseSettings databaseSettings)
        {
            this._connectionString = databaseSettings.DefaultConnection;
        }

        public void CreateProduct(Product product)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                var id = connection.QuerySingle<int>(
                    "INSERT INTO Products (ProductName, SupplierID, CategoryID," +
                    "QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued) " +
                    "OUTPUT INSERTED.ProductID " +
                    "VALUES (@ProductName, @SupplierID, @CategoryID, " +
                    "@QuantityPerUnit, @UnitPrice, @UnitsInStock,@UnitsOnOrder,@ReorderLevel, @Discontinued);",
                    product
                );

                product.ProductID = id;
            }
        }

        public Product? GetProduct(int productId)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                return connection.QueryFirstOrDefault<Product>(
                    "SELECT * FROM Products WHERE ProductID = @Id;",
                    new
                    {
                        Id = productId,
                    }
                );
            }
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                var affectedRows = connection.Execute(
                    "UPDATE Products SET ProductName=@ProductName, SupplierID=@SupplierID, CategoryID=@CategoryID, " +
                    "QuantityPerUnit=@QuantityPerUnit, UnitPrice=@UnitPrice, UnitsInStock=@UnitsInStock, UnitsOnOrder=@UnitsOnOrder, ReorderLevel=@ReorderLevel, Discontinued=@Discontinued " +
                     "WHERE CategoryID=@CategoryID",
                    product
                );

                if (affectedRows == 0)
                {
                    throw new InvalidOperationException(
                        $"Product with ID {product.ProductID} does not exist.");
                }
            }
        }

        public void DeleteProduct(int productId)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Execute(
                     "DELETE FROM Products WHERE ProductID = @Id;",
                    new { Id = productId }
                );
            }
        }

        public List<Product> ListProducts(ListProductsFilters? filters = null, PaginationParams? pagination = null)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                if (pagination == null)
                {
                    return connection.Query<Product>(
                        "SELECT * FROM Products WHERE @CategoryID IS NULL OR CategoryID=@CategoryID;",
                        new { CategoryID = filters?.CategoryID }
                    )
                        .ToList();
                }

                return connection.Query<Product>(
                    "SELECT * FROM Products WHERE (@CategoryID IS NULL OR CategoryID=@CategoryID) " +
                    "ORDER BY ProductID " +                    "OFFSET (@PageNumber - 1) * @PageSize ROWS " +
                    "FETCH NEXT @PageSize ROWS ONLY;",
                    new {
                        filters?.CategoryID,
                        pagination.PageNumber,
                        pagination.PageSize
                    }
                ).ToList();
            }
        }
    }
}
