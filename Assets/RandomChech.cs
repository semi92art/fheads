using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomChech : MonoBehaviour 
{
    public Text[] text_Percents;
    private float[] percents = new float[10];
    public int[] nums;
    [SerializeField]
    private int total = 0;

    void Update()
    {
        
        int rand = Mathf.FloorToInt(Random.value * (10f - 0.01f));

        for (int i = 0; i < 10; i++)
        {
            if (rand == i)
                nums[i]++;
        }

        //for (int i = 0; i < 10; i++)
            total++;

        for (int i = 0; i < 10; i++)
        {
            percents[i] = 100f * (float)nums[i] / (float)total;
            text_Percents[i].text = percents[i].ToString("N1");
        }
    }
}
