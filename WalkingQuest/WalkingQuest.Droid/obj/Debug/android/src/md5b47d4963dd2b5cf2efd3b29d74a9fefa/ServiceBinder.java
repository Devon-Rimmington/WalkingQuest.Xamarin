package md5b47d4963dd2b5cf2efd3b29d74a9fefa;


public class ServiceBinder
	extends android.os.Binder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("WalkingQuest.Droid.ServiceBinder, WalkingQuest.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ServiceBinder.class, __md_methods);
	}


	public ServiceBinder () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ServiceBinder.class)
			mono.android.TypeManager.Activate ("WalkingQuest.Droid.ServiceBinder, WalkingQuest.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public ServiceBinder (md5b47d4963dd2b5cf2efd3b29d74a9fefa.StepCounterService p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == ServiceBinder.class)
			mono.android.TypeManager.Activate ("WalkingQuest.Droid.ServiceBinder, WalkingQuest.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "WalkingQuest.Droid.StepCounterService, WalkingQuest.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
