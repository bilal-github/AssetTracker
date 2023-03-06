using System.Data.SqlClient;
using System.Data;

namespace AssetTracker.Models
{
    public class ItemRepository : IItem
    {
        private readonly IConfiguration _configuration;

        public ItemRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Item> GetAllItems()
        {
            List<Item> items = new List<Item>();
            try
            {
                string connectionString = _configuration.GetConnectionString("ItemsConnectionString");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT i.ItemId, i.ItemName, i.CategoryId, c.CategoryName, i.Value " +
                                    "FROM Items i " +
                                    "INNER JOIN Categories c " +
                                    "ON i.CategoryId = c.CategoryId";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        conn.Open();

                        SqlDataReader reader;
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Item item = new Item();
                            item.ItemId = reader.GetInt32("ItemId");
                            item.ItemName = reader.GetString("ItemName").ToString();
                            item.CategoryId = reader.GetInt32("CategoryId");
                            item.Value = reader.GetDecimal(reader.GetOrdinal("Value"));
                            items.Add(item);
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return items;
        }

        public bool CheckIfItemExists(Item item)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ItemsConnectionString");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string selectQuery = "SELECT * " +
                                        "FROM Items " +
                                        "WHERE ItemName = @ItemName AND CategoryId = @CategoryId AND Value = @Value";
                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, conn))
                    {
                        selectCommand.Parameters.AddWithValue("@ItemName", item.ItemName);
                        selectCommand.Parameters.AddWithValue("@CategoryId", item.CategoryId);
                        selectCommand.Parameters.AddWithValue("@Value", item.Value);

                        conn.Open();
                        SqlDataReader reader = selectCommand.ExecuteReader();

                        if (reader.HasRows)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public string InsertItem(Item item)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ItemsConnectionString");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string insertQuery = "INSERT INTO Items(ItemName, CategoryId, Value) " +
                                "Values(@ItemName, @CategoryId, @Value);" +
                                "SELECT SCOPE_IDENTITY()";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, conn))
                    {
                        conn.Open();
                        insertCommand.Parameters.AddWithValue("@ItemName", item.ItemName);
                        insertCommand.Parameters.AddWithValue("@CategoryId", item.CategoryId);
                        insertCommand.Parameters.AddWithValue("@Value", item.Value);
                        item.ItemId = Convert.ToInt32(insertCommand.ExecuteScalar());
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return "Item added successfully";
        }

        public int DeleteItem(int id)
        {
            int rowsAffected = 0;
            try
            {
                string connectionString = _configuration.GetConnectionString("ItemsConnectionString");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Items " +
                                    "WHERE ItemId = @ItemId";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("ItemId", id);
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rowsAffected;
        }
    }
}
