using UnityEngine;
using System.Collections;

public class TargetManager : MonoBehaviour {

	public GameObject [] targetsFront;
	public GameObject [] targetsMiddle;
	public GameObject [] targetsBack;
	
	private int num = 0;
	private bool targetSpawned = false;
	
	public bool firstWave, secondWave, thirdWave = false;
	
	public AudioClip [] clips;
	private AudioSource audioSC;
	
	float audioTimer = 0f;
	bool audioPlayed, audioOver = false;
	
	
	// Use this for initialization
	void Start () {
		audioSC = gameObject.GetComponent<AudioSource>();
		firstWave = true;	
		playSound();
	}
	
	void Update () {
		
		if (!audioSC.isPlaying && audioOver) {
			spawnTarget();
			
		}
		
		
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
		
	/*	if (!audioSC.isPlaying) {
			audioSC.clip = clips[num];
			audioSC.Play();
		}
		
		audioTimer += Time.deltaTime;
		
		if (audioOver && audioTimer >= audioSC.clip.length) {
			audioOver = true;
			num ++;
			spawnTarget();
			checkTarget();
			
		}*/
		
	}
	
	private void spawnTarget() {
		if (!targetSpawned) {
			targetsFront[num].SetActive(true);
			targetsFront[num].animation.Play("levantar");
			targetSpawned = true;	
		}	
	}
		
	private void checkTarget () {
		if (targetsFront[num].GetComponent<HealthSystem>().isDead){
			targetSpawned = false;			
		}
		
	}
		
		
		
		
		
		/*
		if (firstWave) 
		{
			if (!lastTargetDead) {
				HealthSystem currentTarget = targetsFront[num].GetComponent<HealthSystem>();
				print (currentTarget.name);
				
				if (currentTarget.isDead) {
					
					num++;
					lastTargetDead = true;
					
					
				}	
			}
			else {
			
				
				if (playSound(clips[num]))
				{	
					print("ea ea ea");
					targetsFront[num].SetActive(true);
					targetsFront[num].animation.Play("levantar");
					lastTargetDead = false;
				}
			}
			

			
			
			
		}
		*/
	
	
	private IEnumerator playSound() {
		
		audioSC.clip = clips[num];
		audioSC.Play();
		
		yield return new WaitForSeconds(audioSC.clip.length);
		
		/*
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
		return false;*/
	}
	
}
