using System;
using System.Linq;
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
            db = new DataClasses1DataContext(Properties.Settings.Default.Student_InformationConnectionString1);

            // Hook up the event handler for the "Proceed" button
            btn_Proceed.Click += Btn_Proceed_Click;
            tbx_Age.PreviewTextInput += NumericOnlyInput;
            tbx_ContactNumber.PreviewTextInput += NumericOnlyInput;
        }

        void Btn_Proceed_Click(object sender, RoutedEventArgs e)
{
    // Check if all textboxes are filled
    if (IsAllFieldsFilled())
    {
        // If all fields are filled, proceed with the registration logic
        string lastName = tbx_LastName.Text;
        string firstName = tbx_FirstName.Text;
        string middleName = tbx_MiddleName.Text;
        string ageText = tbx_Age.Text;
        string address = tbx_Address.Text;
        string contactNumberText = tbx_ContactNumber.Text;
        DateTime birthday = date_Birthday.SelectedDate ?? DateTime.Now;

        // Get selected gender
        string gender = (rbtn_Male.IsChecked == true) ? "Male" : "Female";

        // Try to parse age and contact number, handle parsing errors
        if (int.TryParse(ageText, out int age) && IsNumeric(contactNumberText))
        {
            // Save details to the database
            SaveStudentToDatabase(lastName, firstName, middleName, age, gender, birthday, address, contactNumberText);

        }
        else
        {
            // If parsing fails, show an error message
            MessageBox.Show("Invalid Age or Contact Number. Please enter valid numeric values.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    else
    {
        // If any of the fields is empty or invalid, show an error message
        MessageBox.Show("Please fill in all the required fields with valid data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}

void SaveStudentToDatabase(string lastName, string firstName, string middleName, int age, string gender, DateTime birthday, string address, string contactNumber)
{
    try
    {
        // Check if the student with the same first name, last name, and middle name already exists
        var existingStudent = db.StudentDetails
            .Where(s => s.FirstName == firstName && s.LastName == lastName && s.MiddleName == middleName)
            .FirstOrDefault();

        if (existingStudent == null)
        {
            // Create a new Student object
            StudentDetails newStudent = new StudentDetails
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

            // Submit changes to the database only if the student is new
            db.SubmitChanges();

            MessageBox.Show($"Registration successful! Information saved to the database.", "Registration Success", MessageBoxButton.OK, MessageBoxImage.Information);
            MessageBox.Show($"You can now proceed to additional details", "Registration Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Open the AdditionalDetails window
            this.Hide();
            AdditionalDetails additionalDetailsWindow = new AdditionalDetails();
            additionalDetailsWindow.Show();
        }
        else
        {
            // Display a message indicating that the student already exists
            MessageBox.Show("Student information already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error saving student information: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
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


        bool IsAllFieldsFilled()
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

         bool IsContactNumberValid(string contactNumber)
        {
            // Check if the contact number has exactly 11 digits
            if (contactNumber.Length != 11 )
            {
                MessageBox.Show("Contact Number should be exactly 11 digits.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
