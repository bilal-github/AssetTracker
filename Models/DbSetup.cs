using System.Data.SqlClient;

namespace AssetTracker.Models
{
    public class DbSetup
    {
        private const string DATABASE_NAME = "ItemsDB";

        public static void SetUpDatabase(string serverName)
        {
            // connection string for master database
            string connectionString = $"Data Source={serverName};Initial Catalog=master;Integrated Security=True";

            CreateDatabaseIfNotExists(connectionString, DATABASE_NAME);

            // connect to the new database
            string connectionStringForNewDatabase = $"Data Source={serverName};Initial Catalog={DATABASE_NAME};Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionStringForNewDatabase))
            {
                connection.Open();

                string categoriesTableColumns = "CategoryId INT IDENTITY(1,1) PRIMARY KEY, " +
                    "CategoryName VARCHAR(500) NOT NULL ";

                string itemsTableColumns = "ItemId INT IDENTITY(1,1) PRIMARY KEY, " +
                                   "ItemName VARCHAR(500) NOT NULL, " +
                                   "CategoryId INT FOREIGN KEY REFERENCES dbo.Categories(CategoryId) NOT NULL, " +
                                   "Value DECIMAL(10,2) NOT NULL, ";

                CreateTableIfNotExists(connection, "Categories", categoriesTableColumns);
                CreateTableIfNotExists(connection, "Items", itemsTableColumns);

                SeedDatabase(connection);
               
            }
        }

        private static void CreateDatabaseIfNotExists(string connectionString, string databaseName)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            builder.InitialCatalog = "master"; // connect to the master database
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand($"SELECT db_id('{databaseName}')", connection);
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return;
                    }

                    command = new SqlCommand($"CREATE DATABASE [{databaseName}]", connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void CreateTableIfNotExists(SqlConnection connection, string tableName, string tableColumns)
        {
            try
            {

                SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'", connection);
                int tableCount = (int)command.ExecuteScalar();

                if (tableCount > 0)
                {
                    return;
                }

                command = new SqlCommand($"CREATE TABLE [{tableName}] ({tableColumns})", connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static bool CheckIfSeedingIsNeeded(SqlConnection connection, string tableName)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT COUNT(*) FROM {tableName}";
                int countRecords = (int)command.ExecuteScalar();

                if (countRecords > 0)
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return true;
            }
            return true;
        }

        private static void SeedDatabase(SqlConnection connection)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                if (CheckIfSeedingIsNeeded(connection,"Categories"))
                {
                    string insertIntoCategories = "INSERT INTO Categories(CategoryName) " +
                                           "VALUES " +
                                           "('Electronics'), " +
                                           "('Kitchen'), " +
                                           "('Clothing');";
                    command.CommandText = insertIntoCategories;
                    command.ExecuteNonQuery();
                }



                if (CheckIfSeedingIsNeeded(connection, "Items"))
                {
                    string insertIntoItems = "INSERT INTO Items(ItemName, CategoryId, Value) " +
                                            "VALUES " +
                                            "('TV', 1, 500.00), " +
                                            "('Phone', 1, 800.00)," +
                                            "('Knives', 2, 50.00), " +
                                            "('Pan', 2, 30.00), " +
                                            "('Shirts', 3, 20.00), " +
                                            "('Pants', 3, 30.00);";

                    command.CommandText = insertIntoItems;
                    command.ExecuteNonQuery();
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
