using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Device_Manager {
    public class Fragment1 : SupportFragment {
        //Get InputLayouts from fragment2.axml
        TextInputLayout nameWrapper;
        TextInputLayout ipWrapper1;
        TextInputLayout ipWrapper2;
        TextInputLayout ipWrapper3;
        TextInputLayout ipWrapper4;

        public override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            View view = inflater.Inflate(Resource.Layout.Fragment2, container, false);

            nameWrapper = view.FindViewById<TextInputLayout>(Resource.Id.txtInputLayoutDeviceName);
            ipWrapper1 = view.FindViewById<TextInputLayout>(Resource.Id.txtInputLayoutDeviceIp);
            ipWrapper2 = view.FindViewById<TextInputLayout>(Resource.Id.txtInputLayoutDeviceIp2);
            ipWrapper3 = view.FindViewById<TextInputLayout>(Resource.Id.txtInputLayoutDeviceIp3);
            ipWrapper4 = view.FindViewById<TextInputLayout>(Resource.Id.txtInputLayoutDeviceIp4);

            Button btnCreate = view.FindViewById<Button>(Resource.Id.btnCreate);

            btnCreate.Click += BtnCreate_Click;
            return view;
        }

        private void BtnCreate_Click(object sender, EventArgs e) {
            string txtDeviceName = nameWrapper.EditText.Text;
            string txtIp1 = ipWrapper1.EditText.Text.ToString();
            string txtIp2 = ipWrapper2.EditText.Text.ToString();
            string txtIp3 = ipWrapper3.EditText.Text.ToString();
            string txtIp4 = ipWrapper4.EditText.Text.ToString();

            if (txtDeviceName == "") {
                nameWrapper.Error = "You must enter a device name";
                return;
            }

            if (txtIp1 == "")
                txtIp1 = "0";
            if (txtIp2 == "")
                txtIp2 = "0";
            if (txtIp3 == "")
                txtIp3 = "0";
            if (txtIp4 == "")
                txtIp4 = "0";

            string fullIp = txtIp1 + "." + txtIp2 + "." + txtIp3 + "." + txtIp4;

            //Creating web client;
            WebClient client = new WebClient();
            Uri url = new Uri("http://android-sql-dunderboy.c9users.io/createDevice.php");
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("name", txtDeviceName);
            parameters.Add("ip", fullIp);

            client.UploadValuesCompleted += Client_UploadValuesCompleted;
            client.UploadValuesAsync(url, parameters);
        }

        private void Client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e) {
            Context context = Application.Context;

            string status = Encoding.UTF8.GetString(e.Result);

            if (status == "completed") {
                nameWrapper.EditText.Text = "";
                ipWrapper1.EditText.Text = "";
                ipWrapper2.EditText.Text = "";
                ipWrapper3.EditText.Text = "";
                ipWrapper4.EditText.Text = "";
                Toast.MakeText(context, "Data Inserted Successfully", ToastLength.Short).Show();
            } else {
                Toast.MakeText(context, "ERROR: contact developer", ToastLength.Short).Show();
            }
        }
    }
}