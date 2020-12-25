using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Work_with_delegate
{
    delegate void AccountStateHandler(string message);
    class Account
    {
        int _sum;
        AccountStateHandler _del;
        public void RegisterHandler(AccountStateHandler del)
        {
            _del += del;
        }
        public void UnregisterHandler(AccountStateHandler del)
        {
            _del -= del;
        }

        public Account() { }
        public Account(int sum) => _sum = sum; 

        public void Put(int sum)
        {
            _sum += sum;
            if (_del != null)
            _del($"На счет поступило {sum}");
        }
        public void WithDraw(int sum)
        {
            if (_sum >= sum)
            {
                _sum -= sum;
                if (_del != null)
                    _del($"Со счета снято { sum}");
            }
            else
            {
                if (_del != null)
                    _del("На счете недостаточно средств");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account();
            account.RegisterHandler(Display);
            account.RegisterHandler(ColorDisplay);
            account.Put(150);
            account.WithDraw(100);
            account.UnregisterHandler(ColorDisplay);
            account.WithDraw(150);
        }

        static void Display(string message)
        {
            Console.WriteLine(message);
        }
        static void ColorDisplay(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}


/* Простые примеры на Делегаты
     delegate void Message();
      Message msg;
      msg = Display;
      msg.Invoke();
      msg();
  static void Display()
  {
      Console.WriteLine("Hello");
  }*/

/*
        delegate int Operation(int x, int y)
            Operation operation = Add;
        int result = operation.Invoke(5, 4);
        Console.WriteLine(result);
            Console.WriteLine("loool");
            operation = Mult;
            result = operation(12, 8);
        Console.WriteLine(result);

              static int Add(int x, int y)
        {
            return x + y;
        }

        static int Mult(int x, int y)
        {
            return x * y;
        }
           */
