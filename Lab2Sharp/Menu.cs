using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;

namespace Lab2Sharp
{
    internal class Menu
    {
        private Dictionary<int, Action> _PossibleClasses = new Dictionary<int, Action>()
        {
            {1, GetCarsForFile},
            {2, GetPeopleForFile},
            {3, GetConnectionsForFile},
            {4, GetMarksForFile},
            {5, GetProducersForFile},
            {6, GetBodiesForFile},
            {7, GetStatesForFile},
            {8, GetDataFromLists },
            {9, ShowQueries }
        };
        public void RunMenu()
        {
            int chosen;
            string symbol;
            do
            {
                Console.WriteLine("Оберіть, що ви хочете зробити\n"
                + "1. Записати транспорт\n" + "2. Записати людей\n" + "3. Записати зв'язки\n"
                + "4. Записати марки\n" + "5. Записати виробників\n" + "6. Записати кузови\n"
                + "7. Записати стани\n" + "8. Завантажити дані зі списків\n" + "9. Показати запити\n");
                chosen = int.Parse(Console.ReadLine());
                if (chosen >= 1 && chosen <= 9)
                    foreach (var el in _PossibleClasses)
                        if (el.Key == chosen) el.Value.Invoke();
                Console.WriteLine("Введіть y якщо хочете вийти, або будь-який інший символ щоб продовжити");
                symbol = Console.ReadLine();
            }while(symbol != "y");   
            
        }
        static public void GetMarksForFile() 
        {
            List<Mark> marks = new List<Mark>();
            string symbol;
            do
            {
                Mark newobject = new Mark();
                Console.WriteLine("Введiть ID");
                newobject.MarkID = int.Parse(Console.ReadLine());
                Console.WriteLine("Введiть марку");
                newobject.MarkName = Console.ReadLine();
                marks.Add(newobject);
                Console.WriteLine("Натисніть 'y' якщо хочете вийти або будь-яку iншу клавішу якщо ввести ще");
                symbol = Console.ReadLine();
            } while ((symbol) != "y");
            WriterXml writer = new WriterXml();
            writer.WriteFile("marks.xml", marks);
        }
        static public void GetPeopleForFile()
        {
            List<Person> people = new List<Person>();
            string symbol;
            do
            {
                Person newobject = new Person();
                Console.WriteLine("Введiть номер ліцензії");
                newobject.LicsenseNumber = int.Parse(Console.ReadLine());
                Console.WriteLine("Введiть ім'я");
                newobject.Name = Console.ReadLine();
                Console.WriteLine("Введiть прізвище");
                newobject.Surname = Console.ReadLine();
                Console.WriteLine("Введiть по-батькові");
                newobject.Patronymic = Console.ReadLine();
                Console.WriteLine("Введіть дату народження");
                newobject.BirthDate = Console.ReadLine();
                Console.WriteLine("Введіть адресу");
                newobject.RegisterAdress = Console.ReadLine();
                Console.WriteLine("Введiть стаж");
                newobject.Experience = int.Parse(Console.ReadLine());
                people.Add(newobject);
                Console.WriteLine("Натисніть 'y' якщо хочете вийти або будь-яку iншу клавішу якщо ввести ще");
                symbol = Console.ReadLine();
            } while ((symbol) != "y");
            WriterXml writer = new WriterXml();
            writer.WriteFile("people.xml", people);
        }
        static public void GetCarsForFile()
        {
            List<Transport> cars = new List<Transport>();
            string symbol;
            do
            {
                Transport newobject = new Transport();
                Console.WriteLine("Введiть ID машини");
                newobject.CarID = int.Parse(Console.ReadLine());
                Console.WriteLine("Введiть марку");
                newobject.MarkID = int.Parse(Console.ReadLine());
                Console.WriteLine("Введiть виробника");
                newobject.ProducerID = int.Parse(Console.ReadLine());
                Console.WriteLine("Введiть кузов");
                newobject.BodyID = int.Parse(Console.ReadLine());
                Console.WriteLine("Введiть стан");
                newobject.StateID = int.Parse(Console.ReadLine());
                Console.WriteLine("Введiть номер шасі");
                newobject.ChassisNumber = int.Parse(Console.ReadLine());
                Console.WriteLine("Введiть рік вироблення");
                newobject.ManufactureYear = int.Parse(Console.ReadLine());
                Console.WriteLine("Введіть колір");
                newobject.CarColor = Color.FromName(Console.ReadLine());  
                Console.WriteLine("Введіть модель");
                newobject.Model = Console.ReadLine();
                Console.WriteLine("Введiть знак");
                newobject.Sign = Console.ReadLine();
                cars.Add(newobject);
                Console.WriteLine("Натисніть 'y' якщо хочете вийти або будь-яку iншу клавішу якщо ввести ще");
                symbol = Console.ReadLine();
            } while ((symbol) != "y");
            WriterXml writer = new WriterXml();
            writer.WriteFile("cars.xml", cars);
        }
        static public void GetConnectionsForFile()
        {
            List<PersonCarConnection> connections = new List<PersonCarConnection>();
            string symbol;
            do
            {
                PersonCarConnection newobject = new PersonCarConnection();
                Console.WriteLine("Введiть номер ліцензії людини");
                newobject.PersonLicenseNumber = int.Parse(Console.ReadLine());
                Console.WriteLine("Введiть номер реєстрації");
                newobject.RegistrationNumber = int.Parse(Console.ReadLine());
                Console.WriteLine("Введiть ID машини");
                newobject.CarIDNumber = int.Parse(Console.ReadLine());
                Console.WriteLine("Введiть номер реєстрації");
                newobject.isOwner = int.Parse(Console.ReadLine()) == 0 ? false : true;
                connections.Add(newobject);
                Console.WriteLine("Натисніть 'y' якщо хочете вийти або будь-яку iншу клавішу якщо ввести ще");
                symbol = Console.ReadLine();
            } while ((symbol) != "y");
            WriterXml writer = new WriterXml();
            writer.WriteFile("connections.xml", connections);
            return;
        }
        static public void GetProducersForFile()
        {
            List<Producer> producers = new List<Producer>();
            string symbol;
            do
            {
                Producer newobject = new Producer();
                Console.WriteLine("Введiть ID");
                newobject.CountryID = int.Parse(Console.ReadLine());
                Console.WriteLine("Введiть країну");
                newobject.CountryName = Console.ReadLine();
                producers.Add(newobject);
                Console.WriteLine("Натисніть 'y' якщо хочете вийти або будь-яку iншу клавішу якщо ввести ще");
                symbol = Console.ReadLine();
            } while ((symbol) != "y");
            WriterXml writer = new WriterXml();
            writer.WriteFile("producers.xml", producers);
        }
        static public void GetBodiesForFile()
        {
            List<Body> bodies = new List<Body>();
            string symbol;
            do
            {
                Body newobject = new Body();
                Console.WriteLine("Введiть ID");
                newobject.BodyID = int.Parse(Console.ReadLine());
                Console.WriteLine("Введiть назву типу кузова");
                newobject.BodyName = Console.ReadLine();
                bodies.Add(newobject);
                Console.WriteLine("Натисніть 'y' якщо хочете вийти або будь-яку iншу клавішу якщо ввести ще");
                symbol = Console.ReadLine();
            } while ((symbol) != "y");
            WriterXml writer = new WriterXml();
            writer.WriteFile("bodies.xml", bodies);
        }
        static public void GetStatesForFile()
        {
            List<State> states = new List<State>();
            string symbol;
            do
            {
                State newobject = new State();
                Console.WriteLine("Введiть ID");
                newobject.StateID = int.Parse(Console.ReadLine());
                Console.WriteLine("Введiть назву стану");
                newobject.StateName = Console.ReadLine();
                states.Add(newobject);
                Console.WriteLine("Натисніть 'y' якщо хочете вийти або будь-яку iншу клавішу якщо ввести ще");
                symbol = Console.ReadLine();
            } while ((symbol) != "y");
            WriterXml writer = new WriterXml();
            writer.WriteFile("bodys.xml", states);
        }
        static public void GetDataFromLists()
        {
            Lists l = new Lists();
            WriterXml writer = new WriterXml();
            writer.WriteFile("marks.xml", l.Marks);
            writer.WriteFile("people.xml", l.People);
            writer.WriteFile("cars.xml", l.Cars);
            writer.WriteFile("connections.xml", l.Connections);
            writer.WriteFile("bodies.xml", l.Bodies);
            writer.WriteFile("producers.xml", l.Producers);
            writer.WriteFile("states.xml", l.States);
            Console.WriteLine("Дані успішно завантажені");
        }
        static public void ShowQueries()
        {
            Queries queries = new Queries();
            Printer printer = new Printer();
            Console.WriteLine("Query 1");
            printer.Prints(queries.AllCars());
            Console.WriteLine("Query 2");
            printer.Prints(queries.CarsWithYearMoreThatTwentyTen());
            Console.WriteLine("Query 3");
            printer.Prints(queries.OrderByLicenseDesc());
            Console.WriteLine("Query 4");
            printer.Prints(queries.CarsWithExperiencedDrivers());
            Console.WriteLine("Query 5");
            printer.Prints(queries.CarsFromFrance());
            Console.WriteLine("Query 6");
            printer.Prints(queries.OwnersByExperience());
            Console.WriteLine("Query 7");
            printer.Prints(queries.DriverMaxExp());
            Console.WriteLine("Query 8");
            printer.Prints(queries.CarsInCertainChassis());
            Console.WriteLine("Query 9");
            printer.Prints(queries.PersonsWithExpLessThanAverage());
            Console.WriteLine("Query 10");
            printer.Prints(queries.MarksWithTwoWords());
            Console.WriteLine("Query 11");
            printer.Prints(queries.CarsOwnersExpMoreThanNumber(17));
            Console.WriteLine("Query 12");
            printer.Prints(queries.OwnersGroupingWithDriverMoreExp(17));
            Console.WriteLine("Query 13");
            printer.Prints(queries.OwnersWithBlueAndRedCars());
            Console.WriteLine("Query 14");
            printer.Prints(queries.DriversOfRightOwners());
            Console.WriteLine("Query 15");
            printer.Prints(queries.RedCarDriversID());
            Console.WriteLine("Query 16");
            printer.Prints(queries.MaxAndMinExp());
        }
    }
}
