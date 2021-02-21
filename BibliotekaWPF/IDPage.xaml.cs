using System.Windows;
using System.Windows.Controls;

namespace BibliotekaWPF
{
    /// <summary>
    /// Logika interakcji dla klasy IDPage.xaml
    /// </summary>
    public partial class IDPage : Page
    {
        MainWindow Mw { get; set; }
        public IDPage(int userID)
        {
            InitializeComponent();
            Mw = (MainWindow)Application.Current.MainWindow;
            IDField.Content += " ";
            IDField.Content += userID.ToString();
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.Content = new LogInPage();
        }
    }
}
