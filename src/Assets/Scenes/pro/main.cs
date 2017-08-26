using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class main : MonoBehaviour, IBasicVoids
{
	public Text Num, Right;

	public Image TimeBar;
	public float dTime,tB,stT;
	private bool isLose = false, isPaused = false;
	private float tmp;
	public float violence = 0.015f;
	public int scores = 0;
	public Text ScrText;
	public Lang lang = new Lang ();
	GameController c = new GameController ();

	//Implementation
	public void GenNum ()
	{
		int Numerical = Random.Range (1,4096);
		Num.text = "" + Numerical;
		GameObject.Find ("RightBinary").GetComponent<Text>().text = System.Convert.ToString (Numerical,2);
		stT = dTime;
	}

	//implementation
	public void Restart(){
		Application.LoadLevel (Application.loadedLevel);
	}
	//implementation
	public void MenuGegangen(){
		Application.LoadLevel (0);
	}
	//Implementation
	public void OnPauseUpauseClick(int a){
		if (a == 1) {
			isPaused = c.Pause (dTime, out tB);
		} else if (a == 0) {
			isPaused = c.UnPause ();
			GenNum ();
		}
	}


	public void PressedOne(){
		GameObject.Find ("Keyed").GetComponent<Text> ().text += "1";
	}
	public void PressedZero(){
		GameObject.Find ("Keyed").GetComponent<Text> ().text += "0";
	}
	public void CLS(){
		GameObject.Find ("Keyed").GetComponent<Text> ().text = "";
	}

	private void Conversation(){
		int ress = 0;
		for (int i = 0; i < GameObject.Find ("Keyed").GetComponent<Text> ().text.Length; i++) {
			ress += (int)(float.Parse (GameObject.Find ("Keyed").GetComponent<Text> ().text[i]+"") * Mathf.Pow (2, GameObject.Find ("Keyed").GetComponent<Text> ().text.Length - i - 1));
		}
		Right.text = ress + "";
	}



	void Start ()
	{
		c.prepareMenu ();
		GenNum ();
	}

	void Update ()
	{
		Conversation ();
		if (isPaused)
			dTime = tB;

		if (dTime >= (1 / violence)) {
			isLose = true;
			dTime = 0;
			//GameObject.Find ("Main Camera").GetComponent<Camera> ().backgroundColor = new Color (1, 0, 0);
			GameObject.Find ("RightAnswer").GetComponent<Animation> ().Play ();
			GameObject.Find ("LosePanel").GetComponent<Animation> ().Play ();
			GameObject.Find ("RightBinary").GetComponent<Animation> ().Play ();
		}
		if ((Num.text != Right.text)&&((!isLose))) {
			dTime += Time.deltaTime;
			//отнять время
		} else if ((Num.text == Right.text)&&((!isLose))) {
			scores += Mathf.RoundToInt(float.Parse(Num.text))/*(dTime - stT)*/;
			dTime += 0;
			GameObject.Find ("Num").GetComponent<Animation> ().Play ();
			CLS ();
			GenNum ();
//			GetComponent<MainUnarC> ().ResetArr ();
		}
		ScrText.text = lang.gamePlay[3] + scores;
		if (scores > PlayerPrefs.GetInt ("HighScore")) {
			PlayerPrefs.SetInt ("HighScore", scores);
		}
		TimeBar.rectTransform.localScale = new Vector2 (1 - violence * dTime, 1);
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.Menu))
			isPaused = c.Pause(dTime, out tB);
	}
}
