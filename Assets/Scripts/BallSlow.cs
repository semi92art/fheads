using UnityEngine;
//using System.Collections;

public class BallSlow : MonoBehaviour
{
	public Scripts scr;

	public float drag;
	public CircleCollider2D bCol1;

	void OnTriggerStay2D(Collider2D other)
	{
		if (other == bCol1) 
		{
			if (scr.pMov.restart) 
				scr.objLev.ballRb.drag = drag;		
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other == bCol1) 
		{
			if (scr.pMov.restart) 
				scr.objLev.ballRb.drag = 0;	
		}
	}
}
