using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Scripts scr;

    public OpponentType _enType;
    public float whenKick;
    public float distToBall;
    public float whenKick_State_1;
    public float moveForce;

    public float
    maxSpeed,
    jumpForce,
    kickTorque,
    kickSpeed;

    public Collider2D headColl1;
    public HingeJoint2D legHJ;
    public SpriteRenderer enSpr;

    [HideInInspector]
    public bool isKick;
    [HideInInspector]
    public Vector2 fVectEn;

    public static bool gameStop;

    private Transform enSprTr;
    [SerializeField]
    private float Force;
    private int timer;
    private bool chDir2;
    private int jumpTimer;
    //[SerializeField]
    private float enX, enY, bX, bY, plX;
    private float timerSprAnim;
    private int sprAnimVal;
    private float boredTimer;
    private int boredTimer1;
    private int boredMove = 200, boredJump = 200;
    private float startTime;
    private bool isKickControl;
    private int readyToKickOverHead;
    private bool chDir1;
    private int distState;
    private float defPosX;
    private float dTim, lim;
    public Rigidbody2D _rb;
    [HideInInspector]
    public Transform _tr;
    public Collider2D col_body;
    private bool isGrounded;


    void Awake()
    {
        gameStop = false;
        _tr = transform;
        whenKick_State_1 = scr.bonObjMan.whenKick_State_1_Norm;
        enSprTr = enSpr.transform;

        kickSpeed = scr.pMov.kickSpeed0;

        if (_enType != OpponentType.Goalkeeper)
        {
            _enType = scr.buf.oppType;
            enSpr.sprite = scr.buf.enSpr;
            maxSpeed = scr.pMov.maxSpeed0 * scr.buf.enSkillSpeed / 100f;
            jumpForce = scr.pMov.jumpForce0 * scr.buf.enSkillJump / 100f;
            kickTorque = scr.pMov.kickTorque0 * scr.buf.enSkillKick / 100f;
            
        }
        else
        {
            enSpr.sprite = scr.buf.enSpr_1;
            maxSpeed = scr.pMov.maxSpeed0 * scr.buf.enSkillSpeed_1 / 100f;
            jumpForce = scr.pMov.jumpForce0 * scr.buf.enSkillJump_1 / 100f;
            kickTorque = scr.pMov.kickTorque0 * scr.buf.enSkillKick_1 / 100f;
        }

        SKJ_Upgrades();

        jumpForceDef = jumpForce;
        JointAngleLimits2D limits = legHJ.limits;
        JointMotor2D motor = legHJ.motor;
        legHJ.limits = limits;
        motor.maxMotorTorque = 0;
        legHJ.motor = motor;
    }

    void Start()
    {
        legHJ.transform.rotation = Quaternion.Euler(0, 0, 90);
    }

    private void SKJ_Upgrades()
    {
        float coeff_game = (100f + (float)scr.alPrScr.game * 0.01f) / 100f;
        float coeff_lg = (100f + (float)scr.alPrScr.lg * 0.01f) / 100f;
        float coeff_rand = (100f + Random.value * 0.1f) / 100f;

        if (scr.alPrScr.isRandGame == 0)
        {
            maxSpeed = maxSpeed * coeff_game * coeff_lg * 1.015f;
            jumpForce = jumpForce * coeff_game * coeff_lg * 1.02f;
            kickTorque = kickTorque * coeff_game * coeff_lg * 1.02f;
            kickSpeed = kickSpeed * coeff_game * coeff_lg * 1.02f;
        }
        else
        {
            maxSpeed = maxSpeed * coeff_rand * coeff_rand * 1.015f;
            jumpForce = jumpForce * coeff_rand * coeff_rand * 1.02f;
            kickTorque = kickTorque * coeff_rand * coeff_rand * 1.02f;
            kickSpeed = kickSpeed * coeff_rand * coeff_rand * 1.02f;
        }
    }

    void Update()
    {
        switch (_enType)
        {
            case OpponentType.Classic:
                isGrounded = scr.grTr.isEnemyGrounded;
                defPosX = bX < scr.marks.midTr.position.x ?
                    scr.marks.gkStateTr.position.x : scr.marks.defPosTr.position.x;
                break;
            case OpponentType.Bycicle:
                isGrounded = scr.grTr.isEnemyGrounded;
                defPosX = bX < scr.marks.midTr.position.x ?
                    scr.marks.gkStateTr.position.x : scr.marks.defPosTr.position.x;
                break;
            case OpponentType.Goalkeeper:
                isGrounded = scr.grTr.isEnemy1Grounded;
                defPosX = scr.marks.gkStateTr.position.x + 12f * (0.5f + 1f * Mathf.Sin(Time.time)) + 3f;
                break;
        }

        if (scr.pMov.startGame && !scr.pMov.startGameCheck)
            startTime = Time.timeSinceLevelLoad;
        else if (scr.pMov.startGame && scr.pMov.startGameCheck)
        {
            if (Time.timeSinceLevelLoad > startTime + PlayerMovement.restartDelay1 &&
                !isKickControl)
                isKickControl = true;
        }

        if (!kOvH)
        {
            if (sprAnimVal != -1)
            {
                switch ((int)Force)
                {
                    case -1:
                        sprAnimVal = transform.position.x > defPosX + 1 ? 1 : 3;
                        break;
                    case 1:
                        sprAnimVal = 2;
                        break;
                    case 0:
                        sprAnimVal = 3;
                        break;
                }
            }

            if (scr.bonObjMan.isEnemyFast == 2)
                enSprTr.localRotation = Quaternion.Euler(0, 0, 0);
            else
            {
                switch (sprAnimVal)
                {
                    case 1:
                        enSprTr.localRotation = Quaternion.Euler(0, 0, 9);
                        sprAnimVal = -1;
                        break;
                    case 2:
                        enSprTr.localRotation = Quaternion.Euler(0, 0, -9);
                        sprAnimVal = -1;
                        break;
                    case 3:
                        enSprTr.localRotation = Quaternion.Euler(0, 0, 0);
                        sprAnimVal = -1;
                        break;
                }
            }

            if (sprAnimVal == -1)
            {
                timerSprAnim += Time.deltaTime;

                if (timerSprAnim > 0.2f)
                {
                    sprAnimVal = 0;
                    timerSprAnim = 0;
                }
            }
        }

        DistanceToBallState();
    }

    void FixedUpdate()
    {
        EnemyMovementControl();
        MaxSpeedControl();
        KickControl();

        if (_enType == OpponentType.Goalkeeper)
            GoalkeeperJump();
        else if (_enType == OpponentType.Bycicle)
            KickOverHeadFcn();
    }

    private void KickControl()
    {
        if (Mathf.Abs(bY - enY) < 5f)
        {
            if (bX - enX > 1 && bX - enX < whenKick + 3f && legHJ.jointAngle >= -1f)
                isKick = true;
            else
            {
                if (isKick && legHJ.jointAngle <= -90f)
                    isKick = false;
            }
        }
        else
            isKick = false;

        if (scr.jScr.isMolniya && _enType != OpponentType.Goalkeeper)
            isKick = false;

        JointMotor2D motor = legHJ.motor;
        motor.motorSpeed = isKick ? -kickSpeed : kickSpeed * 0.9f;
        motor.maxMotorTorque = kickTorque;
        legHJ.motor = motor;

        JointAngleLimits2D limits = legHJ.limits;

        switch (_enType)
        {
            case OpponentType.Classic:
                limits.min = -180f;
                limits.max = -90f;
                legHJ.limits = limits;
                break;
            case OpponentType.Bycicle:
                if (!kOvH)
                {
                    limits.min = -180f;
                    limits.max = -90f;
                    legHJ.limits = limits;
                }
                break;
            case OpponentType.Goalkeeper:
                limits.min = -180f;
                limits.max = -90f;
                legHJ.limits = limits;
                break;
        }
    }
    
    private void EnemyBored()
    {
        boredTimer += Time.deltaTime;
        boredTimer1++;

        if (boredTimer1 % 10 == 0)
        {
            boredMove = Mathf.FloorToInt(Random.value * 10 + 10);
            boredJump = Mathf.FloorToInt(Random.value * 100 + 100);
        }

        if (boredTimer1 % boredMove == 0)
        {
            if (scr.grTr.gameObject.activeSelf)
                Force = -Force;
        }

        if (boredTimer1 % boredJump == 0)
        {
            if (isGrounded)
                isKick = true;
        }

        boredTimer = boredTimer > 200 ? 0 : boredTimer;
    }
    
    private void Defend()
    {
        if (bX < scr.marks.midTr.position.x)
        {
            boredTimer = 0;

            if (enX > bX - distToBall)
            {
                if (enX < defPosX)
                    Force = enX < defPosX - 0.5f ? 1 : 0;
                else
                    Force = -1;
            }
            else
            {
                if (enY > bY)
                    Force = enX > bX - distToBall - 1 ? -0.5f : 1;
                else
                    Force = 1;
            }
        }
        else
        {
            boredTimer += Time.deltaTime;

            if (boredTimer < 0.5f)
            {
                if (enX < defPosX)
                    Force = enX < defPosX - 0.5f ? 1 : 0;
                else
                {
                    if (enX > defPosX + 2)
                        boredTimer = 0;

                    Force = -1;
                }
            }
            else
            {
                if (enX < defPosX)
                    Force = 1;
                else if (enX > defPosX + 3)
                    Force = -1;
                else
                    EnemyBored();
            }
        }
    }

    private void GoalKeeper()
    {
        if (bX < transform.position.x)
            Force = -1;
        else
        {
            if (transform.position.x < defPosX)
                Force = 1;
            else
                Force = -1;
        }
    }
    
    private void Attack()
    {
        if (enX > bX - distToBall)
            Force = -1;
        else
        {
            if (enY > bY + 1)
                Force = enX > bX - distToBall - 1 ? -0.5f : 1;
            else
                Force = 1;
        }
    }

    private void DistanceToBallState()
    {
        if (bX < scr.marks.midTr.position.x)
        {
            if (plX > scr.marks.midTr.position.x)
            {
                dTim += Time.deltaTime;
                distState = dTim < lim ? 0 : 1;
            }
            else
            {
                lim = Random.value * 2.0f;
                dTim = 0.0f;
                distState = 1;
            }

        }
        else
        {
            lim = Random.value * 2.0f;
            dTim = 0.0f;
            distState = 1;
        }

        switch (distState)
        {
            case 0:
                whenKick = 3.5f;
                distToBall = 5.0f;
                break;
            case 1:
                whenKick = whenKick_State_1;
                distToBall = 3.5f;
                break;
        }
    }

    private int prevScoreDiff;
    public bool isTimDef = true;
    public float timDef;
    public float distToBall_1;

    private void EnemyMovementControl()
    {
        //Debug.Log("Enemy Alive -6!");
        bX = scr.ballScr.transform.position.x;
        bY = scr.ballScr.transform.position.y;
        plX = scr.pMov.transform.position.x;
        enY = transform.position.y;
        enX = transform.position.x;

        if (_enType == OpponentType.Goalkeeper)
            GoalKeeper();
        else
        {

            if (prevScoreDiff != Score.score - Score.score1)
            {
                timDef = 0f;

                if (Score.score - Score.score1 < 3)
                    isTimDef = true;
            }
            
            if (!gameStop && !scr.pMov.restart && !scr.pMov.freezeOnStart)
            {
                float timLim = scr.tM.matchPeriods == 2 ?
                    35f : (float)prevScoreDiff / 2f + 1f;
                if (isTimDef)
                {
                    timDef += Time.fixedDeltaTime;

                    if (timDef > timLim)
                    {
                        timDef = 0f;
                        isTimDef = false;
                    }
                }
                if (enX > plX && enX < bX - distToBall_1)
                    Force = 1;
                else
                {
                    if (Score.score < Score.score1)
                        Defend();
                    else
                    {
                        if (!scr.tM.isGoldenGoal)
                        {
                            if (Score.score - Score.score1 >= 3)
                                Attack();
                            else
                            {
                                if (isTimDef)
                                    Defend();
                                else
                                    Attack();
                            }
                        }
                        else
                            Defend();
                    }
                }
            }

            prevScoreDiff = Score.score - Score.score1;
        }
    }

    private void MaxSpeedControl()
    {
        if (!scr.pMov.restart && !gameStop && !scr.pMov.freezeOnStart)
        {
            if (Mathf.Abs(_rb.velocity.x) < maxSpeed)
            {
                if (Force > float.Epsilon)
                    _rb.AddForce(new Vector2(Force * moveForce, 0));
                else
                {
                    if (transform.position.x > -20)
                    {
                        if (scr.tM.matchPeriods == 2)
                        {
                            if (Score.score1 - Score.score - 3 >= 0)
                                _rb.AddForce(new Vector2(-moveForce, 0));
                        }
                        else if (scr.tM.matchPeriods == 1)
                        {
                            if (Score.score1 - Score.score >= 0)
                                _rb.AddForce(new Vector2(-moveForce, 0));
                        }
                    }
                }
            }
            else
            {
                _rb.velocity = new Vector2(
                    Mathf.Sign(_rb.velocity.x) * 0.98f * maxSpeed,
                    _rb.velocity.y);
            }
        }
    }

    private bool jump;
    private bool isOffGround;
    private int offGroundTimer;
    private bool isRealJump;

    [Header("Kick Overhead Objects:")]
    public bool kOvH;
    private float kOvHTorque;

    public EdgeCollider2D col_EdgeSlide;
    public CircleCollider2D col_CircSlide;
    public Transform tr_GeneralLeg;
    public float genTr_x, genTr_y, genTr_rot;
    public float kickOvHTorq_0;
    public float torqTime;
    public float timOvH;
    public float timH;
    private float jumpForceDef;


    private bool isKickOverHead()
    {
        return kOvH ? true : (bX > enX - 1f && bX < enX + whenKick + 3f) && (bY < enY + 15f && bY > enY + 2f);
    }

    private void KickOverHeadFcn()
    {
        kOvH = isKickOverHead();
        
        if (kOvH)
        {
            if (!scr.jScr.isMolniya)
            {
                col_CircSlide.enabled = false;
                col_EdgeSlide.enabled = false;

                tr_GeneralLeg.localPosition = new Vector3(genTr_x, genTr_y, 0f);
                tr_GeneralLeg.localRotation = Quaternion.Euler(0f, 0f, genTr_rot);

                _rb.freezeRotation = false;

                transform.localScale = new Vector3(
                    -Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z);

                timH += Time.deltaTime;

                kOvHTorque = timH < torqTime ? kickOvHTorq_0 : kOvHTorque / 2f;
                _rb.AddTorque(kOvHTorque);

                jump = isGrounded ? true : jump;
                kOvH = timH > 0.5f ? false : true;

                JointAngleLimits2D limits = legHJ.limits;
                limits.min = timH < timOvH ? 180f : 270f;
                limits.max = timH < timOvH ? 180f : 270f;
                legHJ.limits = limits;
            }
        }
        else
        {
            col_CircSlide.enabled = true;
            col_EdgeSlide.enabled = true;

            tr_GeneralLeg.localPosition = new Vector3(0f, 0f, 0f);
            tr_GeneralLeg.localRotation = Quaternion.Euler(0f, 0f, 0f);

            timH = 0.0f;
            kOvHTorque = kickOvHTorq_0;
            _rb.freezeRotation = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);

            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z);
        }

        jumpForce = kOvH ? jumpForceDef * 0.85f : jumpForceDef;
        Single_Jump();
    }

    private void Single_Jump()
    {
        if (jump)
        {
            jump = false;

            if (offGroundTimer == 0)
                isOffGround = true;
        }

        if (isOffGround)
        {
            offGroundTimer++;

            if (offGroundTimer == 1)
                isRealJump = true;
            else if (offGroundTimer >= 2 && offGroundTimer < 10)
                isRealJump = false;
            else
            {
                offGroundTimer = 0;
                isOffGround = false;
            }
        }

        if (isRealJump && isGrounded)
        {
            _rb.velocity = new Vector2(
                _rb.velocity.x,
                jumpForce);
        }
    }

    private void GoalkeeperJump()
    {
        if (Time.timeSinceLevelLoad > PlayerMovement.restartDelay1 + 1)
        {
            if (Mathf.Abs(bX - enX) < 10f)
            {
                if (Mathf.Abs(bY - enY) < 20f)
                    jump = true;
            }
            else
                jump = false;

            Single_Jump();
        }
    }

}
