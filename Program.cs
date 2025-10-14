using System;

namespace FractionFunctionApp
{
    // 🔸 Базовий клас
    class FractionFunction
    {
        protected const double EPS = 1e-9;
        protected double a1, a0, b1, b0;

        public FractionFunction(double a1 = 0, double a0 = 0, double b1 = 1, double b0 = 0)
        {
            this.a1 = a1;
            this.a0 = a0;
            this.b1 = b1;
            this.b0 = b0;
        }

        // Метод для задання коефіцієнтів
        public virtual void SetCoefficients()
        {
            Console.Write("Введіть a1: "); a1 = double.Parse(Console.ReadLine());
            Console.Write("Введіть a0: "); a0 = double.Parse(Console.ReadLine());
            Console.Write("Введіть b1: "); b1 = double.Parse(Console.ReadLine());
            Console.Write("Введіть b0: "); b0 = double.Parse(Console.ReadLine());
        }

        // Метод для виведення коефіцієнтів
        public virtual void PrintCoefficients()
        {
            Console.WriteLine($"a1 = {a1}, a0 = {a0}, b1 = {b1}, b0 = {b0}");
        }

        // 🔹 Динамічний поліморфізм — метод буде перевизначений у спадкоємцях
        public virtual double Calculate(double x)
        {
            double denominator = b1 * x + b0;

            if (Math.Abs(denominator) < EPS)
            {
                Console.WriteLine("⚠️ Помилка: знаменник наближений до нуля!");
                return double.NaN;
            }

            return (a1 * x + a0) / denominator;
        }

        public override string ToString()
        {
            return $"f(x) = ({a1}x + {a0}) / ({b1}x + {b0})";
        }
    }

    // 🔸 Похідний клас — дробова функція
    class QuadraticFractionFunction : FractionFunction
    {
        private double a2, b2;

        public QuadraticFractionFunction(double a2 = 0, double a1 = 0, double a0 = 0,
                                         double b2 = 0, double b1 = 1, double b0 = 0)
            : base(a1, a0, b1, b0)
        {
            this.a2 = a2;
            this.b2 = b2;
        }

        // Перевизначення методу для задання коефіцієнтів
        public override void SetCoefficients()
        {
            Console.Write("Введіть a2: "); a2 = double.Parse(Console.ReadLine());
            Console.Write("Введіть a1: "); a1 = double.Parse(Console.ReadLine());
            Console.Write("Введіть a0: "); a0 = double.Parse(Console.ReadLine());
            Console.Write("Введіть b2: "); b2 = double.Parse(Console.ReadLine());
            Console.Write("Введіть b1: "); b1 = double.Parse(Console.ReadLine());
            Console.Write("Введіть b0: "); b0 = double.Parse(Console.ReadLine());
        }

        // Перевизначення методу для виведення коефіцієнтів
        public override void PrintCoefficients()
        {
            Console.WriteLine($"a2 = {a2}, a1 = {a1}, a0 = {a0}, b2 = {b2}, b1 = {b1}, b0 = {b0}");
        }

        // 🔹 Перевизначення методу (динамічний поліморфізм)
        public override double Calculate(double x)
        {
            double denominator = b2 * x * x + b1 * x + b0;

            if (Math.Abs(denominator) < EPS)
            {
                Console.WriteLine("⚠️ Помилка: знаменник наближений до нуля!");
                return double.NaN;
            }

            return (a2 * x * x + a1 * x + a0) / denominator;
        }

        public override string ToString()
        {
            return $"f(x) = ({a2}x² + {a1}x + {a0}) / ({b2}x² + {b1}x + {b0})";
        }

        // 🔹 Статичний поліморфізм — перевантаження методів
        public double Calculate(double x, bool absoluteValue)
        {
            double result = Calculate(x);
            return absoluteValue ? Math.Abs(result) : result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Демонстрація динамічного і статичного метаморфізму ===\n");

            Console.WriteLine("Оберіть режим роботи:");
            Console.WriteLine("1 — Дробово-лінійна функція");
            Console.WriteLine("2 — Дробова квадратична функція");
            Console.Write("Ваш вибір: ");
            string userChoose = Console.ReadLine();

            FractionFunction func;

            if (userChoose == "1")
            {
                func = new FractionFunction();
                Console.WriteLine("\nВведіть коефіцієнти для дробово-лінійної функції:");
                func.SetCoefficients();
            }
            else
            {
                func = new QuadraticFractionFunction();
                Console.WriteLine("\nВведіть коефіцієнти для дробової квадратичної функції:");
                func.SetCoefficients();
            }

            Console.WriteLine("\nКоефіцієнти функції:");
            func.PrintCoefficients();

            Console.Write("\nВведіть значення x: ");
            double x = double.Parse(Console.ReadLine());

            Console.WriteLine($"\n{func}");
            Console.WriteLine($"f({x}) = {func.Calculate(x):F3}");

            // Статичний поліморфізм (перевантаження) — тільки для QuadraticFractionFunction
            if (func is QuadraticFractionFunction qf)
            {
                Console.WriteLine($"|f({x})| = {qf.Calculate(x, true):F3}");
            }
        }
    }
}
