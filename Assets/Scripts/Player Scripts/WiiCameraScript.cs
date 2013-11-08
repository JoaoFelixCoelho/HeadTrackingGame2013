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
	private float midY;
	public bool menu;
	
	void Start () {
		if (menu == true){
		
		}
		else{
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
	}
	
	
	void Update () {
		MoveCamera();
		OnGUI();
	}
	
	void MoveCamera() {
		X = WiiMote.wiimote_getIrX(WiiMote.cameraWiimote) * 10;
		Y = WiiMote.wiimote_getIrY(WiiMote.cameraWiimote) * 2f;

		Z = 60f;
		
		if(X>-9.5f && X<9.5f && Y<1f){
			if (menu == true){
				vec = new Vector3(-X, -Y ,0f);
				gameObject.transform.LookAt(vec);
			}
			else{
				transform.position = oldVec;
				oldVec = new Vector3(-X, -Y+1.8f ,Z);
				gameObject.transform.LookAt(center);
			}
		}
	}
	
	void OnGUI() {
        GUI.Label(new Rect(10, 50, 300, 300), "Y: "+ Y + "X: " +X);
    }
}


