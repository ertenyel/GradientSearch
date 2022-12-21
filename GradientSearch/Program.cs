using System;
using System.Collections.Generic;
using System.Linq;


namespace GradientSearch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FillDataTableByFirstExpression(2, 1, 0.001);
            //FillDataTableBySecondExpression(0.1, -1, 1);
        }
        private static double Function(bool FirstExpression, double X, double Y)
        {
            double sqX = Math.Pow(X, 2);
            double sqY = Math.Pow(Y, 2);
            double result;
            if (FirstExpression)
                result = Math.Pow(Math.E, X + Y) * (sqX - (2 * sqY));
            else
                result = 4 + Math.Pow(Math.Pow(sqX + sqY, 2), 1.0 / 3.0);
            return result;
        }
        private static double GetStep(bool FirstExpression, double X, double Y)
        {
            double sqX = Math.Pow(X, 2);
            double sqY = Math.Pow(Y, 2);
            double result;
            if (FirstExpression)
                result = Math.Pow(Math.E, X + Y) * (sqX - (2 * sqY));
            else
                result = 4 + Math.Pow(Math.Pow(sqX + sqY, 2), 1.0 / 3.0);
            return result;
        }

        private static void FillDataTableByFirstExpression(double X, double Y, double h)
        {
            int count = 0;
            double FloatPointSide = Math.Pow(10, -20);
            List<List<double>> HistoryValues = new List<List<double>>();                        
            while (true)
            {
                count++;
                double sqX = Math.Pow(X, 2);
                double sqY = Math.Pow(Y, 2);
                double Fxy = Function(true, X, Y);
                double DfDx = (2 * X * Math.Exp(X + Y)) + ((sqX - (2 * sqY)) * Math.Exp(X + Y));
                double DfDy = (-4 * Y * Math.Exp(X + Y)) + ((sqX - (2 * sqY)) * Math.Exp(X + Y));
                double gradF = Math.Sqrt(Math.Pow(DfDx, 2) + Math.Pow(DfDy, 2));
                List<double> TempHistoryValues = new List<double>() { X, Y, Fxy, gradF };
                HistoryValues.Add(TempHistoryValues);
                if (HistoryValues.Count > 4)
                    HistoryValues.RemoveAt(0);
                

                if (count < 7)
                {
                    Console.Write($"{X}\t{Y}\t{Fxy}\t{gradF}\n");
                }
                if (HistoryValues.Count > 1)
                {
                    if (/*Math.Abs(gradF - HistoryValues[^2].Last())*/gradF < FloatPointSide)
                    {
                        Console.WriteLine("\nПоследние значения");
                        foreach (List<double> item in HistoryValues)
                        {
                            foreach (var row in item)
                                Console.Write($"{row}\t");
                            Console.WriteLine();
                        }
                        return;
                    }
                }


                X -= h * DfDx;
                Y -= h * DfDy;
                if (Function(true, X, Y) < Fxy)
                {
                    h /= 2;
                }
            }
        }
        private static void FillDataTableBySecondExpression(double X, double Y, double h)
        {
            int count = 0;
            double FloatPointSide = Math.Pow(10, -20);
            List<List<double>> HistoryValues = new List<List<double>>();
            double Fxy;
            double DfDx;
            double DfDy;
            double gradF;
            Console.Write("X\t\t\tY\t\t\tF(X;Y)\t\t\tDfDx\t\t\tDfDY\t\t\tGradF\t\n");
            while (true)
            {
                count++;
                double sqX = Math.Pow(X, 2);
                double sqY = Math.Pow(Y, 2);
                Fxy = 4 + Math.Pow(Math.Pow(sqX + sqY, 2), 1 / 3);
                DfDx = 4 * X / (3 * Math.Pow(sqX + sqY, 1 / 3));
                DfDy = 4 * Y / (3 * Math.Pow(sqX + sqY, 1 / 3));
                gradF = Math.Abs(Math.Sqrt(Math.Pow(DfDx, 2) + Math.Pow(DfDy, 2)));
                List<double> TempHistoryValues = new List<double>() { X, Y, Fxy, gradF };
                HistoryValues.Add(TempHistoryValues);
                if (HistoryValues.Count > 4)
                    HistoryValues.RemoveAt(0);

                if (count < 5)
                    Console.Write(X + "\t" + Y + "\t" + Fxy + "\t" + gradF + "\t\n");

                if (HistoryValues.Count > 1)
                {
                    double checkValues = Math.Abs(gradF - HistoryValues[^2].Last());
                    if (checkValues < FloatPointSide)
                    {
                        Console.WriteLine("Последние значения");
                        foreach (List<double> item in HistoryValues)
                        {
                            foreach (var row in item)
                                Console.Write($"{row}\t");
                            Console.WriteLine();
                        }
                        return;
                    }
                }

                X -= h * gradF;
                Y -= h * gradF;
            }
        }
    }
}
