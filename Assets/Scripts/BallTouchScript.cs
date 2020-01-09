using UnityEngine;
using System.Collections;

public class BallTouchScript : MonoBehaviour
{
    [SerializeField]
	private Scripts scr;

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
	private Transform tr;
	public bool isStart;
	private bool isShot;


	void Awake()
	{
		tr = transform;
		ballRb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (tr.position.y < -8)
			isStart = true;

		if (scr.pMov.startGame && isStart)
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

	private bool bool1 = true;
	//private float ang0;//, ang;

	private void ShotAnim()
	{
		if (isCollision)
		{
			if (bool1)
			{
				isShot = true;

				if (isShot)
				{
					isShot = false;
					bool1 = false;

					//ang0 = Mathf.Atan(Vx / Vy);
					//ang = (-ang0 - 0.5f * Mathf.PI) * 180 / Mathf.PI;
				}
			}
		}
		else
			bool1 = true;
	}
}
