using System;

namespace FractionFunctionApp
{
    // 🔹 Базовий клас — дробово-лінійна функція виду (a1*x + a0) / (b1*x + b0)
    class FractionLinear
    {
        protected double a1, a0, b1, b0;

        // Метод задання коефіцієнтів
        public virtual void SetCoefficients(double a1, double a0, double b1, double b0)
        {
            this.a1 = a1;
            this.a0 = a0;
            this.b1 = b1;
            this.b0 = b0;
        }

        // Метод виведення коефіцієнтів
        public virtual void ShowCoefficients()
        {
            Console.WriteLine($"Функція: f(x) = ({a1}x + {a0}) / ({b1}x + {b0})");
        }

        // Метод обчислення значення функції в точці x0
        public virtual double Calculate(double x0)
        {
            if (b1 * x0 + b0 == 0)
            {
                Console.WriteLine("Ділення на нуль!");
                return double.NaN;
            }

            return (a1 * x0 + a0) / (b1 * x0 + b0);
        }
    }

    // 🔹 Похідний клас — дробова функція виду (a2*x² + a1*x + a0) / (b2*x² + b1*x + b0)
    class FractionQuadratic : FractionLinear
    {
        protected double a2, b2;

        // Перевизначений метод задання коефіцієнтів
        public void SetCoefficients(double a2, double a1, double a0, double b2, double b1, double b0)
        {
            this.a2 = a2;
            this.a1 = a1;
            this.a0 = a0;
            this.b2 = b2;
            this.b1 = b1;
            this.b0 = b0;
        }

        // Перевизначений метод виведення
        public override void ShowCoefficients()
        {
            Console.WriteLine($"Функція: f(x) = ({a2}x² + {a1}x + {a0}) / ({b2}x² + {b1}x + {b0})");
        }

        // Перевизначений метод обчислення
        public override double Calculate(double x0)
        {
            double denominator = b2 * x0 * x0 + b1 * x0 + b0;
            if (denominator == 0)
            {
                Console.WriteLine("Ділення на нуль!");
                return double.NaN;
            }

            return (a2 * x0 * x0 + a1 * x0 + a0) / denominator;
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
                func = new FractionLinear();
                func.SetCoefficients(2, 3, 1, 4);  // Наприклад: (2x + 3) / (1x + 4)
            }
            else
            {
                func = new FractionQuadratic();
                ((FractionQuadratic)func).SetCoefficients(1, 2, 3, 2, 1, 4); // (x² + 2x + 3) / (2x² + x + 4)
            }

            func.ShowCoefficients();

            Console.Write("\nВведіть значення x0: ");
            double x0 = Convert.ToDouble(Console.ReadLine());

            double result = func.Calculate(x0);
            Console.WriteLine($"Значення функції у точці x0 = {x0} дорівнює {result:F3}");
        }
    }
}
