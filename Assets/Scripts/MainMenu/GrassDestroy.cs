using UnityEngine;
using System.Collections;

public class GrassDestroy : MonoBehaviour {
	public Transform camTr;
	
	void Update(){
		float camX = camTr.position.x;
		float posY = transform.position.y;
		float posZ = transform.position.z;
		transform.position = new Vector3 (camX - 175, posY, posZ);
	}

	void OnTriggerEnter(Collider other){
		Destroy (other.gameObject);
	}
}
