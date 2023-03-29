using UnityEngine;
using System.Collections;
public class LightSplash : MonoBehaviour {
	public Transform cameraTr;
	public float ampLevel;
	public float deltaT;
	public float deltaPhase;
	public float randomX = 2;
	public float sdvig = -20;
	public GameObject[] splash;
	public float currVal, randVal;
	public float[] cosVal;
	private int timer;
	public int period;
	void Update () {
		timer++;
		currVal += deltaT;
		for (int i = 0; i < splash.Length; i++) {
			randVal = Random.value;
			cosVal[i] = Mathf.Cos (currVal + deltaPhase + randVal);
			float posY = splash[i].transform.position.y;
			float posZ = splash[i].transform.position.z;
			if ((timer + Mathf.CeilToInt(randVal * 10))%period == 0){
				splash[i].transform.position = new Vector3(cameraTr.position.x + randomX * randVal + sdvig, posY, posZ);
			}


		}
	}
}
