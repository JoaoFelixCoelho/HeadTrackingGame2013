using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	
	AudioSource audioSC;
	public float audioFadeTime = 10f;
	private float songTimer = 0f;
	public float firstAudio;
	private bool started = false;
	public AudioClip [] songs;
	int contador = 0;
	float lastSongLength = 0f;
	
	// Use this for initialization
	void Start () {
		/*Object [] tmpResources = Resources.LoadAll("Audio/Music/GameSongs");
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
	*/
	}
	
	// Update is called once per frame
	void Update () {
		
		songTimer += Time.deltaTime;
		
		if (songTimer >= firstAudio && !started) {
			started = true;
			gameObject.GetComponent<RoundManager>().startNewRound();
			songTimer = 0f;
		}
		
		if(songTimer >= lastSongLength && started) {
			audio.clip = songs[contador];
			audio.Play();
			lastSongLength = songs[contador].length;
			contador ++;
			songTimer = 0f;
			
		}
	
		
	
	}
}
