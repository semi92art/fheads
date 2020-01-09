////////////////////////////////////////////////////////////////////////////////
//  
// @module Stan's Assets Android Native Fitness
// @author Alexey Yaremenko (Stan's Assets) 
// @support support@stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Fitness {
	public static class Proxy {

		private const string CLASS_NAME = "com.stansassets.fitness.Bridge";

		private static void Call(string methodName, params object[] args) {
			AN_ProxyPool.CallStatic(CLASS_NAME, methodName, args);
		}

#if UNITY_ANDROID
		private static ReturnType Call<ReturnType>(string methodName, params object[] args) {
			return AN_ProxyPool.CallStatic<ReturnType>(CLASS_NAME, methodName, args);
		}
#endif

		public static void Connect(string connectionRequest) {
			Call ("connect", connectionRequest);
		}

		public static void Disconnect() {
			Call ("disconnect");
		}

		public static void RequestDataSources (string request) {
			Call ("requestDataSources", request);
		}

		public static void RegisterSensorListener (string request) {
			Call ("addSensorListener", request);
		}
	}
}
