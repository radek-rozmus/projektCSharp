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

namespace BibliotekaWPF
{
    /// <summary>
    /// Logika interakcji dla klasy ReturnBookWindow.xaml
    /// </summary>
    public partial class ReturnBookWindow : Window
    {
        MainWindow mw;
        ApplicationPage applicationContext;
        private int login;
        public int Login { get => login; set => login = value; }
        public MainWindow Mw { get => mw; set => mw = value; }
        public ApplicationPage ApplicationContext { get => applicationContext; set => applicationContext = value; }


        public ReturnBookWindow(int _login)
        {
            InitializeComponent();
            Mw = (MainWindow)Application.Current.MainWindow;
            Login = _login;
            ApplicationContext = (ApplicationPage)Mw.MainFrame.Content;
        }

        public void GenerateReturnPage()
        {
            User.Content = User.Content.ToString() + " " + Login;
        }

        private void ReturnBookDecisionButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnBookWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ApplicationContext.ReturnBook = null;
        }
    }
}
