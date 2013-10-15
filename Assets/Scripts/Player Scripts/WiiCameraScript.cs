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
	
	public float Xlimit = 10f;
	public float Ylimit = 10f;
	
	
	
	void Start () {
		
		WiiMote.wiimote_start();
		midY = transform.position.y;
		X = WiiMote.wiimote_getIrX(0)*10;
		Y = WiiMote.wiimote_getIrY(0)*10;
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

		X = WiiMote.wiimote_getIrX(0) * 10;
		Y = WiiMote.wiimote_getIrY(0) * 2f;

		Z = 60f;
		
		
		transform.position = oldVec;
		if(X>-9.5f && X<9.5f){
			oldVec = new Vector3(-X, - Y + 1.8f ,Z);
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


