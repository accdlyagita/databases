using System;
using System.Data.SqlClient;

namespace PetStoreApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-00L2CO6\\SQLEXPRESS;Database=PetStoreDB;Trusted_Connection=True;";
            SqlConnection conn = new SqlConnection(connectionString);

            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("0 - Display All Tables");
                Console.WriteLine("1 - Select Table Data");
                Console.WriteLine("2 - Insert New Pet Record");
                Console.WriteLine("3 - Display Orders with JOIN");
                Console.WriteLine("4 - Filter Products by Price");
                Console.WriteLine("5 - Get Aggregate Stock Data");
                Console.WriteLine("6 - Delete Record");
                Console.WriteLine("7 - Delete Table Record");
                Console.WriteLine("8 - Update Pet Record");
                Console.WriteLine("9 - Exit");
                Console.Write("Enter option number: ");

                if (!int.TryParse(Console.ReadLine(), out int option) || option < 0 || option > 9)
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                    continue;
                }

                if (option == 9)
                {
                    Console.WriteLine("Exiting the program...");
                    break;
                }

                switch (option)
                {
                    case 0:
                        DisplayTables(conn);
                        break;
                    case 1:
                        SelectTableData(conn);
                        break;
                    case 2:
                        InsertPetRecord(conn);
                        break;
                    case 3:
                        DisplayOrdersWithJoin(conn);
                        break;
                    case 4:
                        FilterProductsByPrice(conn);
                        break;
                    case 5:
                        GetAggregateStock(conn);
                        break;
                    case 6:
                        DeleteRecord(conn);
                        break;
                    case 7:
                        DeleteTableRecord(conn);
                        break;
                    case 8:
                        UpdatePetRecord(conn);
                        break;
                }

                Console.WriteLine();
            }
        }

        static void DisplayTables(SqlConnection conn)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader[0].ToString());
            }
            conn.Close();
        }

        static void SelectTableData(SqlConnection conn)
        {
            Console.WriteLine("Enter table name:");
            string tableName = Console.ReadLine();

            conn.Open();
            SqlCommand cmd = new SqlCommand($"SELECT * FROM {tableName}", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i] + "\t");
                }
                Console.WriteLine();
            }
            conn.Close();
        }

        static void InsertPetRecord(SqlConnection conn)
        {
            Console.WriteLine("Enter Pet Name, Species, Breed, Age (comma-separated):");
            string[] petData = Console.ReadLine().Split(',');

            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Pet (Name, Species, Breed, Age) VALUES (@Name, @Species, @Breed, @Age)", conn);
            cmd.Parameters.AddWithValue("@Name", petData[0].Trim());
            cmd.Parameters.AddWithValue("@Species", petData[1].Trim());
            cmd.Parameters.AddWithValue("@Breed", petData[2].Trim());
            cmd.Parameters.AddWithValue("@Age", Convert.ToInt32(petData[3].Trim()));
            cmd.ExecuteNonQuery();
            conn.Close();

            Console.WriteLine("Pet added successfully.");
        }

        static void DisplayOrdersWithJoin(SqlConnection conn)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(
                "SELECT o.OrderID, o.OrderDate, p.Name, op.Quantity FROM [Order] o " +
                "JOIN OrderProduct op ON o.OrderID = op.OrderID " +
                "JOIN Product p ON op.ProductID = p.ProductID", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["OrderID"]}, {reader["OrderDate"]}, {reader["Name"]}, {reader["Quantity"]}");
            }
            conn.Close();
        }

        static void FilterProductsByPrice(SqlConnection conn)
        {
            Console.WriteLine("Enter price to filter:");
            if (decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Product WHERE Price < @Price", conn);
                cmd.Parameters.AddWithValue("@Price", price);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["ProductID"]}, {reader["Name"]}, {reader["Price"]}, {reader["Stock"]}");
                }
                conn.Close();
            }
            else
            {
                Console.WriteLine("Invalid price input.");
            }
        }

        static void GetAggregateStock(SqlConnection conn)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT SUM(Stock) AS TotalStock FROM Product", conn);
            object result = cmd.ExecuteScalar();
            Console.WriteLine($"Total Stock: {result}");
            conn.Close();
        }

        static void DeleteRecord(SqlConnection conn)
        {
            Console.WriteLine("Enter table name:");
            string tableName = Console.ReadLine();
            Console.WriteLine("Enter primary key column name:");
            string columnName = Console.ReadLine();
            Console.WriteLine("Enter record ID:");
            int id = Convert.ToInt32(Console.ReadLine());

            conn.Open();
            SqlCommand cmd = new SqlCommand($"DELETE FROM {tableName} WHERE {columnName} = @ID", conn);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.ExecuteNonQuery();
            conn.Close();

            Console.WriteLine("Record deleted successfully.");
        }

        static void DeleteTableRecord(SqlConnection conn)
        {
            Console.WriteLine("Enter the table name:");
            string tableName = Console.ReadLine();
            Console.WriteLine("Enter the primary key column name:");
            string columnName = Console.ReadLine();
            Console.WriteLine("Enter the ID of the record to delete:");
            int id = Convert.ToInt32(Console.ReadLine());

            conn.Open();

            try
            {
                SqlCommand deleteCmd = new SqlCommand($"DELETE FROM {tableName} WHERE {columnName} = @ID", conn);
                deleteCmd.Parameters.AddWithValue("@ID", id);
                deleteCmd.ExecuteNonQuery();
                Console.WriteLine($"Record with ID {id} deleted successfully. Cascade delete executed automatically.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during delete: " + ex.Message);
            }

            conn.Close();
        }

        static void UpdatePetRecord(SqlConnection conn)
        {
            Console.WriteLine("Enter Pet ID to update:");
            int petId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter new data (Name, Species, Breed, Age):");
            string[] petData = Console.ReadLine().Split(',');

            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Pet SET Name = @Name, Species = @Species, Breed = @Breed, Age = @Age WHERE PetID = @PetID", conn);
            cmd.Parameters.AddWithValue("@PetID", petId);
            cmd.Parameters.AddWithValue("@Name", petData[0].Trim());
            cmd.Parameters.AddWithValue("@Species", petData[1].Trim());
            cmd.Parameters.AddWithValue("@Breed", petData[2].Trim());
            cmd.Parameters.AddWithValue("@Age", Convert.ToInt32(petData[3].Trim()));
            cmd.ExecuteNonQuery();
            conn.Close();

            Console.WriteLine("Pet record updated successfully.");
        }
    }
}
