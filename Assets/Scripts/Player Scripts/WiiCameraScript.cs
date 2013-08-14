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
	
	void Start () {
		wiimote_start();
	}
	
	
	void Update () {
		
		MoveCamera();
		
		
		
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
		//Camera.main.fieldOfView = 80;
		X = wiimote_getIrX(0)*10;
		Y = wiimote_getIrY(0)*10;
		Z = 0f;
		gameObject.transform.LookAt(center);
		transform.position = new Vector3(X, Y, Z);

	}

	
		
	
}

