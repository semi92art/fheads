using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class CameraSize : MonoBehaviour
{
    private enum CameraType
    {
        Small,
        Big
    }

    #region private fields
    
    private CameraType m_CameraType;
    
    #endregion
    
    public Scripts scr;

    
    
    
    public GameObject[] obj_CamButtons;

    public Image[] im_GameButtons;

    public LineRenderer ballLineR;
    public GameObject obj_FireTrail;
    public RectTransform rTr_Circle;


    public Transform roofTr, roof1Tr;
    public Transform stadiumsTr;

    public float lerpX;

    private float defY;
    private Transform followTr;
    private Camera cam;

    public Vector3 camDefPos;
    private Vector3 camDefRot;
    private float resMy0, resMy0_1;
    [HideInInspector]
    public Camera _cam;

    private float leftCamEdge, rightCamEdge, topCamEdge;
    private float newAng0, newAng;
    [Range(0.0f, 0.1f)]
    public float angCoeff;
    public float tim;
    private bool isFromButton;


    void Awake()
    {
        _cam = GetComponent<Camera>();
        cam = GetComponent<Camera>();

        followTr = scr.pMov.transform;
        float screenW = (float)Screen.width;
        float screenH = (float)Screen.height;
        resMy0_1 = screenW / screenH;
        resMy0 = 14f / 10f;

        SetCameraType(PrefsManager.Instance.CameraType);
        SetGraphics(1);

        camDefRot = transform.localRotation.eulerAngles;
    }

    void Update()
    {
        tim += Time.deltaTime;

        if (tim > PlayerMovement.restartDelay2 &&
            tim < PlayerMovement.restartDelay1)
            transform.position = camDefPos;
        else
        {
            if ((!scr.pMov.restart && tim > PlayerMovement.restartDelay1) ||
                scr.pMov.restart)
                CameraTransform();
        }
    }

    private void CameraTransform()
    {
        Vector3 position = transform.position;
        Vector3 playerPosition = scr.pMov.transform.position;
        Vector3 ballPosition = scr.ballScr.transform.position;
        
        if (scr.objLev.isTiltOn)
        {
            angCoeff = scr.objLev.isTiltOn ? 0.03f : 0f;
            newAng = scr.pMov._rb.velocity.x * angCoeff;

            if (playerPosition.x > scr.marks.rightTiltEdgeTr.position.x ||
                playerPosition.x < scr.marks.leftTiltEdgeTr.position.x)
                newAng = 0;

            transform.rotation = Quaternion.AngleAxis(
                newAng,
                Vector3.forward);
        }

        float newX = Mathf.Clamp(0.5f * (followTr.position.x + ballPosition.x), leftCamEdge, rightCamEdge);
        newX = Mathf.Lerp(position.x, newX, lerpX * Time.deltaTime);
        
        float newY = 0f;
        switch (m_CameraType)
        {
            case CameraType.Big:
                newY = -14.78f * resMy0 + 34.3f;
                break;
            case CameraType.Small:
                newY = Mathf.Clamp(0.5f * (followTr.position.y + ballPosition.y), 10f, topCamEdge);
                newY = Mathf.Lerp(position.y, newY, lerpX * Time.deltaTime * 5f);
                break;
        }
        
        transform.position = new Vector3(newX, newY, position.z);
    }

    public void SetGraphics(int fromAwake)
    {
        float posX = 0f;
        int graph_1 = 0;

        if (fromAwake == 0)
        {
            PrefsManager.Instance.GraphicsQuality++;
            rTr_Circle.GetComponent<Animator>().SetTrigger(
                PrefsManager.Instance.GraphicsQuality.ToString());
        }
        else
        {
            rTr_Circle.anchoredPosition = new Vector2(
                posX,
                rTr_Circle.anchoredPosition.y);
        }

        SetGraphics_0(PrefsManager.Instance.GraphicsQuality);

        if (scr.rainMan.isRain)
            scr.rainMan.SetRain_On();
    }

    private void SetGraphics_0(int _case)
    {
        ballLineR.enabled = false;
        obj_FireTrail.SetActive(true);
    }
        
    public void SetCameraDefaultPosition()
    {
        transform.position = camDefPos;
        transform.rotation = Quaternion.Euler(camDefRot);
    }

    public void SetCameraPositionForCongrPan()
    {
        transform.position = new Vector3(
            -20f,
            transform.position.y,
            transform.position.z);

        transform.rotation = Quaternion.Euler(camDefRot);
    }

    public void SetCameraType(int _Type)
    {
        for (int i = 0; i < obj_CamButtons.Length; i++)
        {
            if (i == _Type)
                obj_CamButtons[i].SetActive(true);
            else
                obj_CamButtons[i].SetActive(false);
        }

        float newY = 0f;
        float newY0 = 0f;

        if (_Type == 0)
        {
            m_CameraType = CameraType.Big;
            //newY0 = -14.78f * resMy0 + 34.3f;
            newY0 = 13.6f;
            cam.orthographicSize = 28.42f;

            leftCamEdge = 27.78f * resMy0_1 - 99.22f;
            rightCamEdge = -27.78f * resMy0_1 + 59.17f;
        }
        else if (_Type == 1)
        {
            m_CameraType = CameraType.Small;
            //newY = 0.5f * (followTr.position.y + scr.ballScr.transform.position.y);
            //newY0 = Mathf.Lerp(transform.position.y, newY, lerpX * Time.deltaTime * 5f);
            newY0 = 10.4f;
            cam.orthographicSize = 25f;

            leftCamEdge = 25.56f * resMy0_1 - 100.3f;
            rightCamEdge = -25.56f * resMy0_1 + 60.3f;
            topCamEdge = 2.78f * resMy0_1 + 12.28f;
        }

        transform.position = new Vector3(
            transform.position.x,
            newY0,
            transform.position.z);

        camDefPos = !isFromButton ? 
            transform.position : new Vector3(camDefPos.x, newY0, camDefPos.z);

        isFromButton = true;

        PrefsManager.Instance.CameraType = _Type;
    }
}