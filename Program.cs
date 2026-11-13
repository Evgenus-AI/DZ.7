using System;

class Program
{
    // is: просто true/false
    static bool CheckWithIs(object obj)
    {
        return obj is IFormattable;
    }

    // as: пробует привести тип, не вышло — null
    static bool CheckWithAs(object obj)
    {
        IFormattable f = obj as IFormattable;
        return f != null;
    }

    static void Main()
    {
        // 42 и 3.14 упаковываются в object (boxing)
        object[] items = { 42, 3.14, "строка", DateTime.Now, new object() };

        foreach (object item in items)
        {
            Console.WriteLine($"{item}: is -> {CheckWithIs(item)}, as -> {CheckWithAs(item)}");
        }
    }
}