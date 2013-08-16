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
	public float frecuencia;
	public AudioClip footstep;
	public AudioSource source;
	
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
			source.PlayOneShot(footstep);
		}
		
	}
	
	private void checkWalk() {
		spc += Time.deltaTime;
		transform.localPosition += new Vector3(0, amplitud * (Mathf.Sin( ((2f * Mathf.PI) / frecuencia) * spc)) ,1) * Time.deltaTime * walkSpeed;
	}
	
	private void checkFootstep() {
		
	}
	
	private void step() {
		checkWalk();
		checkFootstep();
	}

	
	
	// Update is called once per frame
	void Update () {
		if(!bloomAnimOver) {
			checkInt();
		}
		else if (!walkAnimOver) {
				step();
		}

	}
}
