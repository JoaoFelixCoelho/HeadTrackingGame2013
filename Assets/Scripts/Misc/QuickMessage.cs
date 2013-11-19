using UnityEngine;
using System.Collections;

public class QuickMessage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.transform.GetChild(0).guiTexture.pixelInset = new Rect(-Screen.width*2,0,Screen.width*4, 60);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public void showMsg(string msg) {
		animation.Play();
		gameObject.guiText.text = msg;
	}
}
