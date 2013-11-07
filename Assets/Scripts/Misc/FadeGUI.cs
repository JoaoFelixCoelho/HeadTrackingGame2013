using UnityEngine;
using System.Collections;

public class FadeGUI : MonoBehaviour {
	
	public float newAlpha = 0f;
	public float timeToFade = 5f;
	private float alphaRate;
	private float counter;
	private bool fadeEnable = true;
	
	void OnEnable () {
		alphaRate = 1f/timeToFade;
		newAlpha = 0;
		transform.position = new Vector3(0.5f, 0.5f, 0.5f);
		newAlpha = 0f;
		counter = 0f;
		fadeEnable = true;	
	}

	
	// Update is called once per frame
	void FixedUpdate () {
		if (fadeEnable) {
			newAlpha += alphaRate * Time.deltaTime;
			Color oldCol = guiText.material.color;
			guiText.material.color = new Color(oldCol.r, oldCol.g, oldCol.b, newAlpha);
			counter += Time.deltaTime;
			if (counter >= timeToFade) {
				animation.Play();
				this.fadeEnable = false;
				this.enabled = false;
			}
		}
		
		
	
	}
}
