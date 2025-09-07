using Dapper;
using System.Data.SqlClient;
using WebApi.Configurations;
using WebApi.Entities;

namespace WebApi.Repositories
{
    public class CategoriesRepository
    {
        private string _connectionString;

        public CategoriesRepository(DatabaseSettings databaseSettings)
        {
            this._connectionString = databaseSettings.DefaultConnection;
        }

        public void CreateCategory(Category category)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                var id = connection.QuerySingle<int>(
                    "INSERT INTO Categories (CategoryName, Description) " +
                    "OUTPUT INSERTED.CategoryID " +
                    "VALUES (@CategoryName, @Description);",
                    category
                );

                category.CategoryID = id;
            }
        }

        public Category? GetCategory(int categoryId)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                return connection.QueryFirstOrDefault<Category>(
                    "SELECT * FROM Categories WHERE CategoryID = @Id;",
                    new
                    {
                        Id = categoryId,
                    }
                );
            }
        }

        public void UpdateCategory(Category category)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                var affectedRows = connection.Execute(
                     "UPDATE Categories SET CategoryName=@CategoryName, Description=@Description" +
                     " WHERE CategoryID=@CategoryID",
                    category
                );

                if (affectedRows == 0)
                {
                    throw new InvalidOperationException(
                        $"Category with ID {category.CategoryID} does not exist.");
                }
            }
        }

        public void DeleteCategory(int categoryId)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Execute(
                     "DELETE FROM Categories WHERE CategoryID = @Id;",
                    new { Id = categoryId }
                );
            }
        }

        public List<Category> ListCategories()
        {
            using (SqlConnection connection = new(_connectionString))
            {
                return connection.Query<Category>("SELECT * FROM Categories;").ToList();
            }
        }
    }
}
