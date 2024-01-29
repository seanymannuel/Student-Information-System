using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Student_Information_System
{
    public partial class Registration : Window
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        public Registration()
        {
            InitializeComponent();
            db = new DataClasses1DataContext(Properties.Settings.Default.Student_InformationConnectionString);

            // Hook up the event handler for the "Proceed" button
            btn_Proceed.Click += Btn_Proceed_Click;

            // Hook up event handlers for text input validation
            tbx_ContactNumber.PreviewTextInput += TextBox_PreviewTextInput;
            tbx_Age.PreviewTextInput += TextBox_PreviewTextInput;
        }

        private void Btn_Proceed_Click(object sender, RoutedEventArgs e)
        {
            // Check if all textboxes are filled
            if (IsAllFieldsFilled())
            {
                // If all fields are filled, proceed with the registration logic
                string lastName = tbx_LastName.Text;
                string firstName = tbx_FirstName.Text;
                string middleName = tbx_MiddleName.Text;
                string age = tbx_Age.Text;
                string address = tbx_Address.Text;
                string contactNumber = tbx_ContactNumber.Text;
                DateTime birthday = date_Birthday.SelectedDate ?? DateTime.Now;

                // Get selected gender
                string gender = (rbtn_Male.IsChecked == true) ? "Male" : "Female";

                // Save details to the database
                SaveStudentToDatabase(lastName, firstName, middleName, int.Parse(age), gender, birthday, address, int.Parse(contactNumber));

                // Display a success message
                MessageBox.Show($"Registration successful! Information saved to the database.", "Registration Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Open the AdditionalDetails window
                this.Hide();
                AdditionalDetails additionalDetailsWindow = new AdditionalDetails();
                additionalDetailsWindow.Show();
            }
            else
            {
                // If any of the fields is empty or invalid, show an error message
                MessageBox.Show("Please fill in all the required fields with valid data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        

        private void SaveStudentToDatabase(string lastName, string firstName, string middleName, int age, string gender, DateTime birthday, string address, int contactNumber)
        {
            try
            {
                // Create a new Student object
                StudentDetail newStudent = new StudentDetail
                {
                    LastName = lastName,
                    FirstName = firstName,
                    MiddleName = middleName,
                    Age = age,
                    Gender = gender,
                    Birthday = birthday,
                    HomeAddress = address,
                    ContactNumber = contactNumber
                };

                // Add the new student to the Students table
                db.StudentDetails.InsertOnSubmit(newStudent);

                // Submit changes to the database
                db.SubmitChanges();

                // Display a success message
                MessageBox.Show($"Registration successful! Information saved to the database.", "Registration Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving student information: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Validate that the input is numeric
            if (!IsNumeric(e.Text))
            {
                e.Handled = true; // Ignore the input
            }
        }

        private bool IsNumeric(string input)
        {
            return Regex.IsMatch(input, @"^\d+$"); // Check if the input consists of digits only
        }

        private bool IsAllFieldsFilled()
        {
            // Check if all textboxes, radio buttons, and date picker have non-empty or selected values
            return !string.IsNullOrEmpty(tbx_LastName.Text) &&
                   !string.IsNullOrEmpty(tbx_FirstName.Text) &&
                   !string.IsNullOrEmpty(tbx_MiddleName.Text) &&
                   !string.IsNullOrEmpty(tbx_Age.Text) &&
                   !string.IsNullOrEmpty(tbx_Address.Text) &&
                   !string.IsNullOrEmpty(tbx_ContactNumber.Text) &&
                   (rbtn_Male.IsChecked == true || rbtn_Female.IsChecked == true) &&
                   date_Birthday.SelectedDate.HasValue &&
                   IsContactNumberValid(tbx_ContactNumber.Text);
        }

        private bool IsContactNumberValid(string contactNumber)
        {
            // Check if the contact number has exactly 11 digits
            if (contactNumber.Length != 11 || !IsNumeric(contactNumber))
            {
                MessageBox.Show("Contact Number should be exactly 11 digits.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
