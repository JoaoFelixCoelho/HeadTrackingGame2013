using UnityEngine;
using System.Collections;

public class KeyboardScript : MonoBehaviour {
	
	private string letter;
	public string name;
	public GameObject monitor;
	private TextMesh textoMonitor;
	public bool tick;
	public float tickstart, counter=0,timeranimation=0;
	public Bloom_Autowalker bloom;
	private bool fin = false, finteleport = true;
	
	private float tickRate, teleportTimer;
	public GameObject pass;
	public TextMesh textopass;	
	private string tigers= "tigers";
	private string passletter;
	private int i,o;
	private bool anim= false;
	
	private bool animPlaying = false;
	public bool enable=false,startAnim=false;
	public GameObject rotateleft, rotateright,teleportparticles, player, passTitle;
	public float positionx;

	
	void Timer () {
		tickRate += Time.deltaTime;
		if (tickRate > 0.09f){
		tick = true;
		tickRate = 0;
		}
		else{tick=false;}
	}
	
/*	public void TimerAnimation(){
		float counteranimation=0;
		timeranimation+=Time.deltaTime;	
		
		if (timeranimation<0.5f){
		
		//transform.FindChild(letter).transform.position.x += 0.1f;
		}
		else if (timeranimation>0.5f){
			tecla.transform.position = new Vector3(tecla.transform.position.x, tecla.transform.position.y, tecla.transform.position.z);
		}
		else if (timeranimation>1){
			startAnim=false;
		}
	}*/
	     
	void Password(){	
		bool isReturn = Input.GetKeyDown(KeyCode.Return);
		bool isEnter = Input.GetKeyDown(KeyCode.KeypadEnter);
		if (isEnter||isReturn){
			passTitle.SetActive(true);
			enable=true;
			PlayerBehave.playerName= textoMonitor.text;	
			//cambiar esto de lugar
		}
		Timer ();
		if (tick && textopass.text.Length < 9 && enable){
			textopass.text += "*";
			audio.Play();
			if (o<6){
				passletter=tigers[o].ToString();
				transform.FindChild(passletter).animation.Play(passletter+"Key");
				o+=1;

			}

		}
		else if (textopass.text.Length == 9){
			if(!anim){
				anim = true;
				monitor.animation.Play();
				player.audio.Play();
				player.animation.clip = player.animation ["playerBlurTeleport"].clip;
				player.animation.Play();
			}
			counter+=Time.deltaTime;
			if (counter>player.animation.clip.length){
				player.animation.Stop();
				Application.LoadLevel(2);
			}
		}
		
	}
	
	void teleport(){
		teleportTimer+=Time.deltaTime;
		player.GetComponent<GlowEffect>().enabled = true;
	}
	
	void pressKeyAnimation(){
		timeranimation+=Time.deltaTime;
		print(timeranimation);
		GameObject tecla = transform.FindChild(letter).gameObject;
		tecla.transform.position = new Vector3(tecla.transform.position.x, tecla.transform.position.y-timeranimation, tecla.transform.position.z);

	}

	void press(){
		bool isBackSpace = Input.GetKeyDown(KeyCode.Backspace);
		bool isSpace = Input.GetKeyDown(KeyCode.Space);

		
		
		if(Input.anyKeyDown){
			audio.Play();
		
		}
		
		if (Input.anyKey){
			letter = Input.inputString;
			if (isBackSpace && name.Length-1>=0){
				name = name.Substring(0, name.Length - 1);
				transform.FindChild("backspace").animation.Play("eraseKey");
			}
			
			if (isSpace){
					 name += " ";
				transform.FindChild("space").animation.Play("spaceKey");
			}

			if (transform.FindChild(letter.ToUpper() + "001")!=null && name.Length<15) {
				name += letter;
				transform.FindChild(letter.ToUpper() + "001").animation.Play(letter+"Key");

			/*if (transform.FindChild(letter.ToUpper() + "001")!=null && name.Length<15) {
				GameObject tecla = transform.FindChild(letter.ToUpper() + "001").gameObject;
				tecla.transform.position = new Vector3(tecla.transform.position.x, tecla.transform.position.y-0.1f, tecla.transform.position.z);

				
			}*/
			textoMonitor.text = name;
		}
	}
	}
	

	// Use this for initialization
	void Start () {
		textoMonitor = monitor.transform.FindChild("_Name").gameObject.GetComponent<TextMesh>();
	}
	
	
	// Update is called once per frame
	void Update () {

		if (bloom.walkAnimOver){
			Password ();
			 press ();
		}
		
		if (startAnim){
			pressKeyAnimation();
		}
	
       
	}
}
