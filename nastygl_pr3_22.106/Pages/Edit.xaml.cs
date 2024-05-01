using nastygl_pr3_22._106.Model2;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
using FluentValidation;
using HashPasswords;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Remoting.Contexts;

namespace nastygl_pr3_22._106.Pages
{
    /// <summary>
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Page
    {
        public Sotrudniki sotrudniki;
        public atelie2Model context;
        public Edit(int id)
        {
            InitializeComponent();
            context = atelie2Model.GetContext();
            sotrudniki = atelie2Model.GetContext().Sotrudniki.Where(i => i.ID_Sotrudnika == id).FirstOrDefault();
            txbFamiliya.Text = sotrudniki.Familiya;
            txbImya.Text = sotrudniki.Imya;
            txbOtchestvo.Text = sotrudniki.Otchestvo;
            txbVozrast.Text = sotrudniki.Vozrast.ToString();
            decimal zarplata = sotrudniki.Zarplata;
            txbZarplata.Text = sotrudniki.Zarplata.ToString();
            txbStag.Text = sotrudniki.Stag.ToString();
            txbDoljnost.Text = GetDoljnost(sotrudniki);
            txbPochta.Text = sotrudniki.Email;
        }
        private string GetDoljnost(Sotrudniki s)
        {
            string doljnost = "";
            if (s.Doljnost == 1) doljnost = "Администратор";
            if (s.Doljnost == 2) doljnost = "Портной";
            if (s.Doljnost == 3) doljnost = "Дизайнер";
            if (s.Doljnost == 4) doljnost = "Работник склада";
            if (s.Doljnost == 5) doljnost = "Консультант";
            return doljnost;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txbFamiliya.Text = "";
            txbImya.Text = "";
            txbOtchestvo.Text = "";
            txbZarplata.Text = "";
            txbStag.Text = "";
            txbDoljnost.Text = "";
            txbVozrast.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string errorPassword = "";
            sotrudniki.Familiya = txbFamiliya.Text;
            sotrudniki.Imya = txbImya.Text;
            sotrudniki.Otchestvo = txbOtchestvo.Text;
            txbPochta.Text = sotrudniki.Email;
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
           

           
            var contextSotrudniki = new ValidationContext(sotrudniki);//ошибки по сотрудникам
            
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();      
            bool res1;//переменные с содержанием валидации данных
            res1 = Validator.TryValidateObject(sotrudniki, contextSotrudniki, results, true);//валидация таблицы сотрудники
            
            if (res1)
            {

               
                context.SaveChanges();
                context.Sotrudniki.Add(sotrudniki);
                context.SaveChanges();

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
       
        
    }
}
