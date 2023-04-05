using UnityEngine;
//using System.Collections;

public class BallSlow : MonoBehaviour
{
    [SerializeField]
	private Scripts scr;

	public float drag;
	public CircleCollider2D bCol1;


	void OnTriggerStay2D(Collider2D other)
	{
		if (other == bCol1) 
		{
			if (scr.pMov.restart)
                scr.ballScr._rb.drag = drag;	
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other == bCol1) 
		{
			if (scr.pMov.restart)
                scr.ballScr._rb.drag = 0;	
		}
	}
}
