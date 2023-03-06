using System.Data.SqlClient;
using System.Data;
namespace AssetTracker.Models
{
    public class CategoryRepository
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
    }
}
