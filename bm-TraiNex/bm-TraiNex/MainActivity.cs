using System.Net;
using Android.App;
using Android.OS;
using Android.Webkit;
using Android.Widget;

namespace bm_TraiNex
{
    [Activity(Label = "bm_TraiNex", MainLauncher = true, Icon = "@drawable/Icon")]
    public class MainActivity : Activity
    {
        #region Declare variables
        // Reference types
        WebView trainex;
        #endregion

        #region Main function "OnCreate()"
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Algorithm
            SetWebviewSettings();

            LoadUrl();
        }
        #endregion

        #region System functions
        public override void OnBackPressed()
        {
            KillProcess();
        }
        #endregion

        #region Custom functions
        public bool CheckInternetConnection()
        {
            // Declare variables
            string CheckUrl = "http://www.trainex24.de/bm-trainex/pda/";
            HttpWebRequest iNetRequest;
            WebResponse iNetResponse;

            try
            {
                iNetRequest = (HttpWebRequest)WebRequest.Create(CheckUrl);
                iNetRequest.Timeout = 5000;
                iNetResponse = iNetRequest.GetResponse();

                iNetResponse.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void KillProcess()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }

        void SetWebviewSettings()
        {
            trainex = FindViewById<WebView>(Resource.Id.Trainex);
            trainex.SetWebViewClient(new WebViewClient());
            trainex.Settings.JavaScriptEnabled = true;
        }

        void LoadUrl()
        {
            if (CheckInternetConnection())
                trainex.LoadUrl("http://www.trainex24.de/bm-trainex/pda/kalender.cfm");
            else if (!CheckInternetConnection())
            {
                Toast.MakeText(this, "INTERNET_CONNECTION_ERROR", ToastLength.Short).Show();

                KillProcess();
            }
        }
        #endregion
    }
}