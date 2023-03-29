using UnityEngine;
using UnityEngine.UI;

public class Sliders : MonoBehaviour {
	FastDOF fmDof;
	void Start () {
		fmDof = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<FastDOF> ();
	}

	void Update () {
		
	}
	public void Focus(Slider a){
		fmDof.Focus = a.value;
	}
}
