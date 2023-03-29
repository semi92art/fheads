using UnityEngine;
using System.Collections;

public class BallTouchScript : MonoBehaviour {
	public Scripts scr;

	[Range (0, 20)]
	public float minVelDiff;
	public float Vx, Vy;
	private float VxPrev, VyPrev;
	private float signVx, signVy;
	private float signVxPrev, signVyPrev;
	private float absVxDiff, absVyDiff;
	public float velMagnitude;
	private Rigidbody2D ballRb;
	public bool isCollision;
	private int timer;
	

	void Awake()
	{
		ballRb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (scr.pMov.startGame)
		{
			timer++;

			Vx = ballRb.velocity.x;
			Vy = ballRb.velocity.y;

			signVx = Mathf.Sign(Vx);
			signVy = Mathf.Sign(Vy);

			if (timer > 1)
			{
				absVxDiff = Mathf.Abs(Vx - VxPrev);
				absVyDiff = Mathf.Abs(Vy - VyPrev);

				velMagnitude = Mathf.Sqrt(absVxDiff * absVxDiff + absVyDiff * absVyDiff);

				if (signVx != signVxPrev || signVy != signVyPrev)
				{
					if (velMagnitude > minVelDiff) 
						isCollision = true;
				} 
				else 
					isCollision = false;
			}
				
			VxPrev = Vx;
			VyPrev = Vy;

			signVxPrev = signVx;
			signVyPrev = signVy;
		}
	}
}
