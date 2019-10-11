using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Setup
{
    /// <summary>
    /// Interaction logic for CountrySetup.xaml
    /// </summary>
    public partial class CountrySetup : Window
    {
        public System.Windows.Controls.TextBox CountryName;
        public System.Windows.Controls.TextBox CountryPath;
        public System.Windows.Controls.TextBox CountryAbbreviation;
        public System.Windows.Controls.ComboBox Languages;

        public string _connectionString;
        public string _cloneRepoLocation;
        public List<Guid> CountryIds;

        public CountrySetup(string conString, string cloneRepoLocation)
        {
            CountryIds = new List<Guid>();
            _connectionString = conString;
            _cloneRepoLocation = cloneRepoLocation;

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CountryIds.Count > 0 )
            {
                Credentials credentialsWindow = new Credentials(_connectionString, _cloneRepoLocation, CountryIds);
                credentialsWindow.Show();
                this.Close();
            }
        }

        private bool ValidateForm(string countryName, string countryPath, string countryAbbreviation)
        {
            bool isCountryNameValid = true;
            bool isCountryPathValid = true;
            bool isCountryAbbreviationValid = true;

            if (!(Regex.IsMatch(countryName, @"^[a-zA-Z ]+$") && countryName.Length > 2))
            {
                MessageBox.Show("Please use country name with at least 2 characters using only characters and space.");
                isCountryNameValid = false;
            }
            if (!(Regex.IsMatch(countryPath, @"^[a-zA-Z]+$") && countryPath.Length > 0))
            {
                MessageBox.Show("Please use country path with at least 1 character using only charachters.");
                isCountryPathValid = false;
            }
            if (!(Regex.IsMatch(countryAbbreviation, @"^[a-zA-Z]+$") && countryAbbreviation.Length > 0))
            {
                MessageBox.Show("Please use country abbreviation with at least 1 character using only charachters.");
                isCountryAbbreviationValid = false;
            }

            return isCountryNameValid && isCountryPathValid && isCountryAbbreviationValid;
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            CountryName = (System.Windows.Controls.TextBox)this.FindName("CountryNameTextBox");
            CountryPath = (System.Windows.Controls.TextBox)this.FindName("CountryPathTextBox");
            CountryAbbreviation = (System.Windows.Controls.TextBox)this.FindName("CountryAbbreviationTextBox");
            Languages = (System.Windows.Controls.ComboBox)this.FindName("LanguageComboBox");
            NextButton = (System.Windows.Controls.Button)this.FindName("NextButton");

            if (ValidateForm(CountryName.Text, CountryPath.Text, CountryAbbreviation.Text))
            {
                var selectedTag = ((ComboBoxItem)Languages.SelectedItem).Tag.ToString();
                var selectedName = ((ComboBoxItem)Languages.SelectedItem).Content.ToString();
                CultureInfo defaultCultureInfo = new CultureInfo(selectedTag);

                Guid countryId = ImportSQL.ImportCountryAndDefaultLanguage(_connectionString,
                        new CountryLanguageModel
                        {
                            CountryAbbreviation = CountryAbbreviation.Text,
                            CountryName = CountryName.Text,
                            CountryPath = CountryPath.Text,
                            LanguageName = selectedName,
                            LanguageShortName = selectedName,
                            LCID = defaultCultureInfo.LCID
                        }
                    );

                if (countryId != null && countryId != Guid.Empty)
                {
                    NextButton.IsEnabled = true;
                    CountryIds.Add(countryId);

                    CountryNameTextBox.Clear();
                    CountryPathTextBox.Clear();
                    CountryAbbreviationTextBox.Clear();

                    MessageBox.Show("Success!");
                }
                else
                {
                    MessageBox.Show("Country import was not succesfull.");
                }

            }
        }
    }
}
