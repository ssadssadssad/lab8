using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Lab8
{
    public interface IMain<T> //ограничение

    {
        void Remove(); //удалить
        void Add(T value); //добавить
        void Display(); //просмотр
    }
    public interface IMain1<T> where T : struct //ограничение

    {
        void Remove();
        void Add(T value);
        void Display();
    }
    class Program //лаба 4
    {
        static void Main(string[] args)
        {
            try
            {
                List<int> i1 = new List<int>(new int[6] { 1, 5, 3, 54, 4, 43 });
                List<int> i2 = new List<int>(new int[6] { 10, 5, 33, 7, 2, 82 });
                List<int> q1 = new List<int>(new int[6] { 1, 2, 3, 4, 5, 6 });
                List<int> q2 = new List<int>(new int[3] { 2, 3, 4 });
                i1.Display();
                Console.WriteLine("Прибавили в конец массива");
                i1 = i1 + 2;
                i1.Display();
                Console.WriteLine("Объединение");
                List<int> i3 = i1 + i2;
                i3.Display();
                Console.WriteLine("Пересечение");
                List<int> i4 = i1 * i2;
                i4.Display();
                Console.WriteLine($"Мощность множества {!i1}");
                Console.WriteLine($"Подмножество множества {q1 | q2}");
                List<int>.Owner owner = new List<int>.Owner(228, "Grisha", "BSTU");
                List<int>.Date date = new List<int>.Date(09, 01, 2002);
                i1.Display();
                i3.Remove();
                i3.Remove();
                i3.Remove();
                i3.Add(10);
                i3.Display();
            }
            catch (ListException ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
            finally
            {
                Console.WriteLine($"Finally");
            }
            List<float> f1 = new List<float>(new float[6] { 1.55f, 5.22f, 3.344f, 54.1f, 4.88f, 43.72f });
            f1.Display();
            List<string> s1 = new List<string>(new string[6] { "ff", "fgsf", "gg", "hello", "good", "bye" });
            s1.Display();
            Transport transport1 = new Transport(4, 2500, 3.5);
            Transport transport2 = new Transport(4, 3500, 2.5);
            Transport transport3 = new Transport(4, 4500, 4);
            List<Transport> t1 = new List<Transport>(new Transport[3] { transport1, transport2, transport3 });
            t1.Display();

            Console.WriteLine("Запись в файл и чтение из него");

            string path = @"D:\Objects";
            string rwPath = @"D:\Objects\info.txt";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            // запись в файл
            f1.WriteFile(rwPath, false);
            s1.WriteFile(rwPath);
            t1.WriteFile(rwPath);

            // чтение
            using (StreamReader sr = new StreamReader(rwPath))
            {
                Console.WriteLine(sr.ReadToEnd());
            }

            List1<float> f10 = new List1<float>(new float[6] { 1.55f, 5.22f, 3.344f, 54.1f, 4.88f, 43.72f });
            f10.Display();

        }
    }
    public class List<T> : IMain<T> //класс
    {
        public void Remove() //прописываем удаление
        {
            T[] result = new T[plenty.Length - 1];
            for (int i = 0; i < plenty.Length - 1; i++)
                result[i] = plenty[i];
            this.plenty = result;
        }
        public void Add(T value) //прописываем удаление
        {
            T[] result = new T[plenty.Length + 1];
            result[result.Length - 1] = value;
            for (int i = plenty.Length - 1; i >= 0; i--)
            {
                result[i] = plenty[i];
            }
            this.plenty = result;
            if (plenty.Length > 10) throw new ListException("Вы не можете добавить новые элементы если длина текущего списка превышает или равна 10-ти элементам!");
        }
        public void Display() //прописываем вывод
        {
            if (plenty.Length == 0)
            {
                Console.WriteLine($"Список пуст!");
                return;
            }

            string result = "(";
            for (int i = 0; i < plenty.Length; i++)
            {
                if (i == plenty.Length - 1) result += $"{plenty[i]}";
                else result += $"{plenty[i]}, ";

            }
            result += ")";
            Console.WriteLine($"Список: {result}");
        }

        public string dis() // запись в строку
        {
            if (plenty.Length == 0)
            {
                Console.WriteLine($"Список пуст!");
            }

            string result = "(";
            for (int i = 0; i < plenty.Length; i++)
            {
                if (i == plenty.Length - 1) result += $"{plenty[i]}";
                else result += $"{plenty[i]}, ";

            }
            result += ")";
            return result;
        }

        public T[] plenty;

        public List(T[] values)
        {
            plenty = values;
        }
        public static List<T> operator +(List<T> set, T value) //добавление элемента в коллекцию
        {
            int l = set.plenty.Length;
            Array.Resize(ref set.plenty, ++l);
            set.plenty[--l] = value;
            return set;
        }
        public static List<T> operator +(List<T> s1, List<T> s2) //соединяем коллекции
        {
            T[] z = new T[s1.plenty.Length + s2.plenty.Length];
            s1.plenty.CopyTo(z, 0);
            s2.plenty.CopyTo(z, s1.plenty.Length);
            return new List<T>(z);
        }
        public static List<T> operator *(List<T> s1, List<T> s2) //пересечение
        {
            int len = 0, ind = 0;
            foreach (T w1 in s1.plenty)
            {
                foreach (T w2 in s2.plenty)
                {
                    if (w1.Equals(w2))
                    {
                        len++;
                    }
                }
            }
            T[] z = new T[len];
            foreach (T w1 in s1.plenty)
            {
                foreach (T w2 in s2.plenty)
                {
                    if (w1.Equals(w2))
                    {
                        z[ind] = w1;
                        ind++;
                    }
                }
            }
            return new List<T>(z);
        }
        public void WriteFile(string writePath, bool rw=true) //запись в файл
        {
            using (StreamWriter sw = new StreamWriter(writePath, rw, System.Text.Encoding.Default))
            {
                sw.WriteLine(dis());
            }
        }
        public static bool operator |(List<T> s1, List<T> s2) //проверка на подмножество
        {
            int buf = 0;
            for (int i = 0; i < s1.plenty.Length; i++)
            {
                for (int j = 0; j < s2.plenty.Length; j++)
                {
                    if (s1.plenty[i].Equals(s2.plenty[j])) 
                    {
                        buf++;
                    }
                }
            }
            if (buf == s2.plenty.Length)
                return true;
            else
                return false;
        }
        public static int operator !(List<T> s1) //длина массива
        {
            return s1.plenty.Length;
        }
        public class Owner //классы
        {
            private int id;
            private string name;
            private string organisation;

            public Owner(int id, string name, string organisation)
            {
                this.id = id;
                this.name = name;
                this.organisation = organisation;
            }

            public int ID
            {
                get => id;
            }

            public string Name
            {
                get => name;
            }
            public string Organisation
            {
                get => organisation;
            }
        }

        public class Date
        {
            private int day, month, year;
            public int Day
            {
                get => day;
            }
            public int Month
            {
                get => month;
            }
            public int Year
            {
                get => year;
            }
            public Date(int d, int m, int y)
            {
                day = d; month = m; year = y;
            }
        }
    }
    public class ListException : Exception //исключение
    {
        public ListException(string message)
            : base(message)
        { }
    }

    public class List1<T> : IMain1<T> where T: struct //ограничение
    {
        public void Remove() //удаление
        {
            T[] result = new T[plenty.Length - 1];
            for (int i = 0; i < plenty.Length - 1; i++)
                result[i] = plenty[i];
            this.plenty = result;
        }
        public void Add(T value) //добавление
        {
            T[] result = new T[plenty.Length + 1];
            result[result.Length - 1] = value;
            for (int i = plenty.Length - 1; i >= 0; i--)
            {
                result[i] = plenty[i];
            }
            this.plenty = result;
            if (plenty.Length > 10) throw new ListException("Вы не можете добавить новые элементы если длина текущего списка превышает или равна 10-ти элементам!");
        }
        public void Display() //просмотр
        {
            if (plenty.Length == 0)
            {
                Console.WriteLine($"Список пуст!");
                return;
            }

            string result = "(";
            for (int i = 0; i < plenty.Length; i++)
            {
                if (i == plenty.Length - 1) result += $"{plenty[i]}";
                else result += $"{plenty[i]}, ";

            }
            result += ")";
            Console.WriteLine($"Список: {result}");
        }

        public string dis() //запись в строку
        {
            if (plenty.Length == 0)
            {
                Console.WriteLine($"Список пуст!");
            }

            string result = "(";
            for (int i = 0; i < plenty.Length; i++)
            {
                if (i == plenty.Length - 1) result += $"{plenty[i]}";
                else result += $"{plenty[i]}, ";

            }
            result += ")";
            return result;
        }
        //все тоже самое просто под ограничением
        public T[] plenty;

        public List1(T[] values)
        {
            plenty = values;
        }
        public static List1<T> operator +(List1<T> set, T value)
        {
            int l = set.plenty.Length;
            Array.Resize(ref set.plenty, ++l);
            set.plenty[--l] = value;
            return set;
        }
        public static List1<T> operator +(List1<T> s1, List1<T> s2)
        {
            T[] z = new T[s1.plenty.Length + s2.plenty.Length];
            s1.plenty.CopyTo(z, 0);
            s2.plenty.CopyTo(z, s1.plenty.Length);
            return new List1<T>(z);
        }
        public static List1<T> operator *(List1<T> s1, List1<T> s2)
        {
            int len = 0, ind = 0;
            foreach (T w1 in s1.plenty)
            {
                foreach (T w2 in s2.plenty)
                {
                    if (w1.Equals(w2))
                    {
                        len++;
                    }
                }
            }
            T[] z = new T[len];
            foreach (T w1 in s1.plenty)
            {
                foreach (T w2 in s2.plenty)
                {
                    if (w1.Equals(w2))
                    {
                        z[ind] = w1;
                        ind++;
                    }
                }
            }
            return new List1<T>(z);
        }
        public void WriteFile(string writePath, bool rw = true)
        {
            using (StreamWriter sw = new StreamWriter(writePath, rw, System.Text.Encoding.Default))
            {
                sw.WriteLine(dis());
            }
        }
        public static bool operator |(List1<T> s1, List1<T> s2)
        {
            int buf = 0;
            for (int i = 0; i < s1.plenty.Length; i++)
            {
                for (int j = 0; j < s2.plenty.Length; j++)
                {
                    if (s1.plenty[i].Equals(s2.plenty[j]))
                    {
                        buf++;
                    }
                }
            }
            if (buf == s2.plenty.Length)
                return true;
            else
                return false;
        }
        public static int operator !(List1<T> s1)
        {
            return s1.plenty.Length;
        }
        public class Owner
        {
            private int id;
            private string name;
            private string organisation;

            public Owner(int id, string name, string organisation)
            {
                this.id = id;
                this.name = name;
                this.organisation = organisation;
            }

            public int ID
            {
                get => id;
            }

            public string Name
            {
                get => name;
            }
            public string Organisation
            {
                get => organisation;
            }
        }

        public class Date
        {
            private int day, month, year;
            public int Day
            {
                get => day;
            }
            public int Month
            {
                get => month;
            }
            public int Year
            {
                get => year;
            }
            public Date(int d, int m, int y)
            {
                day = d; month = m; year = y;
            }
        }
    }
}
