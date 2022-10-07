using System;
using System.Collections;
using System.Diagnostics;

namespace hashTableSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Stopwatch stp = new Stopwatch();
            Timing timing = new Timing();

            // Создание и инициализация новой хэш таблицы
            var myHT = new Hashtable();
            myHT.Add(0, "Russia");
            myHT.Add(1, "United Kingdom");
            myHT.Add(2, "United States of America");
            myHT.Add(3, "Canada");
            myHT.Add(4, "France");
            myHT.Add(5, "Italian");
            myHT.Add(6, "Spain");

            // Отображение значений хэш-таблицы
            Console.WriteLine("The Hashtable contains the following values:");
            PrintIndexAndKeysAndValues(myHT);

            stp.Start();
            timing.StartTime();
            // Поиск по значению (используйте Russia - время в Timing будет отображаться)
            Console.WriteLine("Ввведите название страны");
            string myCountry = Console.ReadLine();
            Console.WriteLine("Значение \"{0}\" is {1}.", myCountry, myHT.ContainsValue(myCountry) ? "находится в хэш таблице!" : " НЕ находится в хэш таблице!");
            stp.Stop();
            timing.StopTime();
            Console.WriteLine($"StopWatch: {stp.Elapsed}");
            Console.WriteLine($"Timing: {timing.Result()}");

        }
        public static void PrintIndexAndKeysAndValues(Hashtable myHt)
        {
            int i = 0;
            Console.WriteLine("\t-Индекс-\t-Ключ-\t-Значение-");
            foreach (DictionaryEntry de in myHt)
                Console.WriteLine($"\t[{i++}]:\t{de.Key}\t{de.Value}");
            Console.WriteLine();
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
                try
                {
                    tmp = Process.GetCurrentProcess().Threads[i].UserProcessorTime.Subtract(threads[i]);
                    if (tmp > TimeSpan.Zero)
                    {
                        duration = tmp;
                    }
                }
                catch (Exception)
                {
                    break;
                }
                
            }
        }
        public TimeSpan Result()
        {
            return duration;
        }
    }
}
