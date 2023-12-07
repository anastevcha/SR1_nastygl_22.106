using HashPasswords;
using nastygl_pr3_22._106.Model;
using System;
using System.Linq;
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


        public Autho()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            txtboxCaptcha.Visibility = Visibility.Hidden;//скрытие полей
            txtBlockCaptcha.Visibility = Visibility.Hidden;//капчи от пользователя

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

            var user = Model11.GetContext().Avtorizacia.Where(p => p.Login == login && p.Parol == hashPassword).FirstOrDefault();

            if (login.Length > 0 && inpitPassword.Length > 0)//проверка на заполнение логина и пароля
            {
                if (countUnsuccessful < 2)//проверка на количество попыток входа 
                {
                    if (user != null)//пользователь существует, т.е. не пустое значение
                    {
                        MessageBox.Show("Учетная запись найдена");

                        var s = Model11.GetContext().Sotrudniki.Where(s1 => s1.ID_Avtorizacii == user.ID_Avtorizacii).FirstOrDefault();//получение ID должности
                        int id = s.Doljnost;
                        LoadForm(id);
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
        private void LoadForm(int role)
        {
            switch (role)
            {
                case 1:
                    NavigationService.Navigate(new Admin());
                    break;
                case 2:
                    NavigationService.Navigate(new Portnoy());
                    break;
                case 3:
                    NavigationService.Navigate(new Dizayner());
                    break;
                case 4:
                    NavigationService.Navigate(new Rabotnik_Sklada());
                    break;
                case 5:
                    NavigationService.Navigate(new Konsultant());
                    break;

            }

        }



        private void Timer_Tick(object sender, EventArgs e)//разблокирование полей ввода
        {
            timerLabel.Text =  $"Окно заблокировано на {countdown} секунд"; 

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
    }

}


