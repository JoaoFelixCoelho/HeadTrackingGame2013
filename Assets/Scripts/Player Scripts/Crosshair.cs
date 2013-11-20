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
	public Vector3 limit;
	private static Vector3 oldPos = new Vector3(1,1,1);
	
	void Start () {
		WiiMote.wiimote_start();
	}
 	
	
	
	void Update() {
		//gameObject.transform.LookAt(getWiimoteCrosshair());	
	}
 
		
	public static Vector3 getWiimoteCrosshair() {
		

		
		float ir_x = WiiMote.wiimote_getIrX(Configuration.pointerWiiMote);
		float ir_y = WiiMote.wiimote_getIrY(Configuration.pointerWiiMote);	

		ir_x = ( Screen.width / 2) + ir_x * (float) Screen.width / (float)2.0;
		ir_y = Screen.height - (ir_y * (float) Screen.height / (float)2.0);

		if (ir_x > 0 && ir_y >0) {
			oldPos = Camera.main.camera.ScreenToWorldPoint(new Vector3(ir_x + 37f, Screen.height * 1.5f - ir_y - 37f , 20));	
			return Camera.main.camera.ScreenToWorldPoint(new Vector3(ir_x + 37f, Screen.height * 1.5f - ir_y - 37f , 20));	
		}
		else {	
			return oldPos;
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
				GUI.DrawTexture ( new Rect (temp_x, temp_y, 64, 64), mira, ScaleMode.ScaleToFit, true, 1.0F);
		    }
		}
	}
}
