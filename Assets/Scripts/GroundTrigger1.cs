using UnityEngine;
using System.Collections;

public class GroundTrigger1 : MonoBehaviour 
{
    [SerializeField]
	private Scripts scr;

    public float ballDrag, ballDragGrounded;
	public bool isPlayerGrounded, isEnemyGrounded;
	public bool isBallGrounded;
	public Collider2D playerCollider1;
	
    public Collider2D col_BallGr;
    [SerializeField]
    private float bY_Grounded;
    [SerializeField]
    private float bY_down;

    void Awake()
    {
        bY_Grounded = col_BallGr.bounds.extents.y + col_BallGr.transform.position.y + 0.2f;
    }

    private bool IsPlayerGrounded()
	{
        float dist_0 = playerCollider1.bounds.extents.y;

		return Physics2D.Raycast (
			playerCollider1.transform.position, 
			Vector3.down,
            dist_0 + 0.2f, 1 << 12);
	}

    private bool IsEnemyGrounded(Enemy en)
	{
        float dist_0 = en.col_body.bounds.extents.y;

		return Physics2D.Raycast (
            en.transform.position, 
		    Vector3.down,
            dist_0 + 0.2f, 1 << 12);
	}

    private bool IsBallGrounded()
    {
        float ballR = scr.ballScr._col.radius;
        float bY = scr.ballScr.transform.position.y;
        
        bY_down = bY - ballR * scr.ballScr.transform.localScale.x;

        if (bY_down < bY_Grounded)
            return true;
        else
            return false;
    }


	void Update()
	{
        isPlayerGrounded = IsPlayerGrounded();
        isEnemyGrounded = IsEnemyGrounded(Enemy.Instance);
        isBallGrounded = IsBallGrounded();

		if (!MatchManager.Instance.Restart)
            scr.ballScr._rb.drag = isBallGrounded ? 
                ballDragGrounded : ballDrag;
	}
}
