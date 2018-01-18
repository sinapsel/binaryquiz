using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class helpControl_R : MonoBehaviour {
	public Lang lang = new Lang ();


	public void prepareMenu(){
		GameObject.Find("about").GetComponent<Text>().text = this.lang.help_R[0];
		GameObject.Find("rejimes").GetComponent<Text>().text = this.lang.help_R[1];
		GameObject.Find("about2").GetComponent<Text>().text = this.lang.help_R[2];
		GameObject.Find("B2D").GetComponent<Text>().text = this.lang.help_R[3];
		GameObject.Find("D2B").GetComponent<Text>().text = this.lang.help_R[4];
		GameObject.Find("Table").GetComponent<Text>().text = this.lang.help_R[5];
		GameObject.Find("creds").GetComponent<Text>().text = this.lang.help_R[6];
		GameObject.Find("License").GetComponent<Text>().text = this.lang.help_R[7];
	}
	void Start () {
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

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			this.Back ();
	}

	public void Back(){
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
