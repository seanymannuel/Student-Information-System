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
    /// Interaction logic for AdditionalDetails.xaml
    /// </summary>
    public partial class AdditionalDetails : Window
    {
        public AdditionalDetails()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox newTextBox = new TextBox();

            tbx_LastName.Children.Add(newTextBox);
        }
    }
}
