using UnityEngine;
using System.Collections;

public class Bloom_Autowalker : MonoBehaviour {

	private Bloom bloom;
	public float toIntensity;
	public float fadeTime;
	public float walkSpeed;
	private float frameIntensity;
	private float timerDelta;
	private bool bloomAnimOver = false;
	private bool walkAnimOver  = false;
	
	
	public float amplitud;
	public float T;
	public float B;
	
	private float spc = 0f;
	private float toScale;
	private float animTime;
	bool headUp = true;
	
	
	
	void Start () {
		bloom = gameObject.GetComponent<Bloom>();
		
		frameIntensity = (bloom.bloomIntensity - toIntensity) / fadeTime;
		animTime = (1f-toScale) / 0.5f ;
	}
	
	
	private void checkInt() {
		if (timerDelta < fadeTime) {
			timerDelta += Time.deltaTime;
			bloom.bloomIntensity -= frameIntensity * Time.deltaTime;
		}
		
		else {
			bloomAnimOver = true;	
			timerDelta = 0f;
			//animation.Play();
		}
		
	}
	
	private void checkWalk() {
		spc += Time.deltaTime;
		// si mira Sandra, lo que me ensenaste sirvio para algo!!
		transform.localPosition += new Vector3(0, amplitud * (Mathf.Sin( ((2f * Mathf.PI) / T) * spc)) ,1) * Time.deltaTime * walkSpeed;
	}

	
	
	// Update is called once per frame
	void Update () {
		if(!bloomAnimOver) {
			checkInt();
		}
		else if (!walkAnimOver) {
				checkWalk();
		}

	}
}
