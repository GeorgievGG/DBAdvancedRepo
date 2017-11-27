using System;
using System.Data.SqlClient;

namespace _06._RemoveVillain
{
    public class StartUp
    {
        public static void Main()
        {
            var connection = new SqlConnection("Server=.;Initial Catalog=MinionsDB;Integrated Security=True");
            connection.Open();
            var reqVillainID = int.Parse(Console.ReadLine());
            var transaction = connection.BeginTransaction();

            try
            {
                var command = new SqlCommand(@"DELETE FROM MinionsVillains
                                         WHERE VillainID = @villainID;", connection, transaction);
                command.Parameters.AddWithValue("@villainID", reqVillainID);

                var deletedMinionAffiliations = command.ExecuteNonQuery();

                var command2 = new SqlCommand(@"DELETE FROM Villains
                                            OUTPUT deleted.Name
                                            WHERE Id = @villainID;", connection, transaction);
                command2.Parameters.AddWithValue("@villainID", reqVillainID);
                var deletedVillain = (string)command2.ExecuteScalar();
                if (deletedVillain == null)
                {
                    Console.WriteLine("No such villain was found.");
                    return;
                }
                Console.WriteLine($"{deletedVillain} was deleted.");
                Console.WriteLine($"{deletedMinionAffiliations} minions were released.");
                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                transaction.Rollback();
            }
            connection.Close();
        }
    }
}
