using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Checker : MonoBehaviour, IBasicVoids
{
	public Text Num, Right;

	public Image TimeBar;
	public float dTime,tB,stT;
	private bool isLose = false, isPaused = false;
	private float tmp;
	public float violence = 0.03f;
	public int scores = 0;
	public Text ScrText;

	public Lang lang = new Lang ();
	GameController c = new GameController ();


	//Implementation
	public void GenNum () {
		int Numerical = Random.Range (1, Mathf.CeilToInt (Mathf.Pow (2, GetComponent<MainUnarC> ().SizeOfButtons ()) - 1));
		Num.text = "" + Numerical;
		GameObject.Find ("RightBinary").GetComponent<Text>().text = System.Convert.ToString (Numerical,2);
		stT = dTime;
	}
	//Implementation
	public void Restart(){
		Application.LoadLevel (Application.loadedLevel);
	}	
	//Implementation
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


	void Start ()
	{	
		c.prepareMenu ();
		GenNum ();
	}

	void Update ()
	{
		if (isPaused)
			dTime = tB;
		
		if (dTime >= (1 / violence)) {
			isLose = true;
			dTime = 0;
			GameObject.Find ("RightAnswer").GetComponent<Animation> ().Play ();
			GameObject.Find ("LosePanel").GetComponent<Animation> ().Play ();
			GameObject.Find ("RightBinary").GetComponent<Animation> ().Play ();
		}
		if ((Num.text != Right.text)&&((!isLose))) {
			dTime += Time.deltaTime;
			//отнять время
		} else if ((Num.text == Right.text)&&((!isLose))) {
			scores += Mathf.CeilToInt((Mathf.Log (float.Parse (Num.text)) / Mathf.Log (2))/*(dTime - stT)*/);
			dTime /= 2.25f;
			GameObject.Find ("Num").GetComponent<Animation> ().Play ();
			GenNum ();//Num, GetComponent<MainUnarC> ().SizeOfButtons ()
			GetComponent<MainUnarC> ().ResetArr ();
		}
		ScrText.text =  lang.gamePlay[3] + scores;
		if (scores > PlayerPrefs.GetInt ("HighScore")) {
			PlayerPrefs.SetInt ("HighScore", scores);
		}
		TimeBar.rectTransform.localScale = new Vector2 (1 - violence * dTime, 1);
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.Menu))
			isPaused = c.Pause(dTime, out tB);
	}
}
