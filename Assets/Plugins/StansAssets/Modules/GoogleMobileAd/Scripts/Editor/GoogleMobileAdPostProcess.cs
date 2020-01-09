//#define CODE_DISABLED
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;

public class GoogleMobileAdPostProcess  {
	

	[PostProcessBuild(49)]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {


		#if UNITY_IPHONE && !CODE_DISABLED


		string StoreKit = "StoreKit.framework";
		if(!SA.IOSDeploy.ISD_Settings.Instance.ContainsFreamworkWithName(StoreKit)) {
			SA.IOSDeploy.Framework F =  new SA.IOSDeploy.Framework();
			F.Name = StoreKit;
			SA.IOSDeploy.ISD_Settings.Instance.Frameworks.Add(F);
		}

		string CoreTelephony = "CoreTelephony.framework";
		if(!SA.IOSDeploy.ISD_Settings.Instance.ContainsFreamworkWithName(CoreTelephony)) {
			SA.IOSDeploy.Framework F =  new SA.IOSDeploy.Framework();
			F.Name = CoreTelephony;
			SA.IOSDeploy.ISD_Settings.Instance.Frameworks.Add(F);
		}

		string AdSupport = "AdSupport.framework";
		if(!SA.IOSDeploy.ISD_Settings.Instance.ContainsFreamworkWithName(AdSupport)) {
			SA.IOSDeploy.Framework F =  new SA.IOSDeploy.Framework();
			F.Name = AdSupport;
			SA.IOSDeploy.ISD_Settings.Instance.Frameworks.Add(F);
		}


		string MessageUI = "MessageUI.framework";
		if(!SA.IOSDeploy.ISD_Settings.Instance.ContainsFreamworkWithName(AdSupport)) {
			SA.IOSDeploy.Framework F =  new SA.IOSDeploy.Framework();
			F.Name = MessageUI;
			SA.IOSDeploy.ISD_Settings.Instance.Frameworks.Add(F);
		}
	

		string EventKit = "EventKit.framework";
		if(!SA.IOSDeploy.ISD_Settings.Instance.ContainsFreamworkWithName(AdSupport)) {
			SA.IOSDeploy.Framework F =  new SA.IOSDeploy.Framework();
			F.Name = EventKit;
			SA.IOSDeploy.ISD_Settings.Instance.Frameworks.Add(F);
		}

		string EventKitUI = "EventKitUI.framework";
		if(!SA.IOSDeploy.ISD_Settings.Instance.ContainsFreamworkWithName(EventKitUI)) {
			SA.IOSDeploy.Framework F =  new SA.IOSDeploy.Framework();
			F.Name = EventKitUI;
			SA.IOSDeploy.ISD_Settings.Instance.Frameworks.Add(F);
		}


		string linkerFlag = "-ObjC";
		SA.IOSDeploy.ISD_Settings.Instance.AddLinkerFlag(linkerFlag);

		#endif
	}

}
#endif
