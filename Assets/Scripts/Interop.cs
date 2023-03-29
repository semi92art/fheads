#if UNITY_EDITOR

#else
using UnityEngine;
using System.Collections;
using System;

public static class Interop 
{
	public static event EventHandler AdDuplexEnabled;
	public static event EventHandler AdDuplexDisabled;
	
	public static void DisableAdDuplex() 
	{        
		if (AdDuplexDisabled != null) 
		{
			AdDuplexDisabled(null, null);
		}
	}    
	
	public static void EnableAdDuplex() 
	{
		if (AdDuplexEnabled != null) 
		{
			AdDuplexEnabled(null, null);
		}
	}
}
#endif
