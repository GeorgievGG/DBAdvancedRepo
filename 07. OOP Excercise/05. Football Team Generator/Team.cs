using System;
using System.Collections.Generic;
using System.Linq;

public class Team
{
    private string name;
    private readonly List<Player> players;

    public Team(string name)
    {
        this.Name = name;
        this.players = new List<Player>();
    }

    public string Name
    {
        get
        {
            return name;
        }

        private set
        {
            if (string.IsNullOrWhiteSpace(value) || value == string.Empty)
            {
                throw new ArgumentException("A name should not be empty.");
            }
            name = value;
        }
    }

    public double Rating
    {
        get
        {
            if (this.Players.Count == 0)
            {
                return 0;
            }
            else
            {
                return Math.Round(this.Players.Average(x => x.OverallSkill), 0);
            }
        }
    }

    internal IReadOnlyList<Player> Players
    {
        get
        {
            return players;
        }
    }

    public void AddPlayer(Player pleya)
    {
        this.players.Add(pleya);
    }

    public void RemovePlayer(string playerName)
    {
        if (Players.Count(x => x.Name == playerName) == 0)
        {
            throw new ArgumentException($"Player {playerName} is not in {this.Name} team.");
        }
        var player = players.FirstOrDefault(x => x.Name == playerName);
        this.players.Remove(player);
    }
}