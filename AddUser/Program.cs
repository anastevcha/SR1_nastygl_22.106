using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashPasswords;
using AddUser.Model;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace AddUser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                atelieEntities db = Helper1.GetContext();
                Helper1 helper1 = new Helper1();
                bool write = true;
                while (write)
                {
                    write = true;
                    Sotrudniki sotrudnik = new Sotrudniki();
                    Avtorizacia avtorizacia = new Avtorizacia();
                    Console.WriteLine("Создание новой учетной записи /n");
                    Console.WriteLine("Введите имя пользователя: ");
                    string name = IsFIO();
                    sotrudnik.Imya = name;
                    Console.WriteLine("Введите фамилию пользователя: ");
                    string familiya = IsFIO();
                    sotrudnik.Familiya = familiya;
                    Console.WriteLine("Введите отчество пользователя: ");
                    string otchestvo = IsFIO();
                    sotrudnik.Otchestvo = otchestvo;
                    Console.WriteLine("Введите возраст пользователя: ");//**********
                    Vozrast(sotrudnik);
                    Console.WriteLine("Введите зарплату пользователя: ");
                    Zarplata(sotrudnik);
                    Console.WriteLine("Введите стаж пользователя: ");
                    Stag(sotrudnik);
                    Console.Write("Введите должность пользователя: ");
                    RightDoljnost(db, sotrudnik);

                    Console.WriteLine("Введите логин пользователя");
                    string login = Console.ReadLine();
                    LengthLogin(login);
                    avtorizacia.Login = login;


                    Console.WriteLine("Введите пароль пользователя");
                    string password = Console.ReadLine();
                    LengthPassword(password);
                    string hashPassword = Class1.HashPassword(password);
                    avtorizacia.Parol = hashPassword;
                    Console.WriteLine("Хэшированный пароль пользователя:" + hashPassword);

                    helper1.CreateUsers(avtorizacia);

                    sotrudnik.ID_Avtorizacii = avtorizacia.ID_Avtorizacii;
                    helper1.CreateSotrudnik(sotrudnik);
                    Console.WriteLine("Учетная запись добавлена");
                    Console.WriteLine("Чтобы продолжить нажмите Enter, чтобы завершить введите <конец>");
                    string where = Console.ReadLine();
                    if (where == "конец") { write = false; }
                    else { write = true; }

                    Console.Clear();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
        public static string IsFIO()
        {
            Regex formatFIO = new Regex("^[а-яА-Я]+$");
            string str;
            do
            {
                str = Console.ReadLine();
                if (formatFIO.Match(str).Success)
                {
                    Console.WriteLine("Все верно");
                    return str;
                }
                else
                {
                    MessageBox.Show("Используйте кириллицу, без дополнительных символов!");
                }
            }
            while (true);




        }
        /// <summary>
        /// Метод проверяет правильность ввода логина, проверяет подходит ли под регулярное выражение
        /// </summary>
        /// <returns>Вернет строку</returns>
        public static string IsLogin()
        {

            Regex formatLogina = new Regex("^[а-яА-Я0-9]$");
            string login;
            do
            {
                login = Console.ReadLine();
                if (formatLogina.Match(login).Success)
                {

                    break;
                }
                else
                {
                    MessageBox.Show("Используйте буквы и цифры");
                }
            }
            while (true);
            Console.WriteLine("Все верно");
            return login;
        }
        public static void RightDoljnost(atelieEntities db, Sotrudniki sotrudnik) 
        {
            do
            {
                string doljnostPosition = Console.ReadLine();
                var doljnost = db.Doljnost.FirstOrDefault(p => p.Nazvanie == doljnostPosition);
                if (doljnost != null)
                {
                    MessageBox.Show("Должность найдена");
                    sotrudnik.Doljnost = doljnost.ID_Doljnosti;
                    break;
                }
                else
                {
                    MessageBox.Show("Должность не найдена");
                }
            } while (true);
        }
        public static void LengthLogin(string login)
        {
            if (login == null || login.Length < 5)
            {
                MessageBox.Show("Логин должен состоять минимум из 5 символов");
                return;
            }
        }
        public static void LengthPassword(string password)
        {
            if (password == null || password.Length < 5)
            {
                MessageBox.Show("Пароль должен состоять минимум из 5 символов");
                return;
            }
        }
        public static void Vozrast(Sotrudniki sotrudnik)
        {
            int vozrast = Convert.ToInt32(Console.ReadLine());
            if (vozrast == null || vozrast <=18)
            {
                MessageBox.Show("Сотрудник должен быть старше 18");
            }
            else 
            { 
                sotrudnik.Vozrast = vozrast; 
            }
        }
        public static void Zarplata(Sotrudniki sotrudnik)
        {
            string zarplata = Console.ReadLine();
            decimal result;
            if (decimal.TryParse(zarplata, out result))
            {
                if (result > 0)
                {
                    sotrudnik.Zarplata = result;
                }
                else 
                {
                    MessageBox.Show("Зарплата не может быть отрицательной!");
                }
            }
            else 
            {
                MessageBox.Show("Зарплата числом!");
            }

        }
        public static void Stag(Sotrudniki sotrudnik)
        {
            string stag = Console.ReadLine();
            int result1;
            if (int.TryParse(stag, out result1))
            {
                if (result1 > 0)
                {
                    sotrudnik.Stag = result1;
                }
                else
                {
                    MessageBox.Show("Стаж не может быть отрицательным!");
                }
            }
            else
            {
                MessageBox.Show("Стаж числом!");
            }

        }

    }

    
}

