using UnityEngine;
using System.Collections;

public class MaxHeight : MonoBehaviour 
{
    [SerializeField]
	private Scripts scr;

	public float minVel;
	public float downForce;
	public Collider2D enemyColl, playerColl;
	public bool toDoPl, toDoEn;

    [SerializeField]
	private BoxCollider2D maxHColl;


	void FixedUpdate()
	{
		if (toDoPl)
		{
            if (Player.Instance._rb.velocity.y > minVel)
	            Player.Instance._rb.AddForce(new Vector2(0, downForce));
		}

		if (toDoEn)
		{
			if (Enemy.Instance._rb.velocity.y > minVel)
				Enemy.Instance._rb.AddForce(new Vector2(0, downForce));
		}
	}
		
	void OnTriggerStay2D(Collider2D coll)
	{
        float boundY = transform.position.y - maxHColl.bounds.size.y * 0.5f;

		if (coll == playerColl)
		{
			if (Player.Instance.transform.position.y > boundY)
				toDoPl = true;
		}
		else if (coll == enemyColl)
		{
			if (Enemy.Instance.transform.position.y > boundY)
				toDoEn = true;
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll == playerColl)
			toDoPl = false;
		else if (coll == enemyColl)
			toDoEn = false;
	}
}
