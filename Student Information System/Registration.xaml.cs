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
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {

        DataClasses1DataContext db = new DataClasses1DataContext();

        public Registration()
        {
            db = new DataClasses1DataContext(Properties.Settings.Default.Student_InformationConnectionString);
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void tbx_AddData_Click(object sender, RoutedEventArgs e)
        {
            //database connection here
            StudentDetail newStudent = db.StudentDetails

            
        }
    }
}
