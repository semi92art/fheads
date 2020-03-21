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
    private int graph;


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

        graph = PlayerPrefs.GetInt("Graph");
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
        if (scr.objLev.isTiltOn)
        {
            angCoeff = scr.objLev.isTiltOn ? 0.03f : 0f;
            newAng = scr.pMov._rb.velocity.x * angCoeff;

            if (scr.pMov.transform.position.x > scr.marks.rightTiltEdgeTr.position.x ||
                scr.pMov.transform.position.x < scr.marks.leftTiltEdgeTr.position.x)
                newAng = 0;

            transform.rotation = Quaternion.AngleAxis(
                newAng,
                Vector3.forward);
        }

        float newX = 0.5f * (followTr.position.x + scr.ballScr.transform.position.x);
        float newY = 0f;

        switch (m_CameraType)
        {
            case CameraType.Small:
                newY = 0.5f * (followTr.position.y + scr.ballScr.transform.position.y);
                newY = newY > topCamEdge ? topCamEdge : newY < 10f ? 10f : newY;
                newY = Mathf.Lerp(transform.position.y, newY, lerpX * Time.deltaTime * 5f);
                break;
            case CameraType.Big:
                newY = -14.78f * resMy0 + 34.3f;
                break;
        }

        Vector3 position = scr.ballScr.transform.position;
        float orthographicSize = cam.orthographicSize;
        newX = Mathf.Clamp(newX, position.x - orthographicSize,
            position.x + orthographicSize);
        
        newX = newX > rightCamEdge ? rightCamEdge : newX < leftCamEdge ? leftCamEdge : newX;

        Vector3 position1 = transform.position;
        position1 = new Vector3(
            Mathf.Lerp(position1.x, newX, lerpX * Time.deltaTime),
            newY,
            position1.z);
        transform.position = position1;
    }

    public void SetGraphics(int fromAwake)
    {
        float posX = 0f;
        int graph_1 = 0;

        if (fromAwake == 0)
        {
            graph = graph == 2 ? 0 : graph + 1;
            PlayerPrefs.SetInt("Graph", graph);
            graph_1 = Animator.StringToHash(graph.ToString());
            rTr_Circle.GetComponent<Animator>().SetTrigger(graph_1);
        }
        else
        {
            rTr_Circle.anchoredPosition = new Vector2(
                posX,
                rTr_Circle.anchoredPosition.y);
        }

        SetGraphics_0(graph);

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
            //newY0 = -14.78f * resMy0 + 34.3f;
            newY0 = 13.6f;
            cam.orthographicSize = 28.42f;

            leftCamEdge = 27.78f * resMy0_1 - 99.22f;
            rightCamEdge = -27.78f * resMy0_1 + 59.17f;
        }
        else if (_Type == 1)
        {
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