using System;
using System.Data.SqlClient;
using System.Linq;

namespace _08._IncreaseMinionAge
{
    public class StartUp
    {
        public static void Main()
        {
            var connection = new SqlConnection("Server=.;Initial Catalog=MinionsDB;Integrated Security=True");
            connection.Open();
            var reqMinionIDs = Console.ReadLine().Split().Select(int.Parse).ToList();

            var command = new SqlCommand($@"UPDATE Minions
                                           SET Name = UPPER(Name),
                                           Age += 1
                                           OUTPUT inserted.*
                                           WHERE Id IN ({string.Join(", ", reqMinionIDs)});", connection);

            var updatedMinions = command.ExecuteReader();

            using (updatedMinions)
            {
                while (updatedMinions.Read())
                {
                    Console.WriteLine($"{(string)updatedMinions["Name"]} {(int)updatedMinions["Age"]}");
                }
            }

            connection.Close();
        }
    }
}
