using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class WiiCameraScript: MonoBehaviour {
	
	[DllImport ("UniWii")]
	private static extern void wiimote_start();
	[DllImport ("UniWii")]
	private static extern void wiimote_stop();
	[DllImport ("UniWii")]
	private static extern int wiimote_count();
	[DllImport ("UniWii")]	
private static extern bool wiimote_enableIR( int which );
	
	[DllImport ("UniWii")]
	private static extern bool wiimote_isIRenabled( int which );
	
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
	
	
	float X;
	float Y;
	float Z;
	public Transform center;
	Vector3 vec;
	Vector3 oldVec;
	public GUIText debug;
	private float midY;
	void Start () {
		Camera.main.fieldOfView = 80;
		wiimote_start();
		wiimote_enableIR(0);
		midY = transform.position.y;
		X = wiimote_getIrX(0)*10;
		Y = wiimote_getIrY(0)*10;
		Z = 100f;
		if (X==-1000) {
			oldVec = transform.position;
		}
		else {
			oldVec = new Vector3(X,Y,Z);
		}
		
	}
	
	
	void Update () {
		
		MoveCamera();
		//OnGUI();
		
		
		//transform.position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		//Vector3 p = transform.position = Camera.main.ScreenToViewportPoint(new Vector3(X, Y, Z));
		
		//transform.position = Camera.main.ScreenToViewportPoint(new Vector3(0.5, 0.2, 0));	
		//this.camera.main.transform.rotation.Set (10, 10, 10);
		
		
		//var rotation = Quaternion.Euler(x, y, z);
		//transform.rotation = rotation;
		//var MyCamara = Camera;
		//MyCamera.transform.position.x = 81;
    	//MyCamera.transform.position.y = 81;
		//MyCamara.transform.position.z = 76;
		
		
	}
	

	void MoveCamera() {
/*			int c = wiimote_count();
		if (c>0) {
			for (int i=0; i<=c-1; i++) {*/
		X = wiimote_getIrX(0)*10;
		Y = wiimote_getIrY(0)*10;

		Z = 100f;
		
		
		transform.position = oldVec;
		if(X>-9.5f && X<9.5f){
			oldVec = new Vector3(X,Y,Z);
		} 
		
		//if (X==-1000 || Y<-0){
		//	transform.position = oldVec;
		//}
		//else if(X<=9f) {
		//	vec = new Vector3(X, Y, Z);
		//	oldVec = vec;
    	//	transform.position = vec;
		//}
		
		/*if(X<=9f){
			if (X==-1000 || Y<-0){
				transform.position += oldVec;
			} else {
				vec = new Vector3(X, Y, Z);
				oldVec = vec;
				transform.position += vec;	
			}
		}*/
		
		gameObject.transform.LookAt(center);
		debug.text = "x:" + X + " Y:" + Y;
	}
	

}


