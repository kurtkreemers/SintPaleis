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
using ClassLibrary2;
using System.Data.Odbc;
using System.Windows.Threading;

namespace Sintpaleis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        List<Voorstelling> voorstellingen = new List<Voorstelling>();
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.IsEnabled = true;
            Voorstellingen();
            this.Height = SystemParameters.PrimaryScreenHeight;
            this.Width = SystemParameters.PrimaryScreenWidth;
            header.Width = this.Width - 50;
            dateTime.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();

        }
        private void Voorstellingen()
        {
            try
            {
                var manager = new Manager();

                voorstellingen = manager.GetVoorstelling();

                foreach (Voorstelling vrstelling in voorstellingen)
                {

                    Separator sepD = new Separator();
                    Label labelD = new Label();
                    labelD.Name = "Lb" + vrstelling.Nummer.ToString();
                    labelD.Content = (vrstelling.Dag).ToLongDateString();
                    labelD.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    labelD.Height = 40;
                    labelD.FontSize = 20;

                    Separator sepU = new Separator();
                    Label labelU = new Label();
                    labelU.Content = (vrstelling.Uur).ToShortTimeString() + " uur";
                    labelU.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    labelU.FontSize = 20;
                    labelU.Height = 40;

                    Separator sepTb = new Separator();
                    TextBox tb = new TextBox();
                    tb.Name = "tb" + vrstelling.Nummer.ToString();
                    tb.IsReadOnly = true;
                    tb.Width = 75;
                    tb.Height = 40;
                    tb.FontSize = 28;
                    tb.TextAlignment = TextAlignment.Center;
                    tb.Text = vrstelling.Max.ToString();
                    tb.Background = new SolidColorBrush(Colors.LightGreen);

                    if (stackRechts.Children.OfType<TextBox>().Count() <= 14)
                    {
                        stackLinks.Children.Add(labelD);
                        stackLinks.Children.Add(sepD);
                        stackMidden.Children.Add(labelU);
                        stackMidden.Children.Add(sepU);
                        stackRechts.Children.Add(tb);
                        stackRechts.Children.Add(sepTb);
                    }
                    else
                    {
                        stackLinks2.Children.Add(labelD);
                        stackLinks2.Children.Add(sepD);
                        stackMidden2.Children.Add(labelU);
                        stackMidden2.Children.Add(sepU);
                        stackRechts2.Children.Add(tb);
                        stackRechts2.Children.Add(sepTb);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        int timerClock = 30;
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (timerClock == 30)
                {
                    Trigger.Text = timerClock.ToString();
                    var manager = new Manager();
                    List<Klant> klanten = new List<Klant>();
                    klanten = manager.GetKlanten();
                    var klantenPerVoorst = from klant in klanten
                                           group klant by klant.VoorstelNummer into newKlant
                                           orderby newKlant.Key
                                           select newKlant;

                    int totaalAantalPerVoorst;
                    int aantalPerKlant;

                    foreach (var vrstel in klantenPerVoorst)
                    {
                        totaalAantalPerVoorst = 0;
                        aantalPerKlant = 0;
                        foreach (var aantal in vrstel)
                        {
                            aantalPerKlant = aantal.AantalVolw + aantal.AantalKind;
                            totaalAantalPerVoorst += aantalPerKlant;
                        }
                        vulTextBlock((int)vrstel.Key, totaalAantalPerVoorst);
                    }
                    timerClock -= 1;
                }
                else if (timerClock != 0)
                {
                    timerClock -= 1;
                    Trigger.Text = timerClock.ToString();
                }
                else
                {
                    timerClock = 30;
                    dateTime.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void vulTextBlock(int vrstelnr, int totaal)
        {
            TextBox tb1 = null;
            TextBox tb2 = null;
            TextBox vulTextBox = null;
            tb1 = stackRechts.Children.OfType<TextBox>().Where(vrst => vrst.Name == "tb" + vrstelnr.ToString()).FirstOrDefault();
            tb2 = stackRechts2.Children.OfType<TextBox>().Where(vrst => vrst.Name == "tb" + vrstelnr.ToString()).FirstOrDefault();

            if (tb1 != null)
                vulTextBox = tb1;
            if (tb2 != null)
                vulTextBox = tb2;

            Voorstelling voorstelMax = (from vrstel in voorstellingen
                                        where vrstel.Nummer == vrstelnr
                                        select vrstel).FirstOrDefault(); ;

            int ActueelWaarde = voorstelMax.Max - totaal;
            if (ActueelWaarde < 50 && ActueelWaarde > 25)
            {
                vulTextBox.Background = new SolidColorBrush(Colors.Yellow);
                vulTextBox.Text = (ActueelWaarde).ToString();
            }
            else if (ActueelWaarde <= 25 && ActueelWaarde > 0)
            {
                vulTextBox.Background = new SolidColorBrush(Colors.Orange);
                vulTextBox.Text = (ActueelWaarde).ToString();
            }
            else if (ActueelWaarde <= 0)
            {
                vulTextBox.Background = new SolidColorBrush(Colors.Red);
                vulTextBox.Text = "VOL";
            }
        }
    }
}
