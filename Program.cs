using System;

class Program
{
    static string ReverseString(string s)
    {
        string result = "";

        // Идём с конца к началу и собираем строку
        for (int i = s.Length - 1; i >= 0; i--)
        {
            result += s[i];
        }

        return result;
    }

    static void Main()
    {
        Console.WriteLine(ReverseString("hello"));       // olleh
        Console.WriteLine(ReverseString("Привет, мир")); // рим ,тевирП
        Console.WriteLine(ReverseString("a"));           // a
        Console.WriteLine(ReverseString(""));            // (пусто)

        Console.Write("Введи свою строку: ");
        string text = Console.ReadLine();
        Console.WriteLine("Наоборот: " + ReverseString(text));
    }
}