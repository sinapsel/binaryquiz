using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checker1AND0 : MonoBehaviour, IBasicVoids {

	public Text Num, Right;
	public char[] ops = {'|', '&', '^', '+', '*'};
	public Image TimeBar;
	public float dTime,tB,stT;
	private bool isLose = false, isPaused = false;
	private float tmp;
	public float violence = 0.03f;
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
	public void GenNum () {
		int Numerical1 = Random.Range (1, Mathf.CeilToInt (Mathf.Pow (2, GetComponent<MainUnarC> ().SizeOfButtons ()) - 1));
		Num.text = "" + System.Convert.ToString (Numerical1, (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex <= 8)?2:10) + "\n";
		char op = ops[Random.Range (0, (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex - 4))];
		Num.text += op;
		int Numerical2 = Random.Range (1, Mathf.CeilToInt (Mathf.Pow (2, GetComponent<MainUnarC> ().SizeOfButtons ()) - 1));
		Num.text += "\n" + System.Convert.ToString (Numerical2, ( UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex <= 8)?2:10);
		N = eval(Numerical1, Numerical2, op);
		GameObject.Find ("RightBinary").GetComponent<Text>().text = System.Convert.ToString (eval(Numerical1, Numerical2, op),2);
		stT = dTime;
	}
	//Implementation
	public void Restart(){
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}	
	//Implementation
	public void MenuGegangen(){
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
	//Implementation
	public void OnPauseUpauseClick(){
		if (!isPaused) {
			isPaused = c.Pause (dTime, out tB);
		} else {
			isPaused = c.UnPause ();
			this.GenNum ();
		}
	}


	void Start ()
	{	
		c.prepareMenu ();
		this.GenNum ();
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
		if ((N.ToString() != Right.text)&&((!isLose))) {
			dTime += Time.deltaTime;
			//отнять время
		} else if ((N.ToString() == Right.text)&&((!isLose))) {
			scores += Mathf.CeilToInt((Mathf.Log (N) / Mathf.Log (2))/*(dTime - stT)*/);
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
