using UnityEngine;
using System.Collections;

public class TargetManager : MonoBehaviour {

	public GameObject [] targetsFront;
	public GameObject [] targetsMiddle;
	public GameObject [] targetsBack;
	
	public bool firstWave, secondWave, thirdWave = false;
	
	public AudioClip testClip;
	private AudioSource audioSC;
	
	float audioTimer = 0f;
	bool audioPlayed, audioOver = false;
	
	
	// Use this for initialization
	void Start () {
		audioSC = gameObject.GetComponent<AudioSource>();
		firstWave = true;
		/*for (int i=0; i<targetsFront.Length; i++) {
			targetsFront[i].SetActive(true);
			
		}*/

	
	}
	
	void Update () {
		
		/*
		 * audio: "hola bienvenido bla bla"
		 * audio: "disparale a los blancos"
		 * 
		 * primera ronda: 
		 * 				- aparece 1 target.
		 * 
		 * 
		 * 				audio: "disparale a este mas lejos"
		 * 				- aparece target 2
		 * 
		 * 				comment: estaria piola usar un super ataque o algo asi con el arma de laser
		 * 				audio: "usa el super ataque (?)"
		 * 				- aparece target 3
		 * 				
		 * segunda ronda:
		 * 			
		 * 				audio: "ahora se mueven papa"
		 *				- aparece target 1 moviendose
		 *
		 *
		 * 				audio: "ahora disparale a las dos en el menor tiempo"  ni en pedo hago un contador, es todo chamuyo
		 * 				- aparece target 2
		 * 
		 * tercera ronda:
		 * 
		 * 				audio: "ahora te van a disparar, esquivala guachin"
		 * 				-aparece target1, ataca lento
		 * 
		 * 				audio: "a probar tus reflejos guachin"
		 * 				- aparecen target 1,2 y 3, todas atacan y un poco mas rapido
		 * 
		 * 				audio: "eso concluye la demo *estatica* *se rompe todo*"
		 * 
		 * 
		 * */
		
		if (firstWave) 
		{
			if (playSound(testClip))
			{	
				print("ea ea ea");
				targetsFront[0].SetActive(true);
				targetsFront[0].animation.Play("levantar");
			}
			
			
			
		}
		
	
	}
	
	private bool playSound(AudioClip audioIN) {
		
		if(!audioPlayed) {
			audioSC.PlayOneShot(audioIN);
			audioPlayed = true;	
		}
			
		if (!audioOver) {	
			audioTimer += Time.deltaTime;
			
			if (audioTimer>= audioIN.length) {
				audioTimer = 0f;
				audioOver = true;
				return true;
			}
			else {
				audioOver = false;
				return false;	
			}
		}
		return false;
	}
	
}
