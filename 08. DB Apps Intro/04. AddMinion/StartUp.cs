using System;
using System.Data.SqlClient;

namespace _04._AddMinion
{
    public class StartUp
    {
        public static void Main()
        {
            var connection = new SqlConnection("Server=.;Initial Catalog=MinionsDB;Integrated Security=True");
            connection.Open();
            var transaction = connection.BeginTransaction();
            var minionInfo = Console.ReadLine().Split();
            var villainInfo = Console.ReadLine().Split();

            //Town
            try
            {
                var command = new SqlCommand(@"SELECT Name
                                    FROM Towns

                                    WHERE Name = @cityName;", connection, transaction);
                command.Parameters.AddWithValue("@cityName", minionInfo[3]);
                var reader = command.ExecuteReader();
                var townID = 0;
                var noRow = false;
                using (reader)
                {
                    if (!reader.HasRows)
                    {
                        noRow = true;
                    }
                }
                if (noRow)
                {
                    var insertCmd = new SqlCommand(@"INSERT INTO Towns (Name)
                                    VALUES(@cityName)", connection, transaction);
                    insertCmd.Parameters.AddWithValue("@cityName", minionInfo[3]);
                    insertCmd.ExecuteNonQuery();
                    Console.WriteLine($"Town {minionInfo[3]} was added to the database.");
                }
                noRow = false;
                command = new SqlCommand(@"SELECT Id
                                    FROM Towns

                                    WHERE Name = @cityName;", connection, transaction);
                command.Parameters.AddWithValue("@cityName", minionInfo[3]);
                townID = (int)command.ExecuteScalar();

                //Villains
                command = new SqlCommand(@"SELECT Name
                                    FROM Villains

                                    WHERE Name = @villainName;", connection, transaction);
                command.Parameters.AddWithValue("@villainName", villainInfo[1]);
                reader = command.ExecuteReader();
                var villainID = 0;
                using (reader)
                {
                    if (!reader.HasRows)
                    {
                        noRow = true;
                    }
                }
                if (noRow)
                {
                    var insertCmd = new SqlCommand(@"INSERT INTO Villains (Name)
                                    VALUES(@villainName)", connection, transaction);
                    insertCmd.Parameters.AddWithValue("@villainName", villainInfo[1]);
                    insertCmd.ExecuteNonQuery();
                    Console.WriteLine($"Villain {villainInfo[1]} was added to the database.");
                }

                command = new SqlCommand(@"SELECT Id
                                    FROM Villains

                                    WHERE Name = @villainName;", connection, transaction);
                command.Parameters.AddWithValue("@villainName", villainInfo[1]);
                villainID = (int)command.ExecuteScalar();

                //Minion
                command = new SqlCommand(@"INSERT INTO Minions (Name, Age, TownID)
                                    VALUES (@MinionName, @MinionAge, @TownID);", connection, transaction);
                command.Parameters.AddWithValue("@MinionName", minionInfo[1]);
                command.Parameters.AddWithValue("@MinionAge", int.Parse(minionInfo[2]));
                command.Parameters.AddWithValue("@TownID", townID);
                command.ExecuteNonQuery();
                var command2 = new SqlCommand(@"SELECT ID
                                            FROM Minions
                                            WHERE Name = @MinionName;", connection, transaction);
                command2.Parameters.AddWithValue("@MinionName", minionInfo[1]);
                var minionID = (int)command2.ExecuteScalar();

                var command3 = new SqlCommand(@"INSERT INTO MinionsVillains (MinionID, VillainID)
                                    VALUES (@MinionID, @VillainID);", connection, transaction);
                command3.Parameters.AddWithValue("@MinionID", minionID);
                command3.Parameters.AddWithValue("@VillainID", villainID);
                try
                {
                    command3.ExecuteNonQuery();
                    Console.WriteLine($"Successfully added {minionInfo[1]} to be minion of {villainInfo[1]}.");
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Query failed.");
                    Console.WriteLine(e.Message);
                }
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Console.WriteLine(e.Message);
            }

            transaction.Commit();
            connection.Close();
        }
    }
}
