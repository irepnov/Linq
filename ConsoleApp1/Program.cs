using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    public class A
    {
        public virtual void Start() => Console.WriteLine("A.start");
        public virtual void Work() => Console.WriteLine("A.work");
    }
    public class B : A
    {
        public new void Start() => Console.WriteLine("B.start");
        public override void Work() => Console.WriteLine("B.work");
    }
    public class C : B
    {
        public void Start() => Console.WriteLine("C.start");
        public override void Work() => Console.WriteLine("C.work");
        public void Finish() => Console.WriteLine("C.finish");
    }

  //  [XmlInclude(typeof(Child))]
    public class Parent
    {
        public string firstname;
        public string lastname;
    }
    public class Child: Parent
    {
        public string fullname;
    }

    
    [XmlRoot("Objects")]
    [XmlInclude(typeof(Parent))]
    [XmlInclude(typeof(Child))]
    public class Group
    {
        public int num;            
        public Parent[] objects;
    }

    public class Program
    {
        public static void metodWork(A _in)
        {
            Console.WriteLine("metod Work");
            _in.Work();
        }
        public static void metodWork(B _in)
        {
            Console.WriteLine("metod Work");
            _in.Work();
        }
        public static void metodWork(C _in)
        {
            Console.WriteLine("metod Work");
            _in.Work();
        }
        public static void metodStart(B _in)
        {
            Console.WriteLine("metod Start");
            _in.Start();
        }
        public static void metodStart(A _in)
        {
            Console.WriteLine("metod Start");
            _in.Start();
        }
        public static void metodStart(C _in)
        {
            Console.WriteLine("metod Start");
            _in.Start();
        }


        public void SerializeObject(string filename)
        {
            // Creates a new XmlSerializer.
            XmlSerializer s = new XmlSerializer(typeof(Group));

            // Writing the XML file to disk requires a TextWriter.
            TextWriter writer = new StreamWriter(filename);
            Group group = new Group();

            Parent manager = new Parent();
            Child emp1 = new Child();
            Child emp2 = new Child();
            manager.firstname = "fir";
            manager.lastname = "las";
            emp1.firstname = "fir1";
            emp1.lastname = "las1";
            emp1.fullname = "full1";

            emp2.firstname = "fir2";
            emp2.fullname = "full2";
            Parent[] emps = new Parent[3] { manager, emp1, emp2 };
            group.num = emps.Length;
            group.objects = emps;

            // Serializes the object, and closes the StreamWriter.
            s.Serialize(writer, group);
            writer.Close();
        }

        public void DeserializeObject(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            XmlSerializer x = new XmlSerializer(typeof(Group));
            Group g = (Group)x.Deserialize(fs);
            Console.WriteLine("Members:");

            foreach (Parent e in g.objects)
            {
                Console.WriteLine("\t" + e.firstname + " " + e.ToString());
            }
        }

        public static void Main(string[] args)
        {
            Program test = new Program();
            test.SerializeObject("TypeDoc.xml");
            test.DeserializeObject("TypeDoc.xml");

            C obj = new C();

            metodWork((A)obj); //будет вызван метод Ворк класса C, т.к. класс А является также классом Б, и в классе Б метод override
            metodWork((B)obj); //будет вызван метод Ворк класса C 
            metodWork((C)obj); //будет вызван метод Ворк класса c

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
