using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class main_R : MonoBehaviour, IBasicVoids
{
	public Text Num, Right;

	public Image TimeBar;
	public float dTime,tB,stT;
	private bool isLose = false, isPaused = false;
	private float tmp;
	public char[] ops = {'|', '&', '^', '+', '*'};
	public float violence = 0.015f;
	public int scores = 0;
	public Text ScrText;
	public Lang lang = new Lang ();
	GameController c = new GameController ();

	private int eval(int a, int b, char op){
		switch (op) {
		case '|': 
			return a | b;
		case '&':
			return a & b;
		case '^':
			return a ^ b;
		case '+':
			return a + b;
		case '*':
			return a * b;
		default:
			return 0;
		}
	}
	private int N;


	//Implementation
	public void GenNum ()
	{
		int Numerical1 = Random.Range (1, 256);
		Num.text = "" + System.Convert.ToString (Numerical1, (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex <= 8)?2:10) + "\n";
		char op = ops[Random.Range (0, 5)];
		Num.text += op;
		int Numerical2 = Random.Range (1, 256);
		Num.text += "\n" + System.Convert.ToString (Numerical2, (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex <= 8)?2:10);
		N = eval(Numerical1, Numerical2, op);
		GameObject.Find ("RightBinary").GetComponent<Text>().text = System.Convert.ToString (eval(Numerical1, Numerical2, op),2);
		stT = dTime;
	}

	//implementation
	public void Restart(){
		//Application.LoadLevel (Application.loadedLevel);
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}
	//implementation
	public void MenuGegangen(){
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
	//Implementation
	public void OnPauseUpauseClick(){
		if (!isPaused) {
			isPaused = c.Pause (dTime, out tB);
		} else {
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
		if ((N.ToString() != Right.text)&&((!isLose))) {
			dTime += Time.deltaTime;
			//отнять время
		} else if ((N.ToString() == Right.text)&&((!isLose))) {
			scores += 3*N/*(dTime - stT)*/;
			dTime /= 2.45f;
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
			//OnPauseUpauseClick();
	}
}
