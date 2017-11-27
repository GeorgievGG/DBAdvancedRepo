using System;
using System.Data.SqlClient;

namespace _09._IncreaseAgeStoredProc
{
    public class StartUp
    {
        public static void Main()
        {
            var connection = new SqlConnection("Server=.;Initial Catalog=MinionsDB;Integrated Security=True");
            connection.Open();
            var reqMinionID = int.Parse(Console.ReadLine());

            var command = new SqlCommand($@"EXEC usp_GetOlder {reqMinionID};", connection);

            var updatedMinions = command.ExecuteReader();

            using (updatedMinions)
            {
                while (updatedMinions.Read())
                {
                    Console.WriteLine($"{(string)updatedMinions["Name"]} - {(int)updatedMinions["Age"]} years old");
                }
            }

            connection.Close();
        }
    }
}
