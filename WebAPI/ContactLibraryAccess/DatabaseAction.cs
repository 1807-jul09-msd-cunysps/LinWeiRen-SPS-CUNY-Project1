using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Data.SqlClient;
using ContactLibrary;

namespace ContactLibraryAccess
{

    public static class DatabaseAction
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static PhoneAppDatabaseContext db;
        static Person p;
        static Person_address pa;
        static Person_phone pp;
        static DatabaseAction()
        {
            db = new PhoneAppDatabaseContext();
        }
        public static dynamic GetPeople()
        {
            var pass = from p in db.People
                       select new
                       {
                           p.Pid,
                           p.firstName,
                           p.lastName,

                           address = from address in p.Person_address
                                     select new
                                     {
                                         address.Aid,
                                         address.houseNum,
                                         address.street,
                                         address.address_city,
                                         address.address_state,
                                         address.address_country,
                                         address.zipcode
                                     },

                           phone = from phone in p.Person_phone
                                   select new
                                   {
                                       phone.PHid,
                                       phone.countryCode,
                                       phone.areaCode,
                                       phone.number,
                                       phone.ext
                                   }
                       };
            if (pass.Any())
            {
                return pass;
            }
            else
            {
                return null;
            }
        }
        public static Person FindPersonID(int ID)
        {
            try
            {
                Person people = db.People.Single(p => p.Pid == ID);
                return people;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "DatabaseAction.Find Null Error");
                return null;
            }
        }
        public static Person_phone FindPhoneID(int ID)
        {
            try
            {
                Person_phone phone = db.Person_phone.Single(pp => pp.PHid == ID);
                return phone;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "DatabaseAction.Find Null Error");
                return null;
            }
        }
        public static Person_address FindAddressID(int ID)
        {
            try
            {
                Person_address address = db.Person_address.Single(pa => pa.Aid == ID);
                return address;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "DatabaseAction.Find Null Error");
                return null;
            }
        }
        public static string Edit(ref int opt, Person person, string text)
        {
            switch (opt)
            {
                case 1:
                    person.firstName = text;
                    return Save();
                case 2:
                    person.lastName = text;
                    return Save();
                default:
                    return "No Information Updated";
            }
        }
        public static string Edit(ref int opt, Person_phone phone, string text)
        {
            switch (opt)
            {
                case 1:
                    try
                    {
                        phone.countryCode = (int)Enum.Parse(typeof(Country), text);
                        return Save();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "DatabaseAction.Edit BadInput Error");
                        return "Invalid Country!";
                    }
                case 2:
                    phone.areaCode = Convert.ToInt32(text);
                    return Save();
                case 3:
                    phone.number = Convert.ToInt32(text);
                    return Save();
                case 4:
                    phone.ext = Convert.ToInt32(text);
                    return Save();
                default:
                    return "No Information Updated";
            }
        }
        public static string Edit(ref int opt, Person_address address, string text)
        {
            switch (opt)
            {
                case 1:
                    address.houseNum = text;
                    return Save();
                case 2:
                    address.street = text;
                    return Save();
                case 3:
                    address.address_city = text;
                    return Save();
                case 4:
                    try
                    {
                        address.address_state = Enum.GetName(typeof(State), Enum.Parse(typeof(State), text));
                        return Save();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "DatabaseAction.Edit BadInput Error");
                        return "Invalid State!";
                    }
                case 5:
                    try
                    {
                        address.address_country = Enum.GetName(typeof(Country), Enum.Parse(typeof(Country), text));
                        return Save();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "DatabaseAction.Edit BadInput Error");
                        return "Invalid Country!";
                    }
                case 6:
                    address.zipcode = Convert.ToInt32(text);
                    return Save();
                default:
                    return "No Information Updated";
            }
        }
        public static string Del(Person person)
        {
            db.People.Remove(person);
            return Save();
        }
        public static string Del(Person_phone phone)
        {
            db.Person_phone.Remove(phone);
            return Save();
        }
        public static string Del(Person_address address)
        {
            db.Person_address.Remove(address);
            return Save();
        }
        public static dynamic Search(int i, string s)
        {
            switch (i)
            {
                case 1:
                    var pass = db.People.Where(p => p.firstName.StartsWith(s));
                    if (pass.Any())
                    {
                        return pass;
                    }
                    else
                    {
                        return null;
                    }
                case 2:
                    var pass2 = from person in db.People where person.lastName.StartsWith(s) select person;
                    if (pass2.Any())
                    {
                        return pass2;
                    }
                    else
                    {
                        return null;
                    }
                case 3:
                    var pass3 = from address in db.Person_address where address.houseNum.StartsWith(s) select address;
                    if (pass3.Any())
                    {
                        return pass3;
                    }
                    else
                    {
                        return null;
                    }
                case 4:
                    var pass4 = from address in db.Person_address where address.street.StartsWith(s) select address;
                    if (pass4.Any())
                    {
                        return pass4;
                    }
                    else
                    {
                        return null;
                    }
                case 5:
                    var pass5 = from address in db.Person_address where address.address_city.StartsWith(s) select address;
                    if (pass5.Any())
                    {
                        return pass5;
                    }
                    else
                    {
                        return null;
                    }
                case 6:
                    var pass6 = from address in db.Person_address where address.address_state.StartsWith(s) select address;
                    if (pass6.Any())
                    {
                        return pass6;
                    }
                    else
                    {
                        return null;
                    }
                case 7:
                    var pass7 = from address in db.Person_address where address.address_country.StartsWith(s) select address;
                    if (pass7.Any())
                    {
                        return pass7;
                    }
                    else
                    {
                        return null;
                    }
                case 8:
                    var pass8 = from address in db.Person_address where address.zipcode.Equals(s) select address;
                    if (pass8.Any())
                    {
                        return pass8;
                    }
                    else
                    {
                        return null;
                    }
                case 9:
                    var pass9 = from phone in db.Person_phone where phone.countryCode.ToString().Equals(s) select phone;
                    if (pass9.Any())
                    {
                        return pass9;
                    }
                    else
                    {
                        return null;
                    }
                case 10:
                    var pass10 = from phone in db.Person_phone where phone.areaCode.Equals(s) select phone;
                    if (pass10.Any())
                    {
                        return pass10;
                    }
                    else
                    {
                        return null;
                    }
                case 11:
                    var pass11 = from phone in db.Person_phone where phone.number.Equals(s) select phone;
                    if (pass11.Any())
                    {
                        return pass11;
                    }
                    else
                    {
                        return null;
                    }
                case 12:
                    var pass12 = from phone in db.Person_phone where phone.ext.Equals(s) select phone;
                    if (pass12.Any())
                    {
                        return pass12;
                    }
                    else
                    {
                        return null;
                    }
                default:
                    return null;
            }
        }
        public static void AddToDatabase(Person p)
        {
            db.People.Add(p);
        }
        public static void AddToDatabase(Person_address pa)
        {
            db.Person_address.Add(pa);
        }
        public static void AddToDatabase(Person_phone pp)
        {
            db.Person_phone.Add(pp);
        }
        public static Person AddNewContact(string fn, string ln)
        {
            p = new Person
            {
                firstName = fn,
                lastName = ln
            };
            AddToDatabase(p);
            return p;
        }
        public static void AddNewContact(DeserializedPerson person)
        {
            Person p = new Person
            {
                firstName = person.firstName,
                lastName = person.lastName
            };
            Person_address pa = new Person_address
            {

                houseNum = person.houseNum,
                street = person.street,
                address_city = person.city,
                address_state = person.state,
                address_country = person.country,
                zipcode = person.zipcode
            };
            pa.People.Add(p);
            Person_phone pp = new Person_phone {
                countryCode = person.countryCode,
                areaCode = person.areaCode,
                number = person.number,
                ext = person.ext,
                Person = p
            };
            AddToDatabase(p);
            AddToDatabase(pa);
            AddToDatabase(pp);
        }

        public static Person_address AddNewContact(Person p, string hnum, string street, string city, string state, string country, int zipcode)
        {
            pa = new Person_address
            {
                houseNum = hnum,
                street = street,
                address_city = city,
                address_state = state,
                address_country = country,
                zipcode = zipcode
            };
            pa.People.Add(p);
            return pa;
        }
        public static Person_phone AddNewContact(Person p, int countrycode, int areacode, int number, int ext)
        {
            pp = new Person_phone
            {
                Person = p,
                countryCode = countrycode,
                areaCode = areacode,
                number = number,
                ext = ext
            };
            return pp;
        }
        public static string Save()
        {
            db.SaveChanges();
            return "Save Successful!";
        }
        public static void SerializePeople()
        {
            List<SerializablePerson> people = (from p in db.People select new SerializablePerson() { Pid = p.Pid, FirstName = p.firstName, LastName = p.lastName }).ToList();
            XmlSerializer serializer = new XmlSerializer(people.GetType());
            using (StreamWriter writer = new StreamWriter("people.xml", false))
            {
                serializer.Serialize(writer, people);
            }
            Console.WriteLine("Finished!");
        }
    }
}

