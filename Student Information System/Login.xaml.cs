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

namespace Student_Information_System
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public Login()
        {
            InitializeComponent();
            db = new DataClasses1DataContext(Properties.Settings.Default.Student_InformationConnectionString);
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            var account = db.AccountDetails.SingleOrDefault
            (a => a.Username == tbx_username.Text && a.Password == tbx_password.Password);

            if (account != null )
            {
                MessageBox.Show("Login Successful");
                this.Hide();
                new StudentDatabase().Show();
            }

            else
            {
                MessageBox.Show("Error, enter correct credentials");
                tbx_password.Password = "";
                tbx_username.Text = "";
            }
        }
    }
}
