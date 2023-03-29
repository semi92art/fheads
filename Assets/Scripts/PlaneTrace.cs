using UnityEngine;
using System.Collections;

public class PlaneTrace : MonoBehaviour
{
	public float tailColorPeriod, tailScalePeriod;
	private float cR, cG, cB, cA;
	private float scX, scY;

	void Update()
	{
		scX = transform.localScale.x;
		scY = transform.localScale.y;

		if (scX > tailScalePeriod && scY > tailScalePeriod)
			transform.localScale = new Vector3(scX + tailScalePeriod, scY + tailScalePeriod, 1);

		cR = GetComponent<SpriteRenderer>().color.r;
		cG = GetComponent<SpriteRenderer>().color.g;
		cB = GetComponent<SpriteRenderer>().color.b;
		cA = GetComponent<SpriteRenderer>().color.a;

		if (cA > 0)
			GetComponent<SpriteRenderer>().color = new Vector4(cR, cG, cB, cA + tailColorPeriod);
		else
			transform.localScale = new Vector3(scX, scY, 1);
	}
}
