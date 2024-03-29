﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public Lang lang = new Lang ();
	public Sprite[] flags = new Sprite[2];
	public Sprite[] bg = new Sprite[2];
	public Sprite[] swithcers = new Sprite[2];
	public string l;

	private bool is101 = true;

	public void OnClickExit(){
		Application.Quit ();
	}

	public void getNewLVL(int lvl){
		UnityEngine.SceneManagement.SceneManager.LoadScene(lvl);
	}

	public void prepareMenu(){
		//----BLUE----
		GameObject.Find("Difficulty").GetComponent<Text>().text = this.lang.menuInterface[1];
		GameObject.Find("lvl2to5btnText").GetComponent<Text>().text = this.lang.menuInterface[2];
		GameObject.Find("lvl2to6btnText").GetComponent<Text>().text = this.lang.menuInterface[3];
		GameObject.Find("lvl2to8btnText").GetComponent<Text>().text = this.lang.menuInterface[4];
		GameObject.Find("lvl2torndbtnText").GetComponent<Text>().text = this.lang.menuInterface[5];
		GameObject.Find("CalcText").GetComponent<Text>().text = this.lang.menuInterface[6];
		GameObject.Find("HelpText").GetComponent<Text>().text = this.lang.menuInterface[7];
		GameObject.Find("ExitText").GetComponent<Text>().text = this.lang.menuInterface[8];
		//----RED----
		GameObject.Find("lvl2to5btnText_R").GetComponent<Text>().text = this.lang.menuInterface[2];
		GameObject.Find("lvl2to6btnText_R").GetComponent<Text>().text = this.lang.menuInterface[3];
		GameObject.Find("lvl2to8btnText_R").GetComponent<Text>().text = this.lang.menuInterface[4];
		GameObject.Find("lvl2torndbtnText_R").GetComponent<Text>().text = this.lang.menuInterface[5];
		GameObject.Find("HelpText_R").GetComponent<Text>().text = this.lang.menuInterface[7];
		GameObject.Find("ExitText_R").GetComponent<Text>().text = this.lang.menuInterface[8];
	}

	public void loadLang(){

		#if UNITY_ANDROID && !UNITY_EDITOR
		string path = Path.Combine(Application.streamingAssetsPath, "Languages/" + l+ ".json");
		WWW reader = new WWW(path);
		while (!reader.isDone) { }
		this.lang.json = reader.text;
		#else
		this.lang.json = File.ReadAllText(Application.streamingAssetsPath + "/Languages/" + l + ".json");
		#endif
		this.lang = JsonUtility.FromJson<Lang>(this.lang.json);
		if(l == "ru_RU")
			GameObject.Find ("LangSwitch").GetComponent<Image> ().sprite = flags [0];
		else
			GameObject.Find ("LangSwitch").GetComponent<Image> ().sprite = flags [1];
		
	}

	public void OnChangeLangClick(){
		if (PlayerPrefs.GetString ("Lang") == "ru_RU") {
			GameObject.Find ("LangSwitch").GetComponent<Image> ().sprite = flags [0];
			PlayerPrefs.SetString ("Lang", "en_US");
		}
		else if (PlayerPrefs.GetString ("Lang") == "en_US") {
			GameObject.Find ("LangSwitch").GetComponent<Image> ().sprite = flags [1];
			PlayerPrefs.SetString ("Lang", "ru_RU");
		}
		this.l = PlayerPrefs.GetString ("Lang");
		this.loadLang ();
		this.prepareMenu ();
	}

	public void OnClickSwitcher(){
		if (is101) {
			GameObject.Find ("PanelMitButtons").GetComponent<Animation> ().Play ("PBOutro");
			GameObject.Find ("PanelMitButtons_RED").GetComponent<Animation> ().Play ("swapToDiscrete");
			GameObject.Find ("BG").GetComponent<Image> ().sprite = bg [1];
		} else {
			GameObject.Find ("PanelMitButtons_RED").GetComponent<Animation> ().Play ("UnSwapToDiscrete");
			GameObject.Find ("PanelMitButtons").GetComponent<Animation> ().Play ("PBINtro");
			GameObject.Find ("BG").GetComponent<Image> ().sprite = bg [0];
		}
		is101 ^= true;
	}

	void Start () {
		if (PlayerPrefs.GetInt ("HighScore") <= 0) {
			PlayerPrefs.SetInt ("HighScore", 0);
		}
		if (!PlayerPrefs.HasKey ("Lang")) {
			if (Application.systemLanguage == SystemLanguage.Russian) {
				PlayerPrefs.SetString ("Lang", "ru_RU");
			} else {
				PlayerPrefs.SetString ("Lang", "en_US");
			}
		}

		l = PlayerPrefs.GetString ("Lang");
		loadLang ();
		prepareMenu ();
		GameObject.Find ("PanelMitButtons").GetComponent<Animation>().Play ("PBINtro");
		GameObject.Find ("BG").GetComponent<Image> ().sprite = bg [0];
	}

	void Update () {
		GameObject.Find ("HSCore").GetComponent<Text> ().text = lang.menuInterface[0] + PlayerPrefs.GetInt ("HighScore");
		GameObject.Find ("HSCore_R").GetComponent<Text> ().text = lang.menuInterface[0] + PlayerPrefs.GetInt ("HighScore");
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit();
	}
}
