using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WallForceScript1 : MonoBehaviour {
	public Collider2D ballCollider, playerCollider;
	public Transform middleTr;
	public Rigidbody2D ballRb, playerRb;
	public int timer;

	void OnTriggerStay2D (Collider2D other){
		if (SceneManager.GetActiveScene().name != "LevelManyBalls"){
			if (other.Equals (ballCollider)) {
				timer++;
				if (ballRb.transform.position.x < middleTr.position.x) {
					if (timer > 1000){
						ballRb.position = new Vector3(transform.position.x + 2, ballRb.position.y, 0);
						timer = 0;
					}
				} else {
					if (timer > 1000){
						ballRb.position = new Vector3(transform.position.x - 2, ballRb.position.y, 0);
						timer = 0;
					}
				}
			}
		} else {
			Debug.Log("It Works!");
			if (transform.position.x < middleTr.position.x){
				other.attachedRigidbody.AddForce(Vector2.right * 0.1f);
			} else {
				other.attachedRigidbody.AddForce(Vector2.right * -0.1f);
			}
		}
	}

	void OnTriggerExit2D (Collider2D other){
		if (other.Equals(ballCollider)){
			timer = 0;
		}
	}
}
