using UnityEngine;
using System.Collections;

public class KeyboardScript : MonoBehaviour {
	private string letter;
	public string name;
	public TextMesh textoMonitor;
	public bool tick;
	public float tickstart;
	public Bloom_Autowalker bloom;
	private bool fin = false;
	
	private float tickRate;
	public GameObject pass;
	public TextMesh textopass;	
	public string tigers= "tigers123";
	public string passletter;
	private int i,o = 0;
	public bool enable=false;

	
	void Timer () {
		tickRate += Time.deltaTime;
		if (tickRate > 0.09f){
		tick = true;
		tickRate = 0;
		}
		else{tick=false;}
	}
	
	void Password(){	
		bool isReturn = Input.GetKeyDown(KeyCode.Return);
		bool isEnter = Input.GetKeyDown(KeyCode.KeypadEnter);
		if (isEnter||isReturn){
			enable=true;
			PlayerBehave.playerName= textoMonitor.text;	
			//cambiar esto de lugar
			Application.LoadLevel(2);
		}
		Timer ();
		if (tick && textopass.text.Length < 9 && enable){
			textopass.text += "*";
			passletter=tigers[o].ToString();
			transform.FindChild(passletter).animation.Play(passletter+"Key");
			o+=1;
		}
		
	}
	

	void press(){
		bool isBackSpace = Input.GetKeyDown(KeyCode.Backspace);
		bool isSpace = Input.GetKeyDown(KeyCode.Space);

		
		
		if(Input.anyKeyDown){
			audio.Play();
		}
		
		if (Input.anyKey){

			if (isBackSpace && name.Length-1>=0){
					 name = name.Substring(0, name.Length - 1);
				transform.FindChild("backspace").animation.Play("eraseKey");
			}
			
			if (isSpace){
					 name += " ";
				transform.FindChild("space").animation.Play("spaceKey");
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
		
		if (bloom.walkAnimOver){
			Password ();
		}
        press ();
	}
}
