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
    /// Logika interakcji dla klasy ApplicationPage.xaml
    /// </summary>
    public partial class ApplicationPage : Page
    {
        MainWindow mw;
        ReturnBookWindow returnBook;
        int login;
        public int Login { get => login; set => login = value; }
        public MainWindow Mw { get => mw; set => mw = value; }
        public ReturnBookWindow ReturnBook { get => returnBook; set => returnBook = value; }
       

        public ApplicationPage(int _login)
        {
            InitializeComponent();
            Mw = (MainWindow)Application.Current.MainWindow;
            Login = _login;
            ReturnBook = null;
            User.Content = User.Content.ToString() + " " + Login;
        }

        public void GenerateUserPage()
        {
            BooksList.Items.Clear();
                
            using (var dbContext = new BibliotekaDBContext())
            {
                var books = dbContext.Ksiazki.Where(book => book.Wypozyczona == false);
                foreach (Ksiazki book in books)
                {
                    var Autor = dbContext.Autorzy.Where(autor => autor.IDAutora == book.Autor)
                                                  .Select(autor => autor.ImieAutora)
                                                  .First();
                    Autor = Autor.TrimEnd(' ');
                    Autor += ' ';

                    Autor += dbContext.Autorzy.Where(autor => autor.IDAutora == book.Autor)
                                                  .Select(autor => autor.NazwiskoAutora)
                                                  .First();
                    Autor = Autor.TrimEnd(' ');

                    BooksList.Items.Add(new ListBoxItem() {Tag = (int)book.IDKsiazki, Content = $"\"{book.Tytul.TrimEnd(' ')}\" - {Autor}", FontSize=12 });
                    
                }
            }

            
        }

        private void LogOutButtonClick(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.Content = new LogInPage();
            if (ReturnBook != null)
            {
                ReturnBook.Close();
                ReturnBook = null;
            }
        }

        private void RentBookButtonClick(object sender, RoutedEventArgs e)
        {
            if (BooksList.SelectedItem != null)
            {
                using (var dbContext = new BibliotekaDBContext())
                {
                    ListBoxItem item = (ListBoxItem)BooksList.SelectedItem;
                    int tag = (int)item.Tag;
                    dbContext.Ksiazki.Where(book => book.IDKsiazki == tag).First().Wypozyczona = true;

                    var wypozyczenie = new Wypozyczenia()
                    {
                        IDCzytelnika = Login,
                        IDKsiazki = tag,
                        DataWypozyczenia = DateTime.Today,
                        StatusWypozyczenia = "AKTYWNE"
                    };

                    dbContext.Wypozyczenia.Add(wypozyczenie);

                    dbContext.SaveChanges();
                }
                this.GenerateUserPage();
                if (ReturnBook != null) ReturnBook.GenerateReturnPage();
            }
        }

        private void ReturnBookButtonClick(object sender, RoutedEventArgs e)
        {
            if (ReturnBook == null)
            {
                ReturnBook = new ReturnBookWindow(Login);
                ReturnBook.GenerateReturnPage();
                ReturnBook.Show();
            }
        }
    }
}
