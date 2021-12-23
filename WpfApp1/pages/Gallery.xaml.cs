﻿using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApp1.pages
{

    public partial class Gallery : Page
    {
        List<usersimage> userImg;
        int i;
        public Gallery(users currentUser)
        {
            InitializeComponent();
            i = 0;
            BitmapImage BI = new BitmapImage();
            usersimage check = BaseConnect.BaseModel.usersimage.FirstOrDefault(x => x.id_user == currentUser.id);
            if (check != null)
            {
                userImg = BaseConnect.BaseModel.usersimage.Where(x => x.id_user == currentUser.id && x.avatar == false).ToList();
                usersimage avatarUser = BaseConnect.BaseModel.usersimage.FirstOrDefault(x => x.id_user == currentUser.id && x.avatar == true);
                if (avatarUser != null)
                {
                    userImg.Insert(0, avatarUser);
                }
                if (userImg[0].path != null)//если присутствует путь к картинке
                {
                    BI = new BitmapImage(new Uri(userImg[0].path, UriKind.Relative));
                }
                else//если присутствуют двоичные данные
                {
                    BI.BeginInit();//начать инициализацию BitmapImage (для помещения данных из какого-либо потока)
                    BI.StreamSource = new MemoryStream(userImg[0].image);//помещаем в источник данных двоичные данные из потока
                    BI.EndInit();//закончить инициализацию
                }
            }
            else
            {
                btnPrev.Visibility = Visibility.Collapsed;
                btnChange.Visibility = Visibility.Collapsed;
                btnNext.Visibility = Visibility.Collapsed;
                switch (currentUser.gender)//в зависимости от пола пользователя устанавливаем ту или иную картинку
                {
                    case 1:
                        BI = new BitmapImage(new Uri(@"/название/male.jpg", UriKind.Relative));
                        break;
                    case 2:
                        BI = new BitmapImage(new Uri(@"/название/female.jpg", UriKind.Relative));
                        break;
                    default:
                        BI = new BitmapImage(new Uri(@"/название/other.jpg", UriKind.Relative));
                        break;
                }
            }
            userImages.Source = BI;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            int a = userImg[i].id;
            usersimage findUser = BaseConnect.BaseModel.usersimage.FirstOrDefault(x => x.id == a && x.avatar == false);
            if (findUser == null)
                MessageBox.Show("Данное фото уже является аватаром профиля");
            else
            {
                a = userImg[i].id_user;
                usersimage avatarUser = BaseConnect.BaseModel.usersimage.FirstOrDefault(x => x.avatar == true && x.id_user == a);
                findUser.avatar = true;
                if (avatarUser != null)
                    avatarUser.avatar = false;
                BaseConnect.BaseModel.SaveChanges();
                MessageBox.Show("Аватар пользователя изменен!");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }

        private void imgChange(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            BitmapImage BI2 = new BitmapImage();
            switch (btn.Content)
            {
                case "Следующее":
                    if (i < userImg.Count - 1)
                        i++;
                    else
                        i = 0;
                    if (i < userImg.Count)
                    {
                        if (userImg[i].path != null)//если присутствует путь к картинке
                        {
                            BI2 = new BitmapImage(new Uri(userImg[i].path, UriKind.Relative));
                        }
                        else//если присутствуют двоичные данные
                        {
                            BI2.BeginInit();//начать инициализацию BitmapImage (для помещения данных из какого-либо потока)
                            BI2.StreamSource = new MemoryStream(userImg[i].image);//помещаем в источник данных двоичные данные из потока
                            BI2.EndInit();//закончить инициализацию
                        }
                        userImages.Source = BI2;
                    }
                    break;
                case "Предыдущее":
                    if (i != 0)
                        i--;
                    else
                        i = userImg.Count - 1;
                    if (i >= 0)
                    {
                        if (userImg[i].path != null)//если присутствует путь к картинке
                        {
                            BI2 = new BitmapImage(new Uri(userImg[i].path, UriKind.Relative));
                        }
                        else//если присутствуют двоичные данные
                        {
                            BI2.BeginInit();//начать инициализацию BitmapImage (для помещения данных из какого-либо потока)
                            BI2.StreamSource = new MemoryStream(userImg[i].image);//помещаем в источник данных двоичные данные из потока
                            BI2.EndInit();//закончить инициализацию
                        }
                        userImages.Source = BI2;
                    }
                    break;
            }
        }
    }
}
