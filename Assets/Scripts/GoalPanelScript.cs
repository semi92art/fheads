using UnityEngine; 
using System.Collections;
using UnityEngine.UI;

public class GoalPanelScript : MonoBehaviour 
{
    [SerializeField]
	private Scripts scr;
    [Header("Refferee Animator:")]
    public Animator refAnim;
    [Header("Star Animators:")]
	public Animator[] starsAnim;
    [Header("Star Images:")]
	public Image[] starsIm;


    public void PlayStarsAnim()
    {
        //Debug.Log("Play Stars Anim");
        string callB;

        for (int i = 0; i < starsAnim.Length; i++)
        {
            starsIm[i].gameObject.SetActive(true);
			starsIm[i].enabled = true;
			starsAnim[i].enabled = true;

            callB = "call" + i.ToString();
            starsAnim[i].ResetTrigger(callB);
            starsAnim[i].SetTrigger(callB);
        }
    }

    public void RefereeAnimRight()
    {
        //if (!isCalled)
        //{
            scr.goalPanScr.refAnim.SetTrigger("right");
            //isCalled = true;   
        //}
    }

    public void RefereeAnimLeft()
    {
        //if (!isCalled)
        //{
            scr.goalPanScr.refAnim.SetTrigger("left");
            //isCalled = true;   
        //}
    }

    /*public void RefereeAnimBack()
    {
        if (isCalled)
        {
            scr.goalPanScr.refAnim.SetTrigger("back");
            isCalled = false;
        }  
    }*/




    /*public void StopStarsAnim()
    {
        for (int i = 0; i < starsAnim.Length; i++)
		{
			starsAnim[i].ResetTrigger("back");
			starsAnim[i].SetTrigger("back");
			starsAnim[i].enabled = false;
			starsIm[i].enabled = false;
		}  
    }*/
}
