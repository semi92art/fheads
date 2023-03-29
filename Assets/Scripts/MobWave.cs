using UnityEngine;
using System;

[Serializable]
public struct MobWave {
	
	public enum WaveType {
		Mobs,
		Boss,Kokoko
	}
	
	public WaveType Type;
	public GameObject Prefab;
	public int Count;
	
}
