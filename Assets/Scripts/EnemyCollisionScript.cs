using UnityEngine;
using System.Collections;

public class EnemyCollisionScript : MonoBehaviour 
{
	public Scripts scr;

	public Collider2D headCollEnemy;
	public bool isPlayerColl;

	void Update()
	{
		if(scr.objLev.playerTr.position.x > scr.objLev.enemy0Tr.position.x + 4)
			isPlayerColl = false;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll == headCollEnemy)
			isPlayerColl = true;
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll == headCollEnemy)
			isPlayerColl = false;
	}
}
