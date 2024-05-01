using HashPasswords;
using nastygl_pr3_22._106.Model2;
using System;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace nastygl_pr3_22._106.Pages
{

    public partial class Autho : Page
    {
        private int countUnsuccessful = 0;
        private DispatcherTimer timer;
        private int countdown = 10;
        int id;
        string imya;
        string familiya;
        string otchestvo;
        string privetstviya;
        string codVerification;
        string sotrudnikEmail;


        public Autho()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            txtboxCaptcha.Visibility = Visibility.Hidden;//скрытие полей
            txtBlockCaptcha.Visibility = Visibility.Hidden;//капчи от пользователя
            btnCod.Visibility = Visibility.Collapsed;  

        }



        private void btnEnterGuests_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Guests());
        }


        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = txtbLogin.Text.Trim();
            string inpitPassword = psswbPassword.Password.Trim();
            string hashPassword = Class1.HashPassword(inpitPassword);

            var user = atelie2Model.GetContext().Avtorizacia.Where(p => p.Login == login && p.Parol == hashPassword).FirstOrDefault();

            if (login.Length > 0 && inpitPassword.Length > 0)//проверка на заполнение логина и пароля
            {
                if (countUnsuccessful < 2)//проверка на количество попыток входа 
                {
                    if (user != null)//пользователь существует, т.е. не пустое значение
                    {
                        MessageBox.Show("Учетная запись найдена");

                        var s = atelie2Model.GetContext().Sotrudniki.Where(s1 => s1.ID_Avtorizacii == user.ID_Avtorizacii).FirstOrDefault();//получение ID должности
                        id = s.Doljnost;
                        sotrudnikEmail = s.Email;
                        privetstviya = GetDate();
                        familiya = s.Familiya;
                        imya = s.Imya;
                        otchestvo = s.Otchestvo;
                        DateTime currentTime = DateTime.Now;
                        if (currentTime.TimeOfDay >= new TimeSpan(10, 0, 0) && currentTime.TimeOfDay <= new TimeSpan(19, 0, 0))
                        {
                            //LoadForm(id, imya, familiya, otchestvo, privetstviya);//теперь метод принимает 4 параметра, чтобы передавать ФИО в другие окна
                            txtLogin.Text = "Введите пароль для подтверждения";
                            txtbLogin.Text = "";
                            btnCod.Visibility = Visibility.Visible;
                            codVerification = Verification();
                            txtLogin.Visibility = Visibility.Visible;
                            txtbLogin.Visibility = Visibility.Visible;
                            txtParol.Visibility = Visibility.Hidden;
                            psswbPassword.Visibility = Visibility.Hidden;

                            btnEnter.Visibility = Visibility.Hidden;
                            btnEnterGuests.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            txtLogin.Visibility = Visibility.Hidden;
                            txtbLogin.Visibility = Visibility.Hidden;
                            txtParol.Visibility = Visibility.Hidden;
                            psswbPassword.Visibility = Visibility.Hidden;

                            btnEnter.IsEnabled = false;
                            btnEnterGuests.IsEnabled = false;
                            MessageBox.Show("Cейчас нерабочее время");
                        }

                           
                    }
                    else
                    {
                        MessageBox.Show("Такой учетной записи нет");
                        txtbLogin.Text = null;
                        psswbPassword.Password = null;
                        countUnsuccessful++;
                        Random random = new Random();
                        txtBlockCaptcha.Text = GetCaptcha(random.Next(4, 7));//капча
                    }
                }
                else
                {
                    txtLogin.Visibility = Visibility.Hidden;
                    txtbLogin.Visibility = Visibility.Hidden;
                    txtParol.Visibility = Visibility.Hidden;
                    psswbPassword.Visibility = Visibility.Hidden;

                    btnEnter.IsEnabled = true;


                    txtboxCaptcha.Visibility = Visibility.Visible;
                    txtBlockCaptcha.Visibility = Visibility.Visible;




                    btnEnter.Content = "Отправить";

                    string capthaInpit = txtboxCaptcha.Text;
                    txtBlockCaptcha.TextDecorations = TextDecorations.Strikethrough;
                    if (capthaInpit == txtBlockCaptcha.Text)
                    {
                        btnEnter.Content = "Войти";
                        txtLogin.Visibility = Visibility.Visible;
                        txtbLogin.Visibility = Visibility.Visible;
                        txtParol.Visibility = Visibility.Visible;
                        psswbPassword.Visibility = Visibility.Visible;
                        txtboxCaptcha.Visibility = Visibility.Hidden;
                        txtBlockCaptcha.Visibility = Visibility.Hidden;

                        countUnsuccessful = 0;
                        txtbLogin.Text = null;
                        psswbPassword.Password = null;
                    }
                    else
                    {
                        MessageBox.Show("Текст введен неверно");
                        Random rnd = new Random();
                        txtBlockCaptcha.Text = GetCaptcha(rnd.Next(3, 6));
                        txtboxCaptcha.Text = null;
                        countUnsuccessful++;
                        if (countUnsuccessful == 4)
                        {
                            txtbLogin.Visibility = Visibility.Visible;
                            txtLogin.Visibility = Visibility.Visible;
                            txtParol.Visibility = Visibility.Visible;
                            psswbPassword.Visibility = Visibility.Visible;
                            txtboxCaptcha.Visibility = Visibility.Hidden;
                            txtBlockCaptcha.Visibility = Visibility.Hidden;
                            timerLabel.Visibility = Visibility.Visible;
                            // Блокировка полей
                            txtbLogin.IsEnabled = false;
                            psswbPassword.IsEnabled = false;
                            btnEnter.IsEnabled = false;
                            btnEnterGuests.IsEnabled = false;
                            countdown = 10;
                            //timerLabel.Text = countdown.ToString();
                            // Запустить таймер
                            timer.Start();
                            timerLabel.Foreground = Brushes.Red;
                            //timerLabel.Text = "10"; 
                            countUnsuccessful = 0;
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("Поля для заполнения пароля и логина не должны быть пустыми");
            }
        }
        /// <summary>
        /// Метод для получения набора символов капчи
        /// </summary>
        /// <param name="length"></param>
        /// <returns>Вернет строку указанной длины</returns>
        private string GetCaptcha(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            Random rnd = new Random();
            char[] captchaChars = new char[length];
            for (int i = 0; i < length; i++)
            {
                captchaChars[i] = chars[rnd.Next(chars.Length)];
            }
            return new string(captchaChars);
        }
        private void LoadForm(int role, string imya, string familiya, string otchestvo, string privetstvie)
        {
            switch (role)
            {
                case 1:
                    NavigationService.Navigate(new Admin());
                    break;
                case 2:
                    NavigationService.Navigate(new Portnoy(imya, familiya, otchestvo, privetstvie));
                    break;
                case 3:
                    NavigationService.Navigate(new Dizayner(imya, familiya, otchestvo, privetstvie));
                    break;
                case 4:
                    NavigationService.Navigate(new Rabotnik_Sklada(imya, familiya, otchestvo, privetstvie));
                    break;
                case 5:
                    NavigationService.Navigate(new Konsultant(imya, familiya, otchestvo, privetstvie));
                    break;

            }

        }


        /// <summary>
        /// Метод запускает таймер, по истечению времени открывает поля для ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)//разблокирование полей ввода
        {
            timerLabel.Text = $"Окно заблокировано на {countdown} секунд";

            countdown--;

            //timerLabel.Text = countdown.ToString();

            if (countdown == 0)
            {
                timer.Stop();
                txtbLogin.IsEnabled = true;
                psswbPassword.IsEnabled = true;
                btnEnter.IsEnabled = true;
                btnEnterGuests.IsEnabled = true;
                txtbLogin.Text = null;
                psswbPassword.Password = null;
                timerLabel.Text = "";
            }
        }
        private string GetDate()//метод для получения строки приветствия в зависимости от времени
        {
            DateTime currentTime = DateTime.Now;
            if (currentTime.TimeOfDay >= new TimeSpan(10, 0, 0) && currentTime.TimeOfDay <= new TimeSpan(12, 0, 0))
            {
                return "Доброе утро,";
            }
            else if (currentTime.TimeOfDay > new TimeSpan(12, 0, 0) && currentTime.TimeOfDay <= new TimeSpan(17, 0, 0))
            {
                return "Добрый день,";
            }
            else if (currentTime.TimeOfDay > new TimeSpan(17, 0, 0) && currentTime.TimeOfDay <= new TimeSpan(19, 0, 0))
            {
                return "Добрый вечер,";
            }
            else
            {
                return " ";
            }
        }
        /// <summary>
        /// Метод для отправки кода на почту пользователя
        /// </summary>
        /// <returns>Вернет сгенерированный 4х значный код</returns>
        private string Verification()
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
                    mailMessage.To.Add(sotrudnikEmail);
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

        private void btnCod_Click(object sender, RoutedEventArgs e)
        {
            if (txtbLogin.Text == codVerification)
            {
                LoadForm(id, imya, familiya, otchestvo, privetstviya);
            }
            else
            {
                txtbLogin.Visibility = Visibility.Hidden;//скрыть поля ввода пароля и логина
                txtLogin.Visibility = Visibility.Hidden;
                txtParol.Visibility = Visibility.Hidden;
                psswbPassword.Visibility = Visibility.Hidden;
                btnEnter.IsEnabled = false;
                btnEnterGuests.IsEnabled = false;
                btnCod.Visibility = Visibility.Collapsed;
                MessageBox.Show("Вы не прошли аутентификацию");
            }
        }

        private void noPassword_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Potverjdenie_Parolya());
        }
    }



}


