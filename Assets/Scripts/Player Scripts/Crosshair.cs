using UnityEngine;
using System;
using System.Collections;

public class Crosshair : MonoBehaviour {


	
	// mis attrs
	private string display;
	private int mira_x, mira_y;
	public Texture2D mira;
	private Vector3 oldVec;
	private Vector3 vec3Look;
	public GameObject arma;
	private int fireRate;
	private bool limitChange=false;
	private bool aversh;
	
	

	// Use this for initialization
	void Start () {
		WiiMote.wiimote_start();
		//mira = (Texture2D) Resources.Load("crosshair");
	}
 
	// Update is called once per frame
	void FixedUpdate () {
		int c = WiiMote.wiimote_count();
		bool isE = Input.GetKey(KeyCode.E);
		bool isQ = Input.GetKey(KeyCode.Q);
		bool isB = WiiMote.wiimote_getButtonB(WiiMote.pointerWiimote);
		bool isA = WiiMote.wiimote_getButtonA(WiiMote.pointerWiimote);
		bool isHome = WiiMote.wiimote_getButtonHome(WiiMote.pointerWiimote);
		bool isMinus = WiiMote.wiimote_getButtonMinus(WiiMote.pointerWiimote);
		bool isBtnLeft = WiiMote.wiimote_getButtonLeft(WiiMote.pointerWiimote);
		bool isBtnRight = WiiMote.wiimote_getButtonRight(WiiMote.pointerWiimote);
		
		if(isB) {
			arma.GetComponent<Arma>().shoot();
			
		}
		
		if(isA) {
			Enemy.player.GetComponent<PlayerBehave>().reloadWeapon();
			
		}
		if(isBtnRight) {
			Enemy.player.GetComponent<PlayerBehave>().cambioArma();
		}

		if(isBtnLeft) {
			Enemy.player.GetComponent<PlayerBehave>().cambioArma();
		}
		if(isHome || isMinus && aversh == false) {
			aversh =true;
			Enemy.player.GetComponent<PlayerBehave>().pauseGame();
		}
		else {
			aversh= false;
		}
	

		float roll = Mathf.Round(WiiMote.wiimote_getRoll(WiiMote.pointerWiimote));
		float p = Mathf.Round(WiiMote.wiimote_getPitch(WiiMote.pointerWiimote));
		float yaw = Mathf.Round(WiiMote.wiimote_getYaw(WiiMote.pointerWiimote));
		float ir_x = WiiMote.wiimote_getIrX(WiiMote.pointerWiimote);
		float ir_y = WiiMote.wiimote_getIrY(WiiMote.pointerWiimote);

		Vector3 vec = new Vector3(p, 0 , -1 * roll);
		vec = Vector3.Lerp(oldVec, vec, Time.deltaTime * 5);
		oldVec = vec;

		float temp_x = ( Screen.width / 2) + ir_x * (float) Screen.width / (float)2.0;
		float temp_y = Screen.height - (ir_y * (float) Screen.height / (float)2.0);
		mira_x = Mathf.RoundToInt(temp_x);
		mira_y = Mathf.RoundToInt(temp_y);
		if (mira_x > 0 && mira_y >0) {
			vec3Look = Camera.main.camera.ScreenToWorldPoint(new Vector3(mira_x + 37f, Screen.height * 1.5f - mira_y - 37f , 20));
			arma.transform.LookAt(vec3Look);
		}
		    
	}
 
		
		

 
	void OnGUI() {
		int c = WiiMote.wiimote_count();
		for (int i=0; i<=c-1; i++) {
			float ir_x = WiiMote.wiimote_getIrX(WiiMote.pointerWiimote);
			float ir_y = WiiMote.wiimote_getIrY(WiiMote.pointerWiimote);
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
}
