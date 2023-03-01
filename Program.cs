using Labb3._1Database.Data;
using Labb3._1Database.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Labb3._1Database
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunProgram();
        }

        public static void RunProgram()
        {
            bool closeProgram = true;
            do
            {
                Console.WriteLine("EF eller ADO");
                switch (Console.ReadLine().ToUpper())
                {
                    case "EF":
                        closeProgram = EFMenu();
                        break;
                    case "ADO":
                        closeProgram = ADOMenu();
                        break;
                    case "EXIT":
                        closeProgram = false;
                        break;
                    default:
                        break;
                }

            } while (closeProgram);
        }
        public static bool ADOMenu()
        {
            Console.WriteLine("1. Hämta alla lärare");
            Console.WriteLine("2. Lägg till personal");
            Console.WriteLine("3. Hämta lön per avdelning");
            Console.WriteLine("4. Hämta medellön per avdelning");
            Console.WriteLine("5. Hämta viktig elev information");
            switch (Console.ReadLine().ToLower())
            {
                case "1":
                    ShowWorkerInfo();
                    break;
                case "2":
                    AddNewWorker();
                    break;
                case "3":
                    SalaryPerRole();
                    break;
                case "4":
                    MedianSalaryPerRole();
                    break;
                case "5":
                    ImpoStudentInfo();
                    break;
                case "6":
                    TransactionOfGrades();
                    break;
                case "exit":
                    return false;
                default:
                    break;
            }
            return true;
        }
        public static bool EFMenu()
        {
            DatabaseDBContext context = new DatabaseDBContext();
            Console.WriteLine("1. Hämta alla elever");
            Console.WriteLine("2. Hämta alla elever i en viss klass");
            Console.WriteLine("3. Hämta hur många som jobbar inom samma avdelning");
            Console.WriteLine("4. Vissa all student information");
            Console.WriteLine("5. Vissa alla aktiva kurser");

            //Console.WriteLine("3. Lägg till ny personal");
            switch (Console.ReadLine().ToLower())
            {
                case "1":
                    GetStudents1(context, false);
                    break;
                case "2":
                    GetStudents1(context, true);
                    break;
                case "3":
                    PersonalInEachRole(context);
                    break;
                case "4":
                    ShowAllStudentInfo(context);
                    break;
                case "5":
                    ShowAllActivCourses(context);
                    break;
                //case "3":
                //    AddWorker(context);
                //    break;
                case "exit":
                    return false;
                default:
                    break;
            }
            return true;
        }
        public static void TransactionOfGrades()
        {
            List<string> entityInformation = new List<string>();

            Console.WriteLine("Ange StudentID (ex. 2 för Max Dahlberg)");
            string gradeStudentID = Console.ReadLine();
            Console.WriteLine("Ange KursID (ex. 1 för Matematik)");
            string gradeCourseID = Console.ReadLine();
            Console.WriteLine("Ange betyg (ex. Ett värde från 1 till 10 där 10 är bäst)");
            string gradeGrade = Console.ReadLine();
            Console.WriteLine("Ange LärareID (ex. 1003 för Oskar Jannson)");
            string gradeTeacherID = Console.ReadLine();
            

            entityInformation.Add(gradeStudentID);
            entityInformation.Add(gradeCourseID);
            entityInformation.Add(gradeGrade);
            entityInformation.Add(gradeTeacherID);

            TransactionOfGradesADO(entityInformation);
        }
        public static void TransactionOfGradesADO(List<string> entityInformation)
        {
            string connectionString = "Data Source = LAPTOP-GV0D7VCD; initial catalog = SchoolDBFirst; Integrated Security = True; TrustServerCertificate = True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand($"INSERT INTO Betygs(StudentId, KursID, Betygbetyg, PersonalID, Date)\r\n    VALUES ({entityInformation[0]}, {entityInformation[1]}, {entityInformation[2]}, {entityInformation[3]}, GETDATE());", connection, transaction);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                    Console.WriteLine("Shit should work!!");
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Console.WriteLine("oj, nått gick fel, rollback " + e);
                } 
            }
        }
        public static void ImpoStudentInfoADO(int studentID)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection("Data Source = LAPTOP-GV0D7VCD; database = SchoolDBFirst; Integrated Security = True; TrustServerCertificate = True;");

                SqlCommand command = new SqlCommand("GetStudentInfo", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter parameter1 = new SqlParameter
                {
                    ParameterName = "@StudentID",
                    SqlDbType = SqlDbType.Int,
                    Value = studentID,
                    Direction = ParameterDirection.Input

                };
                command.Parameters.Add(parameter1);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader["ID"]}, {reader["Name"]}, {reader["LastName"]}, {reader["PersonNumber"]}, {reader["KlassID"]}");
                }

                Console.WriteLine("Shit should work!!");
            }
            catch (Exception e)
            {
                Console.WriteLine("oj, nått gick fel " + e);
            }
            finally
            {
                connection.Close();
            }

        }
        public static void ImpoStudentInfo()
        {
            Console.WriteLine("Skriv ID för eleven");
            int studentID = int.Parse(Console.ReadLine());

            ImpoStudentInfoADO(studentID);
        }
        public static void SalaryPerRole()
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection("Data Source = LAPTOP-GV0D7VCD; database = SchoolDBFirst; Integrated Security = True; TrustServerCertificate = True;");

                SqlCommand command = new SqlCommand("SELECT Roles.RoleName AS Department, SUM(Salary) AS TotalSalary\r\nFROM Personals\r\nINNER JOIN Roles ON Personals.RoleID = Roles.ID\r\nGROUP BY Roles.RoleName", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Department"]}, {reader["TotalSalary"]}");
                }

                Console.WriteLine("Shit should work!!");
            }
            catch (Exception e)
            {
                Console.WriteLine("oj, nått gick fel " + e);
            }
            finally
            {
                connection.Close();
            }
        }
        public static void MedianSalaryPerRole()
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection("Data Source = LAPTOP-GV0D7VCD; database = SchoolDBFirst; Integrated Security = True; TrustServerCertificate = True;");

                SqlCommand command = new SqlCommand("SELECT Roles.RoleName AS Department, AVG(Salary) AS AverageSalary\r\nFROM Personals\r\nINNER JOIN Roles ON Personals.RoleID = Roles.ID\r\nGROUP BY Roles.RoleName", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Department"]}, {reader["AverageSalary"]}");
                }

                Console.WriteLine("Shit should work!!");
            }
            catch (Exception e)
            {
                Console.WriteLine("oj, nått gick fel " + e);
            }
            finally
            {
                connection.Close();
            }
        }
        public static void AddNewWorker()
        {
            List<string> entityInformation = new List<string>();

            Console.WriteLine("Ange Förnamn (ex. Max)");
            string workerFirstName = Console.ReadLine();
            Console.WriteLine("Ange Efternamn (ex. Dahlberg)");
            string workerLastName = Console.ReadLine();
            Console.WriteLine("Ange Personnummer (ex. 20011107-xxxx)");
            string workerSocial = Console.ReadLine();
            Console.WriteLine("Ange Lön (ex. 30000)");
            string workerSalary = Console.ReadLine();
            Console.WriteLine("Ange Anställnings Datum (ex. 2023-02-23)");
            string workerDate = Console.ReadLine();
            Console.WriteLine("Ange Befattnings ID (ex. 1 för Lärare)");
            string workerRole = Console.ReadLine();

            entityInformation.Add(workerFirstName);
            entityInformation.Add(workerLastName);
            entityInformation.Add(workerSocial);
            entityInformation.Add(workerSalary);
            entityInformation.Add(workerDate);
            entityInformation.Add(workerRole);

            string queryInsertWorker = $"INSERT INTO Personals(FirstName, LastName, PersonNumber, Salary, DateEmployed, RoleID)\r\nValues ('{entityInformation[0]}','{entityInformation[1]}','{entityInformation[2]}',{entityInformation[3]},'{entityInformation[4]}',{entityInformation[5]});";

            AddNewWorkerADO(entityInformation, queryInsertWorker);
        }
        public static void AddNewWorkerADO(List<string> entityInformation, string sqlInsertQuery)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection("Data Source = LAPTOP-GV0D7VCD; database = SchoolDBFirst; Integrated Security = True; TrustServerCertificate = True;");

                SqlCommand command = new SqlCommand(sqlInsertQuery, connection);

                connection.Open();

                command.ExecuteNonQuery();

                Console.WriteLine("Shit should work!!");
            }
            catch (Exception e)
            {
                Console.WriteLine("oj, nått gick fel " + e);
            }
            finally
            {
                connection.Close();
            }
        }
        public static void ShowWorkerInfo()
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection("Data Source = LAPTOP-GV0D7VCD; database = SchoolDBFirst; Integrated Security = True; TrustServerCertificate = True;");

                SqlCommand command = new SqlCommand("SELECT FirstName,LastName, Roles.RoleName, DATEDIFF(year, DateEmployed, GETDATE()) AS YearsOfService\r\nFROM Personals\r\nINNER JOIN Roles ON Personals.RoleID = Roles.ID;", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader["FirstName"]}, {reader["LastName"]}, {reader["RoleName"]}, {reader["YearsOfService"]}");
                }

                Console.WriteLine("Shit should work!!");
            }
            catch (Exception e)
            {
                Console.WriteLine("oj, nått gick fel " + e);
            }
            finally
            {
                connection.Close();
            }
        }
        public static void ShowAllActivCourses(DatabaseDBContext context)
        {
            var aktivCourses = context.Kurses.Where(x => x.Activ == true);

            foreach (var aktivCourse in aktivCourses)
            {
                Console.WriteLine($"ID: [{aktivCourse.ID}] Kurs: [{aktivCourse.Name}]");
            }
        }
        public static void ShowAllStudentInfo(DatabaseDBContext context)
        {
            foreach (var student in context.Students)
            {
                Console.WriteLine($"ID: [{student.ID}] Namn [{student.Name}] Efternamn [{student.LastName}] Personnumber [{student.PersonNumber}] Klass [{student.KlassID}]");
            }
        }
        public static void PersonalInEachRole(DatabaseDBContext context)
        {
            int role1 = 0;
            int role2 = 0;
            int role3 = 0;

            foreach (var person in context.Personals)
            {
                switch (person.RoleID)
                {
                    case 1:
                        role1++;
                        break;
                    case 2:
                        role2++;
                        break;
                    case 3:
                        role3++;
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine($"Personal i Avdelning [{context.Roles.Find(1).RoleName}] : {role1}");
            Console.WriteLine($"Personal i Avdelning [{context.Roles.Find(2).RoleName}] : {role2}");
            Console.WriteLine($"Personal i Avdelning [{context.Roles.Find(3).RoleName}] : {role3}");
        }

        //public static Personal CreateWorker()
        //{
        //    Console.WriteLine("Ange För- och LastName (ex. Max Dahlberg)");
        //    string workerName = Console.ReadLine();
        //    Console.WriteLine("Ange Personnummer (ex. 20011107-xxxx)");
        //    string workerSocial = Console.ReadLine();

        //    Personal worker = new Personal()
        //    {
        //        FirstName = workerName,
        //        PersonNumber = workerSocial,
        //    };

        //    return worker;
        //}
        //public static void AddWorker(DatabaseDBContext context)
        //{
        //    Personal worker = CreateWorker();
        //    context.Personals.Add(worker);
        //    context.SaveChanges();
        //}
        public static void GetStudents1(DatabaseDBContext context, bool sortByKlass)
        {
            int klassName = 0;
            if (sortByKlass)
            {
                Console.WriteLine("Vilken Klass vill du sortera efter?");
                foreach (var item in context.Klasses)
                {
                    Console.WriteLine($"-{item.ID}) Klass: {item.Name}");
                }
                klassName = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("Vill du sortera efter Name eller LastName");
            switch (Console.ReadLine().ToUpper())
            {
                case "Name":
                    SortStudentsByName(context, klassName);
                    break;
                case "LastName":
                    SortStudentsByLastName(context, klassName);
                    break;
                default:
                    Console.WriteLine("Du måste svara antingen (Name) eller (LastName)");
                    break;
            }


        }
        public static void SortStudentsByName(DatabaseDBContext context, int klassIDNumber)
        {
            bool sortedStudents = GetStudentsSorted();

            if (sortedStudents && klassIDNumber == 0)
            {
                var students = context.Students
                        .OrderBy(x => x.Name);
                DisplaySortedStudent(students);
                return;
            }
            else if (klassIDNumber == 0)
            {
                var students = context.Students
                        .OrderByDescending(x => x.Name);
                DisplaySortedStudent(students);
                return;

            }
            else if (sortedStudents)
            {
                var students = context.Students
                        .Where(x => x.KlassID == klassIDNumber)
                        .OrderBy(x => x.Name);
                DisplaySortedStudent(students);
                return;
            }
            else
            {
                var students = context.Students
                        .Where(x => x.KlassID == klassIDNumber)
                        .OrderByDescending(x => x.Name);
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
                        .OrderBy(x => x.LastName);
                DisplaySortedStudent(students);
                return;
            }
            else if (klassIDNumber == 0)
            {
                var students = context.Students
                        .OrderByDescending(x => x.LastName);
                DisplaySortedStudent(students);
                return;
            }
            else if (sortedStudents)
            {
                var students = context.Students
                        .Where(x => x.KlassID == klassIDNumber)
                        .OrderBy(x => x.LastName);
                DisplaySortedStudent(students);
                return;
            }
            else
            {
                var students = context.Students
                        .Where(x => x.KlassID == klassIDNumber)
                        .OrderByDescending(x => x.LastName);
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
                Console.WriteLine($"FörName: {student.Name} LastName: {student.LastName} Klass: {student.KlassID}");
            }
        }
    }
}