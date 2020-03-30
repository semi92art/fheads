using UnityEngine;

public class LevelSounds : MonoBehaviour 
{
	#region public fields
	
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
	public AudioSource[] _tribunes;

	public bool SoundOn { get; set; }


	#endregion
	
	#region attributes
	
    [SerializeField]
	private Scripts scr;
	private AudioSource[] audSources;
    private int timerGoal;
	private int timerJumpPlayer, timerJumpEnemy;
	private int timerBallTouch;
    private int reset;
    private readonly float maxTribVolume = 0.25f;
    private AudioSource tribSource;
    
    private int tribInd;
    
    #endregion
    
    #region constructors

    // public static LevelSounds Create(Audio _GoalSource,
	   //  string _JumpSource,
	   //  string _BallTouchSource,
	   //  string _MenuButtonsSource,
	   //  string _MoneyWinSource,
	   //  string _TicTocSource,
	   //  string _WhistleSource,
	   //  string _LongWhistleSource,
	   //  string _KickSource,
	   //  string _Bonus1Source,
	   //  string _Bonus2Source,
	   //  string _LightningSource,
	   //  string _TribuneSource1,
	   //  string _TribuneSource2,
	   //  string _TribuneSource3,
	   //  string _MixerSource
	   //  )
    // {
	   //  
    // }
    
    #endregion

    #region engine methods
    
    void Awake()
    {
	    SetTribunesSource();
        _tribunes[tribInd].volume = 0.05f;
    }
    
    void Update()
    {
	    BallTouchSound();
	    JumpSoundPlayer();
	    JumpSoundEnemy();
	    Attenuation_TribunesSound();
    }
    
    #endregion
    
    #region private methods
    
    private void SetTribunesSource()
    {
	    int trib = PrefsManager.Instance.Tribunes;
	    _tribunes[0].enabled = trib == 3;
	    _tribunes[1].enabled = trib == 1 || trib == 4;
	    _tribunes[2].enabled = trib == 2 || trib == 5;
	    goalSource.enabled = trib != 0;
	    molniya.enabled = trib != 0;
	    tribInd = trib == 0 || trib == 3 ? 0 : trib == 1 || trib == 4 ? 1 : 2;
    }

	
		
    private void JumpSoundPlayer()
    {
	    if (Player.Instance.jump)
	    {
		    timerJumpPlayer++;
		    if (timerJumpPlayer == 1 && SoundOn)
			    playerJumpSource.Play();
		    else if (timerJumpPlayer != 0)
			    timerJumpPlayer = timerJumpPlayer > 20 ? 0 : timerJumpPlayer + 1;
	    } 
	    else if (timerJumpPlayer != 0)
	    {
		    timerJumpPlayer ++;
		    if (timerJumpPlayer >= 20)
			    timerJumpPlayer = 0;
	    }
    }
		
    private void JumpSoundEnemy()
    {
	    if (scr.jScr.jump)
	    {
		    timerJumpEnemy++;
		    if (timerJumpEnemy == 1 && SoundOn)
			    enemyJumpSource.Play();
		    else if (timerJumpEnemy != 0)
		    {
			    timerJumpEnemy ++;
			    if (timerJumpEnemy >= 20)
				    timerJumpEnemy = 0;
		    }
	    } 
	    else if (timerJumpEnemy != 0)
	    {
		    timerJumpEnemy++;
		    if (timerJumpEnemy >= 20)
			    timerJumpEnemy = 0;
	    }
    }
    
    private void Attenuation_TribunesSound()
    {
	    if (!scr.tM.isGoldenGoal && LevelTimeManager.resOfGame == 0)
	    {
		    if (_tribunes[tribInd].volume < maxTribVolume)
			    _tribunes[tribInd].volume *= 1.001f;
	    }
	    else if (!scr.tM.isGoldenGoal && LevelTimeManager.resOfGame != 0)
	    {
		    if (_tribunes[tribInd].volume > 0.05f)
			    _tribunes[tribInd].volume *= 0.99f;
	    }
	    else if (scr.tM.isGoldenGoal)
	    {
		    if (_tribunes[tribInd].volume > 0.005f)
			    _tribunes[tribInd].volume *= 0.997f;
	    }
    }
    
    #endregion

    #region public methods
    
    public void BallTouchSound()
    {
	    reset = scr.ballScr.transform.position.y < scr.marks.topBarTr.position.y ? 10 : 5;

	    if (scr.ballScr.transform.gameObject.activeSelf) 
	    {
		    if (scr.ballTScr.isCollision)
		    {
			    if (timerBallTouch == 0)
				    timerBallTouch++;
			    if (timerBallTouch == 1)
			    {
				    ballTouchSource.volume = scr.ballTScr.velMagnitude > 20 ? 
					    1 + 0.2f : scr.ballTScr.velMagnitude * 0.02f * (1 + 0.2f);
				    if (SoundOn)
					    ballTouchSource.Play();
			    }
			    else if (timerBallTouch != 0)
			    {
				    timerBallTouch ++;
				    if (timerBallTouch > reset)
					    timerBallTouch = 0;
			    }
		    } 
		    else if (timerBallTouch != 0)
		    {
			    timerBallTouch++;
			    if (timerBallTouch > reset)
				    timerBallTouch = 0;
		    }	
	    }
    }

    public void EnableSound(int isAwake)
    {
	    audSources = FindObjectsOfType<AudioSource>();
	    SoundOn = isAwake == 1 ? SoundOn : !SoundOn;
	    
	    if (isAwake == 0)
		    PrefsManager.Instance.SoundOn = SoundOn;

	    for (int i = 0; i < audSources.Length; i++)
		    audSources[i].mute = !SoundOn;

	    if (PrefsManager.Instance.Tribunes == 1 || PrefsManager.Instance.Tribunes == 5)
	    {
		    AudioSource[] audS = scr.rainMan.rainScr.gameObject.GetComponents<AudioSource>();
		    for (int i = 0; i < audS.Length; i++)
			    audS[i].mute = !SoundOn;
	    }
    }

    public void Button_Sound()
    {
	    if (!menuButtonsSource.mute && menuButtonsSource.enabled)
		    menuButtonsSource.Play();
    }
    
    #endregion


    public void PlayGoal()
    {
	    goalSource.Play();
    }
}
