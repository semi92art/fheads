using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSearch : MonoBehaviour 
{
    [SerializeField]
    private Scripts scr;

    [SerializeField]
    private int kratnost;
    private bool tim, prevTim;
    [SerializeField]
    private CircleCollider2D col;


    void Update()
    {
        float time_10 = scr.tM.time0 * 10f;
        int div = Mathf.RoundToInt(time_10 % kratnost);

        if (scr.bonObjMan.isSearchObjReady && div == 0)
        {
            if (IsContactWithObjects())
                tim = !tim;
        }


        if (tim != prevTim)
            scr.bonObjMan.isSearchObjReady = false;
        else
        {
            if (transform.position.y < scr.marks.rightUpBonusTr.position.y &&
                !scr.bonObjMan.isSearchObjReady)
                scr.bonObjMan.isSearchObjReady = true;
        }
            
        prevTim = tim;
    }

    private bool[] bools = new bool[32];
    private int[] layers = new int[4];

    private bool IsContactWithObjects()
    {
        layers[0] = 8;
        layers[1] = 9;
        layers[2] = 11;
        layers[3] = 14;

        Vector2 pos2d = new Vector2(
            transform.position.x,
            transform.position.y);

        for (int i = 0; i < layers.Length; i++)
        {
            bools[8 * i] = Physics2D.Raycast(pos2d, Vector2.down, col.radius, 1 << layers[i]);
            bools[8 * i + 1] = Physics2D.Raycast(pos2d, Vector2.up, col.radius, 1 << layers[i]);
            bools[8 * i + 2] = Physics2D.Raycast(pos2d, Vector2.left, col.radius, 1 << layers[i]);
            bools[8 * i + 3] = Physics2D.Raycast(pos2d, Vector2.right, col.radius, 1 << layers[i]);
            bools[8 * i + 4] = Physics2D.Raycast(pos2d, new Vector2(1, 1), col.radius, 1 << layers[i]);
            bools[8 * i + 5] = Physics2D.Raycast(pos2d, new Vector2(-1, 1), col.radius, 1 << layers[i]);
            bools[8 * i + 6] = Physics2D.Raycast(pos2d, new Vector2(1, -1), col.radius, 1 << layers[i]);
            bools[8 * i + 7] = Physics2D.Raycast(pos2d, new Vector2(-1, -1), col.radius, 1 << layers[i]);
        }

        bool isContact = false;

        for (int i = 0; i < bools.Length; i++)
        {
            if (bools[i])
            {
                isContact = true;
                break;
            }  
        }

        return isContact;
    }
}
