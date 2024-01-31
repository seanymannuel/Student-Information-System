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
    public partial class StudentDatabase : Window
    {
        private List<StudentDetails> studentData;
        private DataClasses1DataContext db;

        public StudentDatabase()
        {
            InitializeComponent();
            InitializeDatabase();
            InitializeListBox();
        }

        private void InitializeDatabase()
        {
            try
            {
                // Use the connection string from the application settings
                string connectionString = Properties.Settings.Default.Student_InformationConnectionString1;
                db = new DataClasses1DataContext(connectionString);
            }
            catch (Exception ex)
            {
                // Handle any exceptions during database initialization
                MessageBox.Show($"Error initializing database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InitializeListBox()
        {
            // Retrieve all student data from the database
            studentData = db.StudentDetails.ToList();

            // Populate ListBox with Student IDs
            List<int> studentIDs = studentData.Select(s => s.StudentID).ToList();
            ListBox_Students.ItemsSource = studentIDs;
        }
        private void tbx_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = tbx_Search.Text.Trim();

            // If search text is empty, show all students
            if (string.IsNullOrEmpty(searchText))
            {
                ListBox_Students.ItemsSource = studentData.Select(s => s.StudentID).ToList();
            }
            else
            {
                // Filter students based on the search text
                List<int> filteredStudentIDs = studentData
                    .Where(s => s.StudentID.ToString().Contains(searchText))
                    .Select(s => s.StudentID)
                    .ToList();

                ListBox_Students.ItemsSource = filteredStudentIDs;
            }
        }

        private void ListBox_Students_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change event
            if (ListBox_Students.SelectedItem != null)
            {
                // Retrieve selected student ID
                int selectedStudentId = (int)ListBox_Students.SelectedItem;

                // Retrieve student details based on the selected ID
                StudentDetails studentDetails = studentData.FirstOrDefault(s => s.StudentID == selectedStudentId);

                // Display student details in textboxes
                DisplayStudentDetails(studentDetails);

                // Clear other textboxes if data is not available
                ClearUnusedTextboxes(studentDetails);
            }
        }


        private void ClearUnusedTextboxes(StudentDetails studentDetails)
        {
            // Clear family data textboxes if family details are not available
            if (studentDetails == null || db.ParentDetails.FirstOrDefault(f => f.StudentID == studentDetails.StudentID) == null)
            {
                tbx_dbMothersName.Clear();
                tbx_dbMotherOccupation.Clear();
                tbx_dbMotherContact.Clear();
                tbx_dbFathersName.Clear();
                tbx_dbFatherOccupation.Clear();
                tbx_dbFatherContact.Clear();
            }

            // Clear siblings data textboxes if sibling details are not available
            if (studentDetails == null || db.SiblingDetails.FirstOrDefault(s => s.StudentID == studentDetails.StudentID) == null)
            {
                tbx_dbSiblingLname.Clear();
                tbx_dbSiblingFname.Clear();
                tbx_dbSiblingMname.Clear();
                tbx_dbSiblingAge.Clear();
                rbtn_dbMale1.IsChecked = false;
                rbtn_dbFemale1.IsChecked = false;
            }

            // Clear emergency data textboxes if emergency details are not available
            if (studentDetails == null || db.EmergencyContacts.FirstOrDefault(e => e.StudentID == studentDetails.StudentID) == null)
            {
                tbx_dbGuardianName.Clear();
                tbx_dbGuardianAddress.Clear();
                tbx_dbRelationship.Clear();
                tbx_dbGuardianContact.Clear();
            }
        }

        private void ClearAllTextboxes()
        {
            tbx_dbFirstName.Clear();
            tbx_dbLastName.Clear();
            tbx_dbMiddleName.Clear();
            date_dbBirthday.SelectedDate = null;
            tbx_dbAge.Clear();
            tbx_dbAddress.Clear();
            tbx_dbContactNumber.Clear();
            rbtn_dbMale.IsChecked = false;
            rbtn_dbFemale.IsChecked = false;

            tbx_dbMothersName.Clear();
            tbx_dbMotherOccupation.Clear();
            tbx_dbMotherContact.Clear();
            tbx_dbFathersName.Clear();
            tbx_dbFatherOccupation.Clear();
            tbx_dbFatherContact.Clear();

            tbx_dbSiblingLname.Clear();
            tbx_dbSiblingFname.Clear();
            tbx_dbSiblingMname.Clear();
            tbx_dbSiblingAge.Clear();
            rbtn_dbMale1.IsChecked = false;
            rbtn_dbFemale1.IsChecked = false;

            tbx_dbGuardianName.Clear();
            tbx_dbGuardianAddress.Clear();
            tbx_dbRelationship.Clear();
            tbx_dbGuardianContact.Clear();
        }


        private void DisplayStudentDetails(StudentDetails studentDetails)
        {
            // Display personal data
            tbx_dbFirstName.Text = studentDetails.FirstName;
            tbx_dbLastName.Text = studentDetails.LastName;
            tbx_dbMiddleName.Text = studentDetails.MiddleName;
            date_dbBirthday.SelectedDate = studentDetails.Birthday;
            tbx_dbAge.Text = studentDetails.Age.ToString();
            tbx_dbAddress.Text = studentDetails.HomeAddress;
            tbx_dbContactNumber.Text = studentDetails.ContactNumber;

            // Display gender in radio buttons for student details
            if (studentDetails.Gender == "Male")
            {
                rbtn_dbMale.IsChecked = true;
                rbtn_dbFemale.IsChecked = false;
            }
            else
            {
                rbtn_dbMale.IsChecked = false;
                rbtn_dbFemale.IsChecked = true;
            }

            // Display family data
            ParentDetails familyDetails = db.ParentDetails.FirstOrDefault(f => f.StudentID == studentDetails.StudentID);
            if (familyDetails != null)
            {
                tbx_dbMothersName.Text = familyDetails.MotherName;
                tbx_dbMotherOccupation.Text = familyDetails.MotherOccupation;
                tbx_dbMotherContact.Text = familyDetails.MotherContactNumber;
                tbx_dbFathersName.Text = familyDetails.FatherName;
                tbx_dbFatherOccupation.Text = familyDetails.FatherOccupation;
                tbx_dbFatherContact.Text = familyDetails.FatherContactNumber;
            }

            SiblingDetails siblingDetails = db.SiblingDetails.FirstOrDefault(s => s.StudentID == studentDetails.StudentID);
            if (siblingDetails != null)
            {
                tbx_dbSiblingLname.Text = siblingDetails.LastName;
                tbx_dbSiblingFname.Text = siblingDetails.FirstName;
                tbx_dbSiblingMname.Text = siblingDetails.MiddleName;
                tbx_dbSiblingAge.Text = siblingDetails.Age.ToString();

                // Display gender in radio buttons for siblings
                if (siblingDetails.Gender == "Male")
                {
                    rbtn_dbMale1.IsChecked = true;
                    rbtn_dbFemale1.IsChecked = false;
                }
                else
                {
                    rbtn_dbMale1.IsChecked = false;
                    rbtn_dbFemale1.IsChecked = true;
                }
            }
            else
            {
                // If sibling details are not available, clear radio buttons
                rbtn_dbMale1.IsChecked = false;
                rbtn_dbFemale1.IsChecked = false;
            }

            // Display emergency data
            EmergencyContact emergencyDetails = db.EmergencyContacts.FirstOrDefault(e => e.StudentID == studentDetails.StudentID);
            if (emergencyDetails != null)
            {
                tbx_dbGuardianName.Text = emergencyDetails.GuardianName;
                tbx_dbGuardianAddress.Text = emergencyDetails.GuardianAddress;
                tbx_dbRelationship.Text = emergencyDetails.Relationship;
                tbx_dbGuardianContact.Text = emergencyDetails.EmergencyContactNumber;
            }
        }



        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            // Ensure a student is selected
            if (ListBox_Students.SelectedItem == null)
            {
                MessageBox.Show("Please select a student before updating.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Retrieve selected student ID
            int selectedStudentId = (int)ListBox_Students.SelectedItem;

            // Retrieve student details based on the selected ID
            StudentDetails studentDetails = db.StudentDetails.FirstOrDefault(s => s.StudentID == selectedStudentId);

            // Update personal details
            if (studentDetails != null)
            {
                studentDetails.FirstName = tbx_dbFirstName.Text;
                studentDetails.LastName = tbx_dbLastName.Text;
                studentDetails.MiddleName = tbx_dbMiddleName.Text;              
                studentDetails.Age = Convert.ToInt32(tbx_dbAge.Text);
                studentDetails.HomeAddress = tbx_dbAddress.Text;
                studentDetails.ContactNumber = tbx_dbContactNumber.Text;

                // Check if a date is selected
                if (date_dbBirthday.SelectedDate.HasValue)
                {
                    studentDetails.Birthday = date_dbBirthday.SelectedDate.Value;
                }
                else
                {
                    // Handle the case when no date is selected (you may choose to set a default or handle it differently)
                    studentDetails.Birthday = DateTime.MinValue;
                }


                // Update gender based on radio button selection
                studentDetails.Gender = rbtn_dbMale.IsChecked == true ? "Male" : "Female";
            }

            // Update family details
            ParentDetails familyDetails = db.ParentDetails.FirstOrDefault(f => f.StudentID == selectedStudentId);
            if (familyDetails != null)
            {
                familyDetails.MotherName = tbx_dbMothersName.Text;
                familyDetails.MotherOccupation = tbx_dbMotherOccupation.Text;
                familyDetails.MotherContactNumber = tbx_dbMotherContact.Text;
                familyDetails.FatherName = tbx_dbFathersName.Text;
                familyDetails.FatherOccupation = tbx_dbFatherOccupation.Text;
                familyDetails.FatherContactNumber = tbx_dbFatherContact.Text;
            }

            // Update siblings details
            SiblingDetails siblingDetails = db.SiblingDetails.FirstOrDefault(s => s.StudentID == selectedStudentId);
            if (siblingDetails != null)
            {
                siblingDetails.LastName = tbx_dbSiblingLname.Text;
                siblingDetails.FirstName = tbx_dbSiblingFname.Text;
                siblingDetails.MiddleName = tbx_dbSiblingMname.Text;
                siblingDetails.Age = Convert.ToInt32(tbx_dbSiblingAge.Text);

                // Update gender based on radio button selection
                siblingDetails.Gender = rbtn_dbMale1.IsChecked == true ? "Male" : "Female";
            }

            // Update emergency contact details
            EmergencyContact emergencyDetails = db.EmergencyContacts.FirstOrDefault(ee => ee.StudentID == selectedStudentId);
            if (emergencyDetails != null)
            {
                emergencyDetails.GuardianName = tbx_dbGuardianName.Text;
                emergencyDetails.GuardianAddress = tbx_dbGuardianAddress.Text;
                emergencyDetails.Relationship = tbx_dbRelationship.Text;
                emergencyDetails.EmergencyContactNumber = tbx_dbGuardianContact.Text;
            }

            // Save changes to the database
            try
            {
                db.SubmitChanges();
                MessageBox.Show("Student details updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearAllTextboxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating student details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearAllTextboxes();
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new Login().Show();
        }
    }
}
