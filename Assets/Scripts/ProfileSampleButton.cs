using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProfileSampleButton : MonoBehaviour 
{
    public GameObject lockedObj;
	public Button button;
	public Text nameLabel;
    public Image im_MoneyCoastShowcase;
	public Image icon;
	public Text moneyCoast;
	public Image showcase, showcase_2;
	public Image leg;
    public bool isOpened;
    //[HideInInspector]
    public int plInd;
    //[HideInInspector]
    public int cntrInd;
    //[HideInInspector]
    public int buttonInd;
    
    // Skills:
    public Image im_Speed;
    public Image im_Kick;
    public Image im_Jump;

	void Start()
	{
		transform.localScale = new Vector3 (1, 1, 1);

	}
}
