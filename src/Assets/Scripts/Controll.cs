using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameController{
	public Lang lang = new Lang ();


	public void prepareMenu(){
		#if UNITY_ANDROID && !UNITY_EDITOR
		string path = Path.Combine(Application.streamingAssetsPath, "Languages/" +  PlayerPrefs.GetString("Lang") + ".json");
		WWW reader = new WWW(path);
		while (!reader.isDone) { }
		this.lang.json = reader.text;
		#else
		this.lang.json = File.ReadAllText(Application.streamingAssetsPath + "/Languages/" + PlayerPrefs.GetString("Lang") + ".json");
		#endif
		this.lang = JsonUtility.FromJson<Lang>(this.lang.json);
		GameObject.Find("PauseText").GetComponent<Text>().text = this.lang.gamePlay[0];
		GameObject.Find("ResumeButtonText").GetComponent<Text>().text = this.lang.gamePlay[1];
		GameObject.Find("MenuGehenText").GetComponent<Text>().text = this.lang.gamePlay[2];
		GameObject.Find("RestartText").GetComponent<Text>().text = this.lang.gamePlay[4];
		GameObject.Find("MenuGehenText2").GetComponent<Text>().text = this.lang.gamePlay[2];
	}
	public bool Pause(float dTime, out float tB){
		GameObject.Find ("PausePanel").GetComponent<Animation> ().Play ();
		tB = dTime;
		return true;
	}
	public bool UnPause(){
		GameObject a = new GameObject();
		Vector2 temp = a.transform.position;	
		temp.y = 1280;
		GameObject.Find ("PausePanel").GetComponent<RectTransform> ().position = temp;
		//isPaused = false;
		GameObject.Find ("Num").GetComponent<Animation> ().Play ();
		//GenNum ();
		return false;
	}

}

public interface IBasicVoids{
	void GenNum();
	void Restart ();
	void MenuGegangen();
	void OnPauseUpauseClick (int a);
}

public class Controll : MonoBehaviour {}