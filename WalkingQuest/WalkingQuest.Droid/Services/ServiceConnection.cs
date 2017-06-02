


using System;
using Android.Content;
using Android.OS;
using System.ComponentModel;

namespace WalkingQuest.Droid {

    public class ServiceConnection : Java.Lang.Object, IServiceConnection
    {

        MainActivity activity;

        public ServiceConnection(MainActivity activity)
        {
            this.activity = activity;
        }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            //throw new NotImplementedException();
            var serviceBinder = service as ServiceBinder;
            if (serviceBinder != null)
            {
                this.activity.Binder = serviceBinder;
                this.activity.IsBound = true;

                this.activity.Binder.StepCounterService.PropertyChanged += this.activity.HandlePropertyChanged;

            }

        }

        public void OnServiceDisconnected(ComponentName name)
        {
            // throw new NotImplementedException();
            this.activity.IsBound = false;
        }

    }


}