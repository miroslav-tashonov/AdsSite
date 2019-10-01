using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Setup
{

    /// <summary>
    /// Interaction logic for Credentials.xaml
    /// </summary>
    public partial class Credentials : Window
    {
        public string filepath = String.Empty;
        public string connectionString = String.Empty;
        public System.Windows.Controls.TextBox Username;
        public System.Windows.Controls.PasswordBox Password;

        public Credentials(string conString, string cloneRepoLocation)
        {
            connectionString = conString;
            filepath = cloneRepoLocation;

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Username = (System.Windows.Controls.TextBox)this.FindName("UsernameTextBox");
            Password = (System.Windows.Controls.PasswordBox)this.FindName("PasswordTextBox");

            if (ValidateUsernameAndPassword(Username.Text, Password.Password))
            {
                CreateAdminAccount(Username.Text, Password.Password);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            EndingWindow endingWindow = new EndingWindow(filepath);
            endingWindow.Show();
            this.Close();
        }

        private void CreateAdminAccount(string username, string password)
        {
            try
            {
                System.Windows.Controls.Button NextButton = (System.Windows.Controls.Button)this.FindName("NextButton");
                ImportSQL.ImportAdminCredentials(connectionString, username, password);
                MessageBox.Show("Success!");

                NextButton.IsEnabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ValidateUsernameAndPassword(string username, string password)
        {
            bool isValidUsername, isValidPassword = false;

            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            isValidUsername = regex.IsMatch(username);

            if (!isValidUsername)
                MessageBox.Show("Username email address is invalid.");

            if (HasUpperCase(password) && password.Length > 6)
                isValidPassword = true;
            else
                MessageBox.Show("Please use password with at least 7 characters and one upper letter.");

            return isValidPassword && isValidUsername;
        }

        bool HasUpperCase(string str)
        {
            return !string.IsNullOrEmpty(str) && str.Any(c => char.IsUpper(c));
        }



    }
}
