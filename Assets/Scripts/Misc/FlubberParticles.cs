using UnityEngine;
using System.Collections;

public class FlubberParticles : MonoBehaviour {
	public ParticleSystem shootParticles;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (shootParticles.startSpeed>=-0.6f){
			shootParticles.startSpeed-=0.1f;
			
		}
		else{
			Destroy(this);
		}
}
}