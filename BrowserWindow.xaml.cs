using Microsoft.Win32;
using ServiceStack.Stripe;
using ServiceStack.Stripe.Types;
using System;
using System.Windows;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for BrowserWindow.xaml
    /// </summary>
    public partial class BrowserWindow : Window
    {
        string customerID;
        public bool Productkeyentry = false;
        public BrowserWindow()
        {
            InitializeComponent();
            WebBrowserIE11Fix.SetIE11KeyforWebBrowserControl();
            Country.ItemsSource = CountryArrays.Abbreviations;
        }
        //Machine ID & Subcription
        public void SubmitPayment_Click(object sender, RoutedEventArgs e)
        {
            //Machin ID
            string gi = "{2B57C22C-2577-4BBA-A51D-BD0A00498AFB}\\";
            string mc = "1";
            try
            {
                string name1 = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\" + gi;

                RegistryKey key = Registry.LocalMachine.OpenSubKey(name1);
                if (key != null)
                {
                    mc = key.GetValue("uuid") as string;
                    key.Close();
                }
                if (mc == "1")
                {
                    string name2 = "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\" + gi;
                    key = Registry.LocalMachine.OpenSubKey(name2);
                    if (key != null)
                    {
                        mc = key.GetValue("uuid") as string;
                        key.Close();
                    }
                }
            }
            catch (Exception)
            {
            }
            //subscription
            try
            {
                var gateway = new StripeGateway("sk_live_DmUVQDTpe9tOV0b6kFvy9y6K00bNafAJyN");
                int MonthValue = Convert.ToInt32(Month.SelectedValue);
                int YearValue = Convert.ToInt32(Year.Text);
                var cardToken = gateway.Post(new CreateStripeToken
                {
                    Card = new StripeCard
                    {
                        Name = FirstName.Text + " " + LastName.Text,
                        Number = CreditCardNumber.Text,
                        Cvc = CVV.Text,
                        ExpMonth = MonthValue,
                        ExpYear = YearValue,
                        AddressLine1 = Address.Text,
                        AddressZip = Zip.Text,
                        AddressState = State.Text,
                        AddressCountry = (string)Country.SelectedValue,
                    },
                });
                var customer = gateway.Post(new CreateStripeCustomerWithToken
                {
                    Card = cardToken.Id,
                    Description = FirstName.Text + " " + LastName.Text,
                    Email = Email.Text,
                });
                var charge = gateway.Post(new ChargeStripeCustomer
                {
                    Amount = 999,
                    Customer = customer.Id,
                    Currency = "usd",
                });
                { customerID = customer.Id; };
            }
            catch(Exception g)
            {
                MessageBox.Show(g.Message + Environment.NewLine + g.InnerException +
                    Environment.NewLine + g.StackTrace + Environment.NewLine + 
                    g.Data + Environment.NewLine + g.HResult + Environment.NewLine + g.GetBaseException().Message);
            }
            try
            {
                var gateway = new StripeGateway("sk_live_DmUVQDTpe9tOV0b6kFvy9y6K00bNafAJyN");
                var subscription = gateway.Post(new SubscribeStripeCustomer
                {
                    CustomerId = customerID,
                    Plan = "price_1H2kCBJHdLsTZND2MIJsWEEk",
                    Quantity = 1,
                });


                WebBrowser1.Visibility = Visibility.Visible;
                WebBrowser1.Navigate("http://alliancetechhub.com/submit.php?uid=" + mc);
                Productkeyentry = true;
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message + Environment.NewLine + f.InnerException +
                    Environment.NewLine + f.StackTrace + Environment.NewLine +
                    f.Data + Environment.NewLine + f.HResult + Environment.NewLine + f.GetBaseException().Message);
            }
        }

        private void Country_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Console.WriteLine(Country.SelectedValue);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = Productkeyentry;
        }
    }
}
