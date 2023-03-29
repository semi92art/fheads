using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanguageScript : MonoBehaviour 
{
	public Scripts scr;

	[System.Serializable]
	public class StaticTextList
	{
		public string russian;
		public string spanish;
		public string french;
		public string deutch;
		public Text text;
	}

	[System.Serializable]
	public class CountriesTextList
	{
		public string english;
		public string russian;
		public string spanish;
		public string french;
		public string deutch;
	}

	public List<StaticTextList> staticTextL;
	public List<CountriesTextList> countriesTextL;
	private bool langChange;

	void Awake()
	{
		SetCountriesLanguage ();

		if (SceneManager.GetActiveScene().name == "Start")
			langChange = true;
		else
			langChange = scr.alPrScr.langChange;

		if ((Application.systemLanguage == SystemLanguage.Russian ||
			Application.systemLanguage == SystemLanguage.Ukrainian ||
			Application.systemLanguage == SystemLanguage.Belarusian) && langChange)
		{
			for (int i = 0; i < staticTextL.Count; i++) 
			{
				if (staticTextL [i].text != null)
					staticTextL [i].text.text = staticTextL [i].russian;
			}
		}
		else if (Application.systemLanguage == SystemLanguage.Spanish)
		{
			for (int i = 0; i < staticTextL.Count; i++) 
			{
				if (staticTextL [i].text != null)
					staticTextL [i].text.text = staticTextL [i].spanish;
			}
		}
	}

	private void SetCountriesLanguage()
	{
		/*while (countriesTextL.Count < 39)
		{
			CountriesTextList item = null;
			countriesTextL.Add(item);
		}*/
			
		countriesTextL [0].english = "Algeria";
		countriesTextL [0].russian = "Алжир";
		countriesTextL [0].spanish = "Algeria";

		countriesTextL [1].english = "Argentina";
		countriesTextL [1].russian = "Аргентина";
		countriesTextL [1].spanish = "Argentina";

		countriesTextL [2].english = "Australia";
		countriesTextL [2].russian = "Австралия";
		countriesTextL [2].spanish = "Australia";

		countriesTextL [3].english = "Belgium";
		countriesTextL [3].russian = "Бельгия";
		countriesTextL [3].spanish = "Belgica";

		countriesTextL [4].english = "Bosnia";
		countriesTextL [4].russian = "Босния";
		countriesTextL [4].spanish = "Bosnia";

		countriesTextL [5].english = "Brazil";
		countriesTextL [5].russian = "Бразилия";
		countriesTextL [5].spanish = "Brasil";

		countriesTextL [6].english = "Chili";
		countriesTextL [6].russian = "Чили";
		countriesTextL [6].spanish = "Chile";

		countriesTextL [7].english = "Colombia";
		countriesTextL [7].russian = "Колумбия";
		countriesTextL [7].spanish = "Colombia";

		countriesTextL [8].english = "Costa Rica";
		countriesTextL [8].russian = "Коста Рика";
		countriesTextL [8].spanish = "Costa Rica";

		countriesTextL [9].english = "Cote d'Voire";
		countriesTextL [9].russian = "Кот д'Вуар";
		countriesTextL [9].spanish = "Costa de Marfil";

		countriesTextL [10].english = "Croatia";
		countriesTextL [10].russian = "Хорватия";
		countriesTextL [10].spanish = "Croacia";

		countriesTextL [11].english = "Czech";
		countriesTextL [11].russian = "Чехия";
		countriesTextL [11].spanish = "Republica Checa";

		countriesTextL [12].english = "Denmark";
		countriesTextL [12].russian = "Алжир";
		countriesTextL [12].spanish = "Dinamarca";

		countriesTextL [13].english = "England";
		countriesTextL [13].russian = "Англия";
		countriesTextL [13].spanish = "Inglaterra";

		countriesTextL [14].english = "France";
		countriesTextL [14].russian = "Франция";
		countriesTextL [14].spanish = "Francia";

		countriesTextL [15].english = "Germany";
		countriesTextL [15].russian = "Германия";
		countriesTextL [15].spanish = "Alemania";

		countriesTextL [16].english = "Ghana";
		countriesTextL [16].russian = "Гана";
		countriesTextL [16].spanish = "Ghana";

		countriesTextL [17].english = "Greece";
		countriesTextL [17].russian = "Греция";
		countriesTextL [17].spanish = "Grecia";

		countriesTextL [18].english = "Hungary";
		countriesTextL [18].russian = "Венгрия";
		countriesTextL [18].spanish = "Hungria";

		countriesTextL [19].english = "Ireland";
		countriesTextL [19].russian = "Ирландия";
		countriesTextL [19].spanish = "Irlanda";

		countriesTextL [20].english = "Italy";
		countriesTextL [20].russian = "Италия";
		countriesTextL [20].spanish = "Italia";

		countriesTextL [21].english = "Mexico";
		countriesTextL [21].russian = "Мексика";
		countriesTextL [21].spanish = "Méjico";

		countriesTextL [22].english = "Netherlands";
		countriesTextL [22].russian = "Нидерланды";
		countriesTextL [22].spanish = "Paises Bajos";

		countriesTextL [23].english = "Nigeria";
		countriesTextL [23].russian = "Нигерия";
		countriesTextL [23].spanish = "Nigeria";

		countriesTextL [24].english = "Poland";
		countriesTextL [24].russian = "Польша";
		countriesTextL [24].spanish = "Polonia";

		countriesTextL [25].english = "Portugal";
		countriesTextL [25].russian = "Поргугалия";
		countriesTextL [25].spanish = "Portugal";

		countriesTextL [26].english = "Puerto Rico";
		countriesTextL [26].russian = "Пуэрто Рико";
		countriesTextL [26].spanish = "Puerto Rico";

		countriesTextL [27].english = "Russia";
		countriesTextL [27].russian = "Россия";
		countriesTextL [27].spanish = "Rusia";

		countriesTextL [28].english = "Spain";
		countriesTextL [28].russian = "Испания";
		countriesTextL [28].spanish = "Espana";

		countriesTextL [29].english = "Sweden";
		countriesTextL [29].russian = "Швеция";
		countriesTextL [29].spanish = "Suecia";

		countriesTextL [30].english = "Switzerland";
		countriesTextL [30].russian = "Швейцария";
		countriesTextL [30].spanish = "Suiza";

		countriesTextL [31].english = "Turkey";
		countriesTextL [31].russian = "Турция";
		countriesTextL [31].spanish = "Turquia";

		countriesTextL [32].english = "Ukraine";
		countriesTextL [32].russian = "Украина";
		countriesTextL [32].spanish = "Ucrania";

		countriesTextL [33].english = "Uruguay";
		countriesTextL [33].russian = "Уругвай";
		countriesTextL [33].spanish = "Uruguay";

		countriesTextL [34].english = "USA";
		countriesTextL [34].russian = "США";
		countriesTextL [34].spanish = "Estados Unidos";

		countriesTextL [35].english = "USSR";
		countriesTextL [35].russian = "СССР";
		countriesTextL [35].spanish = "URSS";

		countriesTextL [36].english = "Venezuela";
		countriesTextL [36].russian = "Венесуэла";
		countriesTextL [36].spanish = "Venezuela";

		countriesTextL [37].english = "Wales";
		countriesTextL [37].russian = "Уэльс";
		countriesTextL [37].spanish = "Gales";

		countriesTextL [38].english = "Austria";
		countriesTextL [38].russian = "Австрия";
		countriesTextL [38].spanish = "Austria";

		countriesTextL [39].english = "Cameroon";
		countriesTextL [39].russian = "Камерун";
		countriesTextL [39].spanish = "Cameroon";

		countriesTextL [39].english = "Ecuador";
		countriesTextL [39].russian = "Эквадор";
		countriesTextL [39].spanish = "Ecuador";
	}
}
