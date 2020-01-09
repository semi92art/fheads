////////////////////////////////////////////////////////////////////////////////
//  
// @module Stan's Assets Android Native Fitness
// @author Alexey Yaremenko (Stan's Assets) 
// @support support@stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Fitness {
	public class DataPoint {

		private DataType dataType;
		private Dictionary<string, object> fields = new Dictionary<string, object>();

		public DataPoint (DataType type, string[] bundle) {
			dataType = type;

			for (int i = 1; i < bundle.Length; i++) {
				if (!bundle[i].Equals(string.Empty)) {
					string[] array = bundle [i].Split (new string[] {Connection.SEPARATOR1}, StringSplitOptions.None);
					fields.Add (array [0], array [1]);
				}
			}
		}

		public DataType DataType {
			get {
				return dataType;
			}
		}

		public Dictionary<string, object> Fields {
			get {
				return fields;
			}
		}
	}
}
