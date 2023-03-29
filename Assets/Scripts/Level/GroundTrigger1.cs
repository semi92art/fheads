using UnityEngine;
using System.Collections;

public class GroundTrigger1 : MonoBehaviour 
{
	public Scripts scr;

	public bool isPlayerGrounded, isEnemyGrounded;
	public bool isBallGrounded;
	public Collider2D playerCollider1;
	public Collider2D enemyCollider1;
	public Collider2D ballCollider;
	private Transform ballTr;
	public float dist, distBall;

	public float distToGrPlayer, distToGrEnemy, distToGrBall;

	void Awake()
	{
		ballTr = ballCollider.transform;
	}

	void Start()
	{
		distToGrPlayer = playerCollider1.bounds.extents.y;
		distToGrEnemy = enemyCollider1.bounds.extents.y;
		distToGrBall = ballCollider.bounds.extents.y;
	}

	private bool IsPlayerGrounded()
	{
		return Physics2D.Raycast (
			playerCollider1.transform.position, 
			Vector3.down, 
			distToGrPlayer + dist, 1 << 12);
	}

	private bool IsEnemyGrounded()
	{
		return Physics2D.Raycast (
			enemyCollider1.transform.position, 
			Vector3.down, 
			distToGrEnemy + dist, 1 << 12);
	}

	private bool IsBallGrounded()
	{
		return Physics2D.Raycast (
			ballCollider.transform.position, 
			Vector3.down, 
			distToGrBall + distBall, 1 << 12);
	}

	void Update()
	{
		if(ballTr.position.y > -10.4f)
			isBallGrounded = false;
		else 
			isBallGrounded = true;

		if (!scr.pMov.restart) 
		{
			if (isBallGrounded)
				scr.objLev.ballRb.drag = 2.7f;
			else 
				scr.objLev.ballRb.drag = 0;
		}
			
			isPlayerGrounded = IsPlayerGrounded ();
			isEnemyGrounded = IsEnemyGrounded ();
	}
}
