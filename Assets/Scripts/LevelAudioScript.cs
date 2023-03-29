using UnityEngine;
using System.Collections;

public class LevelAudioScript : MonoBehaviour 
{
	public Scripts scr;

	public AudioSource goalSource;
	public AudioSource playerJumpSource, enemyJumpSource;
	public AudioSource ballTouchSource;
	public AudioSource menuButtonsSource;
	public AudioSource moneyWinSource, moneyWinSource1;
	public AudioSource mainThemeSource;
	public AudioSource ticTocSource;
	public AudioSource refereeWhistleSource;

	public AudioClip goalClip1, goalClip2;
	public JumpScript jScr;
	[HideInInspector]
	public bool goal;
	[HideInInspector]
	public bool isPlayingGoalClip;
	[HideInInspector]
	public bool isPlayingJumpClipPlayer, isPlayingJumpClipEnemy;

	private int timerGoal;
	private int timerJumpPlayer, timerJumpEnemy;
	private int timerBallTouch;
	private GameObject ball;

	void Awake()
	{
		ball = scr.ballScr.gameObject;
	}
	
	void Update()
	{
		JumpSoundPlayer();
		JumpSoundEnemy();
		BallTouchSound();
	}


	public void CongradulateMusic()
	{
		if (Score.score >= Score.score1)
			mainThemeSource.gameObject.SetActive(true);
	}
		
	private void JumpSoundPlayer()
	{
		if (scr.pMov.jump)
		{
			timerJumpPlayer ++;

			if (timerJumpPlayer == 1)
			{
				if (scr.alPrScr.soundVolume != 0)
					playerJumpSource.Play();
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
		if (jScr.jump)
		{
			timerJumpEnemy ++;

			if (timerJumpEnemy == 1)
			{
				if (scr.alPrScr.soundVolume != 0)
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
		if (ball.activeSelf) 
		{
			if (scr.ballTScr.isCollision)
			{
				if (timerBallTouch == 0)
					timerBallTouch ++;

				if (timerBallTouch == 1)
				{
					if (scr.ballTScr.velMagnitude > 20)
						ballTouchSource.volume = scr.objLev.soundSlider.value + 0.2f;
					else
						ballTouchSource.volume = scr.ballTScr.velMagnitude * 0.02f * (scr.objLev.soundSlider.value + 0.2f);

					if (scr.alPrScr.soundVolume != 0)
						ballTouchSource.Play();
				}
				else
				{
					if (timerBallTouch != 0)
					{
						timerBallTouch ++;

						if (timerBallTouch >= 20)
							timerBallTouch = 0;
					}
				}
			} 
			else
			{
				if (timerBallTouch != 0)
				{
					timerBallTouch ++;

					if (timerBallTouch >= 20)
						timerBallTouch = 0;
				}
			}
				
		}
	}
}
