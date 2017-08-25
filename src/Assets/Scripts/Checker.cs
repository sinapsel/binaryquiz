using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Checker : MonoBehaviour
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

	public void GenNum () {
		int Numerical = Random.Range (1, Mathf.CeilToInt (Mathf.Pow (2, GetComponent<MainUnarC> ().SizeOfButtons ()) - 1));
		Num.text = "" + Numerical;
		GameObject.Find ("RightBinary").GetComponent<Text>().text = System.Convert.ToString (Numerical,2);
		stT = dTime;
	}

	public void Restart(){
		Application.LoadLevel (Application.loadedLevel);
	}
	public void prepareMenu(){
		GameObject.Find("PauseText").GetComponent<Text>().text = this.lang.gamePlay[0];
		GameObject.Find("ResumeButtonText").GetComponent<Text>().text = this.lang.gamePlay[1];
		GameObject.Find("MenuGehenText").GetComponent<Text>().text = this.lang.gamePlay[2];
		GameObject.Find("RestartText").GetComponent<Text>().text = this.lang.gamePlay[4];
		GameObject.Find("MenuGehenText2").GetComponent<Text>().text = this.lang.gamePlay[2];
	}

	// Use this for initialization
	void Start ()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		string path = Path.Combine(Application.streamingAssetsPath, "Languages/" +  PlayerPrefs.GetString("Lang") + ".json");
		WWW reader = new WWW(path);
		while (!reader.isDone) { }
		this.lang.json = reader.text;
		#else
		this.lang.json = File.ReadAllText(Application.streamingAssetsPath + "/Languages/" + PlayerPrefs.GetString("Lang") + ".json");
		#endif
		this.lang = JsonUtility.FromJson<Lang>(this.lang.json);
		this.prepareMenu ();


		GenNum ();
	}

	public void Pause(){
		isPaused = true;
		GameObject.Find ("PausePanel").GetComponent<Animation> ().Play ();
		tB = dTime;
	}
	public void UnPause(){
		Vector2 temp = transform.position;
		temp.y = 1280;
		GameObject.Find ("PausePanel").GetComponent<RectTransform> ().position = temp;
		isPaused = false;
		GameObject.Find ("Num").GetComponent<Animation> ().Play ();
		GenNum ();
	}
	public void MenuGegangen(){
		Application.LoadLevel (0);
	}

	// Update is called once per frame
	void Update ()
	{
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
			scores += Mathf.CeilToInt((Mathf.Log (float.Parse (Num.text)) / Mathf.Log (2))/*(dTime - stT)*/);
			dTime /= 2.25f;
			GameObject.Find ("Num").GetComponent<Animation> ().Play ();
			GenNum ();
			GetComponent<MainUnarC> ().ResetArr ();
		}
		ScrText.text =  lang.gamePlay[3] + scores;
		if (scores > PlayerPrefs.GetInt ("HighScore")) {
			PlayerPrefs.SetInt ("HighScore", scores);
		}
		TimeBar.rectTransform.localScale = new Vector2 (1 - violence * dTime, 1);
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.Menu))
			this.Pause ();
	}
}
