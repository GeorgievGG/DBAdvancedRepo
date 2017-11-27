using System;
using System.Collections.Generic;
using System.Linq;

public class Player
{
    private string name;
    private readonly Dictionary<string, string> stats;

    public Player(string name, List<string> stats)
    {
        this.Name = name;
        this.stats = new Dictionary<string, string>();
        this.stats.Add("Endurance", stats[0]);
        this.stats.Add("Sprint", stats[1]);
        this.stats.Add("Dribble", stats[2]);
        this.stats.Add("Passing", stats[3]);
        this.stats.Add("Shooting", stats[4]);
        foreach (var item in this.stats)
        {
            if (item.Value == string.Empty || string.IsNullOrWhiteSpace(item.Value) || int.Parse(item.Value) < 0 || int.Parse(item.Value) > 100)
            {
                throw new ArgumentException($"{item.Key} should be between 0 and 100.");
            }
        }
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

    public double OverallSkill
    {
        get
        {
            return this.Stats.Values.Select(x => int.Parse(x)).Average();
        }
    }

    public IReadOnlyDictionary<string, string> Stats
    {
        get
        {
            return stats;
        }
    }
}