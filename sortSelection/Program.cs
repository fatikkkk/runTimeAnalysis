using System;
using System.Diagnostics;

namespace sortSelection
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
                a[i] = rnd.Next();
            }
            Stopwatch stp = new Stopwatch();
            stp.Start();
            Timing timing = new Timing();
            timing.StartTime();

            int min; // Минимальный индекс массива
            int temp; // Вспомогательная переменная
            for (int i = 0; i < a.Length; i++)
            {
                min = i;
                for (int j = i + 1; j < a.Length; j++)
                {
                    // Сравниваем элемент массива j с минимальным элементом
                    if (a[j] < a[min])
                    {
                        min = j;
                    }
                }
                temp = a[min]; // Копируем значение минимального элемента
                a[min] = a[i]; // На место минимального элемента присваиваем значение неотсортированной части
                a[i] = temp; // Присваиваем значение временной переменной
            }
            stp.Stop();
            timing.StopTime();

            //Вывод массива
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine(a[i]);
            }
            Console.WriteLine("Ваш код выполнился за следующее время:");
            Console.WriteLine($"StopWatch: {stp.Elapsed}");
            Console.WriteLine($"Timing: {timing.Result()}");
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