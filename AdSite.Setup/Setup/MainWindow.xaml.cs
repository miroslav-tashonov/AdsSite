//using LibGit2Sharp;
using LibGit2Sharp;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Forms;

namespace Setup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool testConnectionSuccess, cloneRepoSuccess = false;
        public string SOURCE_REPO_URL = "https://github.com/miroslav-tashonov/AdsSite/";
        private FolderBrowserDialog folderBrowserDialog1;

        public MainWindow()
        {
            InitializeComponent();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog1.SelectedPath = path;
            System.Windows.Controls.TextBox CloneRepoTextBox = (System.Windows.Controls.TextBox)this.FindName("CloneRepoTextBox");
            CloneRepoTextBox.Text = path;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult result = this.folderBrowserDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                System.Windows.Controls.TextBox CloneRepoTextBox = (System.Windows.Controls.TextBox)this.FindName("CloneRepoTextBox");
                CloneRepoTextBox.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        public void Clone(string sourceUrl, string workingDir)
        {
            var cloneOptions = new CloneOptions();
            cloneOptions.IsBare = false;
            cloneOptions.Checkout = true;
            cloneOptions.CertificateCheck += delegate (Certificate certificate, bool valid, string host)
            {
                return true;
            };

            try
            {
                Repository.Clone(sourceUrl, workingDir, cloneOptions);
                System.Windows.MessageBox.Show("Clone success ! ");
                cloneRepoSuccess = true;
                ValidateNextButton();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Clone fail ! " + ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button nextButtion = (System.Windows.Controls.Button)this.FindName("NextButton");
            System.Windows.Controls.TextBox CloneRepoTextBox = (System.Windows.Controls.TextBox)this.FindName("CloneRepoTextBox");
            Clone(SOURCE_REPO_URL, CloneRepoTextBox.Text);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Server=(localdb)\\MSSQLLocalDB;Database=AdSite;Trusted_Connection=True;MultipleActiveResultSets=true
            System.Windows.Controls.TextBox SqlConnectionTextBox = (System.Windows.Controls.TextBox)this.FindName("SqlConnectionTextBox");
            string connetionString = null;
            SqlConnection cnn;
            connetionString = SqlConnectionTextBox.Text;
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                System.Windows.MessageBox.Show("Test Connection success ! ");
                cnn.Close();

                testConnectionSuccess = true;
                ValidateNextButton();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Test Connection fail ! ");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox SqlConnectionTextBox = (System.Windows.Controls.TextBox)this.FindName("SqlConnectionTextBox");
            System.Windows.Controls.TextBox CloneRepoTextBox = (System.Windows.Controls.TextBox)this.FindName("CloneRepoTextBox");

            if (ImportSQL.ImportSQLScripts(SqlConnectionTextBox.Text, CloneRepoTextBox.Text))
            {
                CountrySetup countrySetupWindow = new CountrySetup(SqlConnectionTextBox.Text, CloneRepoTextBox.Text);
                countrySetupWindow.Show();
                this.Close();
            }
        }

        private void SqlConnectionTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            testConnectionSuccess = false;
            ValidateNextButton();
        }

        public void ValidateNextButton()
        {
            System.Windows.Controls.Button nextButtion = (System.Windows.Controls.Button)this.FindName("NextButton");
            if (cloneRepoSuccess && testConnectionSuccess)
            {
                nextButtion.IsEnabled = true;
            }
            else
            {
                nextButtion.IsEnabled = false;
            }
        }
    }
}
