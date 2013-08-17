using UnityEngine;
using System.Collections;

public class MenuHoverController : MonoBehaviour {
	public Shader glowShader;
	private Shader diffuseShader;
	private Color originalColor;
	// Use this for initialization
	void Start () {
		diffuseShader = Shader.Find("Transparent/Bumped Diffuse");
	}
	
	// Update is called once per frame
	void Update () {
		Ray mouseIn3D = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(mouseIn3D, out hit)) {
			hit.collider.gameObject.renderer.material.shader = glowShader;
		} else {
			for (int i=0 ; i < gameObject.transform.childCount ; i++) {
				transform.GetChild(i).renderer.material.shader = diffuseShader;
			}
		}
		
	}
}
