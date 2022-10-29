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
using Language_Bezrukov.DB;
using static Language_Bezrukov.Classes.ClassHelper;


namespace Language_Bezrukov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<DB.Client> ListClient = new List<DB.Client>();

        int numberPage = 0;
        int countClient;


        public MainWindow()
        {
            InitializeComponent();
            AllInformation.ItemsSource = Classes.ClassHelper.context.Client.ToList();
            List<Gender> genders = context.Gender.ToList();
            genders.Insert(0, new Gender() { GenderName = "Все" });
            CBFilterGender.ItemsSource = genders;
            CBFilterGender.DisplayMemberPath = "GenderName";
            CBFilterGender.SelectedIndex = 0;

            CBSortFilter.ItemsSource = new List<string>
            {
                "По умолчанию",
                "Фамилии(в афлавитном подрядке)",
                "Дата последнего посещения(от новых к старым)",
                "Количеству посещений(от большего к меньшему)"
            };
            CBSortFilter.SelectedIndex = 0;

            CmbPage.ItemsSource = new List<string>
            {
                "Все",
                "10",
                "50",
                "200",
            };
            CmbPage.SelectedIndex = 0;
        }

        /// <summary>
        /// Метод для фильтрации и сортировки списка клиентов.
        /// </summary>
        public void Filter()
        {
            var list = context.Client.ToList();
            list = list.
                Where(i => i.LastName.Contains(TBSearch.Text)
                || i.FirstName.Contains(TBSearch.Text)
                || i.Email.Contains(TBSearch.Text)
                || i.Patronymic.Contains(TBSearch.Text)).ToList();

            if (CBFilterGender.SelectedIndex != 0)

            {
                var gender = CBFilterGender.SelectedItem as Gender;
                list = list.Where(i => i.IDGender == gender.ID).ToList();
            }

            //Сортировка:
            switch (CBSortFilter.SelectedIndex)
            {
                case 1:
                    list = list.OrderBy(i => i.LastName).ToList();
                    break;

                case 2:
                    list = list.OrderByDescending(i => i.LastVisit).ToList();
                    break;

                case 3:
                    list = list.OrderByDescending(i => i.CountVisit).ToList();
                    break;

                default:
                    list = list.OrderBy(i => i.ID).ToList();
                    break;
            }

            //Фильтр по дате рождения.
            if (CheckBBirth.IsChecked == true)
            {
                list = list.Where(i => i.Birthday.Month == DateTime.Now.Month).ToList();
            }

            AllInformation.ItemsSource = list;
            //Постраничный метод
            switch (CmbPage.SelectedIndex)
            {
                case 0: 
                    AllInformation.ItemsSource = list.ToList();
                    break;

                case 1: 
                    AllInformation.ItemsSource = list.Skip(numberPage * 10).Take(10).ToList();
                    break;

                case 2:
                    AllInformation.ItemsSource = list.Skip(numberPage * 50).Take(50).ToList();
                    break;

                case 3:
                    AllInformation.ItemsSource = list.Skip(numberPage * 200).Take(200).ToList();
                    break;

                default:
                    AllInformation.ItemsSource = list.ToList();
                    break;
            }

        }

        private void CBFilterGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void CBSortFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void CheckBBirth_Checked(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void CheckBBirth_Unchecked(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            CheckBBirth.IsChecked = false;
            CBFilterGender.SelectedIndex = 0;
            CBSortFilter.SelectedIndex = 0;
            TBSearch.Clear();
        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var resClick = MessageBox.Show($"Удалить пользователя {(AllInformation.SelectedItem as DB.Client).FirstName}", "Подтвержение", MessageBoxButton.YesNo, MessageBoxImage.Information);


            if (resClick == MessageBoxResult.Yes)
            {
                DB.Client client = new DB.Client();
                if (!(AllInformation.SelectedItem is DB.Client))
                {
                    MessageBox.Show("Запись не выбраны");
                    return;
                }
                client = AllInformation.SelectedItem as DB.Client;

                Classes.ClassHelper.context.Client.Remove(client);
                Classes.ClassHelper.context.SaveChanges();
            }

            Filter();
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AddClients addClients = new AddClients();
            addClients.ShowDialog();
            this.Close();
        }

        private void CmbPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
    }
}
