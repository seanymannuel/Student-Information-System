using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            tbx_SiblingAge.PreviewTextInput += NumericOnlyInput;
            tbx_FatherContact.PreviewTextInput += NumericOnlyInput;
            tbx_MotherContact.PreviewTextInput += NumericOnlyInput;
            tbx_GuardianContact.PreviewTextInput += NumericOnlyInput;

            tbx_SiblingLname.TextChanged += SiblingDetails_TextChanged;
            tbx_SiblingFname.TextChanged += SiblingDetails_TextChanged;
            tbx_SiblingMname.TextChanged += SiblingDetails_TextChanged;
            tbx_SiblingAge.TextChanged += SiblingDetails_TextChanged;
            rbtn_Female.Checked += RadioButton_Checked;
            rbtn_Male.Checked += RadioButton_Checked;
        }

        void NumericOnlyInput(object sender, TextCompositionEventArgs e)
        {
            // Use a regular expression to check if the entered text is numeric
            if (!IsNumeric(e.Text))
            {
                // If not numeric, set Handled to true to prevent the input
                e.Handled = true;
            }
        }

        bool IsNumeric(string value)
        {
            // Use a regular expression to check if the value contains only numeric characters
            return Regex.IsMatch(value, "^[0-9]+$");
        }
        private void Btn_Register_Click(object sender, RoutedEventArgs e)
        {

            // Check if any textbox is empty
            if (IsAllFieldsFilled())
            {
                ParentDetails pd = new ParentDetails();
                {
                    pd.StudentID = StudentIDContainer.StudentID;
                    pd.MotherName = tbx_MothersName.Text;
                    pd.MotherOccupation = tbx_MotherOccupation.Text;

                    // Check if MotherContactNumber contains only numeric characters and has exactly 11 digits
                    if (IsNumeric(tbx_MotherContact.Text) && IsContactNumberValid(tbx_MotherContact.Text))
                    {
                        pd.MotherContactNumber = tbx_MotherContact.Text;
                    }
                    else
                    {
                        MessageBox.Show("Mother's contact number should contain only numeric characters and be exactly 11 digits.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return; // Stop further processing
                    }

                    pd.FatherName = tbx_FathersName.Text;
                    pd.FatherOccupation = tbx_FatherOccupation.Text;

                    // Check if FatherContactNumber contains only numeric characters and has exactly 11 digits
                    if (IsNumeric(tbx_FatherContact.Text) && IsContactNumberValid(tbx_FatherContact.Text))
                    {
                        pd.FatherContactNumber = tbx_FatherContact.Text;
                    }
                    else
                    {
                        MessageBox.Show("Father's contact number should contain only numeric characters and be exactly 11 digits.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return; // Stop further processing
                    }
                }


                SiblingDetails sd = new SiblingDetails();
                {
                    if (int.TryParse(tbx_SiblingAge.Text, out int siblingAge))
                    {
                        sd.StudentID = StudentIDContainer.StudentID;
                        sd.FirstName = tbx_SiblingFname.Text;
                        sd.MiddleName = tbx_SiblingMname.Text;
                        sd.LastName = tbx_SiblingLname.Text;
                        sd.Age = siblingAge;
                        sd.Gender = rbtn_Male.IsChecked == true ? "Male" : "Female";
                    }

                }



                EmergencyContact ec = new EmergencyContact();
                {
                    ec.StudentID = StudentIDContainer.StudentID;
                    ec.GuardianName = tbx_GuardianName.Text;
                    ec.GuardianAddress = tbx_GuardianAddress.Text;
                    ec.Relationship = tbx_Relationship.Text;

                    // Check if EmergencyContactNumber contains only numeric characters and has exactly 11 digits
                    if (IsNumeric(tbx_GuardianContact.Text) && IsContactNumberValid(tbx_GuardianContact.Text))
                    {
                        ec.EmergencyContactNumber = tbx_GuardianContact.Text;
                    }
                    else
                    {
                        MessageBox.Show("Emergency contact number should contain only numeric characters and be exactly 11 digits.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return; // Stop further processing
                    }
                }


                MessageBoxResult result = MessageBox.Show("Do you want to continue?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // Check the user's choice
                if (result == MessageBoxResult.Yes)
                {

                    db.ParentDetails.InsertOnSubmit(pd);
                    db.SiblingDetails.InsertOnSubmit(sd);
                    db.EmergencyContacts.InsertOnSubmit(ec);
                    db.SubmitChanges();

                    MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Hide();
                    new Login().Show();
                }
                else
                {
                    return;
                }

            }
            else
            {
                // Show MessageBox alerting the user to fill in all textboxes
                MessageBox.Show("Please fill in all textboxes.", "Incomplete Information", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

         bool IsContactNumberValid(string contactNumber)
        {
            // Check if the contact number has exactly 11 digits
            return contactNumber.Length == 11;
        }

         void AdditionalDetails_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Prevent the user from closing the window using the close button
            e.Cancel = true;
        }

         bool IsAllFieldsFilled()
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


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // Handle radio button changes here
            // For example, you can use the sender parameter to determine which radio button is checked
            RadioButton radioButton = (RadioButton)sender;
            string gender = radioButton.Content.ToString();
            // You can use the 'gender' value as needed in your logic
        }

        private void SiblingDetails_TextChanged(object sender, TextChangedEventArgs e)
        {
            btn_addSiblings.IsEnabled = !AreSiblingDetailsTextboxesEmpty();
        }

        private bool AreSiblingDetailsTextboxesEmpty()
        {
            // Check if any textbox in the Sibling Details section is empty
            return string.IsNullOrEmpty(tbx_SiblingLname.Text) ||
                   string.IsNullOrEmpty(tbx_SiblingFname.Text) ||
                   string.IsNullOrEmpty(tbx_SiblingMname.Text) ||
                   string.IsNullOrEmpty(tbx_SiblingAge.Text) ||
                   (rbtn_Female.IsChecked != true && rbtn_Male.IsChecked != true);
        }

        private void btn_addSiblings_Click(object sender, RoutedEventArgs e)
        {
            // Check if sibling details are filled
            if (!AreSiblingDetailsTextboxesEmpty())
            {
                // Ask the user if they want to save the sibling details
                MessageBoxResult result = MessageBox.Show("Do you want to save the sibling details?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // Check the user's choice
                if (result == MessageBoxResult.Yes)
                {
                    // Create a new SiblingDetails object
                    SiblingDetails sd = new SiblingDetails();
                    sd.StudentID = StudentIDContainer.StudentID;
                    sd.FirstName = tbx_SiblingFname.Text;
                    sd.MiddleName = tbx_SiblingMname.Text;
                    sd.LastName = tbx_SiblingLname.Text;

                    // Check if SiblingAge contains a valid integer value
                    if (int.TryParse(tbx_SiblingAge.Text, out int siblingAge))
                    {
                        sd.Age = siblingAge;
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid age for the sibling.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return; // Stop further processing
                    }

                    // Set the gender based on the checked radio button
                    sd.Gender = rbtn_Male.IsChecked == true ? "Male" : "Female";

                    // Add the SiblingDetails object to the database
                    db.SiblingDetails.InsertOnSubmit(sd);
                    db.SubmitChanges();

                    // Clear the input fields in the Sibling Details section
                    tbx_SiblingFname.Clear();
                    tbx_SiblingMname.Clear();
                    tbx_SiblingLname.Clear();
                    tbx_SiblingAge.Clear();
                    rbtn_Female.IsChecked = false;
                    rbtn_Male.IsChecked = false;

                    // Optionally, you can provide a success message
                    MessageBox.Show("Sibling details added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                // If "No" was clicked, do nothing
            }
            else
            {
                // Show MessageBox alerting the user to fill in all sibling details
                MessageBox.Show("Please fill in all sibling details.", "Incomplete Information", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
