using System;

namespace LinqTutorials
{
    class Program
    {
        static void Main(string[] args)
        {
            var t1 = LinqTasks.Task1();
            Console.WriteLine(t1.ToString());

            var t2 = LinqTasks.Task2();
            Console.WriteLine(t2.ToString());

            var t3 = LinqTasks.Task3();
            Console.WriteLine(t3);

            var t4 = LinqTasks.Task4();
            Console.WriteLine(t4.ToString());

            var t5 = LinqTasks.Task5();
            Console.WriteLine(t5);

            var t6 = LinqTasks.Task6();
            Console.WriteLine(t6);

            var t7 = LinqTasks.Task7();
            Console.WriteLine(t7);

            var t8 = LinqTasks.Task8();
            Console.WriteLine(t8);

            var t9 = LinqTasks.Task9();
            Console.WriteLine(t9);

            var t10 = LinqTasks.Task10();
            Console.WriteLine(t10);

            var t11 = LinqTasks.Task11();
            Console.WriteLine(t11);

            var t12 = LinqTasks.Task12();
            Console.WriteLine(t12);
            int[] x = { 1, 1, 13, 1, 1, 1, 1, 1, 1, 1, 1 };
            var t13 = LinqTasks.Task13(x);
            Console.WriteLine(t13);

            var t14 = LinqTasks.Task14();
            Console.WriteLine(t14);


        }
    }
}
