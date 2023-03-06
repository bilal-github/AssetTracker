using System.Data.SqlClient;
using System.Data;
namespace AssetTracker.Models
{
    public class CategoryRepository : ICategory
    {
        private readonly IConfiguration _configuration;

        public CategoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            try
            {
                string connectionString = _configuration.GetConnectionString("ItemsConnectionString");
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT CategoryId, CategoryName from Categories";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        conn.Open();

                        SqlDataReader reader;
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Category category = new Category();
                            category.CategoryId = reader.GetInt32("CategoryId");
                            category.CategoryName = reader["CategoryName"].ToString();
                            categories.Add(category);
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return categories;
        }

        public string GetCategoryNameById(int categoryId)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ItemsConnectionString");
                string query = "SELECT CategoryName " +
                                "FROM Categories " +
                                "WHERE CategoryId = @CategoryId";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@CategoryId", categoryId);
                        conn.Open();

                        SqlDataReader reader;
                        reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            return reader.GetString("CategoryName");
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }
    }
}
