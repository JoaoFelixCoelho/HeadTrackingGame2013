using UnityEngine;
using System.Collections;

public class KeyboardScript : MonoBehaviour {
	private string letter;
	public string name;
	void press(){
	if (Input.anyKey){
		letter = Input.inputString;
		transform.FindChild(letter).animation.Play(letter+"Key");
		name += letter;
		print (name);
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
