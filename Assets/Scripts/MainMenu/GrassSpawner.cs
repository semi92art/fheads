using UnityEngine;
using System.Collections;

public class GrassSpawner : MonoBehaviour {
	public float spawnFreq;
	public Transform camTr;
	public GameObject grassPlane;
	public Transform grass;
	public int order;

	private PlaneSampleObject plnSampObj;
	private float checkPosX;
	private bool isSame;
	private float timer;

	void Awake(){
		isSame = true;
	}

	void Update(){
		float camX = camTr.position.x;
		float posX = transform.position.x;
		float posY = transform.position.y;
		float posZ = transform.position.z;
		transform.position = new Vector3 (camX + 60, posY, posZ);
		if((int)posX!=(int)checkPosX){
			isSame = false;
		}
		if (!isSame){
			timer+=Time.deltaTime;
		} else {
			timer = 0;
		}
		if ((int)(posX%spawnFreq + 0)==0 && !isSame && timer > 3){
			if (order==0 || order==-2){
				order = -1;
			} else {
				order = -2;
			}
			GameObject newGrassPlane = Instantiate(grassPlane, 
				new Vector3(posX - 11, posY, posZ), transform.rotation) as GameObject;
			newGrassPlane.transform.SetParent (grass);
			isSame = true;
			plnSampObj = newGrassPlane.GetComponent<PlaneSampleObject>();
			plnSampObj.rend1.sortingOrder = order;
			plnSampObj.rend3.sortingOrder = order + 2;
			for (int i = 0; i < plnSampObj.tribunes.Length; i++){
				plnSampObj.tribunes[i].sortingOrder = order - 2 - 2*i;
			}
		}
		checkPosX = posX;
	}
}
