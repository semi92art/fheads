using UnityEngine;
using System.Collections;

public class GunForce : MonoBehaviour {
	public float force;
	public HingeJoint2D gunJoint;
	public float angle;
	public float depOfAngVal;

	void Update(){
		angle = -gunJoint.jointAngle;
		depOfAngVal = Mathf.Sin (Mathf.Deg2Rad * angle);
	}

	void OnTriggerStay2D (Collider2D other){
		other.attachedRigidbody.AddForce(new Vector2(force + depOfAngVal, 0));
	}

}
