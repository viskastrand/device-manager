using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace Device_Manager {
    class DeviceListAdapter : BaseAdapter<Device> {
        private Context mContext;
        private int mLayout;
        private List<Device> mDevices;
        private TextView mDeviceName;
        private TextView mDeviceIp;
        private LinearLayout mLinDeviceLayout;
        private int mIndex;
        //private Action<LinearLayout> mActionFindMatches;

        public DeviceListAdapter(Context context, int layout, List<Device> device) {
            mContext = context;
            mLayout = layout;
            mDevices = device;
        }


        public override Device this[int position] {
            get { return mDevices[position]; }
        }

        public override int Count {
            get { return mDevices.Count; }
        }

        public override long GetItemId(int position) {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent) {
            View row = convertView;

            if (row == null)
                row = LayoutInflater.From(mContext).Inflate(mLayout, parent, false);

            mDeviceName = row.FindViewById<TextView>(Resource.Id.txtDeviceName);
            mLinDeviceLayout = row.FindViewById<LinearLayout>(Resource.Id.linDeviceLayout);
            mDeviceIp = row.FindViewById<TextView>(Resource.Id.txtIp);

            mDeviceName.Text = mDevices[position].Name;
            mDeviceIp.Text = mDevices[position].IP;
            mIndex = position;

            return row;
        }
    }
}