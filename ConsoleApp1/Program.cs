using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class A
    {
        public virtual void Start() => Console.WriteLine("A.start");
        public virtual void Work() => Console.WriteLine("A.work");
    }
    class B : A
    {
        public new void Start() => Console.WriteLine("B.start");
        public override void Work() => Console.WriteLine("B.work");
    }
    class C : B
    {
        public void Start() => Console.WriteLine("C.start");
        public override void Work() => Console.WriteLine("C.work");
    }

    class Program
    {
        static void metodWork(A _in)
        {
            Console.WriteLine("metod Work");
            _in.Work();
        }
        static void metodWork(B _in)
        {
            Console.WriteLine("metod Work");
            _in.Work();
        }
        static void metodWork(C _in)
        {
            Console.WriteLine("metod Work");
            _in.Work();
        }
        static void metodStart(B _in)
        {
            Console.WriteLine("metod Start");
            _in.Start();
        }

        static void metodStart(A _in)
        {
            Console.WriteLine("metod Start");
            _in.Start();
        }

        static void metodStart(C _in)
        {
            Console.WriteLine("metod Start");
            _in.Start();
        }


        static void Main(string[] args)
        {
            C obj = new C();

           // metodWork((A)obj); //будет вызван метод Ворк класса C, т.к. класс А является также классом Б, и в классе Б метод override
           // metodWork((B)obj); //будет вызван метод Ворк класса C 
           // metodWork((C)obj); //будет вызван метод Ворк класса c

            metodStart((A)obj); //будет вызван метод Старт класса А, т.к. метод Старт класса C, скрыт от класса А (написано в классе C     public void == public new void)
            metodStart((B)obj); //будет вызван метод Старт класса Б, т.к. метод Старт класса C является новым (скрытым)
            metodStart((C)obj); //будет вызван метод Старт класса C, т.к. метод Старт класса C является новым (скрытым)

            Func<int, int, double> f = (int i, int y) => i * y * 1.00;
            object h = 9;
            Console.WriteLine(h);
            Console.WriteLine(f(5,5).ToString());
            Expression<Func<int, int, double>> e = (int i, int y) => i * y;
            Console.WriteLine();
            
            string[] s = { "Gleb", "Pavel", "Guzel", "Sergey", "Mainsis", "Berkak" };

            Expression<Func<string, bool>> exp = (string i) => i.Equals("Pavel");
            var vvv = s.Where(exp.Compile()).Select(x => x.Length).FirstOrDefault();

            Func<string, bool> fun = (string i) => i.Equals("Pavel");
            var vvvv = s.Where(c => fun(c)).Select(x => x.Length).FirstOrDefault();

            Predicate<string> pre = (string i) => i.Equals("Pavel");
            var vvvvv = s.Where(c => pre(c)).Select(x => x.Length).FirstOrDefault();

            var vvvvvv = s.Where(delegate (string c) { return c.Equals("Pavel"); }).Select(x => x.Length).FirstOrDefault();

            var vv = s.Where(c => c.Equals("Pavel")).Select(x => x.Length).FirstOrDefault();
            
            var v = from str in s
                    where str.Length > 4
                    select str
                        into Temp
                    let x = Temp.LastIndexOf('l')
                    where x != -1 && Temp.Substring(x) == "l"
                    select Temp;
            foreach (var Result in v)
            {
                Console.WriteLine(Result);
            }

            Console.ReadKey();
        }
    }
}
