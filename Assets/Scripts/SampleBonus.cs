using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SampleBonus : MonoBehaviour 
{
	public Text _name;
	public int award;
	public Text awardText;
	public string meaning;

	void Start()
	{
		transform.localScale = Vector3.one;
	}

}
