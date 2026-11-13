using System;

// Кому предназначена задача (признак из задания)
enum TaskTarget
{
    Sysadmins,    // системщикам
    Developers,   // разработчикам
    Management    // начальству (начальник и зам начальника отдела)
}

// Сотрудник
class Employee
{
    // Свойства только для чтения: значение задаётся в конструкторе
    public string Name { get; }
    public string Position { get; }
    public Employee Boss { get; }          // прямой начальник
    public Employee SecondBoss { get; }    // второй начальник (у глав секторов), у остальных null

    // Конструктор для сотрудников с одним начальником
    public Employee(string name, string position, Employee boss)
    {
        Name = name;
        Position = position;
        Boss = boss;
        // SecondBoss не трогаем: у ссылочных типов по умолчанию null
    }

    // Перегрузка: конструктор для глав секторов, у них два начальника
    public Employee(string name, string position, Employee boss, Employee secondBoss)
    {
        Name = name;
        Position = position;
        Boss = boss;
        SecondBoss = secondBoss;
    }

    // Задачу берут, только если её даёт прямой начальник
    public bool CanTake(Employee giver)
    {
        return giver != null && (giver == Boss || giver == SecondBoss);
    }
}

// Задача. Назвал WorkTask, чтобы не путать с системным классом Task
class WorkTask
{
    public int Number { get; }
    public string Title { get; }
    public string Action { get; }       // что требуется от пользователя
    public TaskTarget Target { get; }   // кому предназначена

    public WorkTask(int number, string title, string action, TaskTarget target)
    {
        Number = number;
        Title = title;
        Action = action;
        Target = target;
    }
}

class Program
{
    // Один человек даёт задачу другому, и сразу видно, взял тот её или нет
    static void GiveTask(Employee from, Employee to, WorkTask task)
    {
        Console.WriteLine($"{from.Name} ({from.Position}) даёт задачу №{task.Number} «{task.Title}» -> {to.Name} ({to.Position})");

        if (to.CanTake(from))
            Console.WriteLine($"   {to.Name} берёт задачу. Что делать: {task.Action}");
        else
            Console.WriteLine($"   {to.Name} НЕ берёт задачу: {from.Name} не его прямой начальник");
    }

    static void Main()
    {
        // ---------- Иерархия конторы (сверху вниз) ----------
        Employee timur = new Employee("Тимур", "генеральный директор", null);

        Employee rashid = new Employee("Рашид", "финансовый директор", timur);
        Employee ilham = new Employee("О Ильхам", "директор по автоматизации", timur);

        Employee lukas = new Employee("Лукас", "главный бухгалтер", rashid);

        Employee orkadiy = new Employee("Оркадий", "начальник ИТ-отдела", ilham);
        Employee volodya = new Employee("Володя", "зам. начальника ИТ-отдела", orkadiy);

        // Сектор системщиков: главный слушается Оркадия и Володю (второй конструктор)
        Employee ilshat = new Employee("Ильшат", "главный системщиков", orkadiy, volodya);
        Employee ivanych = new Employee("Иваныч", "зам. главного системщиков", ilshat);
        Employee ilya = new Employee("Илья", "системщик", ivanych);
        Employee vitya = new Employee("Витя", "системщик", ivanych);
        Employee zhenya = new Employee("Женя", "системщик", ivanych);

        // Сектор разработчиков
        Employee sergey = new Employee("Сергей", "главный разработчиков", orkadiy, volodya);
        Employee lyaysan = new Employee("Ляйсан", "зам. главного разработчиков", sergey);
        Employee marat = new Employee("Марат", "разработчик", lyaysan);
        Employee dina = new Employee("Дина", "разработчик", lyaysan);
        Employee ildar = new Employee("Ильдар", "разработчик", lyaysan);
        Employee anton = new Employee("Антон", "разработчик", lyaysan);

        // ---------- Задачи ----------
        WorkTask task1 = new WorkTask(1, "Настроить сеть в офисе",
            "проверить роутеры и кабели", TaskTarget.Sysadmins);
        WorkTask task2 = new WorkTask(2, "Автоматизировать отчёт бухгалтерии",
            "сделать кнопку, которая сама формирует отчёт", TaskTarget.Developers);
        WorkTask task3 = new WorkTask(3, "Утвердить закупку серверов",
            "посмотреть смету и поставить подпись", TaskTarget.Management);

        // ---------- Распределение ----------
        // Задача 1 — системщикам: Оркадий -> Ильшат -> Иваныч -> сотрудники
        Console.WriteLine("=== Задача для системщиков ===");
        GiveTask(orkadiy, ilshat, task1);
        GiveTask(ilshat, ivanych, task1);
        GiveTask(ivanych, ilya, task1);
        GiveTask(ivanych, vitya, task1);
        GiveTask(ivanych, zhenya, task1);

        // Задача 2 — разработчикам: даёт Володя (он тоже начальник Сергея)
        Console.WriteLine("\n=== Задача для разработчиков ===");
        GiveTask(volodya, sergey, task2);
        GiveTask(sergey, lyaysan, task2);
        GiveTask(lyaysan, marat, task2);
        GiveTask(lyaysan, dina, task2);
        GiveTask(lyaysan, ildar, task2);
        GiveTask(lyaysan, anton, task2);

        // Задача 3 — начальству: Ильхам -> Оркадий -> Володя
        Console.WriteLine("\n=== Задача для начальства ===");
        GiveTask(ilham, orkadiy, task3);
        GiveTask(orkadiy, volodya, task3);

        // ---------- Проверка отказов: задачу дают «не по цепочке» ----------
        Console.WriteLine("\n=== Нарушение иерархии ===");
        GiveTask(orkadiy, ilya, task1);    // Оркадий не прямой начальник Ильи
        GiveTask(timur, ilshat, task1);    // Тимур не прямой начальник Ильшата
        GiveTask(dina, marat, task2);      // коллега не начальник
    }
}