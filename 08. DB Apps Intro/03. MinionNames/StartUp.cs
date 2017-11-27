using System;
using System.Data.SqlClient;

namespace _03._MinionNames
{
    public class StartUp
    {
        public static void Main()
        {
            var connection = new SqlConnection("Server=.;Initial Catalog=MinionsDB;Integrated Security=True");
            connection.Open();
            var requiredVillainID = int.Parse(Console.ReadLine());
            var command = new SqlCommand(@"SELECT v.Name
                                    FROM Villains v

                                    WHERE Id = @ReqId;", connection);
            command.Parameters.AddWithValue("@ReqId", requiredVillainID);
            try
            {
                var reader = command.ExecuteReader();

                using (reader)
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine($"No villain with ID {requiredVillainID} exists in the database.");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            var villain = (string)reader["Name"];

                            Console.WriteLine($"Villain: {villain}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Query failed.");
                Console.WriteLine(e.Message);
            }

            command = new SqlCommand(@"SELECT m.Name,
                                        m.Age
                                    FROM Villains v

                                    JOIN MinionsVillains mv
                                    ON mv.VillainID = v.Id

                                    JOIN Minions m
                                    ON m.Id = mv.MinionID

                                    WHERE v.Id = @ReqId

                                    ORDER BY m.Name;", connection);
            command.Parameters.AddWithValue("@ReqId", requiredVillainID);
            try
            {
                var reader = command.ExecuteReader();

                using (reader)
                {
                    var i = 0;
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("(no minions)");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            i++;
                            var minionName = (string)reader["Name"];
                            var minionAge = (int)reader["Age"];

                            Console.WriteLine($"{i}. {minionName} {minionAge}");
                        }
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
