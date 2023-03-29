using UnityEngine;
using System.Collections;

public class MaxHeight : MonoBehaviour 
{
	public Scripts scr;

	public float downForce;
	public Collider2D enemyColl, playerColl;


	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll == playerColl)
		{
			if (scr.objLev.playerRb.velocity.y > 0)
				scr.objLev.playerRb.AddForce(new Vector2(0, downForce));
			
			//Debug.Log("Player in collider!");
		}
		else if (coll == enemyColl)
		{
			if (scr.objLev.enemy0Rb.velocity.y > 0)
				scr.objLev.enemy0Rb.AddForce(new Vector2(0, downForce));

			//Debug.Log("Enemy in collider!");
		}
	}
}
