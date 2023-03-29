using UnityEngine;
//using System.Collections;
using UnityEngine.Rendering;

public class BallScript : MonoBehaviour 
{
	public Scripts scr;

	//public float angVel;
	public float maxAngVel;
	public float maxVel;
	public float rotSpeed;
	public Collider2D groundColl1;
	public SpriteRenderer ballRend;

	private Transform ballTr;
	private Rigidbody2D ballRb;
	[HideInInspector]
	public float startGravity;

	public GameObject ballTail;
	private int tailTimer;
	public int tailPeriod;
	private int ballNCurr;

	private LineRenderer ballLineR;
	private bool restartCheck;
	private Vector3[] newTailVect = new Vector3[10];

	void Awake()
	{
		ballLineR = GetComponent<LineRenderer> ();
		ballTr = scr.objLev.ballTr;
		ballRb = scr.objLev.ballRb;

		startGravity = ballRb.gravityScale;

		for (int i = 0; i < newTailVect.Length; i++)
		{
			newTailVect[i] = ballTr.position;
			ballLineR.SetPosition(i, ballTr.position);
		}
	}
		
	private float sign, vel;

	private void Update()
	{
		sign = Mathf.Sign(ballRb.velocity.x);
		vel = Mathf.Abs(ballRb.velocity.x);

		if(vel > 5.0f)
		{
			if (Mathf.Abs(-ballRb.angularVelocity) < maxAngVel)
			{
				if (vel < maxVel) 
					ballRb.gravityScale = startGravity + sign * Mathf.Pow(vel, 2) * -ballRb.angularVelocity * 1e-8f * rotSpeed;
				else 
					ballRb.gravityScale = startGravity + sign * Mathf.Pow(maxVel, 2) * -ballRb.angularVelocity * 1e-8f * rotSpeed;	
			} 
			else 
			{
				if (vel < maxVel) 
					ballRb.gravityScale = startGravity + sign * Mathf.Pow(vel, 2) * maxAngVel * 1e-8f * rotSpeed;
				else 
					ballRb.gravityScale = startGravity + sign * Mathf.Pow(maxVel, 2) * maxAngVel * 1e-8f * rotSpeed;
			}
		} 
		else 
			ballRb.gravityScale = startGravity;

		if (ballRb.gravityScale < 0.3f)
			ballRb.gravityScale = 0.3f;
		else if (ballRb.gravityScale > 1.4f)
			ballRb.gravityScale = 1.4f;

		ballLineR.SetPosition(0, ballTr.position);
	}

	void FixedUpdate()
	{
		if (scr.pMov.startGame)
		{
			tailTimer ++;

			if (tailTimer % tailPeriod == 0)
			{
				for (int i = newTailVect.Length - 1; i > 0; i--)
					newTailVect[i] = newTailVect[i-1];

				newTailVect[0] = ballTr.position;

				for (int i = 1; i < newTailVect.Length; i++)
					ballLineR.SetPosition(i, newTailVect[i]);	
			}

			if (scr.pMov.restart)
				ballLineR.SetColors (Color.clear, Color.clear); 
			else
			{
				if (restartCheck)
					ballLineR.SetColors(new Vector4(1, 1, 1, 1), new Vector4(1, 1, 1, 0));
			}
		}

		restartCheck = scr.pMov.restart;
	}
}
