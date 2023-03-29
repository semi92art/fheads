using UnityEngine;
using System.Collections;

public class WallForceScript : MonoBehaviour {
	public Collider2D ballCollider;
	public Transform middleTr;
	public Rigidbody2D ballRb;

	void OnTriggerStay2D (Collider2D other){
		if (other.Equals (ballCollider) && Time.timeSinceLevelLoad > 8) {
			if (ballRb.transform.position.x < middleTr.position.x){
				ballRb.AddForce(Vector2.right);
			} else {
				ballRb.AddForce(-Vector2.right);
			}
		}
	}
}
