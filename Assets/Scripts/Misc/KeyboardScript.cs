using UnityEngine;
using System.Collections;

public class KeyboardScript : MonoBehaviour {
	private string letter;
	public string name;
	public TextMesh textoMonitor;

	void press(){
		bool isBackSpace = Input.GetKeyDown(KeyCode.Backspace);
		bool isSpace = Input.GetKeyDown(KeyCode.Space);
		if (isBackSpace && name.Length-1>=0){
				 name = name.Substring(0, name.Length - 1);
			transform.FindChild("backspace").animation.Play("eraseKey");
			}
		if (isSpace){
				 name += " ";
			transform.FindChild("space").animation.Play("spaceKey");
		}
		if (Input.anyKey){
		letter = Input.inputString;
			if (transform.FindChild(letter)!=null) {
				transform.FindChild(letter).animation.Play(letter+"Key");
				name += letter;
				print(Input.inputString);
			}
			
		}
		textoMonitor.text = name;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
        press ();
	}
}
