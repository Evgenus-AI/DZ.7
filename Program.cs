using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.Write("Введи имя файла: ");
        string fileName = Console.ReadLine();

        // Файла нет — сообщаем и выходим
        if (!File.Exists(fileName))
        {
            Console.WriteLine("Такого файла не существует");
            return;
        }

        string text = File.ReadAllText(fileName);

        // ToUpper() возвращает новую строку, исходная не меняется
        string upperText = text.ToUpper();

        File.WriteAllText("output.txt", upperText);

        Console.WriteLine("Готово! Результат в файле output.txt");
    }
}