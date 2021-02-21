using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            User.Content = User.Content.ToString() + " " + Login;
        }

        public void GenerateReturnPage()
        {
            BooksList.Items.Clear();

            using (var dbContext = new BibliotekaDBContext())
            {
                var rents = dbContext.Wypozyczenia.Where(rent => (rent.IDCzytelnika == Login && rent.StatusWypozyczenia == "AKTYWNE"));
                foreach (Wypozyczenia rent in rents)
                {
                    var Book = dbContext.Ksiazki.Where(book => book.IDKsiazki == rent.IDKsiazki)
                                                  .Select(book => book)
                                                  .First();
                    var Autor = dbContext.Autorzy.Where(autor => autor.IDAutora == Book.Autor)
                                                  .Select(autor => autor.ImieAutora)
                                                  .First();
                    Autor = Autor.TrimEnd(' ');
                    Autor += ' ';

                    Autor += dbContext.Autorzy.Where(autor => autor.IDAutora == Book.Autor)
                                                  .Select(autor => autor.NazwiskoAutora)
                                                  .First();
                    Autor = Autor.TrimEnd(' ');

                    BooksList.Items.Add(new ListBoxItem() { Tag = (int)rent.IDWypozyczenia, Content = $"\"{Book.Tytul.TrimEnd(' ')}\" - {Autor}", FontSize = 12 });

                }
            }
        }

        private void ReturnBookDecisionButtonClick(object sender, RoutedEventArgs e)
        {
            if (BooksList.SelectedItem != null)
            {
                using (var dbContext = new BibliotekaDBContext())
                {
                    ListBoxItem item = (ListBoxItem)BooksList.SelectedItem;
                    int tag = (int)item.Tag;
                    Wypozyczenia Rent = dbContext.Wypozyczenia.Where(rent => rent.IDWypozyczenia == tag).First();
                    Rent.StatusWypozyczenia = "ODDANE";
                    Rent.DataOddania = DateTime.Now;
                    dbContext.Ksiazki.Where(book => book.IDKsiazki == Rent.IDKsiazki).First().Wypozyczona = false;
                    dbContext.SaveChanges();
                }
                this.GenerateReturnPage();
                ApplicationContext.GenerateUserPage();
            }
        }

        private void ReturnBookWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ApplicationContext.ReturnBook = null;
        }
    }
}
