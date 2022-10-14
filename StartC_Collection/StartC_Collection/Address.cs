using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace StartC_Collection
{
    struct Address
    {
        /// <summary>
        /// Создание 100 случайных чисел
        /// </summary>
        /// <returns></returns>
        public List<int> CreateRandom()
        {
            Console.WriteLine("Задание 1: Работа с листом\n");
            List<int> list = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                list.Add(random.Next(100));
                Console.Write($"{list[i]} ");
            }
            Console.WriteLine($"\nЭлементов всего <{list.Count}>");
            Console.ReadKey();
            return list;
        }
        
        /// <summary>
        /// Результат работы CreateRandom() и DeleteRange()
        /// </summary>
        public void MethodCycle()
        {
            List<int> list = CreateRandom();
            for (int i = 0; i < list.Count; i++)
            {
                DeleteRange(list, i);
            }
            Console.WriteLine($"\nОсталось элементов после удаления <{list.Count}>");
        }

        /// <summary>
        /// Удаление элементов больше 25 и меньше 50
        /// </summary>
        /// <param name="list"></param>
        /// <param name="i"></param>
        public void DeleteRange(List<int> list, int i)
        {
            if (list[i] > 25 && list[i] < 50)
            {
                list.RemoveAt(i);
            }
            for (int j = 0; j < list.Count; j++)
            {
                if (list[j] > 25 && list[j] < 50)
                {
                    list.RemoveAt(j);
                }
            }
            Console.Write($"{list[i]} ");
        }

        /// <summary>
        /// Объединение методов CreateContact() и Find()
        /// </summary>
        public void MainDictionary()
        {
            Console.WriteLine("\nЗадание 2: Телефонная книга");
            Dictionary<int, string> diction = new Dictionary<int, string>();
            for ( ; ; )
            {
                Console.WriteLine("\nВыберите команду\n1 - Создать контакт\n2 - найти\nПустая строка = выйти\n");
                string stop = Console.ReadLine();
                if (stop == String.Empty) break;
                int choose = int.Parse(stop);
                CreateContact(choose, diction);
                Find(choose, diction);
            }
        }
        
        /// <summary>
        /// Создание контакта
        /// </summary>
        /// <param name="choose"></param>
        /// <param name="diction"></param>
        public void CreateContact(int choose, Dictionary<int, string> diction)
        {
            if (choose == 1)
            {
                Console.Write("Введите Ф.И.О: ");
                string person = Console.ReadLine();
                Console.WriteLine("(Оставьте строку пустой чтобы прекратить запись номеров)");
                for (; ; )
                {
                    Console.Write("Номер телефона: ");
                    string contactNumber = Console.ReadLine();
                    if (contactNumber == String.Empty) break;
                    int number = int.Parse(contactNumber);
                    diction[number] = person;
                }
            }
        }
        
        /// <summary>
        /// Поиск контакта
        /// </summary>
        /// <param name="choose"></param>
        /// <param name="diction"></param>
        public void Find(int choose, Dictionary<int, string> diction)
        {
            if (choose == 2)
            {
                Console.WriteLine("Поиск контакта по номеру");
                int find = int.Parse(Console.ReadLine());
                string value = "";
                if (diction.TryGetValue(find, out value))
                {
                    foreach (KeyValuePair<int, string> pair in diction)
                    {
                        if (pair.Value == value)
                        {
                            Console.WriteLine($"Ф.И.О: {pair.Value}");
                        }
                    }
                }
                else Console.WriteLine("Не зарегистрирован");
            }
        }

        /// <summary>
        /// Сохранение в HashSet колллецкию и проверка повторов
        /// </summary>
        public void HashSet()
        {
            Console.WriteLine("Задание 3: Проверка повторов\n");
            HashSet<int> number = new HashSet<int>();
            Console.WriteLine("Пропущенная строка > показать записанные числа и выйти");
            for ( ; ; )
            {
                Console.WriteLine("Вводите числа");
                string stop = Console.ReadLine();
                if (stop == String.Empty) break;
                int num = int.Parse(stop);
                if (number.Contains(num) == false) Console.WriteLine("Сохранено");
                if (number.Contains(num) == true) Console.WriteLine("Такое число было");
                number.Add(num);
            }
            foreach (var nums in number) Console.Write($"{nums} ");
        }

        /// <summary>
        /// Запрос с пользователя определенных данных чтобы передать методу XmlCreate()
        /// </summary>
        public void Input()
        {
            Console.Write("Введите ФИО: ");
            string fullName = Console.ReadLine();
            Console.Write("Улица: ");
            string streets = Console.ReadLine();
            Console.Write("Номер дома: ");
            string numberHome = Console.ReadLine();
            Console.Write("Номер квартиры: ");
            string numberFlat = Console.ReadLine();
            Console.Write("Моб телефон: ");
            string numberMobile = Console.ReadLine();
            Console.Write("Дом телефон: ");
            string homePhone = Console.ReadLine();
            
            XmlCreate(fullName, streets, numberHome, numberFlat, numberMobile, homePhone);
        }
        
        /// <summary>
        /// Запись данных и сохранение в Xml файл
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="streets"></param>
        /// <param name="numberHome"></param>
        /// <param name="numberFlat"></param>
        /// <param name="numberMobile"></param>
        /// <param name="homePhone"></param>
        public void XmlCreate(string fullName, string streets, string numberHome, string numberFlat, string numberMobile, string homePhone)
        {
            XElement user = new XElement("User");

            XElement person = new XElement("Person");
            XElement address = new XElement("Address");
            XElement street = new XElement("Street", streets);
            XElement houseNumber = new XElement("HouseNumber", numberHome);
            XElement flatNumber = new XElement("FlatNumber", numberFlat);
            XElement phones = new XElement("Phone");
            XElement mobilePhone = new XElement("MobilePhone", numberMobile);
            XElement flatPhone = new XElement("FlatPhone", homePhone);

            XAttribute name = new XAttribute("Name", fullName);
            person.Add(name);
            person.Add(address);
            person.Add(phones);
            address.Add(street);
            address.Add(houseNumber);
            address.Add(flatNumber);
            phones.Add(mobilePhone);
            phones.Add(flatPhone);
            user.Add(person);

            user.Save("_User.xml");
        }

        /// <summary>
        /// Загрузка данных с созданного Xml файла и продолжение записи
        /// </summary>
        public void ContinueCreate()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("_User.xml");
            XmlElement xRoot = xmlDocument.DocumentElement;

            XmlElement userElement = xmlDocument.CreateElement("Person");
            XmlAttribute nameAttr = xmlDocument.CreateAttribute("Name");
            XmlElement street = xmlDocument.CreateElement("Street");
            XmlElement houseNumber = xmlDocument.CreateElement("HouseNumber");
            XmlElement flatNumber = xmlDocument.CreateElement("FlatNumber");
            XmlElement address = xmlDocument.CreateElement("Address");
            XmlElement phone = xmlDocument.CreateElement("Phone");
            XmlElement mobilePhone = xmlDocument.CreateElement("MobilePhone");
            XmlElement flatPhone = xmlDocument.CreateElement("FlatPhone");

            Console.Write("Ф.И.О: ");
            string fullName = Console.ReadLine();
            Console.Write("Улица: ");
            string streets = Console.ReadLine();
            Console.Write("Номер дома: ");
            string houseNum = Console.ReadLine();
            Console.Write("Номер квартиры: ");
            string flatNum = Console.ReadLine();
            Console.Write("Моб телефон: ");
            string mobileNum = Console.ReadLine();
            Console.Write("Дом телефон: ");
            string flatNums = Console.ReadLine();

            XmlText nameText = xmlDocument.CreateTextNode(fullName);
            XmlText stret = xmlDocument.CreateTextNode(streets);
            XmlText hoseNUm = xmlDocument.CreateTextNode(houseNum);
            XmlText flatNume = xmlDocument.CreateTextNode(flatNum);
            XmlText phoneMobile = xmlDocument.CreateTextNode(mobileNum);
            XmlText numFlat = xmlDocument.CreateTextNode(flatNums);

            nameAttr.AppendChild(nameText);
            street.AppendChild(stret);
            houseNumber.AppendChild(hoseNUm);
            flatNumber.AppendChild(flatNume);
            mobilePhone.AppendChild(phoneMobile);
            flatPhone.AppendChild(numFlat);

            userElement.Attributes.Append(nameAttr);
            userElement.AppendChild(address);
            userElement.AppendChild(phone);
            address.AppendChild(street);
            address.AppendChild(houseNumber);
            address.AppendChild(flatNumber);
            phone.AppendChild(mobilePhone);
            phone.AppendChild(flatPhone);
            

            xRoot.AppendChild(userElement);

            xmlDocument.Save("_User.xml");
        }

        /// <summary>
        /// Показать данные с Xml файла
        /// </summary>
        public void Show()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load("_User.xml");
            XmlElement element = xml.DocumentElement;
            foreach (XmlNode xnode in element)
            {
                if (xnode.Attributes.Count > 0)
                {
                    XmlNode attr = xnode.Attributes.GetNamedItem("Name");
                    if (attr != null)
                    {
                        Console.WriteLine($"Ф.И.О: {attr.Value}");
                    }
                }
                
                foreach (XmlNode childNode in xnode.ChildNodes)
                {
                    if (childNode.Name == "Address")
                    {
                        foreach (XmlNode xnode2 in childNode.ChildNodes)
                        {
                            if (xnode2.Name == "Street")
                                Console.WriteLine($"Улица: {xnode2.InnerText}");
                            if (xnode2.Name == "HouseNumber")
                                Console.WriteLine($"Номер дома: {xnode2.InnerText}");
                            if (xnode2.Name == "FlatNumber")
                                Console.WriteLine($"Номер квартиры: {xnode2.InnerText}");
                        }
                    }
                    if (childNode.Name == "Phone")
                    {
                        foreach(XmlNode xnode3 in childNode.ChildNodes)
                        {
                            if (xnode3.Name == "MobilePhone")
                                Console.WriteLine($"Мобильный телефон: {xnode3.InnerText}");
                            if (xnode3.Name == "FlatPhone")
                                Console.WriteLine($"Домашний телефон: {xnode3.InnerText}");
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Выбор команд чтения или записи Xml файла
        /// </summary>
        public void Choose()
        {
            Console.WriteLine("\nЗадание 4: Записная книжка");
            while(true)
            {
                Console.WriteLine("\n1 - Для чтения\n2 - Для записи\nПустая строка = закрыть");
                string exit = Console.ReadLine();
                if (exit == String.Empty) break;
                int choose = int.Parse(exit);
                if (choose == 1)
                {
                    if (File.Exists("_User.xml") == false)
                    {
                        Input();
                    }
                    else Show();
                } 
                else if (choose == 2)
                {
                    if (File.Exists("_User.xml") == false)
                    {
                        Input();
                    } 
                    else ContinueCreate();
                } 
            }
        }
    }
}
