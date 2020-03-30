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
		if (MatchManager.Instance.GameStarted)
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

			if (MatchManager.Instance.Restart) 
			{
                if (Player.Instance.restartTimer > Player.restartDelay2 - 0.1f)
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

		restartCheck = MatchManager.Instance.Restart;
	}

    private void GetSomeValues()
    {
        enX = Enemy.Instance._tr.position.x;
        enY = Enemy.Instance._tr.position.y;
        bX = transform.position.x;
        bY = transform.position.y;
        plX = Player.Instance.transform.position.x;
        plY = Player.Instance.transform.position.y;
        cSize = scr.camSize._cam.orthographicSize;
    }

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
