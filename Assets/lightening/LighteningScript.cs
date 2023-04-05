using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LighteningScript : MonoBehaviour
{
    [Header("General Settings")]
    public GameObject target;

    [Header("Material Settings")]
    public Material mat;
    public int columns = 2;
    public int rows = 8;
    public float framesPerSecond = 10f;
    public bool randomize = false;

    [Header("Line Renderer Settings")]
    public int points = 5;
    public float pointsDisplacement = 0.1f;
    public float lineScale = 1f;
    public bool distanceBasedDisplacement = true;
    public bool zDisplacement = false;

    private int max;
    private int index = 0; //the current frame to display

    //[SerializeField]
    public bool run;
    public LineRenderer rend;
    private Vector2[] offsets;
    private Vector2 size;

    void Start()
    {
        Initialize();
        StartCoroutine(updateTiling());
    }

    public void Lightnin_OnOff()
    {
        run = !run;
        rend.enabled = run;
    }

    public void LightningOff()
    {
        run = false;
        rend.enabled = false;
    }

    public void LightningOn()
    {
        run = true;
        rend.enabled = true;
    }

    void OnDisable()
    {
        if (rend != null)
            rend.enabled = false;
        run = false;
    }

    void OnEnable()
    {
        if (rend != null) {
            rend.enabled = true;
            Initialize();
            //StartCoroutine(updateTiling());
        }
    }

    public void Initialize()
    {
        max = rows * columns;
        // add Line renderer

        //if (rend == null)
        //  rend = gameObject.AddComponent<LineRenderer>() as LineRenderer; 

        //rend.SetVertexCount(points);
        //apply material
        rend.material = mat;
        //set the tile size of the texture
        size = new Vector2(1f / columns, 1f / rows);
        mat.SetTextureScale("_MainTex", size);
        //get offsets array (tile offsets for randomization)
        GetRandomOffsets();
        //run = true;
        rend.enabled = false;
        rend.sortingLayerName = "Foreground";
    }

    void Update()
    {
        if (run)
        {
            //move to the next index
            index++;
            if (index >= rows * columns)
                index = 0;

            //rend.SetVertexCount(points);
            //rend.SetWidth(lineScale,lineScale);
            rend.SetPosition(0,transform.position);
            rend.SetPosition(points-1,target.transform.position);

            // if more than 2 points than we can randomize point positions
            if (points >= 3)
            {
                for (int i = 1; i < points-1; i++) 
                {
                    float scale = (float)i / (points-1);
                    var pos = Vector3.Lerp(transform.position,target.transform.position,scale);

                    // random values depend on distance to target
                    if (distanceBasedDisplacement){
                        float distance = Vector3.Distance(transform.position,target.transform.position);
                        distance = distance * pointsDisplacement / points;
                        pos.y += Random.Range(-distance,distance);
                        pos.x += Random.Range(-distance,distance); //uncomment this line if you want X displacement
                        if (zDisplacement){
                            pos.z += Random.Range(-distance,distance);
                        }
                    }
                    else
                    {
                        pos.y += Random.Range(-pointsDisplacement,pointsDisplacement);
                        pos.x += Random.Range(-pointsDisplacement,pointsDisplacement);
                        if (zDisplacement){
                            pos.z += Random.Range(-pointsDisplacement,pointsDisplacement);
                        }
                    }
                    rend.SetPosition(i,pos);
                }
            }
            // this picks random tile animation for each frame and randomly flips tile vertically or horizontally
            if (randomize)
            {
                if (Random.Range(0,1) == 0)
                {
                    size.y = size.y * -1;
                    if (Random.Range(0,1) == 0)
                        size.x = size.x * -1;
                }
                Vector2 offset = offsets[Random.Range(0,max)];
                mat.SetTextureOffset("_MainTex", offset);
            }
            else
            {
                Vector2 offset = new Vector2((float)index / columns - (index / columns), //x index
                    (index / columns) / (float)rows);          //y index

                mat.SetTextureOffset("_MainTex", offset);
            }
            rend.sortingLayerName = "New Layer 2";
            rend.sortingOrder = 500;
            //if (run)
            //    yield return new WaitForSeconds(1f / framesPerSecond);
        }
    }

    private void GetRandomOffsets()
    {
        // get positions of all tiles
        int i = 0;
        offsets = new Vector2[rows * columns];
        while (i < max)
        {
            //split into x and y indexes
            Vector2 offset = new Vector2((float)i / columns - (i / columns), //x index
                (i / columns) / (float)rows);          //y index

            offsets[i] = offset;
            i++;
        }
    }



    // this can be done on update or fixed update
    // coroutine ensures easier delay (fps) management 
    private IEnumerator updateTiling()
    {
        while (run)
        {
            //move to the next index
            index++;
            if (index >= rows * columns)
                index = 0;

            rend.SetVertexCount(points);
            rend.SetWidth(lineScale,lineScale);
            rend.SetPosition(0,transform.position);
            rend.SetPosition(points-1,target.transform.position);

            // if more than 2 points than we can randomize point positions
            if (points >= 3)
            {
                for (int i = 1; i < points-1; i++) 
                {
                    float scale = (float)i / (points-1);
                    var pos = Vector3.Lerp(transform.position,target.transform.position,scale);

                    // random values depend on distance to target
                    if (distanceBasedDisplacement){
                        float distance = Vector3.Distance(transform.position,target.transform.position);
                        distance = distance * pointsDisplacement / points;
                        pos.y += Random.Range(-distance,distance);
                        pos.x += Random.Range(-distance,distance); //uncomment this line if you want X displacement
                        if (zDisplacement){
                            pos.z += Random.Range(-distance,distance);
                        }
                    }
                    else
                    {
                        pos.y += Random.Range(-pointsDisplacement,pointsDisplacement);
                        pos.x += Random.Range(-pointsDisplacement,pointsDisplacement);
                        if (zDisplacement){
                            pos.z += Random.Range(-pointsDisplacement,pointsDisplacement);
                        }
                    }
                    rend.SetPosition(i,pos);
                }
            }
            // this picks random tile animation for each frame and randomly flips tile vertically or horizontally
            if (randomize)
            {
                if (Random.Range(0,1) == 0)
                {
                    size.y = size.y * -1;
                    if (Random.Range(0,1) == 0)
                        size.x = size.x * -1;
                }
                Vector2 offset = offsets[Random.Range(0,max)];
                mat.SetTextureOffset("_MainTex", offset);
            }
            else
            {
                Vector2 offset = new Vector2((float)index / columns - (index / columns), //x index
                    (index / columns) / (float)rows);          //y index

                mat.SetTextureOffset("_MainTex", offset);
            }
            rend.sortingLayerName = "New Layer 2";
            rend.sortingOrder = 500;
            if (run)
                yield return new WaitForSeconds(1f / framesPerSecond);
        }
    }
}