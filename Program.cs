using Labb3._1Database.Data;
using Labb3._1Database.Models;

namespace Labb3._1Database
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunProgram();
        }
        static void RunProgram()
        {
            Menu();
        }
        static void Menu()
        {
            DatabaseDBContext context = new DatabaseDBContext();

            Console.WriteLine("1. Hämta alla elever");
            Console.WriteLine("2. Hämta alla elever i en viss klass");
            Console.WriteLine("3. Lägg till ny personal");
            switch (Console.ReadLine())
            {
                case "1":
                    GetStudents1(context, false);
                    break;
                case "2":
                    GetStudents1(context, true);
                    break;
                case "3":
                    AddWorker(context);
                    break;

                default:
                    break;
            }

        }
        public static Personal CreateWorker()
        {
            Console.WriteLine("Ange För- och Efternamn (ex. Max Dahlberg)");
            string workerName = Console.ReadLine();
            Console.WriteLine("Ange Personnummer (ex. 20011107-xxxx)");
            string workerSocial = Console.ReadLine();
            Console.WriteLine("Ange Befattning (ex. Lärare)");
            string workerSpecification = Console.ReadLine();

            Personal worker = new Personal()
            {
                Namn = workerName,
                Personnum = workerSocial,
                Befattning = workerSpecification
            };

            return worker;
        }
        public static void AddWorker(DatabaseDBContext context)
        {
            Personal worker = CreateWorker();
            context.Personals.Add(worker);
            context.SaveChanges();
        }
        public static void GetStudents1(DatabaseDBContext context, bool sortByKlass)
        {
            int klassName = 0;
            if (sortByKlass)
            {
                Console.WriteLine("Vilken Klass vill du sortera efter?");
                foreach (var item in context.Klasses)
                {
                    Console.WriteLine($"-{item.ID}) Klass: {item.Namn}");
                }
                klassName = int.Parse(Console.ReadLine());
            }
            
            Console.WriteLine("Vill du sortera efter Namn eller Efternamn");
            switch (Console.ReadLine().ToUpper())
            {
                case "NAMN":
                    SortStudentsByName(context, klassName);
                    break;
                case "EFTERNAMN":
                    SortStudentsByLastName(context, klassName);
                    break;
                default:
                    Console.WriteLine("Du måste svara antingen (Namn) eller (Efternamn)");
                    break;
            }
            
            
        }
        public static void SortStudentsByName(DatabaseDBContext context, int klassIDNumber)
        {
            bool sortedStudents = GetStudentsSorted();

            if (sortedStudents && klassIDNumber == 0)
            {
                var students = context.Students
                        .OrderBy(x => x.Namn);
                DisplaySortedStudent(students);
                return;
            }
            else if (klassIDNumber == 0)
            {
                var students = context.Students
                        .OrderByDescending(x => x.Namn);
                DisplaySortedStudent(students);
                return;

            }
            else if (sortedStudents)
            {
                var students = context.Students
                        .Where(x => x.KlassID == klassIDNumber)
                        .OrderBy(x => x.Namn);
                DisplaySortedStudent(students);
                return;
            }
            else
            {
                var students = context.Students
                        .Where(x => x.KlassID == klassIDNumber)
                        .OrderByDescending(x => x.Namn);
                DisplaySortedStudent(students);
                return;
            }
        }
        public static void SortStudentsByLastName(DatabaseDBContext context, int klassIDNumber)
        {
            bool sortedStudents = GetStudentsSorted();

            if (sortedStudents && klassIDNumber == 0)
            {
                var students = context.Students
                        .OrderBy(x => x.Efternamn);
                DisplaySortedStudent(students);
                return;
            }
            else if (klassIDNumber == 0)
            {
                var students = context.Students
                        .OrderByDescending(x => x.Efternamn);
                DisplaySortedStudent(students);
                return;
            }
            else if (sortedStudents)
            {
                var students = context.Students
                        .Where(x => x.KlassID == klassIDNumber)
                        .OrderBy(x => x.Efternamn);
                DisplaySortedStudent(students);
                return;
            }
            else
            {
                var students = context.Students
                        .Where(x => x.KlassID == klassIDNumber)
                        .OrderByDescending(x => x.Efternamn);
                DisplaySortedStudent(students);
                return;
            }
        }
        public static bool GetStudentsSorted()
        {
            do
            {
                Console.WriteLine("Vill du att det ska vara Fallande eller Stigande");
                switch (Console.ReadLine().ToUpper())
                {
                    case "FALLANDE":
                        return true;
                    case "STIGANDE":
                        return false;
                    default:
                        Console.WriteLine("Du måste svara antingen (Fallande) eller (Stigande)");
                        break;
                }
            } while (true);
        }
        public static void DisplaySortedStudent(IOrderedQueryable<Student> students)
        {
            foreach (Student student in students)
            {
                Console.WriteLine($"Förnamn: {student.Namn} Efternamn: {student.Efternamn} Klass: {student.KlassID}");
            }
        }
    }
}