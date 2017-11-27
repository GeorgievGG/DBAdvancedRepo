using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _07._PrintAllMinionNames
{
    public class StartUp
    {
        public static void Main()
        {
            var connection = new SqlConnection("Server=.;Initial Catalog=MinionsDB;Integrated Security=True");
            connection.Open();
            var minions = new List<string>();

            var command = new SqlCommand(@"SELECT Name FROM Minions ORDER BY Id", connection);
            var reader = command.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    minions.Add((string)reader["Name"]);
                }
            }

            connection.Close();

            for (int i = 0; i <= minions.Count / 2; i++)
            {
                if (i == (minions.Count / 2) && minions.Count % 2 == 1)
                {
                    Console.WriteLine(minions[i]);
                    continue;
                }
                else if (i == (minions.Count / 2) && minions.Count % 2 == 0)
                {
                    return;
                }
                Console.WriteLine(minions[i]);
                Console.WriteLine(minions[minions.Count - 1 - i]);
            }
        }
    }
}
