using UnityEngine;

public class CameraSize : MonoBehaviour
{
	public Transform overviewCamTextureTr;
	public Transform buttonsObj;
	
	void Awake()
	{
		Vector3 overviewCamTexturePos = overviewCamTextureTr.position;
		Vector3 butsPos = buttonsObj.transform.position;

		Camera cam = GetComponent<Camera>();

		float screenW = (float)Screen.width;
		float screenH = (float)Screen.height;
		float resMy0 = screenW/screenH;

		cam.orthographicSize = 22.9f/resMy0;

		cam.transform.position = new Vector3(
			cam.transform.position.x,
			cam.transform.position.y + (cam.orthographicSize / 2.0f - 22.9f/(16.0f/9.0f * 2.0f)) * 2.0f,
			cam.transform.position.z);

		buttonsObj.transform.position = new Vector3(butsPos.x, butsPos.y, butsPos.z);
		overviewCamTextureTr.position = overviewCamTexturePos;
	}
}
