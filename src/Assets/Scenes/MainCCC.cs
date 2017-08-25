using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MainCCC : MonoBehaviour {
	public Dropdown SystemCHC;
	public Text InPanel,OutPanel;
	//public string json;
	public Lang lang = new Lang ();


	// Use this for initialization
	void Start () {
		#if UNITY_ANDROID && !UNITY_EDITOR
		string path = Path.Combine(Application.streamingAssetsPath, "Languages/" +  PlayerPrefs.GetString("Lang")+ ".json");
		WWW reader = new WWW(path);
		while (!reader.isDone) { }
		this.lang.json = reader.text;
		#else
		this.lang.json = File.ReadAllText(Application.streamingAssetsPath + "/Languages/" + PlayerPrefs.GetString("Lang") + ".json");
		#endif
		this.lang = JsonUtility.FromJson<Lang>(this.lang.json);

		GameObject.Find("BackText").GetComponent<Text>().text = this.lang.mutattor[0];
	}
	public void Back(){
		Application.LoadLevel (0);
	}
	private int BinToInt(string binaryNumber)
	{
		int multiplier = 1;
		int converted = 0;
		for (int i = binaryNumber.Length - 1; i >= 0; i--)
		{
			int t = System.Convert.ToInt16(binaryNumber[i].ToString());
			converted = converted + (t * multiplier);
			multiplier = multiplier * 2;
		}
		return converted;
	}
	
	// Update is called once per frame
	void Update () {
		if (InPanel.text != "") {
			if (SystemCHC.value == 0) {
				int temp = int.Parse (InPanel.text);
				OutPanel.text = System.Convert.ToString (temp, 2);
			} else if (SystemCHC.value == 1) {
				OutPanel.text = BinToInt (InPanel.text) + "";
			}
		}
		if (Input.GetKeyDown (KeyCode.Escape))
			this.Back ();
	}
	public void changed(){
		string tmp;
		tmp = InPanel.text;
		InPanel.text = OutPanel.text;
		OutPanel.text = tmp;
		if (SystemCHC.value == 0) {
			Vector2 TMP = transform.position;
			TMP.x = GameObject.Find ("Panel").GetComponent<RectTransform> ().position.x - 5.5f;
			TMP.y = GameObject.Find ("Panel").GetComponent<RectTransform> ().position.y;
			GameObject.Find ("Panel").GetComponent<RectTransform> ().position = TMP;
		} else if (SystemCHC.value == 1) {
			Vector2 TMP = transform.position;
			TMP.x = GameObject.Find ("Panel").GetComponent<RectTransform> ().position.x + 5.5f;
			TMP.y = GameObject.Find ("Panel").GetComponent<RectTransform> ().position.y;
			GameObject.Find ("Panel").GetComponent<RectTransform> ().position = TMP;
		}
		//else if (SystemCHC.value == 1) {GameObject.Find ("Panel").GetComponent<Animation> ().Play ("bTdPanel");}
	}
	public void KeyBoard(int index){
		if (index == 100) {
			InPanel.text = "";
			OutPanel.text = "";
		} else if (index == -100) {
			InPanel.GetComponent<Text> ().text = InPanel.text.Substring (0, InPanel.text.Length - 1);
		} else {InPanel.text = InPanel.text + (index+"");
		}
	}
}
