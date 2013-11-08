using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	
	AudioClip [] songs;
	AudioSource audioSC;
	public float audioFadeTime = 10f;
	
	// Use this for initialization
	void Start () {
		Object [] tmpResources = Resources.LoadAll("Audio/Music/GameSongs");
		if(tmpResources != null) {
			songs = new AudioClip[tmpResources.Length];
			for (int i=0; i<tmpResources.Length; i++) {
				songs [i] = (AudioClip) tmpResources[i];
			}
			audioSC = gameObject.GetComponent<AudioSource>();
			audioSC.clip = songs[Random.Range(0,songs.Length)];
			audioSC.Play();
		} else {
			print ("no se encontraron canciones ");
			this.enabled = false;	
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		if (audioSC.time >= (audioSC.time - audioFadeTime)) {
		}
		
	
	}
}
