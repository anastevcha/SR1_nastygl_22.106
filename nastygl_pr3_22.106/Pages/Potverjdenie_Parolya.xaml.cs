using nastygl_pr3_22._106.Model2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
    /// Логика взаимодействия для Potverjdenie_Parolya.xaml
    /// </summary>
    public partial class Potverjdenie_Parolya : Page
    {
        atelie2Model atelie2Model;
        string emailPassword;
        string sotrudnikEmail;
        public Potverjdenie_Parolya()
        {
            InitializeComponent();
            atelie2Model = atelie2Model.GetContext();
            enterCod.Visibility = Visibility.Collapsed;
            text.Visibility = Visibility.Collapsed;
            EnterCod.Visibility = Visibility.Collapsed;
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            sotrudnikEmail = enterEmail.Text;
            var sotrudnik = atelie2Model.GetContext().Sotrudniki.Where(p => p.Email == sotrudnikEmail).FirstOrDefault();
            if (sotrudnik != null)
            {
                enterCod.Visibility = Visibility.Visible;
                text.Visibility = Visibility.Visible;
                EnterCod.Visibility = Visibility.Visible;
                Enter.Visibility = Visibility.Collapsed;
                emailPassword = Verification(sotrudnikEmail);
            }
            else
            {
                MessageBox.Show("Проверьте правильность ввода почты");
            }
        }

        private void EnterCod_Click(object sender, RoutedEventArgs e)
        {
            if (emailPassword == enterCod.Text.ToString())
            {
                NavigationService.Navigate(new ChangePassword(sotrudnikEmail));
            }
            else
            {
                MessageBox.Show("Неправильный код");
            }
        }
        private string Verification(string Email)
        {
            Random random = new Random();
            int codEmail = random.Next(1000, 9999);
            string smtpServer = "smtp.mail.ru";
            int smtpPort = 587;
            string smtpUsername = "verifikacia3025@mail.ru";
            string smtpPassword = "d2iervyw0zhFdgxymAjA";   //m7E-i8m-j74-Sq3 от почты

            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(smtpUsername);
                    mailMessage.To.Add(Email);
                    mailMessage.Subject = "Пароль для подтверждения";
                    mailMessage.Body = $"Введите данный код для входа: {codEmail}";
                    try
                    {
                        smtpClient.Send(mailMessage);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            return codEmail.ToString();
        }
    }
}
