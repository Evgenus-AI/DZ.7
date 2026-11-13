using System;
using System.Collections.Generic;

class Song
{
    private string name;     // название
    private string author;   // автор
    private Song prev;       // предыдущая песня в списке

    public void SetName(string newName) { name = newName; }
    public void SetAuthor(string newAuthor) { author = newAuthor; }
    public void SetPrev(Song newPrev) { prev = newPrev; }
    public Song GetPrev() { return prev; }

    public string Title()
    {
        return name + " - " + author;
    }

    // В задании написано "public bool override" — это опечатка,
    // правильный порядок: public override bool
    public override bool Equals(object d)
    {
        // as вернёт null, если d — не песня
        Song other = d as Song;
        if (other == null)
            return false;

        // Песни равны, если совпали название и автор
        return name == other.name && author == other.author;
    }

    // Переопределил Equals — компилятор просит и GetHashCode (иначе предупреждение)
    public override int GetHashCode()
    {
        return Title().GetHashCode();
    }
}

class Program
{
    static void Main()
    {
        Song s1 = new Song();
        s1.SetName("Bohemian Rhapsody");
        s1.SetAuthor("Queen");

        Song s2 = new Song();
        s2.SetName("Yesterday");
        s2.SetAuthor("The Beatles");

        Song s3 = new Song();
        s3.SetName("Smoke on the Water");
        s3.SetAuthor("Deep Purple");

        Song s4 = new Song();
        s4.SetName("Numb");
        s4.SetAuthor("Linkin Park");

        List<Song> songs = new List<Song> { s1, s2, s3, s4 };

        // Связываем: у каждой песни, кроме первой, prev = предыдущая в списке
        for (int i = 1; i < songs.Count; i++)
        {
            songs[i].SetPrev(songs[i - 1]);
        }

        foreach (Song song in songs)
        {
            Song p = song.GetPrev();
            string prevText = (p == null) ? "нет" : p.Title();
            Console.WriteLine($"{song.Title()} (предыдущая: {prevText})");
        }

        Console.WriteLine("\nПервая и вторая одинаковые? " + songs[0].Equals(songs[1]));

        // Другой объект с теми же данными тоже будет «равен»
        Song copy = new Song();
        copy.SetName("Bohemian Rhapsody");
        copy.SetAuthor("Queen");
        Console.WriteLine("Первая и копия первой? " + songs[0].Equals(copy));
    }
}