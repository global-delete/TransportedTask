using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportedTask
{
    public class WorkClass
    {
        public int n, m, iMin, jMin;
        public double min;
        public double[] a, b, aCurr, bCurr, u, v;
        public double[,] c, d, x, delta, Cij;
        public bool[,] bx;
        public static double sumTask=1380, Cji;

        public WorkClass(int N, int M, double[] A, double[] B, double[,] C)
        {
            n = N;
            m = M;
            a = A;
            b = B;
            c = C;
            u = new double[n];
            v = new double[m];
            x = new double[n,m];
            min = x[1, 1];
            aCurr = new double[n];
            bCurr = new double[m];
            d = new double[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    d[i, j] = 0;
                }
            }
            for (int i = 0; i < n; i++)
            {
                aCurr[i] = a[i];
            }
            for (int j = 0; j < m; j++)
            {
                bCurr[j] = b[j];
            }
            viewPlan(c);
        }
        public void NorthWest()
        {            
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    try
                    {
                        if ((bCurr[j] >= aCurr[i]) & (bCurr[j] != 0))
                        {
                            x[i, j] = aCurr[i];
                            bCurr[j] = bCurr[j] - aCurr[i];
                            aCurr[i] = 0;
                        }
                        else
                        {
                            if ((aCurr[i] > bCurr[j]) & (aCurr[i] != 0))
                            {
                                x[i, j] = bCurr[j];
                                aCurr[i] = aCurr[i] - bCurr[j];
                                bCurr[j] = 0;
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            viewPlan(x);
        }
        public void minTask()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if ((i != 0) || (j != 0))
                    {
                        if (x[i, j] < min)
                        {
                            min = x[i, j];
                            iMin = i;
                            jMin = j;
                        }
                    }
                    else
                    {
                        min = x[i, j];
                        iMin = i;
                        jMin = j;
                    }
                }
            }
            if ((bCurr[jMin] >= aCurr[iMin]) & (bCurr[jMin] != 0))
            {
                x[iMin, jMin] = aCurr[iMin];
                bCurr[jMin] = bCurr[jMin] - aCurr[iMin];
                aCurr[iMin] = 0;
            }
            else
            {
                if ((aCurr[iMin] > bCurr[jMin]) & (aCurr[iMin] != 0))
                {
                    x[iMin, jMin] = bCurr[jMin];
                    aCurr[iMin] = aCurr[iMin] - bCurr[jMin];
                    bCurr[jMin] = 0;
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if ((i == iMin) && (j == jMin))
                    {
                        j++;
                    }
                    if (j <= m)
                    {
                        try
                        {
                            if ((bCurr[j] >= aCurr[i]) & (bCurr[j] != 0))
                            {
                                x[i, j] = aCurr[i];
                                bCurr[j] = bCurr[j] - aCurr[i];
                                aCurr[i] = 0;
                            }
                            else
                            {
                                if ((aCurr[i] > bCurr[j]) & (aCurr[i] != 0))
                                {
                                    x[i, j] = bCurr[j];
                                    aCurr[i] = aCurr[i] - bCurr[j];
                                    bCurr[j] = 0;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                }
            }
            viewPlan(x);
        }
        public void FindSum()
        {
            for (int j = 0; j < b.Length; j++)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    sumTask += c[j, i] * x[j, i];
                }
            }
         
        }
        
        public void findP() //Нахождение потенциалов
        {
            for (int i = 0; i < n; i++)
            {
                u[i] = 0;
            }
            for (int j = 0; j < m; j++)
            {
                v[j] = 0;
            }
            for (int i = 0; i < n; i++) //Вычисление потенциалов
            {
                for (int j = 0; j < m; j++) //
                {
                    if (x[i, j] != 0) //Если ячейка матрицы не пуста
                    {
                        if ((u[i] == 0) && (v[j] == 0)) //Если оба потенциыала не вычислены
                        {
                            v[j] = c[i, j];
                        }
                        else 
                        {
                            if (u[i] != 0) //Вычисляем один через другой
                            {
                                v[j] = c[i, j] - u[i];
                            }
                            else //Вычисляем один через другой
                            {
                                if (v[j] != 0)
                                {
                                    u[i] = c[i, j] - v[j];
                                }
                            }
                        }
                    }
                }
            }
            printP(); //Выводим на консоль
        }
        public void findResiduals() //Нахождение матрицы невязок
        {
            double[,] d = new double[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    d[i, j] = c[i, j] - u[i] - v[j]; //Заполнение матрицы невязок
                }
            }
            viewPlan(d);
        }
        public void viewPlan(double[,] matrix) //Вывод матрицы на консоль
        {
            Console.Write("  ");
            for (int j = 0; j < m; j++)
            {
                Console.Write("{0,6}", b[j]);
            }
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                Console.Write(a[i]);
                for (int j = 0; j < m; j++)
                {
                    Console.Write("{0,6}", matrix[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
      
        }
        public void printP()
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write("u{0} = {1}  ", i + 1, u[i]);
            }
            Console.WriteLine();
            for (int j = 0; j < m; j++)
            {
                Console.Write("v{0} = {1}  ", j + 1, v[j]);
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
