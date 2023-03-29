using UnityEngine;
using System.Collections;

public class Wall4Enemy2Manager : MonoBehaviour {
	public Collider2D wall4Enemy2Collider;
	public Transform wall4Enemy2Tr;
	public Transform enemy2;

	void Awake(){
		wall4Enemy2Collider.enabled = false;
	}

	void Update(){
		if (enemy2.position.x < wall4Enemy2Tr.position.x - 0.5f) {
			wall4Enemy2Collider.enabled = true;
		}
	}
}
