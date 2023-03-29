using UnityEngine;
using System.Collections;

public class RedBlockPush : MonoBehaviour {
	public float force;
	public Rigidbody2D blockRb;
	public float pushTime;

	void Update(){
		if (Time.timeSinceLevelLoad > pushTime) {
			blockRb.AddForce(Vector2.right * force);
		} 
	}
}
