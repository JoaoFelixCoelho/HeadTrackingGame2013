using UnityEngine;
using System.Collections;

public class MenuHoverController : MonoBehaviour {
	public Shader glowShader;
	private Shader diffuseShader;
	private Color originalColor;
	
	public bool mouse;
	public Texture2D mira;
	
	
	// Use this for initialization
	void Start () {
		diffuseShader = Shader.Find("Transparent/Bumped Diffuse");
	}
	
	// Update is called once per frame
	void Update () {
		bool isA = WiiMote.wiimote_getButtonA(WiiMote.pointerWiimote);
		
		
		float ir_x = WiiMote.wiimote_getIrX(WiiMote.pointerWiimote);
		float ir_y = WiiMote.wiimote_getIrY(WiiMote.pointerWiimote);


		float temp_x = ( Screen.width / 2) + ir_x * (float) Screen.width / (float)2.0;
		float temp_y = Screen.height - (ir_y * (float) Screen.height / (float)2.0);
		temp_x = Mathf.RoundToInt(temp_x);
		temp_y  = Mathf.RoundToInt(temp_y);
		
		Ray mouseIn3D = new Ray();
		
		if (temp_x > 0 && temp_y >0) {
			mouseIn3D = Camera.main.camera.ScreenPointToRay(new Vector3(temp_x + 37f, Screen.height * 1.5f - temp_y - 37f , 20));
		}
		
		/*if (mouse) {
			Vector3 mouseIn3D = Camera.main.ScreenPointToRay(Input.mousePosition);
		}*/
		
		RaycastHit hit;
		if (Physics.Raycast(mouseIn3D, out hit)) {
			
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
