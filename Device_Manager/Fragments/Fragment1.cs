using System;
using System.Collections.Generic;
using System.Text;
using Android.OS;
using Android.Views;
using Android.Widget;
using SupportFragment = Android.Support.V4.App.Fragment;
using System.Net;
using Newtonsoft.Json;

namespace Device_Manager {
    public class Fragment1 : SupportFragment {
        private ListView mContent;
        private List<Device> mDeviceList;
        private BaseAdapter<Device> mDeviceAdapter;

        public override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            View view = inflater.Inflate(Resource.Layout.Fragment1, container, false);
            mContent = view.FindViewById<ListView>(Resource.Id.deviceList);

            WebClient client = new WebClient();
            Uri url = new Uri("http://android-sql-dunderboy.c9users.io/getDevice.php");

            client.DownloadDataAsync(url);
            client.DownloadDataCompleted += Client_DownloadDataCompleted;

            return view;
        }

        private void Client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e) {
            string json = Encoding.UTF8.GetString(e.Result);
            mDeviceList = JsonConvert.DeserializeObject<List<Device>>(json);
            mDeviceAdapter = new DeviceListAdapter(Activity, Resource.Layout.device_row, mDeviceList);
            mContent.Adapter = mDeviceAdapter;
        }
    }
}