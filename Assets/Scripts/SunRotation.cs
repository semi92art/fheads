using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SunRotation : MonoBehaviour 
{
	public float rotationSpeed;
	public bool isRotate;

	private RectTransform _rectTr;
	private int timer;

	void Awake()
	{
		_rectTr = GetComponent<RectTransform>();
	}

	void FixedUpdate()
	{
		if (isRotate)
		{
			_rectTr.rotation = Quaternion.Euler (
				_rectTr.rotation.x,
				_rectTr.rotation.y,
				_rectTr.rotation.z - rotationSpeed * (float)Time.frameCount/10.0f);
		}
	}
}
