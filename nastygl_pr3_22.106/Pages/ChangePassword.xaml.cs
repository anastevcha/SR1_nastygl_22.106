using HashPasswords;
using nastygl_pr3_22._106.Model2;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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

namespace nastygl_pr3_22._106.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Page
    {
        Avtorizacia avtorizacia;
        Sotrudniki sotrudnik;
        atelie2Model context;
        public ChangePassword(string email)
        {
            InitializeComponent();
            context = atelie2Model.GetContext();
            sotrudnik = atelie2Model.GetContext().Sotrudniki.Where(p => p.Email == email).FirstOrDefault();
            avtorizacia = atelie2Model.GetContext().Avtorizacia.Where(i => i.ID_Avtorizacii == sotrudnik.ID_Avtorizacii).FirstOrDefault();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (startPassword.Password == finishPassword.Password)
            {
                string password = finishPassword.Password;
                if (password.Length > 3)
                {
                    string hashPassword = Class1.HashPassword(password);
                    avtorizacia.Parol = hashPassword;
                    context.Entry(avtorizacia).State = EntityState.Modified;//редактирование таблицы авторизации
                    context.SaveChanges();
                    MessageBox.Show("Пароль успешно изменен");
                    NavigationService.Navigate(new Autho());
                }
                else
                {
                    MessageBox.Show("Пароль должен состоять минимум из 4 символов");
                }
            }
            else
            {
                startPassword.Password = "";
                finishPassword.Password = "";
                MessageBox.Show("Пароли не совпадают, попробуйте заново");
            }

        }
    }
}
