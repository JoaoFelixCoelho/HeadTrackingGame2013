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
	public AudioClip [] footsteps;
	public AudioSource source;
	
	private bool isStepping;
	private float spc = 0f;
	private int i = 0;
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
			timerDelta = 0.9f;
		}
		
	}
	
	private void checkWalk() {
		spc += Time.deltaTime;
		transform.localPosition += new Vector3(0, amplitud * (Mathf.Sin( ((2f * Mathf.PI) / frecuencia) * spc)) ,1) * Time.deltaTime * walkSpeed;
		
		if ((amplitud * (Mathf.Sin( ((2f * Mathf.PI) / frecuencia) * spc))) <= -amplitud + 0.00012f){
			if (!source.isPlaying) {
				i = Random.Range(0, footsteps.Length);
				source.PlayOneShot(footsteps[i]);
				
			}
		}
	}
	
	
	private void step() {
		timerDelta += Time.deltaTime;
		checkWalk();
	}

	
	
	void FixedUpdate () {
		if(!bloomAnimOver) {
			checkInt();
		}
		else if (!walkAnimOver) {
				step();
		}

	}
}
