using System;
using System.Collections.Generic;
using System.Linq;
using TestForLines.Models;

namespace TestForLines
{
    internal class Program
    {
        static void Main()
        {
            DataWorker dataWorker = new();
            PrintTable(dataWorker.GetDataFromDb(GetMonth(), GetYear(), GetTupeOfSort()));
            Console.WriteLine("Повторить? (да/нет)");
            if (Console.ReadLine() == "да")
            {
                Console.Clear();
                Main();
            }
        }
        private static int GetMonth()
        {
            Console.WriteLine("Введите месяц для вывода отчета, в формате 01");
            var month = Console.ReadLine();

            if (int.TryParse(month, out var res) && month.Length == 2)
                return res;
            return GetMonth();
        }
        private static int GetYear()
        {
            Console.WriteLine("Введите год для вывода отчета, в формате 2002");
            var year = Console.ReadLine();

            if (int.TryParse(year, out var res) && year.Length == 4)
                return res;
            return GetMonth();
        }
        private static string GetTupeOfSort()
        {
            Console.WriteLine("Введите цифру метода сотритвки:");
            Console.WriteLine("1 - Сортировка по дате прибытия");
            Console.WriteLine("2 - Сортировка по имени мастера");
            Console.WriteLine("3 - Сортировка по марке авто");
            var tupeOfSort = Console.ReadLine();

            if (int.TryParse(tupeOfSort, out var res) && tupeOfSort.Length == 1)
            {
                switch (res)
                {
                    case 1:
                        return "WorkOnCars.DateStart";
                    case 2:
                        return "Masters.Name";
                    case 3:
                        return "Cars.Model";
                }
            }
            return GetTupeOfSort();
        }
        private static void PrintTable(List<ModelToTable> list)
        {
            Console.Clear();
            if (list.Count == 0)
            {
                Console.WriteLine("Данный месяц пуст");
                return;
            }

            Console.WriteLine($" Имя мастера\t| Модель машины\t| Стоимость ремонта");
            List<Tuple<string, double, double>> tupleOfMasterAndWorkHours = new();
            foreach (var masterWithHisWork in list)
            {
                Console.WriteLine($"------------------------------------------------------");
                Console.WriteLine($"{masterWithHisWork.Name,10}\t|{masterWithHisWork.Model,10}\t|{masterWithHisWork.Money,10}");
                tupleOfMasterAndWorkHours.Add(new(masterWithHisWork.Name, (masterWithHisWork.DateEnd - masterWithHisWork.DateStart).TotalHours, masterWithHisWork.Money));
            }
            PrintResults(tupleOfMasterAndWorkHours);
        }
        private static void PrintResults(List<Tuple<string, double, double>> tupleOfMasterAndWorkHours)
        {
            var sum = tupleOfMasterAndWorkHours.Sum(X => X.Item2);
            var sum1 = tupleOfMasterAndWorkHours.Sum(X => X.Item3);
            foreach (var item in tupleOfMasterAndWorkHours.GroupBy(x => x.Item1))
            {
                Console.WriteLine($"Промежуточный итог мастера {item.Key}:\t{item.Sum(x => x.Item3)} руб. \tзагрузочность: {(item.Sum(x => x.Item2) / sum) * 100}%");
            }
            Console.WriteLine($"Общий итог: {sum1} руб.");
        }
    }

}
