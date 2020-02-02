using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace App4
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText editTextCharge, editTextInternal, editTextExternal;
        Button buttonChargeCl, buttonChargeNa, buttonChargeK, buttonChargeCa;
        TextView textEk, textTitle, textSubtitle;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            editTextCharge = FindViewById<EditText>(Resource.Id.editTextCharge);
            editTextInternal = FindViewById<EditText>(Resource.Id.editTextInternal);
            editTextExternal = FindViewById<EditText>(Resource.Id.editTextExternal);

            editTextCharge.TextChanged += (sender, e) => { UpdateEk(); };
            editTextInternal.TextChanged += (sender, e) => { UpdateEk(); };
            editTextExternal.TextChanged += (sender, e) => { UpdateEk(); };

            buttonChargeCl = FindViewById<Button>(Resource.Id.buttonChargeCl);
            buttonChargeNa = FindViewById<Button>(Resource.Id.buttonChargeNa);
            buttonChargeK = FindViewById<Button>(Resource.Id.buttonChargeK);
            buttonChargeCa = FindViewById<Button>(Resource.Id.buttonChargeCa);

            buttonChargeCl.Click += (sender, e) => { editTextCharge.Text = "-1"; };
            buttonChargeNa.Click += (sender, e) => { editTextCharge.Text = "1"; };
            buttonChargeK.Click += (sender, e) => { editTextCharge.Text = "1"; };
            buttonChargeCa.Click += (sender, e) => { editTextCharge.Text = "2"; };

            textEk = FindViewById<TextView>(Resource.Id.textEk);
            textTitle = FindViewById<TextView>(Resource.Id.textTitle);
            textSubtitle = FindViewById<TextView>(Resource.Id.textSubtitle);

            textTitle.Click += (sender, e) => { LaunchGithub(); };
            textSubtitle.Click += (sender, e) => { LaunchGithub(); };

            // startup conditions
            editTextCharge.Text = "-1";
            editTextInternal.Text = "11";
            editTextExternal.Text = "168";
        }

        private void LaunchGithub()
        {
            var uri = Android.Net.Uri.Parse("https://github.com/swharden/NernstApp");
            var i = new Android.Content.Intent(Android.Content.Intent.ActionView, uri);
            StartActivity(i);
        }

        private double CalculateEK(double concOut, double concIn, double charge, double TemperatureK = 300)
        {
            const double R = 8.314; // universal gas constant (Joules per Kelvin per mole)
            const double F = 96485; // Faraday's constant (Coulombs per mole)
            try
            {
                double Vek = (R * TemperatureK) / (charge * F) * Math.Log(concOut / concIn);
                return Vek;
            }
            catch
            {
                return double.NaN;
            }
        }

        private void UpdateEk()
        {
            try
            {
                double Vek = CalculateEK(
                    double.Parse(editTextExternal.Text),
                    double.Parse(editTextInternal.Text),
                    double.Parse(editTextCharge.Text)
                    );
                textEk.Text = string.Format("{0:0.00} mV", Vek * 1000);
            }
            catch
            {
                textEk.Text = "invalid input";
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

