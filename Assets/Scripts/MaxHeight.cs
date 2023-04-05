using UnityEngine;
using System.Collections;

public class MaxHeight : MonoBehaviour 
{
    [SerializeField]
	private Scripts scr;

	public float minVel;
	public float downForce;
	public Collider2D enemyColl, enemy1Coll, playerColl;
	public bool toDoPl, toDoEn, toDoEn1;

    [SerializeField]
	private BoxCollider2D maxHColl;


	void FixedUpdate()
	{
		if (toDoPl)
		{
            if (scr.pMov._rb.velocity.y > minVel)
                scr.pMov._rb.AddForce(new Vector2(0, downForce));
		}

		if (toDoEn)
		{
			if (scr.enAlg._rb.velocity.y > minVel)
                scr.enAlg._rb.AddForce(new Vector2(0, downForce));
		}

        if (toDoEn1)
        {
            if (scr.enAlg_1._rb.velocity.y > minVel)
                scr.enAlg_1._rb.AddForce(new Vector2(0, downForce));
        }
	}
		
	void OnTriggerStay2D(Collider2D coll)
	{
        float boundY = transform.position.y - maxHColl.bounds.size.y * 0.5f;

		if (coll == playerColl)
		{
			if (scr.pMov.transform.position.y > boundY)
				toDoPl = true;
		}
		else if (coll == enemyColl)
		{
			if (scr.enAlg.transform.position.y > boundY)
				toDoEn = true;
		}
        else if (coll == enemy1Coll)
        {
            if (scr.enAlg_1.transform.position.y > boundY)
                toDoEn1 = true;
        }
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll == playerColl)
			toDoPl = false;
		else if (coll == enemyColl)
			toDoEn = false;
        else if (coll == enemy1Coll)
            toDoEn1 = false;
	}
}
