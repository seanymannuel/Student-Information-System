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
    public partial class AdditionalDetails : Window
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        public AdditionalDetails()
        {
            InitializeComponent();
            db = new DataClasses1DataContext(Properties.Settings.Default.Student_InformationConnectionString1);

            // Attach the event handler to the Register button
            btn_Register.Click += Btn_Register_Click;

            // Disable closing the window via the close button
            this.Closing += AdditionalDetails_Closing;
        }

        private void Btn_Register_Click(object sender, RoutedEventArgs e)
        {
            // Check if any textbox is empty
            if (IsAllFieldsFilled())
            {
                // Your logic to handle the registration
                MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // You can close the window or navigate to the next step here
                this.Close();
            }
            else
            {
                // Show MessageBox alerting the user to fill in all textboxes
                MessageBox.Show("Please fill in all textboxes.", "Incomplete Information", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AdditionalDetails_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Prevent the user from closing the window using the close button
            e.Cancel = true;
        }

        private bool IsAllFieldsFilled()
        {
            // Check if all textboxes and radio buttons have non-empty or selected values
            return !string.IsNullOrEmpty(tbx_MothersName.Text) &&
                   !string.IsNullOrEmpty(tbx_MotherOccupation.Text) &&
                   !string.IsNullOrEmpty(tbx_MotherContact.Text) &&
                   !string.IsNullOrEmpty(tbx_FathersName.Text) &&
                   !string.IsNullOrEmpty(tbx_FatherOccupation.Text) &&
                   !string.IsNullOrEmpty(tbx_FatherContact.Text) &&
                   !string.IsNullOrEmpty(tbx_SiblingLname.Text) &&
                   !string.IsNullOrEmpty(tbx_SiblingFname.Text) &&
                   !string.IsNullOrEmpty(tbx_SiblingMname.Text) &&
                   !string.IsNullOrEmpty(tbx_SiblingAge.Text) &&
                   (rbtn_Female.IsChecked == true || rbtn_Male.IsChecked == true) &&
                   !string.IsNullOrEmpty(tbx_GuardianName.Text) &&
                   !string.IsNullOrEmpty(tbx_GuardianAddress.Text) &&
                   !string.IsNullOrEmpty(tbx_Relationship.Text) &&
                   !string.IsNullOrEmpty(tbx_GuardianContact.Text);
        }
    }
}
