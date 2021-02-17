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
    /// Logika interakcji dla klasy SingUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        MainWindow Mw { get; set; }
        string GenderBoxes { get; set; }
        public SignUpPage()
        {
            InitializeComponent();
            Mw = (MainWindow)Application.Current.MainWindow;
        }

        private void SignUpButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateSignUpForm(
                    this.NameBox.Text,
                    this.SurnameBox.Text,
                    this.GenderBoxes,
                    this.CityBox.Text,
                    this.PostalCodeBox.Text,
                    this.StreetBox.Text,
                    this.HouseNumberBox.Text,
                    this.LocalNumberBox.Text,
                    this.EmailBox.Text
                    );

                using (var dbContext = new BibliotekaDBContext())
                {
                    var czytelnik = new Czytelnicy()
                    {
                        ImieCzytelnika = this.NameBox.Text,
                        NazwiskoCzytelnika = this.SurnameBox.Text,
                        Plec = this.GenderBoxes,
                        DataPrzystapienia = DateTime.Now,
                        Miasto = this.CityBox.Text,
                        KodPocztowy = this.PostalCodeBox.Text,
                        Ulica = this.StreetBox.Text,
                        NumerDomu = this.HouseNumberBox.Text,
                        NumerLokalu = this.LocalNumberBox.Text,
                        EMail = this.EmailBox.Text
                    };

                    dbContext.Czytelnicy.Add(czytelnik);
                    dbContext.SaveChanges();
                }
            }
            catch(SignUpException exception)
            {
                ComunicateBanner.Text =exception.Message;
            }

        }

        private void ValidateSignUpForm(string _name, string _surname, string _gender, string _city, string _postalcode, string _street, string _housenumber, string _localnumber, string _email)
        {
            SignUpException e = null;
            int spacesCounter = 0;

            if (_name.Length == 0 || _name == null)
            {
                e = new SignUpException("imię");
            }
            else
            {
                foreach (char c in _name)
                {
                    if (c == ' ') spacesCounter++;
                }
                if (spacesCounter == _name.Length)
                {
                    e = new SignUpException("imię");
                }
            }

            spacesCounter = 0;

            if (_surname.Length == 0 || _surname == null)
            {
                e = new SignUpException("nazwisko");
            }
            else
            {
                foreach (char c in _surname)
                {
                    if (c == ' ') spacesCounter++;
                }
                if (spacesCounter == _surname.Length)
                {
                    e = new SignUpException("nazwisko");
                }
            }

            spacesCounter = 0;

            if (!(_gender == "M" || _gender == "K"))
            {
                e = new SignUpException("płeć");
            }

            if (_city.Length == 0 || _city == null)
            {
                e = new SignUpException("miasto");
            }
            else
            {
                foreach (char c in _city)
                {
                    if (c == ' ') spacesCounter++;
                }
                if (spacesCounter == _city.Length)
                {
                    e = new SignUpException("miasto");
                }
            }

            spacesCounter = 0;

            if (_postalcode.Length != 6 || _postalcode == null)
            {
                e = new SignUpException("kod pocztowy");
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    if (i == 2)
                    {
                        if (_postalcode[i] != '-') e = new SignUpException("kod pocztowy");
                    }
                    else
                    {
                        if (!Char.IsDigit(_postalcode[i]))
                        {
                            e = new SignUpException("kod pocztowy");
                        }
                    }
                }
            }

            if (_street.Length == 0 || _street == null)
            {
                e = new SignUpException("ulica");
            }
            else
            {
                foreach (char c in _street)
                {
                    if (c == ' ') spacesCounter++;
                }
                if (spacesCounter == _street.Length)
                {
                    e = new SignUpException("ulica");
                }
            }

            spacesCounter = 0;

            if (_housenumber.Length == 0 || _housenumber == null || _housenumber.Length > 5)
            {
                e = new SignUpException("numer domu");
            }
            else
            {
                foreach (char c in _housenumber)
                {
                    if (!(Char.IsLetterOrDigit(c)))
                    {
                        e = new SignUpException("numer domu");
                        break;
                    }
                }
            }

            if (_localnumber.Length > 3)
            {
                e = new SignUpException("numer lokalu");
            }

            if(!(_email.Contains('@')))
            {
                e = new SignUpException("email");
            }
            else if (!(_email.Contains('.')))
            {
                e = new SignUpException("email");
            }
            else if ((_email.LastIndexOf('@')) > (_email.LastIndexOf('.')))
            {
                e = new SignUpException("email");
            }
            else if ((_email.IndexOf('@')) != (_email.LastIndexOf('@')))
            {
                e = new SignUpException("email");
            }
            else if (_email.IndexOf('@') == 0)
            {
                e = new SignUpException("email");
            }
            else if (_email.LastIndexOf('.') == _email.Length-1)
            {
                e = new SignUpException("email");
            }
            else if ((_email.LastIndexOf('.')) - (_email.IndexOf('@')) == 1)
            {
                e = new SignUpException("email");
            }

            if (e != null)
            {
                throw e;
            }
        }

        private void FemaleChecked(object sender, RoutedEventArgs e)
        {
            this.male.IsChecked = false;
            GenderBoxes = "K";
        }

        private void GenderUnchecked(object sender, RoutedEventArgs e)
        {
            GenderBoxes = null;
        }

        private void MaleChecked(object sender, RoutedEventArgs e)
        {
            this.female.IsChecked = false;
            GenderBoxes = "M";
        }

        private void ToLogInPageClick(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.Content = new LogInPage();
        }

        public class SignUpException : Exception
        {
            public SignUpException()
            {
            }

            public SignUpException(string message)
                : base(message)
            {
            }

            public SignUpException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
    }
}
