public class Song
{
    public Song(string artist, string songName, string length)
    {
        this.Artist = artist;
        this.Name = songName;
        this.Minutes = minutes;
        this.Seconds = seconds;
        var lengthParams = length.Split(':');
        this.Length = length;
        this.Minutes = int.Parse(lengthParams[0]);
        this.Seconds = int.Parse(lengthParams[1]);
    }

    private string artist;
    private string name;
    private string length;
    private int minutes;
    private int seconds;

    public string Artist
    {
        get
        {
            return this.artist;
        }

        set
        {
            if (value.Length < 3 || value.Length > 20)
            {
                throw new InvalidArtistNameException();
            }
            this.artist = value;
        }
    }
    public string Name
    {
        get
        {
            return this.name;
        }

        set
        {
            if (value.Length < 3 || value.Length > 30)
            {
                throw new InvalidSongNameException();
            }
            this.name = value;
        }
    }

    public string Length
    {
        get
        {
            return this.length;
        }

        set
        {
            var dummy = 0;
            if (!(int.TryParse(value.Substring(0, value.IndexOf(':')), out dummy) && int.TryParse(value.Substring(value.IndexOf(':') + 1), out dummy)))
            {
                throw new InvalidSongLengthException();
            }
            this.length = value;
        }
    }

    public int Minutes
    {
        get
        {
            return this.minutes;
        }

        set
        {
            if (value < 0 || value > 14)
            {
                throw new InvalidSongMinutesException();
            }
            this.minutes = value;
        }
    }

    public int Seconds
    {
        get
        {
            return this.seconds;
        }

        set
        {
            if (value < 0 || value > 59)
            {
                throw new InvalidSongSecondsException();
            }
            this.seconds = value;
        }
    }
}