using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BibliotekaWPF
{
    /// <summary>
    /// Logika interakcji dla klasy LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        MainWindow Mw { get; set; }
        public LogInPage()
        {
            InitializeComponent();
            Mw = (MainWindow)Application.Current.MainWindow;
        }

        private void LogInButtonClick(object sender, RoutedEventArgs e)
        {
            using (var dbContext = new BibliotekaDBContext())
            {
                    int target;
                    bool success = Int32.TryParse(this.ReaderID.Text, out target);
                    var login = dbContext.Czytelnicy
                        .Where(reader => reader.IDCzytelnika == target);

                if (success)
                {
                    try
                    {
                        if (login.Count() == 1)
                        {
                            ApplicationPage app = new ApplicationPage(target);
                            Mw.MainFrame.Content = app;
                            app.GenerateUserPage();
                        }
                        else
                        {
                            throw new LogInException();
                        }
                    }
                    catch
                    {
                        LogInExceptionComunicate.Text = "Brak użytkownika w bazie.";
                    }
                }
                else
                {
                    LogInExceptionComunicate.Text = "Błędny login.";
                }
            }

        }
        private void ToSignUpPageClick(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.Content = new SignUpPage();
        }

        public class LogInException : Exception
        {
            public LogInException()
            {
            }

            public LogInException(string message)
                : base(message)
            {
            }

            public LogInException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
    }
}
