using UnityEngine;
using System.Collections;

public class MenuHoverController : MonoBehaviour {
	public Shader glowShader;
	private Shader diffuseShader;
	private Color originalColor;
	public GameObject [] buttons;
	
	public bool mouse;
	public Texture2D mira;
	
	
	// Use this for initialization
	void Start () {
		diffuseShader = Shader.Find("Transparent/Bumped Diffuse");
	}
	
	// Update is called once per frame
	void Update () {
		bool isA = WiiMote.wiimote_getButtonA(WiiMote.pointerWiimote);
		Vector3 fwd;
		
		if (mouse) {
			fwd =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		else {
			fwd = Crosshair.getWiimoteCrosshair();
			
		}
	
		for (int i=0; i < buttons.Length; i++) {
			buttons[i].renderer.material.shader = diffuseShader;
		}		
		
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
				transform.GetChild(i).renderer.material.shader = diffuseShader;
			}
		}
		*/
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
