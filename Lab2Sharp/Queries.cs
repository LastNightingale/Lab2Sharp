using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;
using System.Xml;

namespace Lab2Sharp
{
    internal class Queries
    {
        public IEnumerable<XElement> AllCars()
        {
            return from c in _cars.Descendants("Transport")
                   select c;
        }

        public IEnumerable<XElement> CarsWithYearMoreThatTwentyTen() //# я думаю тут замінити на рік
        {
            return from car in _cars.Descendants("Transport") 
                   where (int)car.Element("ManufactureYear") > 2010 select car;
        }

        public IEnumerable<XElement> OrderByLicenseDesc()
        {
            return from p in _people.Descendants("Person")
                   orderby (int)p.Element("LicsenseNumber") descending
                   select p;
        }

        public IEnumerable<XElement> CarsFromFrance()
        {
            return _cars.Descendants("Transport")
                .Where(car => int.Parse(car.Element("ProducerID").Value) == 3);            
        }
        public IEnumerable<XElement> CarsWithExperiencedDrivers()
        {
            var PersID = _people.Descendants("Person").Where(pers =>
            int.Parse(pers.Element("Experience").Value) > 20)
                .Select(pers => pers.Element("LicsenseNumber").Value);
            return _connections.Descendants("PersonCarConnection")
                .Join(PersID,
                con => con.Element("PersonLicenseNumber").Value,
                pers => pers,
                (con, pers) => con)
                .Where(con => con.Element("isOwner").Value == "false")
                .Select(con => con);
        }
        public Dictionary<int, List<XElement>> OwnersByExperience()
        {
            var res = from p in _people.Descendants("Person")
                   orderby int.Parse(p.Element("Experience").Value)
                   group p by int.Parse(p.Element("Experience").Value);
            return res.ToDictionary(r => r.Key, r => r.ToList());
        }     
        public IEnumerable<XElement>DriverMaxExp()
        {
            var DriverID = _connections.Descendants("PersonCarConnection")
                .Join(_people.Descendants("Person"),
                con => int.Parse(con.Element("PersonLicenseNumber").Value),
                pers => int.Parse(pers.Element("LicsenseNumber").Value),
                (con, pers) => con)
                .Where(con => con.Element("isOwner").Value == "false")
                .Select(con => con.Element("PersonLicenseNumber").Value).
                Distinct();
            int MaxExp = DriverID
                .Join(_people.Descendants("Person"),
                drive => drive,
                pers => pers.Element("LicsenseNumber").Value,
                (drive, pers) => int.Parse(pers.Element("Experience").Value)).Max();
            return _people.Descendants("Person").Where(pers => int.Parse(pers.Element("Experience").Value) == MaxExp);
        }
        public IEnumerable<XElement> CarsInCertainChassis()
        {
            return from car in _cars.Descendants("Transport")
                   where int.Parse(car.Element("ChassisNumber").Value) >= 30000 &&
                   int.Parse(car.Element("ChassisNumber").Value) <= 60000
                   select car;
        }
        public IEnumerable<XElement> PersonsWithExpLessThanAverage()
        {
            var AvgExp = _people.Descendants("Person")
                .Select(p => int.Parse(p.Element("Experience").Value)).Average();
            return _people.Descendants("Person")
                .Where(pers => int.Parse(pers.Element("Experience").Value) <= AvgExp);
        }
        public IEnumerable<XElement> MarksWithTwoWords() 
        {
            return _marks.Descendants("Mark").Where(mark => (mark.Element("MarkName").Value).Contains(' '));                
        }
        public IEnumerable<XElement> CarsOwnersExpMoreThanNumber(int number)
        {
            var ownersNum = _people.Descendants("Person")
                .Where(pers => int.Parse(pers.Element("Experience").Value) >= number)
                .Select(pers => pers.Element("LicsenseNumber").Value);

            var carsNum = _connections.Descendants("PersonCarConnection")
                .Where(con => con.Element("isOwner").Value == "true")
                .Join(ownersNum,
                con => con.Element("PersonLicenseNumber").Value,
                owners => owners,
                (con, owners) => con.Element("CarIDNumber").Value)
                .Distinct();

            return _cars.Descendants("Transport")
                .Join(carsNum,
                car => car.Element("CarID").Value,
                cons => cons,
                (car, cons) => car);
        }
        public Dictionary<int, List<XElement>> OwnersGroupingWithDriverMoreExp(int number) 
        {
            var cardrivers = _connections.Descendants("PersonCarConnection")
                .Where(con => con.Element("isOwner").Value == "false")
                .Join(_people.Descendants("Person").Where(pers => 
                int.Parse(pers.Element("Experience").Value) >= number),
                cons => cons.Element("PersonLicenseNumber").Value,
                pers => pers.Element("LicsenseNumber").Value,
                (cons, cardr) => cons.Element("CarIDNumber").Value).Distinct();

            var licenses = _connections.Descendants("PersonCarConnection")
                .Where(con => con.Element("isOwner").Value == "true")
                .Join(cardrivers,
                cons => cons.Element("CarIDNumber").Value,
                cardr => cardr,                
                (cons, cardr) => cons.Element("PersonLicenseNumber").Value).Distinct();

            var exp = _people.Descendants("Person")
                .Join(licenses,
                pers => pers.Element("LicsenseNumber").Value,
                lic => lic,
                (pers, lic) => pers)
                .GroupBy(pers => int.Parse(pers.Element("Experience").Value));
            return exp.ToDictionary(ex => ex.Key, ex => ex.ToList());
        }
        public IEnumerable<XElement> OwnersWithBlueAndRedCars() 
        {
            var rightcarid = from car in _cars.Descendants("Transport")
                       where car.Element("CarColor").Value == Color.Blue.ToString() ||
                       car.Element("CarColor").Value == Color.Red.ToString()
                             select car.Element("CarID").Value;

            rightcarid = rightcarid.Distinct();

            var rightlicense = from cons in _connections.Descendants("PersonCarConnection")
                       where (cons.Element("isOwner").Value == "true")
                               join rightids in rightcarid on cons.Element("CarIDNumber").Value equals rightids
                       select cons;

            var rightowners = from pers in _people.Descendants("Person")
                   join rightlic in rightlicense 
                   on pers.Element("LicsenseNumber").Value equals rightlic.Element("PersonLicenseNumber").Value
                   select pers;
            return rightowners.Distinct();
        }
        public IEnumerable<XElement> DriversOfRightOwners()  
        {
            var rightcarid = from pers in _people.Descendants("Person")
                               where int.Parse(pers.Element("LicsenseNumber").Value) >= 2500
                               join cons in _connections.Descendants("PersonCarConnection") 
                               on pers.Element("LicsenseNumber").Value equals
                               cons.Element("PersonLicenseNumber").Value
                             where cons.Element("isOwner").Value == "true"
                               select cons.Element("CarIDNumber").Value;            

            var rightdriversid = from con in _connections.Descendants("PersonCarConnection")
                                 where con.Element("isOwner").Value == "false"
                                 join rightcar in rightcarid on con.Element("CarIDNumber").Value equals rightcar
                            select con.Element("PersonLicenseNumber").Value;

            return from pers in _people.Descendants("Person")
                   join rightdriver in rightdriversid on pers.Element("LicsenseNumber").Value equals rightdriver
                   select pers;
        }
        public IEnumerable<XElement> RedCarDriversID() 
        {
            var rightdriverid = _cars.Descendants("Transport")
                .Where(car => car.Element("CarColor").Value == Color.Red.ToString())
                .Join(_connections.Descendants("PersonCarConnection")
                .Where(con => con.Element("isOwner").Value == "false"),
                car => car.Element("CarID").Value,
                con => con.Element("CarIDNumber").Value,
                (car, con) => con.Element("PersonLicenseNumber").Value);

            return _people.Descendants("Person")
                .Join(rightdriverid,
                pers => pers.Element("LicsenseNumber").Value,
                rd => rd,
                (pers, rd) => pers);
        }
        public IEnumerable<XElement> MaxAndMinExp()
        {
            var MaxExp = _people.Descendants("Person")
                .Where(pers => int.Parse(pers.Element("Experience").Value)
                .Equals(_people.Descendants("Person").Select(pers => int.Parse(pers.Element("Experience").Value))
                .Max()));
            var MinExp = _people.Descendants("Person")
                .Where(pers => int.Parse(pers.Element("Experience").Value)
                .Equals(_people.Descendants("Person").Select(pers => int.Parse(pers.Element("Experience").Value))
                .Min()));
            return MaxExp.Concat(MinExp);
        }

        private readonly XDocument _cars = XDocument.Load("cars.xml");
        private readonly XDocument _people = XDocument.Load("people.xml");
        private readonly XDocument _connections = XDocument.Load("connections.xml");
        private readonly XDocument _marks = XDocument.Load("marks.xml");

    }
}
