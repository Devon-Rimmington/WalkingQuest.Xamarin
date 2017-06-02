using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.Widget;
using Android.OS;
using Org.Libsdl.App;
using Urho;
using Urho.Droid;
using System;
using Android.Content;
using Android.Util;

using WalkingQuest;

namespace WalkingQuest.Droid
{
    [Activity(Label = "WalkingQuest.Droid", MainLauncher = true,
        Icon = "@drawable/icon", Theme = "@android:style/Theme.NoTitleBar.Fullscreen",
        ConfigurationChanges = ConfigChanges.KeyboardHidden | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : Activity
    {

        public ServiceBinder binder;
        public bool IsBound { get; set; }

        private bool registered;
        private ServiceConnection serviceConnection;

        private Handler handler;
        

        public ServiceBinder Binder
        {
            get { return binder; }
            set
            {
                binder = value;
                if (binder != null)
                    return;

                HandlePropertyChanged(null, new System.ComponentModel.PropertyChangedEventArgs("NumberOfSteps"));

                if (registered)
                    binder.StepCounterService.PropertyChanged -= HandlePropertyChanged;

                binder.StepCounterService.PropertyChanged -= HandlePropertyChanged;
                registered = true;
            }

        }

        MyGame myGame;
        private bool firstRun = true;

        // todo remove this and use value from the database
        private Int64 numberOfSteps = 0;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            handler = new Handler();

            if (Binder == null)
            {
                numberOfSteps = 1;
            }
            else
            {
                numberOfSteps = Binder.StepCounterService.NumberOfSteps;
            }

            var layout = new AbsoluteLayout(this);
            var surface = UrhoSurface.CreateSurface(this);
            layout.AddView(surface);
            SetContentView(layout);
            myGame = await surface.Show<MyGame>(new ApplicationOptions("Data"));
            //to stop the game use await surface.Stop().


            handler.PostDelayed(() => UpdateStepCount(), 500);

            StartStepService();
        }

        protected override void OnStart()
        {
            base.OnStart();

            if (!firstRun)
            {
                StartStepService();
            }

            if (IsBound)
                return;

            var serviceIntent = new Intent(this, typeof(StepCounterService));
            serviceConnection = new ServiceConnection(this);
            BindService(serviceIntent, serviceConnection, Bind.AutoCreate);

        }

        private void StartStepService()
        {
            try
            {
                var service = new Intent(this, typeof(StepCounterService));
                var componentName = StartService(service);
            }
            catch (Exception e)
            {
            }
        }

        protected override void OnResume()
        {
            UrhoSurface.OnResume();
            base.OnResume();

            if (!firstRun)
            {

                if (handler == null)
                    handler = new Handler();
                handler.PostDelayed(() => UpdateStepCount(), 500);
            }

            firstRun = false;


            Log.Debug("", "" + registered + " " + (binder != null));

            if (!registered && binder != null)
            {
                binder.StepCounterService.PropertyChanged += HandlePropertyChanged;
                registered = true;
            }
        }




        protected override void OnPause()
        {
            UrhoSurface.OnPause();
            base.OnPause();
            if (registered && binder != null)
            {
                binder.StepCounterService.PropertyChanged -= HandlePropertyChanged;
                registered = false;
            }
        }

        public override void OnLowMemory()
        {
            UrhoSurface.OnLowMemory();
            base.OnLowMemory();
            if(IsBound) {
                UnbindService(serviceConnection);
                IsBound = false;
            }
        }

        protected override void OnDestroy()
        {
            UrhoSurface.OnDestroy();
            base.OnDestroy();
            if(IsBound) {
                UnbindService(serviceConnection);
                IsBound = false;
            }
        }

        public override bool DispatchKeyEvent(KeyEvent e)
        {
            if (e.KeyCode == Android.Views.Keycode.Back)
            {
                this.Finish();
                return false;
            }

            return base.DispatchKeyEvent(e);
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            UrhoSurface.OnWindowFocusChanged(hasFocus);
            base.OnWindowFocusChanged(hasFocus);
        }

        // Handler for when the feature NumberOfSteps (found in the StepCounterService class) has been modified
        // functions as a callback
        public void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "NumberOfSteps")
                return;

            // UpdateUI();
            // update the step count when the "NumberOfSteps" property in the service is changed (v.cool!!!)
            UpdateStepCount();

        }

        
        // Update the number of steps detected in the game
        protected void UpdateStepCount()
        {

            if (Binder == null)
            {
                numberOfSteps = 1;
            }
            else
            {
                // get the number of steps from the service
                numberOfSteps = Binder.StepCounterService.NumberOfSteps;
            }

            // numberOfSteps++;
            Log.Debug("Main", "" + numberOfSteps);

            // code from the mainactivity much be sent on the main thread using the below code!
            if (myGame.IsActive)
                Urho.Application.InvokeOnMain(() => myGame.UpdateStepCount(numberOfSteps));
        }

    }
}

