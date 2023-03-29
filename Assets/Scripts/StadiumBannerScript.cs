using UnityEngine;
using System.Collections;

public class StadiumBannerScript : MonoBehaviour {

	public Scripts scr;

	public Animator[] tribuneBanners;

	void Awake()
	{
		/*for (int i = 0; i < tribuneBanners.Length; i++)
		{
			tribuneBanners[i].enabled = false;
		}*/
	}


	void Update() 
	{
		//float timeSinLoad = Time.timeSinceLevelLoad;

		for (int i = 0; i < tribuneBanners.Length; i++)
		{
			if (i < Time.timeSinceLevelLoad * 17) 
			{
				tribuneBanners[i].SetTrigger("call");

				if (i == tribuneBanners.Length - 1)
				{
					gameObject.GetComponent<StadiumBannerScript>().enabled = false;
				}
			}
		}
	}
}
