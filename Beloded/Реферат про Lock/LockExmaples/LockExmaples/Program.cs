using System;
using System.Threading;
using System.Threading.Tasks;

namespace Net9LockDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Сравнение подходов к синхронизации в .NET 9\n");

            // Тестируем старый подход
            var oldCounter = new OldObjectLockCounter();
            await RunTestAsync("Старый подход (object)", () => oldCounter.Increment());
            Console.WriteLine($"Итог: {oldCounter.Count}\n");

            // Тестируем новый подход
            var newCounter = new NewNet9LockCounter();
            await RunTestAsync("Новый подход (System.Threading.Lock)", () => newCounter.Increment());
            Console.WriteLine($"Итог: {newCounter.Count}\n");

            Console.ReadLine();
        }

        // Метод для запуска 100 000 потоков, чтобы проверить надежность замка
        static async Task RunTestAsync(string testName, Action incrementAction)
        {
            Console.WriteLine($"Запуск теста: {testName}...");
            var tasks = new Task[100000];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(incrementAction);
            }

            await Task.WhenAll(tasks);
        }
    }

    // ==========================================
    // 1. СТАРЫЙ ПОДХОД (Костыль до .NET 9)
    // ==========================================
    public class OldObjectLockCounter
    {
        public int Count { get; private set; } = 0;

        // МИНУС: Создается пустой объект в куче (Heap). Нагружает GC.
        private readonly object _lock = new object();

        public void Increment()
        {
            // Используется тяжелый класс Monitor под капотом
            lock (_lock)
            {
                Count++;
            }
        }
    }

    // ==========================================
    // 2. НОВЫЙ ПОДХОД (.NET 9 и выше)
    // ==========================================
    public class NewNet9LockCounter
    {
        public int Count { get; private set; } = 0;

        // ПЛЮС: Специализированный тип. 
        // private - защищает от дедлоков (внешний код его не видит).
        // Lock - защищает память (не создает мусор).
        private readonly Lock _lock = new Lock();

        public void Increment()
        {
            // Компилятор сгенерирует здесь быструю стековую структуру!
            lock (_lock)
            {
                Count++;
            }
        }
    }
}