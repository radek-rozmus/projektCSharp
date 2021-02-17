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
        MainWindow Mw { get; set; }
        private int login;
        private int Login { get => login; set => login = value; }
       

        public ApplicationPage(int _login)
        {
            InitializeComponent();
            Mw = (MainWindow)Application.Current.MainWindow;
            Login = _login;
        }

        public void GenerateUserPage()
        {
                User.Content = User.Content.ToString() + " " + Login;
                
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
        }

        private void RentBookButtonClick(object sender, RoutedEventArgs e)
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
            BooksList.Items.Remove(BooksList.SelectedItem);
        }
    }
}
