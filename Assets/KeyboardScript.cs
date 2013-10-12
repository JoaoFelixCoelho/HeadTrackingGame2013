using UnityEngine;
using System.Collections;

public class KeyboardScript : MonoBehaviour {
	private string letra;
	void press(){
	if (Input.anyKey){
		letra = Input.inputString;
		transform.FindChild(letra).animation.Play(letra+"Key");
		}
		
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
        press ();
	}
}
