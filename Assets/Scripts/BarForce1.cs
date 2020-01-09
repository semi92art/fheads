using UnityEngine;
using System.Collections;

public class BarForce1 : MonoBehaviour 
{
    [SerializeField]
	private Scripts scr;

    [SerializeField]
	private float forceVal;
    public forceVectorEnum forceVectType;

    public enum forceVectorEnum
    {
        right, left, up, down
    }

    private Vector2 forceVector;

    void Awake()
    {
        switch (forceVectType)
        {
            case forceVectorEnum.right:
                forceVector = Vector2.right;
                break;
            case forceVectorEnum.left:
                forceVector = Vector2.left;
                break;
            case forceVectorEnum.up:
                forceVector = Vector2.up;
                break;
            case forceVectorEnum.down:
                forceVector = Vector2.down;
                break;
            default:
                forceVector = Vector2.zero;
                break;
        }
    }

	void OnTriggerStay2D (Collider2D col)
	{
        if (col == scr.ballScr._col)
            scr.ballScr._rb.AddForce(forceVal * forceVector);
	}
}
	

