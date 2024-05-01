using HashPasswords;
using nastygl_pr3_22._106.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Microsoft.Office.Interop.Word;



namespace nastygl_pr3_22._106.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : System.Windows.Controls.Page
    {
        Model1 contextDB;
        Sotrudniki sotrudniki;
        Avtorizacia avtorizacia;
        Pasport pasport;
        public AddUser()
        {
            InitializeComponent();
            contextDB = Model1.GetContext();
            sotrudniki = new Sotrudniki();
            avtorizacia = new Avtorizacia();
            pasport = new Pasport();

        }
        private int Doljnost(string doljnost)
        {
            if (doljnost == "Администратор") return 1;
            else if (doljnost == "Портной") return 2;
            else if (doljnost == "Дизайнер") return 3;
            else if (doljnost == "Работник склада") return 4;
            else if (doljnost == "Консультант") return 5;
            else return 0;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string errorPassword = "";
            sotrudniki.Familiya = txbFamiliya.Text;
            sotrudniki.Imya = txbImya.Text;
            sotrudniki.Otchestvo = txbOtchestvo.Text;
            sotrudniki.Email = txbPochta.Text;

            if (Doljnost(txbDoljnost.Text) != 0) sotrudniki.Doljnost = Doljnost(txbDoljnost.Text);
            if (decimal.TryParse(txbZarplata.Text, out decimal zarplata))
            {
                sotrudniki.Zarplata = zarplata;
            }
            if (int.TryParse(txbStag.Text, out int stag))
            {
                sotrudniki.Stag = stag;
            }
            if (int.TryParse(txbVozrast.Text, out int vozrast))
            {
                sotrudniki.Vozrast = vozrast;
            }
            avtorizacia.Login = txbLogin.Text;

            string inputPassword = txbParol.Text;
            if (inputPassword.Length >= 4)
            {
                string hash = Class1.HashPassword(inputPassword);//хэширование пароля
                avtorizacia.Parol = hash;
            }
            else
            {
                errorPassword = "Пароль обязателен и должен состоять минимум из 4 символов";
            }
            pasport.Seriya = Convert.ToInt32(txbSeriya.Text);
            pasport.Nomer = Convert.ToInt32(txbNomer.Text);
            pasport.Vydan = txbKemVydan.Text;
            var contextSotrudniki = new ValidationContext(sotrudniki);//ошибки по сотрудникам
            var contextUser = new ValidationContext(avtorizacia);//ошибки по юзеру
            var contextPasport = new ValidationContext(pasport);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            bool res1, res2, res3;//переменные с содержанием валидации данных
            res1 = Validator.TryValidateObject(sotrudniki, contextSotrudniki, results, true);//валидация таблицы сотрудники
            res2 = Validator.TryValidateObject(avtorizacia, contextUser, results, true);
            res3 = Validator.TryValidateObject(pasport, contextPasport, results, true);
            if (res1 && res2 && res3)
            {

                
                string zarplata1 = "";
                string zarplataSlovami = "";
                if (sotrudniki.Doljnost == 1)
                {
                    zarplata1 = "20000";
                    zarplataSlovami = "Двадцать тысяч";
                }
                if (sotrudniki.Doljnost == 2)
                {
                    zarplata1 = "30000";
                    zarplataSlovami = "Тридцать тысяч";
                }
                if (sotrudniki.Doljnost == 3)
                {
                    zarplata1 = "40000";
                    zarplataSlovami = "Сорок тысяч";
                }
                if (sotrudniki.Doljnost == 4)
                {
                    zarplata1 = "50000";
                    zarplataSlovami = "Пятьдесят тысяч";
                }

                string imyaRabotnika = sotrudniki.Familiya + " " + sotrudniki.Imya + " " + sotrudniki.Otchestvo;

                var items = new Dictionary<string, string>()
                {
                    {"<nomerDogovora>", sotrudniki.ID_Sotrudnika.ToString()},
                    {"<den>", DateTime.Today.DayOfWeek.ToString()},
                    {"<mesaz>", DateTime.Today.Month.ToString()},
                    {"<god>", DateTime.Today.Year.ToString()},
                    {"<nazvanieKompanii>", "Comp"},
                    {"<nazvanieDoljnosti>", txbDoljnost.Text },
                    {"<imyaDirectora>", "Гослинг Райн Гослингович"},
                    {"<ispitatelnySrok>", "две недели"},
                    {"<zarplata>", zarplata1},
                    {"<zarplataSlovami>", zarplataSlovami},
                    {"<inyeViplaty>", "Не предусмотрены"},
                    {"<inn>", "1456235478"},
                    {"<adres>", "Новосибирск ул. Фадеева д. 66/4 кв. 1"},
                    {"<imyaRabotnika>", imyaRabotnika},
                    {"<seriya>", pasport.Seriya.ToString()},
                    {"<nomer>", pasport.Nomer.ToString()},
                    {"<kemVydan>", pasport.Vydan}
                };

                Microsoft.Office.Interop.Word.Application wordApp = null;
                Document wordDoc;

                try
                {
                    wordApp = new Microsoft.Office.Interop.Word.Application();

                    object missing = Type.Missing;
                    object fileName = "C:\\Users\\psvag\\Desktop\\practicheskie\\nastygl_pr3_22.106\\Files\\Dogovor.docx"; 

                    wordDoc = wordApp.Documents.Open(ref fileName, ref missing, ref missing, ref missing); 

                    foreach (var item in items) // Перебор всех тегов и значений словаря, с последующей
                                                // заменой каждого тега на соответствующее для него значение
                    {
                        Find find = wordApp.Selection.Find;
                        find.Text = item.Key;
                        find.Replacement.Text = item.Value;

                        object wrap = WdFindWrap.wdFindContinue;
                        object replace = WdReplace.wdReplaceAll;

                        find.Execute(FindText: Type.Missing,
                            MatchCase: false, MatchWholeWord: false, MatchWildcards: false,
                            MatchSoundsLike: missing, MatchAllWordForms: false, Forward: true,
                            Wrap: wrap, Format: false, ReplaceWith: missing, Replace: replace);
                    }
                             
                    object newFile = $"C:\\Users\\psvag\\Desktop\\Договоры\\{sotrudniki.Familiya}.docx";
                    wordDoc.SaveAs2(newFile); 
                    wordApp.ActiveDocument.Close(); 
                    wordApp?.Quit(); 
                }
                catch (Exception ex)
                {
                    wordApp.ActiveDocument.Close(); 
                    wordApp?.Quit();
                    MessageBox.Show(ex.Message);
                }
                contextDB.Avtorizacia.Add(avtorizacia);
                contextDB.SaveChanges();
                sotrudniki.ID_Avtorizacii = avtorizacia.ID_Avtorizacii;
                contextDB.Pasport.Add(pasport);
                contextDB.SaveChanges();
                sotrudniki.ID_Pasporta = pasport.ID_Pasporta;
                contextDB.Sotrudniki.Add(sotrudniki);
                contextDB.SaveChanges();

                MessageBox.Show("Новый пользователь успешно добавлен");
                NavigationService.Navigate(new Admin());
            }
            else
            {
                string errorMessage = "Ошибки валидации:\n";
                foreach (var error in results)
                {
                    errorMessage += error.ErrorMessage + "\n";
                }
                errorMessage += errorPassword;
                MessageBox.Show(errorMessage);
                errorPassword = "";
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            txbFamiliya.Text = "";
            txbImya.Text = "";
            txbOtchestvo.Text = "";
            txbZarplata.Text = "";
            txbStag.Text = "";
            txbDoljnost.Text = "";
            txbVozrast.Text = "";
            txbLogin.Text = "";
            txbParol.Text = "";
            txbPochta.Text = "";
            txbSeriya.Text = "";
            txbNomer.Text = "";
           
        }

    }
}


