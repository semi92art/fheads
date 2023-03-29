using UnityEngine;
using System.Collections;

public class JumpBot : MonoBehaviour{
	public Rigidbody2D RbBot;
	public Collider2D ballColl;
	public float jumpForce;
		
	void OnTriggerEnter2D (Collider2D other){
		if (other.Equals(ballColl)){
			RbBot.AddForce(Vector2.up * jumpForce);
		}
	}
}
	

