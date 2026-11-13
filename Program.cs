using System;

enum AccountType
{
    Current,
    Savings
}

class BankAccount
{
    private static int nextNumber = 1;

    private int accountNumber;
    private decimal balance;
    private AccountType type;

    private static int GenerateNumber()
    {
        return nextNumber++;
    }

    public void Fill(decimal balance, AccountType type)
    {
        this.accountNumber = GenerateNumber();
        this.balance = balance;
        this.type = type;
    }

    public int GetAccountNumber() { return accountNumber; }
    public decimal GetBalance() { return balance; }
    public AccountType GetAccountType() { return type; }

    public bool Deposit(decimal amount)
    {
        if (amount <= 0)
            return false;

        balance += amount;
        return true;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount <= 0 || amount > balance)
            return false;

        balance -= amount;
        return true;
    }

    // 8.1: перевод с другого счёта (from) на текущий (this)
    // from — ссылка на тот же объект, поэтому его баланс меняется по-настоящему
    public bool Transfer(BankAccount from, decimal amount)
    {
        if (from == null)
            return false;

        // Сначала снимаем. Не вышло — ничего не зачисляем
        if (!from.Withdraw(amount))
            return false;

        Deposit(amount);
        return true;
    }

    public void Print()
    {
        Console.WriteLine("Счёт №{0}, тип: {1}, баланс: {2}", accountNumber, type, balance);
    }
}

class Program
{
    static void Main()
    {
        BankAccount acc1 = new BankAccount();
        acc1.Fill(1000m, AccountType.Current);

        BankAccount acc2 = new BankAccount();
        acc2.Fill(500m, AccountType.Savings);

        acc1.Print();
        acc2.Print();

        Console.WriteLine("\nПеревод 300: " + acc2.Transfer(acc1, 300m));   // True
        acc1.Print();   // 700
        acc2.Print();   // 800

        Console.WriteLine("\nПеревод 5000: " + acc2.Transfer(acc1, 5000m)); // False
        acc1.Print();   // 700
        acc2.Print();   // 800
    }
}