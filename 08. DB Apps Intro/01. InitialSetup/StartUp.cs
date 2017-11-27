using System;
using System.Data.SqlClient;

public class StartUp
{
    public static void Main()
    {
        var connection = new SqlConnection("Server=.;Initial Catalog=master;Integrated Security=True");
        connection.Open();
        var newDBName = "MinionsDB";
        var command = new SqlCommand($"create database {newDBName};", connection);
        try
        {
            command.ExecuteNonQuery();
            Console.WriteLine($"DB {newDBName} created successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine($"DB {newDBName} creation failed");
            Console.WriteLine(e.Message);
        }
        command = new SqlCommand($"USE {newDBName};", connection);
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
        command = new SqlCommand(@"CREATE TABLE Countries
            (
                Id INT PRIMARY KEY IDENTITY,
                Name VARCHAR(20) NOT NULL UNIQUE
            );

            CREATE TABLE Towns
            (
                Id INT PRIMARY KEY IDENTITY,
                Name VARCHAR(20) NOT NULL UNIQUE,
                CountryID INT NOT NULL FOREIGN KEY REFERENCES Countries(Id)
            );

            CREATE TABLE Minions
            (
                Id INT PRIMARY KEY IDENTITY,
                Name VARCHAR(20) NOT NULL UNIQUE,
                Age INT NOT NULL,
                TownId INT NOT NULL FOREIGN KEY REFERENCES Towns(Id)
            );

            CREATE TABLE EvilnessFactors
            (
                Id INT PRIMARY KEY IDENTITY,
                Name VARCHAR(20) NOT NULL UNIQUE
            );

            CREATE TABLE Villains
            (
                Id INT PRIMARY KEY IDENTITY,
                Name VARCHAR(20) NOT NULL UNIQUE,
                EvilnessFactorID INT NOT NULL FOREIGN KEY REFERENCES EvilnessFactors(Id)
            );

            CREATE TABLE MinionsVillains
            (
                MinionID INT NOT NULL FOREIGN KEY REFERENCES Minions(Id),
                VillainID INT NOT NULL FOREIGN KEY REFERENCES Villains(Id),
                PRIMARY KEY(MinionID, VillainID)
            );", connection);
        try
        {
            var x = command.ExecuteNonQuery();
            Console.WriteLine("Tables created successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine("Tables creation failed");
            Console.WriteLine(e.Message);
        }
        connection.Close();
    }
}