using System;
using System.Diagnostics;

namespace binarySearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.Write("Введите длину массива: ");
            int n = Convert.ToInt32(Console.ReadLine());
            int[] a = new int[n];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                a[i] = rnd.Next(0, 10);
            }
            Timing timing = new Timing();
            timing.StartTime();
            Stopwatch stp = new Stopwatch();
            stp.Start();

            // Создание идексов нижней и верхней границы
            int low = 0;
            int high = a.Length - 1;
            
            // Создание индекса элемента массива, находящегося в середине
            int guess = 0;

            // Создание переменной поиска
            string searchValueStr;
            int searchValue = 0;
            while (searchValue < 1 || searchValue > 1000)
            {
                try
                {
                    Console.Write("Введите число для поиска от 1 до 10:");
                    searchValueStr = Console.ReadLine();
                    searchValue = Convert.ToInt32(searchValueStr);
                }
                catch 
                {
                    continue;
                }
            }
            while (a[guess] != searchValue)
            {
                // Середина массива
                guess = (low + high) / 2;

                // Сдвигаем верхнюю границу
                if (searchValue < a[guess])
                {
                    high = guess - 1;
                }

                // Сдвигаем нижнюю границу
                else if (searchValue > a[guess])
                {
                    low = guess + 1;
                }
            }
            stp.Stop();
            timing.StopTime();
            if (guess == 0)
            {
                Console.WriteLine("Число не найдено(!");
            }
            else
            {
                Console.WriteLine("Позиция числа " + searchValue + " = " + guess );
                Console.WriteLine($"StopWatch: {stp.Elapsed}");
                Console.WriteLine($"Timing: {timing.Result()}");
            }
        }
        class Timing
        {
            TimeSpan duration;
            TimeSpan[] threads;
            public Timing()
            {
                duration = new TimeSpan(0);
                threads = new TimeSpan[Process.GetCurrentProcess().Threads.Count];
            }
            public void StartTime()
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                for (int i = 0; i < threads.Length; i++)
                {
                    threads[i] = Process.GetCurrentProcess().Threads[i].UserProcessorTime;
                }
            }
            public void StopTime()
            {
                TimeSpan tmp;
                for (int i = 0; i < threads.Length; i++)
                {
                    tmp = Process.GetCurrentProcess().Threads[i].UserProcessorTime.Subtract(threads[i]);
                    if (tmp > TimeSpan.Zero)
                    {
                        duration = tmp;
                    }
                }
            }
            public TimeSpan Result()
            {
                return duration;

            }
        }
    }
}
