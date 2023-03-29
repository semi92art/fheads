using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour 
{
	private Scripts scr;

	public float distance;
	public float camSpeed;

	private Transform camTr;
	private Transform followTr;

	void Awake()
	{
		scr = FindObjectOfType<Scripts>();
		camTr = transform;
		followTr = scr.objLev.playerTr;
	}

	void Update()
	{
		camTr.position = new Vector3(
			Mathf.Lerp(camTr.position.x, followTr.position.x + distance, Time.deltaTime * camSpeed),
			camTr.position.y,
			camTr.position.z);
	}
}
