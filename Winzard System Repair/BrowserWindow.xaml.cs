﻿using Microsoft.Win32;
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
                WebBrowser1.Navigate("http://alliancetechhub.com/submit.php?uid=");
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
