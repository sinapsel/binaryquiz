using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUnarC : MonoBehaviour {
	public Text[] ButtonText;
	private int[] ButtonRef;
	public Text RightANSW;


	public void ResetArr(){
		for (int i = 0; i < ButtonText.Length; i++) {
			ButtonRef[i] = 0; 
		}
	}
	// Use this for initialization
	public void Start () {
		System.Array.Resize(ref ButtonRef,ButtonText.Length);
		ResetArr();
	}

	public int SizeOfButtons(){
		return ButtonText.Length;
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < ButtonText.Length; i++) {
			ButtonText[i].text = "" + ButtonRef[i]; 
		}
		//Debug.Log (count());
		RightANSW.text = "" + count();
	}

	public void OnBClick(int ind){
		ButtonRef[ind] = (Mathf.Abs(ButtonRef[ind]-1));
	}

	public int count(){
		int res = 0;
		for (int i = 0; i < ButtonRef.Length; i++) {
			res += (int)(ButtonRef [i] * Mathf.Pow (2, ButtonRef.Length - i-1));
		}
		return res;
	}
		
}
