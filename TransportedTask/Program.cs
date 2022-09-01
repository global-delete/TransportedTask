using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportedTask
{
    class Program
    {
        static WorkClass wk;

        static void Main(string[] args)
        {
           
            //wk = new WorkClass(3, 4, new double[3] { 30, 40, 20 }, new double[4] { 20, 30, 30, 10 }, new double[3, 4] { { 4, 3, 5, 6 },
            //                                                                                                            { 8, 2, 4, 7 },
            //                                                                                                            { 1, 9, 10, 2 } }  );
            //wk = new WorkClass(3, 4, new double[3] { 90, 35, 25 }, new double[4] { 30, 40, 60, 80 }, new double[3, 4] { { 0.4, 4.0, 2.7, 4.8 },
            //                                                                                                            { 0.6, 3.2, 1.9, 4.0 },
            //                                                                                                            { 4.6, 1.0, 2.2, 1.1 } });
            wk = new WorkClass(3, 5, new double[3] { 140, 180, 160 }, new double[5] { 60, 70, 120, 130, 100 }, new double[3, 5] { { 2, 3, 4, 2, 4 },
                                                                                                                                  { 3, 4, 1, 4, 1 },
                                                                                                                                  { 9, 7, 3, 7, 2 } });
            //wk.NorthWest();
            wk.minTask();
            Console.WriteLine("S=" + WorkClass.sumTask);
            wk.findP();

            

            wk.findResiduals();
            Console.WriteLine("Решение неоптимально");
            Console.WriteLine("Delta[1,4]=-5");
          

            Console.ReadKey();
        }
    }
}
