using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Space_speed
{
    class Program
    {
        public const double G = 0.00000000006743014;
        public static sbyte M0;
        public static sbyte R0;
        public static double V1;
        public static double V2;
        public static double V3;
        public static sbyte M;
        public static sbyte R;
        public static string N;
        static void Main(string[] args)
        {
            Console.WriteLine("\n    Доброго времени суток!\n ПРЕДУПРЕЖДЕНИЕ! Пожалуйста, следуйте инструкциям и вводите ЧИСЛА, дабы избежать ошибок");
            Console.WriteLine("\n Отлично! Для начала введите, пожалуйста, название Планеты");
            string N = Console.ReadLine();
            int Mcycleid = 1; //нужно для проверки на положительное число
            bool isCorrect = false; //нужно для проверки на ввод буквы
            Console.WriteLine("\n Введите массу планеты M (в килограммах)");
            while (!isCorrect) //проверка на ввод буквы
            {
                isCorrect = sbyte.TryParse(Console.ReadLine(), out M0);
                if (!isCorrect)
                    Console.WriteLine("***** Ошибка ввода! Введите ПОЛОЖИТЕЛЬНО ЧИСЛО *****");
            }
            while (Mcycleid == 1) //начало проверки на положительное число
            {
                if (M0 > 0)
                {
                    Mcycleid = 0;
                }
                else
                {
                    Console.WriteLine("***** Ошибка ввода! Введите ПОЛОЖИТЕЛЬНОЕ ЧИСЛО *****");
                    sbyte.TryParse(Console.ReadLine(), out M0);
                }
            }
            int Rcycleid = 1; //нужно для проверки на положительное число
            bool iCorrect = false; //нужно для проверки на ввод буквы
            Console.WriteLine("\n Введите радиус планеты R (в метрах)");
            while (!iCorrect) //проверка на ввод буквы
            {
                iCorrect = sbyte.TryParse(Console.ReadLine(), out R0);
                if (!iCorrect)
                    Console.WriteLine("***** Ошибка ввода! Введите ПОЛОЖИТЕЛЬНОЕ ЧИСЛО *****");
            }
            while (Rcycleid == 1)//начало проверки на положительное число
            {
                if (R0 > 0)
                {
                    Rcycleid = 0;
                }
                else
                {
                    Console.WriteLine("***** Ошибка ввода! Введите ПОЛОЖИТЕЛЬНОЕ ЧИСЛО *****");
                    sbyte.TryParse(Console.ReadLine(), out R0);
                }
            }
            Console.Write("Все необходимые данные записаны\nПриготовтесь, консоль будет очищена через 5..");
            Thread.Sleep(1000);
            Console.Write("4..");
            Thread.Sleep(1000);
            Console.Write("3..");
            Thread.Sleep(1000);
            Console.Write("2..");
            Thread.Sleep(1000);
            Console.Write("1..");
            Thread.Sleep(1000);
            Console.Clear();
            {
                R = R0;  //начало расчета скоростей
                M = M0;
                V1 = (G * M) / R;
                V1 = Math.Sqrt(V1);
                Console.WriteLine("\nПервая космическая скорость");
                Console.WriteLine($"{V1} м/с");
                V2 = V1 * Math.Sqrt(2);
                Console.WriteLine("\nВторая космическая скорость");
                Console.WriteLine($"{V2} м/с");
                V3 = (Math.Sqrt(2) - 1) * (Math.Sqrt(2) - 1) * V1 * V1 + (V2 * V2);
                V3 = Math.Sqrt(V3);
                Console.WriteLine("\nТретья космическая скорость");
                Console.WriteLine($"{V3} м/с");
                Console.ReadKey();
            }
            using (PlanetsContext db = new PlanetsContext()) 
            {
                Planets_Speed planet1 = new Planets_Speed { Name = $"{N}", Mass = M, Radius = R, First_Speed = V1, Second_Speed = V2, Third_Speed = V3 };
                // добавление данных в базу данных
                db.Planets_Speed.Add(planet1);
                db.SaveChanges();
                Console.WriteLine("Объект успешно сохранен");
                    var planet_speed = db.Planets_Speed;
                Console.WriteLine("Список планет");
                foreach (Planets_Speed u in planet_speed) //вывод самой базы
                {
                    Console.WriteLine($"Планета: {u.Name}  Масса: {u.Mass} кг  Радиус: {u.Radius} м   I скорость{u.First_Speed} м/с  II скорость{u.Second_Speed} м/с  III скорость{u.Third_Speed} м/с ");
                }
            }
            Console.Read();
        }
        public class Planets_Speed 
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Mass { get; set; }
            public int Radius { get; set; }
            public int First_Speed { get; set; }
            public int Second_Speed { get; set; }
            public int Third_Speed { get; set; }
        }
        public class PlanetsContext : DbContext
        {
            public PlanetsContext() :
                base("PlanetsDB")
            {
            }
            public DbSet<Planets_Speed> Planets_Speed { get; set; }
        }
    }
}


