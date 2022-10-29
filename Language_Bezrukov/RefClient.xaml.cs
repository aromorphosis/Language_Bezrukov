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

namespace Language_Bezrukov
{
    /// <summary>
    /// Логика взаимодействия для RefClient.xaml
    /// </summary>
    public partial class RefClient : Window
    {
        public RefClient()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AddClients addClients = new AddClients();
            addClients.ShowDialog();
            this.Close();
        }
    }
}
