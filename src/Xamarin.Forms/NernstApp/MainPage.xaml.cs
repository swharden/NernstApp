using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NernstApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            UpdateNernstPotential();

            // make clicking the title go to the website
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                Xamarin.Essentials.Browser.OpenAsync(
                    uri: "http://www.GitHub.com/SWHarden/NernstApp", 
                    Xamarin.Essentials.BrowserLaunchMode.SystemPreferred
                    );
            };
            labelTitle.GestureRecognizers.Add(tapGestureRecognizer);
            labelSubtitle.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private double CalculateEK(double concOut, double concIn, double charge, double temperatureKelvin = 300)
        {
            const double R = 8.314; // universal gas constant (Joules per Kelvin per mole)
            const double F = 96485; // Faraday's constant (Coulombs per mole)
            try
            {
                double Vek = (R * temperatureKelvin) / (charge * F) * Math.Log(concOut / concIn);
                return Vek;
            }
            catch
            {
                return double.NaN;
            }
        }

        private void UpdateNernstPotential()
        {
            // update temperature
            try
            {
                double tempK = double.Parse(entryTemperatureK.Text);
                double tempC = tempK - 273.15;
                double tempF = tempK * 9.0 / 5.0 - 459.67;
                labelTemperatureC.Text = Math.Round(tempC, 2).ToString();
                labelTemperatureF.Text = Math.Round(tempF, 2).ToString();
            }
            catch
            {
                labelTemperatureC.Text = "invalid input";
                labelTemperatureF.Text = "invalid input";
            }

            // update Nernst potential
            try
            {
                double Vek = CalculateEK(
                    concOut: double.Parse(entryExternal.Text),
                    concIn: double.Parse(entryInternal.Text),
                    charge: double.Parse(entryCharge.Text),
                    temperatureKelvin: double.Parse(entryTemperatureK.Text)
                    );
                labelNernstPotential.Text = string.Format("{0:0.00} mV", Vek * 1000);
            }
            catch
            {
                labelNernstPotential.Text = "invalid input";
            }
        }

        private void entryInternal_Completed(object sender, EventArgs e)
        {
            UpdateNernstPotential();
        }

        private void entryExternal_Completed(object sender, EventArgs e)
        {
            UpdateNernstPotential();
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            UpdateNernstPotential();
        }

        private void entryCharge_Completed(object sender, EventArgs e)
        {
            UpdateNernstPotential();
        }

        private void entryTemperatureK_Completed(object sender, EventArgs e)
        {
            UpdateNernstPotential();
        }

        private void buttonCl_Pressed(object sender, EventArgs e)
        {
            entryCharge.Text = "-1";
        }

        private void buttonNa_Pressed(object sender, EventArgs e)
        {
            entryCharge.Text = "1";
        }

        private void buttonK_Pressed(object sender, EventArgs e)
        {
            entryCharge.Text = "1";
        }

        private void buttonCa_Pressed(object sender, EventArgs e)
        {
            entryCharge.Text = "2";
        }
    }
}
