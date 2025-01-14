﻿using System;
using TaskDatas;

namespace Solvers
{
    class AnalyticSolver
    {
        protected readonly ITaskData task;
        public double C1;
        public double C2;
        public double lambda1;
        public double lambda2;
        public double uPrivate;

        public AnalyticSolver(ITaskData task)
        {
            this.task = task;
            double lambda = Math.Sqrt(task.Mqx / task.Mkx);
            lambda1 = lambda;
            lambda2 = -lambda;
            uPrivate = task.Mfx / task.Mqx;

            double AA = Math.Sqrt(task.Mqx * task.Mkx);
            double A1 = AA - task.B1;
            double A2 = AA * Math.Exp(lambda) + (task.B2 * Math.Exp(lambda));
            double D1 = AA + task.B1;
            double D2 = -AA * Math.Exp(-lambda) + (task.B2 * Math.Exp(-lambda));
            double G1 = (task.B1 * uPrivate) - task.E1;
            double G2 = -task.E2 + (task.B2 * uPrivate);
            double num2 = (A1 * D2) + (A2 * D1);
            C1 = ((G1 * D2) - (G2 * D1)) / num2;
            C2 = -((A1 * G2) + (A2 * G1)) / num2;
        }

        private double EvaluateSolution(double x) => (C1 * Math.Exp(lambda1 * x)) + (C2 * Math.Exp(lambda2 * x)) + uPrivate;

        public virtual double[] GetSolution(int knots)
        {
            double[] result = new double[knots];
            double step = 1.0 / (knots - 1);
            double currentX = 0.0;
            for (int i = 0; i < knots; i++)
            {
                result[i] = EvaluateSolution(currentX);
                currentX += step;
            }
            return result;
        }

        public void PrintEquation() => Console.WriteLine(value: "u = " + C1 + " * Exp(" + lambda1 + " * x) + " + C2 + " * Exp(" + lambda2 + " * x) + " + uPrivate);


    }
}
