using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BonusObject : MonoBehaviour 
{
    public Scripts scr;

    public BonusType.bonusType _bonusType;
    public bool isReady;
    private CircleCollider2D ballColl;
    private float pl_Porog, en_Porog;

    void Awake()
    {
        pl_Porog = !PrefsManager.Instance.IsRandomOpponent ? PrefsManager.Instance.UpgradeShield * 5f * .01f : .5f;
        en_Porog = !PrefsManager.Instance.IsRandomOpponent ? PrefsManager.Instance.Game * 10f / .01f : .5f;
        
        ballColl = scr.ballScr.GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll == ballColl)
        {
            switch (_bonusType)
            {
                case BonusType.bonusType.BallBig:
                    scr.levAudScr.bonus_1.Play();
                    scr.bonObjMan.BallBig(1);
                    break;
                case BonusType.bonusType.BallSmall:
                    scr.levAudScr.bonus_1.Play();
                    scr.bonObjMan.BallBig(2);
                    break;
                case BonusType.bonusType.PlayerBig:
                    if (Random.value > en_Porog)
                    {
                        scr.levAudScr.bonus_1.Play();
                        scr.bonObjMan.PlayerBig(1);
                    } 
                    else
                    {
                        scr.levAudScr.bonus_2.Play();
                        scr.bonObjMan.PlayerBig(-2);
                        scr.enShield.EnableShield();
                    }
                    break;
                case BonusType.bonusType.PlayerSmall:
                    if (Random.value > pl_Porog)
                    {
                        scr.levAudScr.bonus_1.Play();
                        scr.bonObjMan.PlayerBig(2);
                    }
                    else
                    {
                        scr.levAudScr.bonus_2.Play();
                        scr.bonObjMan.PlayerBig(-1);
                        scr.plShield.EnableShield();
                    }
                    break;
                case BonusType.bonusType.EnemyBig:
                    if (Random.value > pl_Porog)
                    {
                        scr.levAudScr.bonus_1.Play();
                        scr.bonObjMan.EnemyBig(1);
                    }
                    else
                    {
                        scr.levAudScr.bonus_2.Play();
                        scr.bonObjMan.EnemyBig(-2);
                        scr.plShield.EnableShield();
                    }
                    break;
                case BonusType.bonusType.EnemySmall:
                    if (Random.value > en_Porog)
                    {
                        scr.levAudScr.bonus_1.Play();
                        scr.bonObjMan.EnemyBig(2);
                    }
                    else
                    {
                        scr.levAudScr.bonus_2.Play();
                        scr.bonObjMan.EnemyBig(-1);
                        scr.enShield.EnableShield();
                    }
                    break;
                case BonusType.bonusType.PlayerGatesBig:
                    if (Random.value > pl_Porog)
                    {
                        scr.levAudScr.bonus_1.Play();
                        scr.bonObjMan.PlayerGatesBig(1);
                    }
                    else
                    {
                        scr.levAudScr.bonus_2.Play();
                        scr.bonObjMan.PlayerGatesBig(-1);
                        scr.plShield.EnableShield();
                    }
                    break;
                case BonusType.bonusType.PlayerGatesSmall:
                    if (Random.value > en_Porog)
                    {
                        scr.levAudScr.bonus_1.Play();
                        scr.bonObjMan.PlayerGatesBig(2);
                    }
                    else
                    {
                        scr.levAudScr.bonus_2.Play();
                        scr.bonObjMan.PlayerGatesBig(-2);
                        scr.enShield.EnableShield();
                    }
                    break;
                case BonusType.bonusType.EnemyGatesBig:
                    if (Random.value > en_Porog)
                    {
                        scr.levAudScr.bonus_1.Play();
                        scr.bonObjMan.EnemyGatesBig(1);   
                    }
                    else
                    {
                        scr.levAudScr.bonus_2.Play();
                        scr.bonObjMan.EnemyGatesBig(-1);
                        scr.enShield.EnableShield();
                    }
                    break;
                case BonusType.bonusType.EnemyGatesSmall:
                    if (Random.value > pl_Porog)
                    {
                        scr.levAudScr.bonus_1.Play();
                        scr.bonObjMan.EnemyGatesBig(2);
                    }
                    else
                    {
                        scr.levAudScr.bonus_2.Play();
                        scr.bonObjMan.EnemyGatesBig(-2);
                        scr.plShield.EnableShield();
                    }
                    break;
                case BonusType.bonusType.PlayerSpeedDown:
                    if (Random.value > pl_Porog)
                    {
                        //scr.levAudScr.bonus_1.Play();
                        scr.bonObjMan.PlayerSpeedUp(2);
                    }
                    else
                    {
                        scr.levAudScr.bonus_2.Play();
                        scr.bonObjMan.PlayerSpeedUp(-1);
                        scr.plShield.EnableShield();
                    }
                    break;
                case BonusType.bonusType.EnemySpeedDown:
                    if (Random.value > en_Porog)
                    {
                        //scr.levAudScr.bonus_1.Play();
                        scr.bonObjMan.EnemySpeedUp(2);
                    }
                    else
                    {
                        scr.levAudScr.bonus_2.Play();
                        scr.bonObjMan.EnemySpeedUp(-1);
                        scr.enShield.EnableShield();
                    }
                    break;
                case BonusType.bonusType.WatchVideo:
                    scr.levAudScr.bonus_1.Play();
                    scr.bonObjMan.WatchVideo(0);
                    break;
                case BonusType.bonusType.BallClown:
                    scr.levAudScr.bonus_1.Play();
                    scr.bonObjMan.BallBig(-1);
                    break;
                case BonusType.bonusType.BallBeach:
                    scr.levAudScr.bonus_1.Play();
                    scr.bonObjMan.BallBig(-2);
                    break;
                case BonusType.bonusType.BallRugby:
                    scr.levAudScr.bonus_1.Play();
                    scr.bonObjMan.BallBig(-3);
                    break;
            }
        }
    }
}
