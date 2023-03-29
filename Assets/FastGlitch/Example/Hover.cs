using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Mathf.Cos(Time.realtimeSinceStartup) * 10, 180 + Mathf.Sin(Time.realtimeSinceStartup * 2) * 10, Mathf.Sin(Time.realtimeSinceStartup * 2) *10), Time.deltaTime); 
        transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Cos(Time.realtimeSinceStartup * 2), Mathf.Sin(Time.realtimeSinceStartup * 2) + 1f, transform.position.z), Time.deltaTime);
    }
}
