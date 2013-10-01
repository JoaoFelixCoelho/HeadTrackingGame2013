using UnityEngine;
using System.Collections;

public class FadeGUI : MonoBehaviour {
	
	public float newAlpha = 0f;
	public float timeToFade = 5f;
	private float alphaRate;
	private float counter;
	private bool fadeEnable = true;
	
	void Start () {
		alphaRate = 1f/timeToFade;
		newAlpha = 0;
	}
	
	public void startAnim(){
		newAlpha = 0f;
		counter = 0f;
		this.fadeEnable = true;	
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (this.fadeEnable) {
			newAlpha += alphaRate * Time.deltaTime;
			Color oldCol = guiText.material.color;
			guiText.material.color = new Color(oldCol.r, oldCol.g, oldCol.b, newAlpha);
			counter += Time.deltaTime;
			
			if (counter >= timeToFade) {
				animation.Play();
				this.fadeEnable = false;
			}
		}
		
		
	
	}
}
