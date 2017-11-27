using System;
using System.Collections.Generic;
using System.Linq;
class Controller
{
    static void Main()
    {
        var incomingSongs = int.Parse(Console.ReadLine());
        var songs = new List<Song>();
        var songsAdded = 0;
        for (int i = 0; i < incomingSongs; i++)
        {
            try
            {
                var inputArgs = Console.ReadLine().Split(';');
                var artistName = inputArgs[0];
                var songName = inputArgs[1];
                var songLength = inputArgs[2];
                songs.Add(new Song(artistName, songName, songLength));
                songsAdded++;
                Console.WriteLine("Song added.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        Console.WriteLine($"Songs added: {songsAdded}");
        var totalSeconds = songs.Sum(x => x.Minutes) * 60 + songs.Sum(x => x.Seconds);
        var totalMinutes = totalSeconds / 60;
        totalSeconds %= 60;
        var totalHours = totalMinutes / 60;
        totalMinutes %= 60;
        Console.WriteLine($"Playlist length: {totalHours}h {totalMinutes}m {totalSeconds}s");
    }
}
