using UnityEngine;
using System.Collections;
using System;

public class JumpScript : MonoBehaviour 
{
    [SerializeField]
	private Scripts scr;

	public CircleCollider2D ballColl1;
	public int checkTime;
	public bool jump;

	public bool isRestart;
	public float timer;
	public bool secondJump;
	public bool isFirstJump;
    public float zMin, zMax;
    public float rotC;

    [SerializeField]
    private Enemy enAlg;
	private bool isOffGround;
	private int offGroundTimer;
	//private Transform tr;
	private float sign = 1.0f;
    [SerializeField]
    private bool isEnemyGrounded;


	void Update()
	{
        if (Time.timeSinceLevelLoad > PlayerMovement.restartDelay1 + 1)
        {
            if (enAlg._enType == EOpponentType.Classic || enAlg._enType == EOpponentType.Bicycle)
            {
                if (transform.rotation.eulerAngles.z < zMin)
                    sign = 1.0f;
                else if (transform.rotation.eulerAngles.z > zMax)
                    sign = -1.0f;

                transform.Rotate(0, 0, sign * rotC * Time.deltaTime);
            }
            
            if (scr.pMov.restart) 
                isRestart = true;

            if (isRestart) 
            {
                timer += Time.fixedDeltaTime;

                if (timer > 3)
                {
                    isRestart = false;
                    timer = 0;
                }
            }
        }
	}

    private bool isRealJump;

	void FixedUpdate()
	{
        if (Time.timeSinceLevelLoad > PlayerMovement.restartDelay1 + 1)
        {
            // Jump "trigger" says, that we need to jump:
            if (jump)
            {
                jump = false;

                // Start cycle, which controls enemy jump only one time step during next 10 time steps:
                if (offGroundTimer == 0)
                    isOffGround = true;
            }

            // Start cycle:
            if (isOffGround)
            {
                // Start cycle timer:
                offGroundTimer++;

                if (offGroundTimer == 1) // Step 1 – Enemy jumps in this time step;
                    isRealJump = true;
                else if (offGroundTimer >= 2 && offGroundTimer < 10) // No jump of Enemy;
                    isRealJump = false;
                else // End of cycle, set cycle timer value to default;
                {
                    offGroundTimer = 0;
                    isOffGround = false;
                }
            }

            // Real jump of Enemy:
            switch (enAlg._enType)
            {
                case EOpponentType.Classic:
                    isEnemyGrounded = scr.grTr.isEnemyGrounded;
                    break;
                case EOpponentType.Bicycle:
                    isEnemyGrounded = scr.grTr.isEnemyGrounded;
                    break;
                case EOpponentType.Goalkeeper:
                    isEnemyGrounded = scr.grTr.isEnemy1Grounded;
                    break;
            }

            if (isRealJump && isEnemyGrounded && !isMolniya)
            {
                enAlg._rb.velocity = new Vector2(
                    enAlg._rb.velocity.x,
                    enAlg.jumpForce);
            }
        }
	}

    [HideInInspector]
    public bool isMolniya;

	void OnTriggerStay2D (Collider2D other)
	{
        if (Time.timeSinceLevelLoad > PlayerMovement.restartDelay1 + 1)
        {
            if (!isRestart) 
            {
                if (other == ballColl1) 
                {
                    checkTime++;
                    if (scr.grTr.gameObject.activeSelf)
                    {
                        if (scr.grTr.isEnemyGrounded) 
                        {
                            if (checkTime > 0) 
                            {
                                if (secondJump) 
                                {
                                    if (checkTime > 3) 
                                    {
                                        checkTime = 0;
                                        jump = true;
                                    }
                                } 
                                else 
                                {
                                    if (scr.ballScr.transform.position.x < enAlg.transform.position.x)
                                    {
                                        secondJump = true;
                                        checkTime = 0;
                                    }

                                    jump = true;
                                }
                            }
                        } 
                        else
                        {
                            checkTime = 0;
                            jump = false;
                        }
                    }
                }
            }
        }
	}

	void OnTriggerExit2D(Collider2D other)
	{
        if (Time.timeSinceLevelLoad > PlayerMovement.restartDelay1 + 1)
        {
            if (!isRestart)
            {
                if (other == ballColl1)
                {
                    secondJump = false;
                    checkTime = 0;
                    jump = false;
                }
            }  
        }
	}
}
	

