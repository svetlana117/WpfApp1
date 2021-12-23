using System;
using System.Collections.Generic;
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
using System.Data.Entity;
using WPF1;
using System.Windows.Media.Animation;


namespace WpfApp1.pages
{
    /// <summary>
    /// Логика взаимодействия для login.xaml
    /// </summary>
    public partial class login : Page
    {

        public login()
        {
            InitializeComponent();
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.Duration = new TimeSpan(0, 0, 5);
            doubleAnimation.From = 115;
            doubleAnimation.To = 570;
            doubleAnimation.AutoReverse = true;//туда и обратно
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;//количество повторов
            btnLogin.BeginAnimation(WidthProperty, doubleAnimation);
            btnRegn.BeginAnimation(WidthProperty, doubleAnimation);

            ColorAnimation colorAnimation = new ColorAnimation();//анимация цвета
            colorAnimation.From = Color.FromRgb(65, 105, 225);//синий
            colorAnimation.To = Color.FromRgb(0, 179, 255);
            colorAnimation.Duration = TimeSpan.FromSeconds(10);
            colorAnimation.AutoReverse = true;
            colorAnimation.RepeatBehavior = RepeatBehavior.Forever;
            btnLogin.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));//задний план по умолчанию
            btnLogin.Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            btnRegn.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));//задний план по умолчанию
            btnRegn.Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                auth CurrentUsers = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.login == txtLogin.Text && x.password == txtPassword.Password);
                if (CurrentUsers != null)
                {//тут будет алгоритм перехода на страницу в зависимости от роля пользователя
                    switch(CurrentUsers.role)
                    {
                        case 1:
                            MessageBox.Show("Вы зашли как админ");
                            LoadPages.MainFrame.Navigate(new PageUsersList());
                            break;
                        case 2:
                            MessageBox.Show("Вы зашли как обычный пользователь");
                            LoadPages.MainFrame.Navigate(new Info(CurrentUsers));
                            break;
                    }

                }
                else
                {
                    MessageBox.Show("Что-то не так, попробуй снова!");
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка :/"); }

            


        }

        private void btnRegn_Click(object sender, RoutedEventArgs e)
        {
           LoadPages.MainFrame.Navigate(new reg());

        }


        private void GIFKA_MediaEnded(object sender, RoutedEventArgs e)
        {
            GIFKA.Position = new TimeSpan(0, 0, 1);
            GIFKA.Play();
        }
    }
}
