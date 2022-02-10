using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static System.Net.Mime.MediaTypeNames;

namespace File_Picker_AppEx
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button pickBtn;
        private ImageView mypickerview;
        private TextView mytextPicker;
        private PickOptions options;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            pickBtn = FindViewById<Button>(Resource.Id.pickFile);
            mypickerview = FindViewById<ImageView>(Resource.Id.imageViewP);
            mytextPicker = FindViewById<TextView>(Resource.Id.textViewP);

            pickBtn.Click += ButtonPicker_Click;
        }

        private async void ButtonPicker_Click(object sender, EventArgs e)
        {
            var res = await FilePicker.PickAsync(options);
            if (res != null)
            {
                mytextPicker.Text = $"File Name: {res.FileName}";
                if (res.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) || (res.FileName.EndsWith("png",StringComparison.OrdinalIgnoreCase)));
                {
                    var stream = await res.OpenReadAsync();
                    mypickerview.SetImageBitmap(BitmapFactory.DecodeStream(stream));
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}