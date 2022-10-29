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
using System.Windows.Shapes;
using Language_Bezrukov.Classes;
using Language_Bezrukov.DB;
using static Language_Bezrukov.Classes.ClassHelper;

namespace Language_Bezrukov
{
    /// <summary>
    /// Логика взаимодействия для AddClients.xaml
    /// </summary>
    /// 


    public partial class AddClients : Window
    {
        public AddClients()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
            this.Close();
        }

        private void BtnRefCl_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            RefClient refClient = new RefClient();
            refClient.ShowDialog();
            this.Close();
        }

        private void LastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => (ch >= 'а' && ch <= 'я') || (ch >= '-' && ch <= ' ') || (ch >= 'А' && ch <= 'Я') || ch == 'ё' || ch == 'Ё' || (ch >= 'A' && ch <= 'Z') ||  (ch >= 'a' && ch <= 'z')).ToArray()
                    );
            }
        }

        private void FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => (ch >= 'а' && ch <= 'я') || (ch >= '-' && ch <= ' ') || (ch >= 'А' && ch <= 'Я') || ch == 'ё' || ch == 'Ё' || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z')).ToArray()
                    );
            }
        }

        private void Patronymic_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я') || ch == 'ё' || ch == 'Ё' || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z')).ToArray()
                    );
            }
        }

        private void Birthday_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => ch == '.' || (ch >= '0' && ch <= '9') || ch == '/' ).ToArray()
                    );
            }
        }

        private void NumberPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => ch == '+' || (ch >= '0' && ch <= '9') || ch == '(' || ch == ')').ToArray()
                    );
            }
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') || ch >= '@' || ch >= '.').ToArray()
                    );
            }
        }

        private void BntAdd_Click(object sender, RoutedEventArgs e)
        {
            {
                if (string.IsNullOrWhiteSpace(LastName.Text))
                {
                    MessageBox.Show("Заполните поле фамилия!", "Ошибка!", MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(FirstName.Text))
                {
                    MessageBox.Show("Заполните поле имя!", "Ошибка!", MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(Patronymic.Text))
                {
                    MessageBox.Show("Заполните поле отчество!", "Ошибка!", MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(Birthday.Text))
                {
                    MessageBox.Show("Заполните поле день рождения!", "Ошибка!", MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(NumberPhone.Text))
                {
                    MessageBox.Show("Заполните поле номер телефона!", "Ошибка!", MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(Email.Text))
                {
                    MessageBox.Show("Заполните поле электронной почты!", "Ошибка!", MessageBoxButton.OK);
                    return;
                }

                try
                {
                    var resClick = MessageBox.Show("Добавить пользователя?", "Подтверждение.", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resClick == MessageBoxResult.Yes)
                    {
                        DB.Client addClient = new DB.Client();
                        addClient.LastName = LastName.Text;
                        addClient.FirstName = FirstName.Text;
                        addClient.Patronymic = Patronymic.Text;
                        addClient.Birthday = Convert.ToDateTime(Birthday.Text);
                        addClient.NumberPhone = NumberPhone.Text;
                        addClient.Email = Email.Text;

                        ClassHelper.context.Client.Add(addClient);
                        ClassHelper.context.SaveChanges();

                        MessageBox.Show("Пользователь успешно добавлен.", "Выполнено!", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Hide();
                        Client client = new Client();
                        this.Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    throw;
                }
            }
        }
    }
}

