using UnityEngine;
using System.Collections;

public class KeyboardScript : MonoBehaviour {
	private string letter;
	public string name;
	public TextMesh textoMonitor;

	void press(){
		bool isBackSpace = Input.GetKeyDown(KeyCode.Backspace);
		bool isSpace = Input.GetKeyDown(KeyCode.Space);
		bool isReturn = Input.GetKeyDown(KeyCode.Return);
		
		if (Input.anyKey){
			audio.Play();
			
			if (isBackSpace && name.Length-1>=0){
					 name = name.Substring(0, name.Length - 1);
				transform.FindChild("backspace").animation.Play("eraseKey");
			}
			
			if (isSpace){
					 name += " ";
				transform.FindChild("space").animation.Play("spaceKey");
			}
			
			if (isReturn){
				transform.FindChild("enter").animation.Play("enterKey");
			}
			
			letter = Input.inputString;
			if (transform.FindChild(letter)!=null && name.Length<15) {
				transform.FindChild(letter).animation.Play(letter+"Key");
				name += letter;
			}
			textoMonitor.text = name;
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
