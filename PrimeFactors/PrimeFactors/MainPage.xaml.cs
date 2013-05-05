using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace PrimeFactors
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Wird aufgerufen, wenn diese Seite in einem Rahmen angezeigt werden soll.
        /// </summary>
        /// <param name="e">Ereignisdaten, die beschreiben, wie diese Seite erreicht wurde. Die
        /// Parametereigenschaft wird normalerweise zum Konfigurieren der Seite verwendet.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void UpdateFactors(object sender, Windows.UI.Xaml.Controls.TextChangedEventArgs e)
        {
            long number;
            try
            {
                number = Int64.Parse(((TextBox)sender).GetValue(TextBox.TextProperty).ToString());
            }
            catch (FormatException)
            {
                return;
            }

            List<long> factors = AllPrimeFactors(number);

            string verboseFactors = "";
            for (int i = 0; i < factors.Count; i++)
            {
                verboseFactors += factors[i];
                if (i < factors.Count - 1)
                    verboseFactors += "*";
            }
            TextBlock verboseBlock = FindName("FactorsVerbose") as TextBlock;
            verboseBlock.SetValue(TextBlock.TextProperty, verboseFactors);

            string shortFactors = "";
            int pow = 1;
            for (int i = 0; i < factors.Count; i++)
            {
                long lastFactor = i > 0 ? factors[i - 1] : 0;
                long nextFactor = i + 1 < factors.Count ? factors[i + 1] : 0;
                long currentFactor = factors[i];

                if (lastFactor == currentFactor)
                {
                    pow++;
                    if (i == factors.Count-1)
                        shortFactors += "^" + pow;
                }
                else
                {
                    if (pow > 1)
                    {
                        shortFactors += "^" + pow;
                        pow = 1;
                    }
                    if (i > 0)
                        shortFactors += "*";
                    shortFactors += currentFactor;
                }
            }
            TextBlock shortBlock = FindName("FactorsShort") as TextBlock;
            shortBlock.SetValue(TextBlock.TextProperty, shortFactors);
        }
        
        private List<long> AllPrimeFactors(long number)
        {
            List<long> result = new List<long>();
            long candidate = 2;
            // TODO: Check if prime - necessary?
            while (number >= candidate)
            {
                //if (candidate % 2 == 0)
                //    continue; (doesn't work?)
                while (number % candidate == 0)
                {
                    result.Add(candidate);
                    number /= candidate;
                }
                candidate++;
            }
            return result;
        }
    }
}
