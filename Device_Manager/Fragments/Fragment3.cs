using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.IO;
using System.Net;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Device_Manager {
    public class Fragment3 : SupportFragment {
        //Get InputLayouts from fragment3.axml
        TextInputLayout mTilBitcoin;
        TextInputLayout mTilSEK;
        //Store inputed numbers;
        double mSEK;
        double mBitcoin;
        double mConvValue;
        //Exchangerate variables
        double rate;

        public override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            View view = inflater.Inflate(Resource.Layout.Fragment3, container, false);

            //Reference Input fields and button
            mTilBitcoin = view.FindViewById<TextInputLayout>(Resource.Id.intInputLayoutBitcoin);
            mTilSEK = view.FindViewById<TextInputLayout>(Resource.Id.intInputLayoutSEK);
            Button btnConvert = view.FindViewById<Button>(Resource.Id.btnConvert);

            //Enter Default values in edittext fields
            mTilBitcoin.EditText.Text = "0";
            mTilSEK.EditText.Text = "0";

            //Creating WebClient and Url path;
            WebClient webclient = new WebClient();

            Stream stream = webclient.OpenRead("http://api.coindesk.com/v1/bpi/currentprice/sek.json");
            StreamReader reader = new StreamReader(stream);

            //Open Stream to bitcoin sek rate api and get json
            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(reader.ReadLine());
            stream.Close();
            //Extract exangerate data from json
            rate = (double)jObject["bpi"]["SEK"]["rate_float"];

            //Subscribing Button click event to function;
            btnConvert.Click += BtnConvert_Click;

            return view;
        }

        private void BtnConvert_Click(object sender, EventArgs e) {
            //Try to get inputed number values if one field has no value cath exeption and show toast with error!
            try {
                mSEK = Convert.ToDouble(mTilSEK.EditText.Text.ToString());
                mBitcoin = Convert.ToDouble(mTilBitcoin.EditText.Text.ToString());
            } catch (FormatException) {
                Context context = Application.Context;
                Toast.MakeText(context, "ERROR: both fields need values", ToastLength.Short).Show();
                return;
            }

            //Calculate convert values
            if (mBitcoin <= 0 && mSEK > 0) {
                mConvValue = mSEK / rate;
                mTilBitcoin.EditText.Text = mConvValue.ToString();
            } else {
                mConvValue = mBitcoin * rate;
                mTilSEK.EditText.Text = mConvValue.ToString();
            }
        }
    }
}