using UnityEngine;
using System.Collections;

public class MenuHoverController : MonoBehaviour {
	public bool usesShader;
	public Shader glowShader;
	private Shader originalShader;
	//private Color originalColor;
	public GameObject [] buttons;
	public GameObject backMenu;
	public int selectedButton = 1;
	float buttonDownTimer = 0f;
	public bool accessible, isRoot;
	Color originalColor, glowColor;
	
	
	//public bool mouse;
	//public Texture2D mira;
	
	
	// Use this for initialization
	void Start () {
		Configuration.readConfig();
		if(usesShader) {
			originalShader = buttons[0].renderer.material.shader;
			buttons[0].renderer.material.shader = glowShader;
		} 
		else {
			glowColor = new Color(buttons[0].renderer.material.color.r, buttons[0].renderer.material.color.g, buttons[0].renderer.material.color.b, 255);
			originalColor = buttons[0].renderer.material.color;
			buttons[0].renderer.material.color = glowColor;
		}
		
	}
	
	void Update() {
		
		bool downBtn = WiiMote.wiimote_getButtonDown(Configuration.pointerWiiMote);
		bool upBtn   = WiiMote.wiimote_getButtonUp(Configuration.pointerWiiMote);
		bool isA     = WiiMote.wiimote_getButtonA(Configuration.pointerWiiMote);
		
		bool downKey = Input.GetKey(KeyCode.DownArrow);
		bool upKey   = Input.GetKey(KeyCode.UpArrow);
		bool enterKey = Input.GetKey(KeyCode.Return);
		
		bool backKey = Input.GetKey(KeyCode.Backspace);
		bool buttonB = WiiMote.wiimote_getButtonB(Configuration.pointerWiiMote);

		
		if (backKey && !isRoot || buttonB && !isRoot) {
			Configuration.saveConfig();
			navigateTo(backMenu);	
		}		
		
		
		buttonDownTimer += Time.deltaTime;
		
		if (downBtn||downKey) {
			if (buttonDownTimer >= 0.2f) {
				if (selectedButton + 1 <= buttons.Length) {
					
					for (int i=0; i < buttons.Length; i++) {
						if(usesShader) {
							buttons[i].renderer.material.shader = originalShader;
						}
						else {
							buttons[i].renderer.material.color = originalColor;
						}
						
					}				
					selectedButton ++;	
					if (usesShader) {
						buttons[selectedButton-1].renderer.material.shader = glowShader;
					}
					else {
						buttons[selectedButton-1].renderer.material.color = glowColor;
					
					}
					buttonDownTimer = 0f;
				}
				
			}			
			
		}
		
		if (upBtn||upKey) {
			if (buttonDownTimer >= 0.2f) {
				if (selectedButton > 1) {
					
					for (int i=0; i < buttons.Length; i++) {
						if(usesShader) {
							buttons[i].renderer.material.shader = originalShader;
						}
						else {
							buttons[i].renderer.material.color = originalColor;
						}
						
					}	
					selectedButton --;	
					if (usesShader) {
						buttons[selectedButton-1].renderer.material.shader = glowShader;
					}
					else {
						buttons[selectedButton-1].renderer.material.color = glowColor;
					
					}
					buttonDownTimer = 0f;
				}
			}
		}
		
		if (isA||enterKey) {
			if (accessible) {
				if(buttons[selectedButton-1].GetComponent<Menu>().checkButton() != null) {
					navigateTo(buttons[selectedButton-1].GetComponent<Menu>().checkButton());	
				}
			}	
			
		}
	}
	
	
	private void navigateTo(GameObject container) {
		
		/*for (int i=0; i< transform.childCount; i++) {
			transform.GetChild(i).gameObject.SetActive(visib);
		}*/
		container.SetActive(true);
		gameObject.SetActive(false);
		
			
	}
	
	
	
	
	
	// Update is called once per frame
	/*void Update () {
		bool isA = WiiMote.wiimote_getButtonA(Configuration.pointerWiiMote);
		Vector3 fwd;
		
		if (mouse) {
			fwd =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		else {
			fwd = Crosshair.getWiimoteCrosshair();
			
		}
	
		for (int i=0; i < buttons.Length; i++) {
			buttons[i].renderer.material.shader = originalShader;
		}		
		Debug.DrawRay(transform.position, fwd *100);
		RaycastHit hit;
		if (Physics.Raycast(transform.position, fwd, out hit, 4f)) {

			hit.collider.gameObject.renderer.material.shader = glowShader;
			if (isA) {
				if (hit.collider.GetComponent<Menu>()) {
					hit.collider.GetComponent<Menu>().checkButton();	
				}
			}
		}

		/*Vector3 mouseIn3D;
		
		if (mouse) {
			mouseIn3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		else {
			mouseIn3D = Crosshair.getWiimoteCrosshair();
			
		}
		
		Debug.DrawRay(transform.position,mouseIn3D);
		
		/*if (mouse) {
			Vector3 mouseIn3D = Camera.main.ScreenPointToRay(Input.mousePosition);
		}*/
		
	/*	RaycastHit hit;
		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(transform.forward), out hit, 9999f)) {
			
			if (isA) {
				hit.collider.GetComponent<Menu>().checkButton();
			}
			
			
			hit.collider.gameObject.renderer.material.shader = glowShader;
		} 
		
		else {
			for (int i=0 ; i < gameObject.transform.childCount ; i++) {
				transform.GetChild(i).renderer.material.shader = originalShader;
			}
		}
		
	}
	
	void OnGUI() {
		int c = WiiMote.wiimote_count();
		for (int i=0; i<=c-1; i++) {
			float ir_x = WiiMote.wiimote_getIrX(Configuration.pointerWiiMote);
			float ir_y = WiiMote.wiimote_getIrY(Configuration.pointerWiiMote);
		    if ( (ir_x != -100) && (ir_y != -100) ) {
			    float temp_x = ((ir_x + (float) 1.0)/ (float)2.0) * (float) Screen.width;
			    float temp_y = (float) Screen.height - (((ir_y + (float) 1.0)/ (float)2.0) * (float) Screen.height);
			    temp_x = Mathf.RoundToInt(temp_x);
			    temp_y = Mathf.RoundToInt(temp_y);
				//if ((mira_x != 0) || (mira_y != 0))
				GUI.DrawTexture ( new Rect (temp_x, temp_y, 64, 64), mira, ScaleMode.ScaleToFit, true, 1.0F);
		    }
		}
	}	
	
	
	*/
	
}
