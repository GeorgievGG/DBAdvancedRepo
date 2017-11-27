using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.DependencyInversion
{
    public class StartUp
    {
        public static void Main()
        {
            var input = string.Empty;
            var teams = new Dictionary<string, Team>();
            while ((input = Console.ReadLine()) != "END")
            {
                var inputParams = input.Split(';');
                var teamName = inputParams[1];
                if (!teams.ContainsKey(teamName) && inputParams[0] != "Team")
                {
                    Console.WriteLine($"Team {teamName} does not exist.");
                    continue;
                }
                switch (inputParams[0])
                {
                    case "Team":
                        CreateTeam(inputParams.Skip(1).ToList(), teams);
                        break;
                    case "Add":
                        AddPlayer(inputParams.Skip(1).ToList(), teams);
                        break;
                    case "Remove":
                        RemovePlayer(inputParams.Skip(1).ToList(), teams);
                        break;
                    case "Rating":
                        PrintRating(inputParams.Skip(1).ToList(), teams);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void CreateTeam(List<string> list, Dictionary<string, Team> teams)
        {
            var teamName = list[0];

            try
            {
                teams.Add(teamName, new Team(teamName));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void AddPlayer(List<string> list, Dictionary<string, Team> teams)
        {
            var teamName = list[0];
            var playerName = list[1];
            var skills = list.Skip(2).ToList();
            try
            {
                teams[teamName].AddPlayer(new Player(playerName, skills));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void RemovePlayer(List<string> list, Dictionary<string, Team> teams)
        {
            var teamName = list[0];
            var playerName = list[1];

            try
            {
                teams[teamName].RemovePlayer(playerName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void PrintRating(List<string> list, Dictionary<string, Team> teams)
        {
            var teamName = list[0];

            try
            {
                Console.WriteLine($"{teamName} - " + Math.Round(teams[teamName].Rating, 0));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}