using UnityEngine;
using System.Collections;

public class BallShoot : MonoBehaviour 
{
    [SerializeField]
    private Scripts scr;

    [SerializeField]
    private bool isPlayer;
    [SerializeField]
    private Animator shotAnim;
    [SerializeField]
    private Transform shotTr;
    [SerializeField]
    private Collider2D bColl;
    [SerializeField]
    private Transform ballTr;


    private bool isBall;
    private Vector3 shotPos;
    private int _call;

    void Awake()
    {
        _call = Animator.StringToHash("call");
    }

    void Update()
    {
        if (isPlayer)
        {
            if (isBall && scr.pMov.kick1)
            {
                if (scr.levAudScr.isSoundOn)
				    scr.levAudScr.kickSource.Play();

                shotAnim.SetTrigger(_call);
                shotPos = ballTr.position;
                isBall = false;
            }
        }
        else
        {
            if (isBall && scr.enAlg.isKick)
            {
                if (scr.levAudScr.isSoundOn)
                    scr.levAudScr.kickSource.Play();

                shotAnim.SetTrigger(_call);
                shotPos = ballTr.position;
                isBall = false;
            }
        }
        

        shotTr.position = new Vector3(
            shotPos.x,
            shotPos.y,
            shotPos.z);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col == bColl)
            isBall = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col == bColl)
            isBall = false;
    }
}
