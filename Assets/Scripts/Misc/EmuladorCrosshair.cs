using UnityEngine;
using System.Collections;

public class EmuladorCrosshair : MonoBehaviour {
	
	private string display;
	private float mira_x, mira_y;
	public Texture2D mira;
	private Vector3 oldVec;
	private Vector3 vec3Look;
	public GameObject arma;	
	public GameObject cubo;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		mira_x = Input.mousePosition.x;
		mira_y = Input.mousePosition.y;
		vec3Look = camera.ScreenToWorldPoint(new Vector3(mira_x, mira_y, 20));
		arma.transform.LookAt(vec3Look);
			
			
		
	
	}
	
	void OnGUI() {

		    /*float temp_x = ((ir_x + (float) 1.0)/ (float)2.0) * (float) Screen.width;
		    float temp_y = (float) Screen.height - (((ir_y + (float) 1.0)/ (float)2.0) * (float) Screen.height);
		    temp_x = Mathf.RoundToInt(temp_x);
		    temp_y = Mathf.RoundToInt(temp_y);
			//if ((mira_x != 0) || (mira_y != 0))*/
			float temp_x = Input.mousePosition.x;
			float temp_y = Input.mousePosition.y;
		
		
			GUI.DrawTexture ( new Rect (temp_x, temp_y, 64, 64), mira, ScaleMode.ScaleToFit, true, 1.0F);
	}	
	
	
}
