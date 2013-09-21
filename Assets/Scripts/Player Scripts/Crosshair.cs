using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class Crosshair : MonoBehaviour {

	[DllImport ("UniWii")]
	private static extern void wiimote_start();
 
	[DllImport ("UniWii")]
	private static extern int wiimote_count();
 
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccX(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccY(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccZ(int which);
 
	[DllImport ("UniWii")]
	private static extern float wiimote_getIrX(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getIrY(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getRoll(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getPitch(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getYaw(int which);
 
	
	// mis attrs
	private string display;
	private int mira_x, mira_y;
	public Texture2D mira;
	private Vector3 oldVec;
	private Vector3 vec3Look;
	public GameObject arma;

	
	
	// Use this for initialization
	void Start () {
		wiimote_start();
		//mira = (Texture2D) Resources.Load("crosshair");
	}
 
	// Update is called once per frame
	void FixedUpdate () {
		int c = wiimote_count();
		int pepe=2;
	//	if (c>0) {
		//	display = "";
		//	for (int i=0; i<=c-1; i++) {
				int x = wiimote_getAccX(pepe);
				int y = wiimote_getAccY(pepe);
				int z = wiimote_getAccZ(pepe);
				float roll = Mathf.Round(wiimote_getRoll(pepe));
				float p = Mathf.Round(wiimote_getPitch(pepe));
				float yaw = Mathf.Round(wiimote_getYaw(pepe));
				float ir_x = wiimote_getIrX(pepe);
				float ir_y = wiimote_getIrY(pepe);
				//display += "Wiimote " + i + " accX: " + x + " accY: " + y + " accZ: " + z + " roll: " + roll + " pitch: " + p + " yaw: " + yaw + " IR X: " + ir_x + " IR Y: " + ir_y + "\n";
				//if (!float.IsNaN(roll) && !float.IsNaN(p) && (i==c-1)) {

				    Vector3 vec = new Vector3(p, 0 , -1 * roll);
				    vec = Vector3.Lerp(oldVec, vec, Time.deltaTime * 5);
				    oldVec = vec;
					print("x: " + oldVec.x + " y: " +  oldVec.y);
					//oldVec.z = -(transform.position.x - Camera.mainCamera.transform.position.x);
					
				//}
			   // if ( (i==c-1) && (ir_x != -100) && (ir_y != -100) ) {
				    	//float temp_x = ((ir_x + (float) 1.0)/ (float)2.0) * (float) Screen.width;
				    	//float temp_y = (float) Screen.height - (((ir_y + (float) 1.0)/ (float)2.0) * (float) Screen.height);
				    	float temp_x = ( Screen.width / 2) + ir_x * (float) Screen.width / (float)2.0;
				    	float temp_y = Screen.height - (ir_y * (float) Screen.height / (float)2.0);
				    	mira_x = Mathf.RoundToInt(temp_x);
				    	mira_y = Mathf.RoundToInt(temp_y);
						vec3Look = camera.ScreenToWorldPoint(new Vector3(mira_x, Screen.height*1.5f - mira_y , -20));
						arma.transform.LookAt(vec3Look);
					print ("hola" + vec3Look);
			//	}
	//		}
	//	}
	//	else display = "Press the '1' and '2' buttons on your Wii Remote.";


		    
	}
 
	void OnApplicationQuit() {
		
	}
 
	void OnGUI() {
		//GUI.Label( new Rect(10,10, 500, 100), display);
		//if ((mira_x != 0) || (mira_y != 0)) GUI.Box ( new Rect (mira_x, mira_y, 50, 50), mira); //"Pointing\nHere");
		int c = wiimote_count();
		for (int i=0; i<=c-1; i++) {
			float ir_x = wiimote_getIrX(i);
			float ir_y = wiimote_getIrY(i);
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
