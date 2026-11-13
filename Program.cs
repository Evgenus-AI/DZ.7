using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    // ref: метод меняет саму переменную снаружи.
    // Приходит вся строка, уходит только e-mail
    static void SearchMail(ref string s)
    {
        // Делим по # : [0] — ФИО, [1] — почта
        string[] parts = s.Split('#');
        s = parts[1].Trim();
    }

    static void Main()
    {
        if (!File.Exists("input.txt"))
        {
            Console.WriteLine("Файл input.txt не найден");
            return;
        }

        string[] lines = File.ReadAllLines("input.txt");
        List<string> emails = new List<string>();

        foreach (string line in lines)
        {
            // Пустые строки и строки без # пропускаем
            if (!line.Contains("#"))
                continue;

            string s = line;
            SearchMail(ref s);
            emails.Add(s);
        }

        File.WriteAllLines("emails.txt", emails);

        Console.WriteLine($"Записано адресов: {emails.Count}. Файл emails.txt");
    }
}