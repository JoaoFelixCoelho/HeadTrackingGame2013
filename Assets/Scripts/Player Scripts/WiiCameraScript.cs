using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class WiiCameraScript: MonoBehaviour {
	
	float X;
	float Y;
	float Z;
	public Transform center;
	Vector3 vec;
	Vector3 oldVec;
	public GUIText debug;
	private float midY;
	public bool menu;
	
	public float Xlimit = 10f;
	public float Ylimit = 10f;
	
	void Start () {
		WiiMote.wiimote_start();
		midY = transform.position.y;
		X = WiiMote.wiimote_getIrX(WiiMote.cameraWiimote)*10;
		Y = WiiMote.wiimote_getIrY(WiiMote.cameraWiimote)*10;
		Z = 100f;
		if (X==-1000) {
			oldVec = transform.position;
		}
		else {
			oldVec = new Vector3(-X, - Y + 1.8f ,Z);
		}
	}
	
	
	void Update () {
		MoveCamera();		
	}
	
	void MoveCamera() {
		X = WiiMote.wiimote_getIrX(WiiMote.cameraWiimote) * 10;
		Y = WiiMote.wiimote_getIrY(WiiMote.cameraWiimote) * 2f;

		Z = 60f;

		transform.position = oldVec;
		if(X>-9.5f && X<9.5f){
			oldVec = new Vector3(-X, - Y + 1.8f ,Z);
			if (menu == true){
				gameObject.transform.LookAt(oldVec);
			}
		}
		
		gameObject.transform.LookAt(center);
	}
}


