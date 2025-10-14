using System;

namespace FractionFunctionApp
{
    // 🔹 Базовий клас — дробово-лінійна функція виду (a1*x + a0) / (b1*x + b0)
    class FractionLinear
    {
        private double a1, a0, b1, b0;
        protected const double EPS = 1e-9; // поріг для перевірки ділення на нуль

        // 🔸 Властивості (Properties)
        public double A1 { get => a1; set => a1 = value; }
        public double A0 { get => a0; set => a0 = value; }
        public double B1 { get => b1; set => b1 = value; }
        public double B0 { get => b0; set => b0 = value; }

        // 🔸 Конструктор без параметрів
        public FractionLinear() { }

        // 🔸 Параметризований конструктор
        public FractionLinear(double a1, double a0, double b1, double b0)
        {
            A1 = a1;
            A0 = a0;
            B1 = b1;
            B0 = b0;
        }

        // 🔸 Метод задання коефіцієнтів
        public virtual void SetCoefficients(double a1, double a0, double b1, double b0)
        {
            A1 = a1;
            A0 = a0;
            B1 = b1;
            B0 = b0;
        }

        // 🔸 Метод обчислення значення функції в точці x0
        public virtual double Calculate(double x0)
        {
            double denominator = B1 * x0 + B0;

            if (Math.Abs(denominator) < EPS)
            {
                Console.WriteLine("⚠️ Помилка: знаменник наближений до нуля!");
                return double.NaN;
            }

            return (A1 * x0 + A0) / denominator;
        }

        // 🔸 Метод виведення (ToString)
        public override string ToString()
        {
            return $"f(x) = ({A1}x + {A0}) / ({B1}x + {B0})";
        }
    }

    // 🔹 Похідний клас — дробова функція виду (a2*x² + a1*x + a0) / (b2*x² + b1*x + b0)
    class FractionQuadratic : FractionLinear
    {
        private double a2, b2;

        public double A2 { get => a2; set => a2 = value; }
        public double B2 { get => b2; set => b2 = value; }

        // 🔸 Конструктор без параметрів
        public FractionQuadratic() { }

        // 🔸 Параметризований конструктор
        public FractionQuadratic(double a2, double a1, double a0, double b2, double b1, double b0)
            : base(a1, a0, b1, b0)
        {
            A2 = a2;
            B2 = b2;
        }

        // 🔸 Перевизначення SetCoefficients (повністю узгоджене з базовим)
        public override void SetCoefficients(double a2, double a1, double b2, double b1)
        {
            A2 = a2;
            A1 = a1;
            B2 = b2;
            B1 = b1;
        }

        // 🔸 Перевантажений варіант SetCoefficients для повного набору
        public void SetCoefficients(double a2, double a1, double a0, double b2, double b1, double b0)
        {
            A2 = a2;
            A1 = a1;
            A0 = a0;
            B2 = b2;
            B1 = b1;
            B0 = b0;
        }

        // 🔸 Перевизначений метод Calculate
        public override double Calculate(double x0)
        {
            double denominator = B2 * x0 * x0 + B1 * x0 + B0;

            if (Math.Abs(denominator) < EPS)
            {
                Console.WriteLine("⚠️ Помилка: знаменник наближений до нуля!");
                return double.NaN;
            }

            return (A2 * x0 * x0 + A1 * x0 + A0) / denominator;
        }

        // 🔸 Перевизначення ToString
        public override string ToString()
        {
            return $"f(x) = ({A2}x² + {A1}x + {A0}) / ({B2}x² + {B1}x + {B0})";
        }
    }

    // 🔹 Головна програма
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Оберіть тип функції (1 — дробово-лінійна, 2 — дробова): ");
            char userChoose = Console.ReadKey().KeyChar;
            Console.WriteLine();

            FractionLinear func; // покажчик на базовий клас

            if (userChoose == '1')
            {
                func = new FractionLinear(2, 3, 1, 4); // (2x + 3)/(1x + 4)
            }
            else if (userChoose == '2')
            {
                func = new FractionQuadratic(1, 2, 3, 2, 1, 4); // (x² + 2x + 3)/(2x² + x + 4)
            }
            else
            {
                Console.WriteLine("❌ Некоректний вибір! Програма завершена.");
                return;
            }

            Console.WriteLine("\n" + func.ToString());

            Console.Write("\nВведіть значення x0: ");
            if (!double.TryParse(Console.ReadLine(), out double x0))
            {
                Console.WriteLine("❌ Некоректне значення x0!");
                return;
            }

            double result = func.Calculate(x0);
            Console.WriteLine($"Результат: f({x0}) = {result:F3}");
        }
    }
}
