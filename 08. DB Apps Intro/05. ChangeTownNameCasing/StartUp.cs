using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _05._ChangeTownNameCasing
{
    public class StartUp
    {
        public static void Main()
        {
            var connection = new SqlConnection("Server=.;Initial Catalog=MinionsDB;Integrated Security=True");
            connection.Open();
            var reqCountry = Console.ReadLine();

            var command = new SqlCommand(@"UPDATE Towns
                                            SET Name = UPPER(t.Name)
                                            FROM Towns t

                                            JOIN Countries c
                                            ON c.Id = t.CountryID

                                            WHERE c.Name = @Country;", connection);
            command.Parameters.AddWithValue("@Country", reqCountry);

            var ChangedTownsCount = command.ExecuteNonQuery();

            var command2 = new SqlCommand(@"SELECT t.Name
                                            FROM Towns t

                                            JOIN Countries c
                                            ON c.Id = t.CountryID

                                            WHERE c.Name = @Country;", connection);
            command2.Parameters.AddWithValue("@Country", reqCountry);
            var reader = command2.ExecuteReader();
            var cities = new List<string>();
            if (!reader.HasRows)
            {
                Console.WriteLine("No town names were affected.");
            }
            else
            {
                Console.WriteLine($"{ChangedTownsCount} town names were affected.");
                using (reader)
                {
                    while (reader.Read())
                    {
                        cities.Add((string)reader["Name"]);
                    }
                }
            }
            Console.WriteLine("[" + string.Join(", ", cities) + "]");
            connection.Close();
        }
    }
}
