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
            // Check if any textbox is empty
            if (string.IsNullOrEmpty(tbx_MothersName.Text) ||
                string.IsNullOrEmpty(tbx_MotherOccupation.Text) ||
                string.IsNullOrEmpty(tbx_MotherContact.Text) ||
                string.IsNullOrEmpty(tbx_FathersName.Text) ||
                string.IsNullOrEmpty(tbx_FatherOccupation.Text) ||
                string.IsNullOrEmpty(tbx_FatherContact.Text) ||
                string.IsNullOrEmpty(tbx_SiblingLname.Text) ||
                string.IsNullOrEmpty(tbx_SiblingFname.Text) ||
                string.IsNullOrEmpty(tbx_SiblingMname.Text) ||
                string.IsNullOrEmpty(tbx_SiblingAge.Text) ||
                (!rbtn_Female.IsChecked.HasValue || !rbtn_Male.IsChecked.HasValue) ||
                string.IsNullOrEmpty(tbx_GuardianName.Text) ||
                string.IsNullOrEmpty(tbx_GuardianAddress.Text) ||
                string.IsNullOrEmpty(tbx_Relationship.Text) ||
                string.IsNullOrEmpty(tbx_GuardianContact.Text))
            {
                // Show MessageBox alerting the user to fill in all textboxes
                MessageBox.Show("Please fill in all textboxes.", "Incomplete Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // All textboxes have input, proceed with your logic here
            // For example, you can save the data to a database or perform other operations.
        }

    }
}
