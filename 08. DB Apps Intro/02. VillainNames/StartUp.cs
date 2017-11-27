using System;
using System.Data.SqlClient;

namespace _02._VillainNames
{
    public class StartUp
    {
        public static void Main()
        {
            var connection = new SqlConnection("Server=.;Initial Catalog=master;Integrated Security=True");
            connection.Open();
            var newDBName = "MinionsDB";
            var command = new SqlCommand($"USE {newDBName};", connection);
            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine($"Successfully switched to {newDBName}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Can\'t switch to {newDBName}");
                Console.WriteLine(e.Message);
            }
            command = new SqlCommand(@"SELECT v.Name,
                                    COUNT(*) as Minions
                                    FROM Villains v

                                    JOIN MinionsVillains mv
                                    ON mv.VillainID = v.Id

                                    JOIN Minions m
                                    ON m.Id = mv.MinionID

                                    GROUP BY v.Name

                                    ORDER BY Minions DESC;", connection);
            try
            {
                var reader = command.ExecuteReader();

                using(reader)
                {
                    while (reader.Read())
                    {
                        var villain = (string)reader["Name"];
                        var minionsCt = (int)reader["Minions"];
                        Console.WriteLine($"{villain} - {minionsCt}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Query failed.");
                Console.WriteLine(e.Message);
            }
        }
    }
}
