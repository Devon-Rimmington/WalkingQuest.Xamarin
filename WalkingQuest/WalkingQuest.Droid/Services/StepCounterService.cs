using System;
using Android.App;
using Android.OS;
using Android.Hardware;
using System.ComponentModel;
using Android.Runtime;
using Android.Content;
using Android.Util;

namespace WalkingQuest.Droid {


    [Service(Enabled = true)]
    [IntentFilter(new String[] { "com.WalkingQuest.Services.StepService" })]
    public class StepCounterService : Service, ISensorEventListener, INotifyPropertyChanged
    {

        private Int64 numberOfSteps = 0;
        private bool isRunning;

        public Int64 NumberOfSteps
        {
            get { return numberOfSteps; }
            set
            {
                if (numberOfSteps == value)
                    return;

                numberOfSteps = value;
                OnPropertyChanged("NumberOfSteps");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override void OnCreate()
        {
            base.OnCreate();
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {

            RegisterListener(SensorType.StepDetector);
            isRunning = true;


            // get the number of steps from the SQLite Database

            // ... here ...

            return StartCommandResult.Sticky;

            // return base.OnStartCommand(intent, flags, startId);
        }

        public override void OnTaskRemoved(Intent rootIntent)
        {
            base.OnTaskRemoved(rootIntent);
            UnregisterListener();
            isRunning = false;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterListener();
            isRunning = false;
        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            // throw new NotImplementedException();
        }

        ServiceBinder binder;

        public override IBinder OnBind(Intent intent)
        {
            // throw new NotImplementedException();
            binder = new ServiceBinder(this);
            return binder;
        }

        public void OnSensorChanged(SensorEvent e)
        {
            // throw new NotImplementedException();
            Log.Debug("", "step counted " + numberOfSteps + 1);
            NumberOfSteps++;
        }

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }


        protected void RegisterListener(SensorType sensorType)
        {
            var sensorManager = (SensorManager)GetSystemService(Context.SensorService);
            var sensor = sensorManager.GetDefaultSensor(sensorType);

            //get faster why not, nearly fast already and when
            //sensor gets messed up it will be better
            sensorManager.RegisterListener(this, sensor, SensorDelay.Fastest);
            Console.WriteLine("Sensor listener registered of type: " + sensorType);
        }

        protected void UnregisterListener()
        {

            if (!isRunning)
                return;

            try
            {
                var sensorManager = (SensorManager)GetSystemService(Context.SensorService);
                sensorManager.UnregisterListener(this);
                isRunning = false;
            }
            catch (Exception e)
            {
                Android.Util.Log.Debug("STEPSERVICE", "Unable to unregister: " + e);
            }
        }

    }


}