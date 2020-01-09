using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public enum BonusBall_0
{
    SimpleBall,
    ClownBall,
    BeachBall,
    RugbyBall
}


[System.Serializable]
public class BonusBall
{
    public BonusBall_0 _bonusBall;
    public Sprite _spr;
    public float _mass;
    public float _drag;
    public float _angDrag;
}

public class BallScript : MonoBehaviour 
{
    public Scripts scr;
    [Header("Bonus Balls:")]
    public List<BonusBall> _bonusBalls;
    [Space(5)]
    public SpriteRenderer sprRend_Ball;
	[Header("Ball Rotation and Gravity Parameters:")]
	public float minV;
	public float maxV = 40;
	public float rotV = 70;
	public float minGravity;
	public float maxGravity;

	[Header("Tail Parameters:")]
	public int tailPeriod;
	public Color stCol;
	public Color endCol;

	[Header("Other Parameters:")]
	public Transform enArrBoxTr;
	public Transform plArrBoxTr;
	public GameObject enArr, plArr;
	public GameObject ballTail;

	private float stGrav;
    [HideInInspector]
	public Transform _tr;
	public Rigidbody2D _rb;
	private Transform camTr;
	private int tailTimer;
	private int ballNCurr;
	private LineRenderer ballLineR;
	private bool restartCheck;
	private Vector3[] newTailVect = new Vector3[10];
	private float sign, vel, angV;
    private float newV;
    private float bX, bY;
    private float plX, plY;
    private float enX, enY;
    private float cSize;
    public CircleCollider2D _col;
    public PolygonCollider2D _colRugby;
    private bool isRandTraectory;
    private BonusBall_0 _currBonBall;

	void Awake()
	{
        camTr = scr.camSize.transform;

		ballLineR = GetComponent<LineRenderer> ();
        _tr = transform;

		stGrav = _rb.gravityScale;

		for (int i = 0; i < newTailVect.Length; i++)
		{
			newTailVect[i] = _tr.position;
			ballLineR.SetPosition(i, _tr.position);
		}

        ballLineR.startColor = stCol;
        ballLineR.endColor = endCol;
	}

    private float randCoeff;
    private float randTim;

	private void Update()
	{
        GetSomeValues();

		angV = -_rb.angularVelocity;
		sign = Mathf.Sign(_rb.velocity.x);
		vel = Mathf.Abs(_rb.velocity.x);

        randTim += Time.deltaTime;

        if (randTim > 0.5f)
        {
            randCoeff = isRandTraectory ? Random.value * 3f : 1f;    
            randTim = 0f;
        }

        newV = vel < maxV ? vel : maxV;
        _rb.gravityScale = stGrav + 
            randCoeff * sign * Mathf.Pow(newV, 3) * angV * 1e-8f * rotV;

        if (!isRandTraectory)
        {
            if (vel < minV)
                _rb.gravityScale = stGrav;

            minGravity = 0.5f;
            maxGravity = 3.5f;
        }
        else
        {
            minGravity = -4f;
            maxGravity = 3.5f;
        }

        if (_rb.gravityScale < minGravity)
            _rb.gravityScale = minGravity;
        else if (_rb.gravityScale > maxGravity)
            _rb.gravityScale = maxGravity;


		//ballLineR.SetPosition(0, _tr.position);
		//EnemyArrow();
		//PlayerArrow();
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

				newTailVect[0] = _tr.position;

                for (int i = 1; i < newTailVect.Length; i++)
                    ballLineR.SetPosition(i,newTailVect[i]);
			}

			if (scr.pMov.restart) 
			{
                if (scr.pMov.restartTimer > PlayerMovement.restartDelay2 - 0.1f)
                {
                    ballLineR.startColor = Color.clear;
                    ballLineR.endColor = Color.clear;
                }
			}
			else
			{
				if (restartCheck) 
				{
                    ballLineR.startColor = stCol;
                    ballLineR.endColor = endCol;
				}
			}
		}

		restartCheck = scr.pMov.restart;
	}

    private void GetSomeValues()
    {
        enX = scr.enAlg._tr.position.x;
        enY = scr.enAlg._tr.position.y;
        bX = transform.position.x;
        bY = transform.position.y;
        plX = scr.pMov.transform.position.x;
        plY = scr.pMov.transform.position.y;
        cSize = scr.camSize._cam.orthographicSize;
    }

	/*private void EnemyArrow()
	{
		float ang0 = Mathf.Atan((bX - enX) / (bY - enY));
		float ang = (-ang0 - 0.5f * Mathf.PI) * 180 / Mathf.PI;

		if (enX < camTr.position.x - cSize * 2.0f || 
            enX > camTr.position.x + cSize * 2.0f)
		{
            float ang1 = bY > enY ? ang : ang + 180f;
            enArrBoxTr.rotation = Quaternion.AngleAxis(ang1, Vector3.forward);
			enArr.SetActive(true);	
		}
		else
			enArr.SetActive(false);
	}*/

	/*private void PlayerArrow()
	{
		float ang0 = Mathf.Atan((bX - plX) / (bY - plY));
		float ang = (-ang0 - 0.5f * Mathf.PI) * 180 / Mathf.PI;

		if (plX < camTr.position.x - cSize * 2.0f || 
            plX > camTr.position.x + cSize * 2.0f)
		{
            float ang_1 = bY > plY ? ang + 180f : ang;
            plArrBoxTr.rotation = Quaternion.AngleAxis(ang_1, Vector3.forward);
			plArr.SetActive(true);	
		}
		else
			plArr.SetActive(false);
	}*/

    public void SetBonusBall(int _ind)
    {
        sprRend_Ball.sprite = _bonusBalls[_ind]._spr;
        _rb.mass = _bonusBalls[_ind]._mass;
        scr.grTr.ballDrag = _bonusBalls[_ind]._drag;
        _rb.angularDrag = _bonusBalls[_ind]._angDrag;
        _currBonBall = _bonusBalls[_ind]._bonusBall;

        switch (_bonusBalls[_ind]._bonusBall)
        {
            case BonusBall_0.SimpleBall:
                _col.enabled = true;
                _colRugby.enabled = false;
                isRandTraectory = false;
                break;
            case BonusBall_0.ClownBall:
                _col.enabled = true;
                _colRugby.enabled = false;
                isRandTraectory = true;
                break;
            case BonusBall_0.BeachBall:
                _col.enabled = true;
                _colRugby.enabled = false;
                isRandTraectory = false;
                break;
            case BonusBall_0.RugbyBall:
                _col.enabled = false;
                _colRugby.enabled = true;
                isRandTraectory = false;
                break;
        }
    }
}
