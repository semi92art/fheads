using UnityEngine;
using System.Collections;
using System.Threading;

//this script used for test purpose ,it do by default 1000 logs  + 1000 warnings + 1000 errors
//so you can check the functionality of in game logs
//just drop this scrip to any empty game object on first scene your game start at
public class TestReporter : MonoBehaviour {
	
	public int logTestCount = 100 ;
	public int threadLogTestCount = 100 ;
	public bool logEverySecond = true;
	int currentLogTestCount;
	//Reporter reporter ;
	GUIStyle style ;
	/*Rect rect1 ;
	Rect rect2 ;
	Rect rect3 ;
	Rect rect4 ;
	Rect rect5 ;
	Rect rect6 ;*/
	// Use this for initialization
	void Start () {
		Application.runInBackground = true ;

		//reporter = FindObjectOfType( typeof(Reporter)) as Reporter ;
		
		style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter ;
		style.normal.textColor = Color.white ;
		style.wordWrap = true ;

		/*rect1 = new Rect (Screen.width/2-120, Screen.height/2-225, 240, 50) ;
		rect2 = new Rect (Screen.width/2-120, Screen.height/2-175, 240, 100) ;
		rect3 = new Rect (Screen.width/2-120, Screen.height/2-50, 240, 50) ;
		rect4 = new Rect (Screen.width/2-120, Screen.height/2, 240, 50) ;
		rect5 = new Rect (Screen.width/2-120, Screen.height/2+50, 240, 50) ;
		rect6 = new Rect (Screen.width/2-120, Screen.height/2+100, 240, 50) ;*/

		Thread thread = new Thread( new ThreadStart( threadLogTest ));
		thread.Start();
	}

	void threadLogTest()
	{
		for( int i = 0 ; i < threadLogTestCount ; i ++ )
		{
			Debug.Log("Test Log from Thread");
			Debug.LogWarning("Test Warning from Thread");
			Debug.LogError("Test Error from Thread");
		}
	}

	float elapsed;

	void Update () 
	{
		//int drawn = 0;
		 
		/*elapsed += Time.deltaTime;
		if( elapsed >= 1)
		{
			elapsed = 0;
			Debug.Log("One Second Passed");
		}*/
	}
}
