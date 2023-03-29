using UnityEngine;
using System.Collections;

public class JumpScriptStarter : MonoBehaviour 
{
	public JumpScript jScr;

	void Awake ()
	{
		jScr.enabled = false;
	}

	void Update () 
	{
		if (Time.timeSinceLevelLoad > PlayerMovement.restartDelay1 + 1) 
		{
			jScr.enabled = true;
			gameObject.GetComponent<JumpScriptStarter>().enabled = false;
		}
	}
}
