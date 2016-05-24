using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;


namespace Device_Manager {
    [Activity(Label = "Device Manager", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.DeviceManager")]
    public class MainActivity : AppCompatActivity {
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Reference Tabs Menu
            TabLayout tabs = FindViewById<TabLayout>(Resource.Id.tabs);

            SupportToolbar toolBar = FindViewById<SupportToolbar>(Resource.Id.toolBar);
            SetSupportActionBar(toolBar);

            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);

            SetUpViewPager(viewPager);

            tabs.SetupWithViewPager(viewPager);
        }

        private void SetUpViewPager(ViewPager viewPager) {
            TabAdapter adapter = new TabAdapter(SupportFragmentManager);
            adapter.AddFragment(new Fragment1(), "Device List");
            adapter.AddFragment(new Fragment2(), "Create Device");
            adapter.AddFragment(new Fragment3(), "Bitcoin Converter");

            viewPager.Adapter = adapter;
        }
    }
}

