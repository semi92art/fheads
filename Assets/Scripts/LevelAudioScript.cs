using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelAudioScript : MonoBehaviour 
{
    [SerializeField]
	private Scripts scr;

    public Animator anim_SoundOn;
    public bool isSoundOn;

    private AudioSource[] audSources;
	public AudioSource goalSource;
	public AudioSource playerJumpSource, enemyJumpSource;
	public AudioSource ballTouchSource;
	public AudioSource menuButtonsSource;
	public AudioSource moneyWinSource, moneyWinSource1;
	public AudioSource ticTocSource;
	public AudioSource refereeWhistleSource;
	public AudioSource longWhistle;
	public AudioSource kickSource;
    public AudioSource bonus_1, bonus_2;
    public AudioSource molniya;
    public AudioSource timeSlow_In, timeSlow_Out;
    [Header("Goal Clips:")]
    public AudioClip goalClip1;
    public AudioClip goalClip2;

	[HideInInspector]
	public bool goal;
	[HideInInspector]
	public bool isPlayingGoalClip;
	[HideInInspector]
	public bool isPlayingJumpClipPlayer, isPlayingJumpClipEnemy;

	private int timerGoal;
	private int timerJumpPlayer, timerJumpEnemy;
	private int timerBallTouch;
    private int reset;
    private float maxTribVolume;
    private AudioSource tribSource;
    public AudioSource[] _tribunes;
    private int tribInd;

    void Awake()
    {
        maxTribVolume = scr.alPrScr.tribunes == 0 ? 0.1f : 0.25f;
        SetTribunesSource();
        _tribunes[tribInd].volume = 0.05f;
    }

    private void SetTribunesSource()
    {
        int trib = scr.alPrScr.tribunes;

        if (trib == 0)
        {
	        _tribunes[0].enabled = false;
	        _tribunes[1].enabled = false;
	        _tribunes[2].enabled = false;
	        goalSource.enabled = false;
	        molniya.enabled = false;
	        tribInd = 0;
        } 
        else if (trib == 3)
        {
            _tribunes[0].enabled = true;
            _tribunes[1].enabled = false;
            _tribunes[2].enabled = false;
            tribInd = 0;
        }
        else if (trib == 1 || trib == 4)
        {
            _tribunes[0].enabled = false;
            _tribunes[1].enabled = true;
            _tribunes[2].enabled = false;
            tribInd = 1;
        }
        else if (trib == 2 || trib == 5)
        {
            _tribunes[0].enabled = false;
            _tribunes[1].enabled = false;
            _tribunes[2].enabled = true;
            tribInd = 2;
        }
    }

	void Update()
	{
		BallTouchSound();
        Attenuation_TribunesSound();
	}
		
	private void JumpSoundPlayer()
	{
		if (scr.pMov.jump)
		{
			timerJumpPlayer ++;

			if (timerJumpPlayer == 1)
			{
                if (isSoundOn)
				    playerJumpSource.Play();
			}
			else 
			{
				if (timerJumpPlayer != 0)
				{
                    timerJumpPlayer = timerJumpPlayer > 20 ? 0 : timerJumpPlayer + 1;
				}
			}
		} 
		else 
		{
			if (timerJumpPlayer != 0)
			{
				timerJumpPlayer ++;

				if (timerJumpPlayer >= 20)
					timerJumpPlayer = 0;
			}
		}
	}
		
	private void JumpSoundEnemy()
	{
		if (scr.jScr.jump)
		{
			timerJumpEnemy ++;

			if (timerJumpEnemy == 1)
			{
                if (isSoundOn)
				    enemyJumpSource.Play ();
			} 
			else
			{
				if (timerJumpEnemy != 0)
				{
					timerJumpEnemy ++;

					if (timerJumpEnemy >= 20)
						timerJumpEnemy = 0;
				}
			}
		} 
		else
		{
			if (timerJumpEnemy != 0)
			{
				timerJumpEnemy ++;

				if (timerJumpEnemy >= 20)
					timerJumpEnemy = 0;
			}
		}
	}

	public void BallTouchSound()
	{
		if (scr.ballScr.transform.position.y < scr.marks.topBarTr.position.y)
			reset = 10;
		else
			reset = 5;

        if (scr.ballScr.transform.gameObject.activeSelf) 
		{
			if (scr.ballTScr.isCollision)
			{
				if (timerBallTouch == 0)
					timerBallTouch ++;

				if (timerBallTouch == 1)
				{
					if (scr.ballTScr.velMagnitude > 20)
						ballTouchSource.volume = 1 + 0.2f;
					else
						ballTouchSource.volume = scr.ballTScr.velMagnitude * 0.02f * (1 + 0.2f);

                    if (isSoundOn)
						ballTouchSource.Play();
				}
				else
				{
					if (timerBallTouch != 0)
					{
						timerBallTouch ++;

						if (timerBallTouch > reset)
							timerBallTouch = 0;
					}
				}
			} 
			else
			{
				if (timerBallTouch != 0)
				{
					timerBallTouch ++;

					if (timerBallTouch > reset)
						timerBallTouch = 0;
				}
			}	
		}
	}

    public void EnableSound(int isAwake)
    {
        audSources = FindObjectsOfType<AudioSource>();
        isSoundOn = isAwake == 1 ? isSoundOn : !isSoundOn;
        int isSoundOn_int = isSoundOn ? 1 : 0;

        anim_SoundOn.SetTrigger(
            Animator.StringToHash(isAwake.ToString() + isSoundOn_int.ToString()));

        if (isAwake == 0)
            PlayerPrefs.SetInt("SoundOn", isSoundOn_int);

        for (int i = 0; i < audSources.Length; i++)
            audSources[i].mute = !isSoundOn;

        if (scr.alPrScr.tribunes == 1 || scr.alPrScr.tribunes == 5)
        {
            AudioSource[] audS =
                scr.rainMan.rainScr.gameObject.GetComponents<AudioSource>();

            for (int i = 0; i < audS.Length; i++)
            {
                audS[i].mute = !isSoundOn;
            }
        }
    }

    public void Button_Sound()
    {
        if (!menuButtonsSource.mute && menuButtonsSource.enabled)
            menuButtonsSource.Play();
    }

    private void Attenuation_TribunesSound()
    {
        if (!scr.tM.isGoldenGoal)
        {
            if (TimeManager.resOfGame == 0)
            {
                if (_tribunes[tribInd].volume < maxTribVolume)
                    _tribunes[tribInd].volume *= 1.001f;
            }
            else
            {
                if (_tribunes[tribInd].volume > 0.05f)
                    _tribunes[tribInd].volume *= 0.99f;
            }
        }
        else
        {
            if (_tribunes[tribInd].volume > 0.005f)
                _tribunes[tribInd].volume *= 0.997f;
        }
    }
}
