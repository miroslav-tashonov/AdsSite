using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;

namespace Setup
{
    /// <summary>
    /// Interaction logic for EndingWindow.xaml
    /// </summary>
    public partial class EndingWindow : Window
    {
        public string repositoryPath;

        public EndingWindow(string cloneRepositoryPath)
        {
            repositoryPath = cloneRepositoryPath;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox setupCheckbox = (System.Windows.Controls.CheckBox)this.FindName("SetupCheckbox");
            if ((bool)setupCheckbox.IsChecked)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var processInfo = new ProcessStartInfo("cmd.exe", "/c" + "\"" + repositoryPath + "\\build.bat" + "\"");

                    processInfo.CreateNoWindow = true;
                    processInfo.UseShellExecute = false;
                    processInfo.RedirectStandardError = true;
                    processInfo.RedirectStandardOutput = true;

                    var process = Process.Start(processInfo);

                    process.Start();
                }

            }

            this.Close();
        }
    }
}
