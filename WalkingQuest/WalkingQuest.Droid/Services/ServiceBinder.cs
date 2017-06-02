using Android.OS;

namespace WalkingQuest.Droid
{
    public class ServiceBinder : Binder
    {
        StepCounterService stepCounterService;

        public ServiceBinder(StepCounterService service)
        {
            this.stepCounterService = service;
        }

        public StepCounterService StepCounterService
        {
            get{ return this.stepCounterService; }
        }

    }

}