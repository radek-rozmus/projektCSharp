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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
