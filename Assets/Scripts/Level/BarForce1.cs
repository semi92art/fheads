using UnityEngine;
using System.Collections;

public class BarForce1 : MonoBehaviour 
{
	public Scripts scr;
	public float force;
	public CircleCollider2D ballColl1;

	void OnTriggerStay2D (Collider2D other)
	{
		if (other == ballColl1) 
			scr.objLev.ballRb.AddForce (new Vector2 (force, 0));
	}
}
	

