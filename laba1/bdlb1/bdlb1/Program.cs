using System;
using System.Data;
using System.Data.SqlClient;

namespace PetStoreApp
{
    class Program
    {
       
        static string connectionString = @"Server=DESKTOP-00L2CO6\SQLEXPRESS01;Database=PetStoreDB;Trusted_Connection=True;";

        static void Main(string[] args)
        {
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                
                Console.WriteLine("Pets Table:");
                DisplayTable(conn, "SELECT * FROM Pets");
                Console.WriteLine("\nCustomers Table:");
                DisplayTable(conn, "SELECT * FROM Customers");

                
                Console.WriteLine("\nAdding a new Pet...");
                InsertPet(conn, "Labrador", 1, 600.00M);
                DisplayTable(conn, "SELECT * FROM Pets");

                
                Console.WriteLine("\nCustomers with Orders (JOIN):");
                ExecuteJoinQuery(conn);

                Console.WriteLine("\nPets in 'Dogs' Category (Filter):");
                ExecuteFilterQuery(conn);

                Console.WriteLine("\nTotal Pets Sold (Aggregate):");
                ExecuteAggregateQuery(conn);
            }
        }

        
        static void DisplayTable(SqlConnection conn, string query)
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow row in table.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        Console.Write(item + "\t");
                    }
                    Console.WriteLine();
                }
            }
        }

        
        static void InsertPet(SqlConnection conn, string name, int categoryId, decimal price)
        {
            string query = "INSERT INTO Pets (PetName, CategoryID, Price) VALUES (@name, @categoryId, @price)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                cmd.Parameters.AddWithValue("@price", price);

                cmd.ExecuteNonQuery();
                Console.WriteLine("New pet added successfully.");
            }
        }

        
        static void ExecuteJoinQuery(SqlConnection conn)
        {
            string query = @"
                SELECT Customers.CustomerName, Orders.OrderID, Orders.OrderDate 
                FROM Customers 
                JOIN Orders ON Customers.CustomerID = Orders.CustomerID";

            DisplayTable(conn, query);
        }

        
        static void ExecuteFilterQuery(SqlConnection conn)
        {
            string query = @"
                SELECT PetName, Price 
                FROM Pets 
                WHERE CategoryID = (SELECT CategoryID FROM Categories WHERE CategoryName = 'Dogs')";

            DisplayTable(conn, query);
        }

        
        static void ExecuteAggregateQuery(SqlConnection conn)
        {
            string query = "SELECT COUNT(*) FROM OrderDetails";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                int count = (int)cmd.ExecuteScalar();
                Console.WriteLine($"Total Pets Sold: {count}");
            }
        }
    }
}
