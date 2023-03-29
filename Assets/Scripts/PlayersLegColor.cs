using UnityEngine;
//using System.Collections;
using System.Collections.Generic;

public class PlayersLegColor : MonoBehaviour 
{
	[System.Serializable]
	public class FlagColors
	{
		public string name;
		public Color flagColor1, flagColor2;
		public Sprite legSpr;
		public Sprite flagSpr;
	}

	public List<FlagColors>FlagColorsList;
}
