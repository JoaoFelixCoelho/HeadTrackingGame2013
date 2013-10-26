using UnityEngine;
using System.Collections;

public class TargetManager : MonoBehaviour {

	public GameObject [] targets;
	
	private AudioSource audioSC;
	
	float audioTimer = 0f;
	bool audioPlayed, audioOver = false;
	
	public AudioClip [] clips;
	private int contador     = 1;
	private int animsPlayed  = 0;
	private int soundsPlayed = 0;
	private int targetWave   = 1;
	
	
	
	// Use this for initialization
	void Start () {
		audioSC = gameObject.GetComponent<AudioSource>();
	}
	
	void FixedUpdate () {
		
		Transform currTarget = targets[contador - 1].transform.GetChild(0);
		
		
  		if (soundsPlayed == contador && !audioSC.isPlaying) {
		
  			if (animsPlayed == contador) 
			{
  				if (currTarget.GetComponent<HealthSystem>().isDead) 
				{	
					Destroy(currTarget.transform.parent.gameObject);
					contador ++;
					if(contador > targets.Length) {
						gameObject.GetComponent<RoundManager>().startNewRound();
						this.enabled = false;

					}

  				}
				
  			}
			
			
  			else 
			{
				if (animsPlayed < contador) {
					targets[contador - 1].SetActive(true);
					if(!currTarget.animation.isPlaying) {
						if (currTarget.animation.GetClipCount() > 1) {
							currTarget.animation.CrossFade("MoveTargetCircle");
			  				animsPlayed ++;
						}
						else {
							animsPlayed ++;
						}
					}
				}
  			}
  
  		}
		else
		{
			if (soundsPlayed < contador) {
	  			audioSC.clip = clips[contador - 1];
				audioSC.Play();
	 			soundsPlayed ++;
			}
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
		 * 				audio: "disparale a este mas lejos"
		 * 				- aparece target 3
		 * 				
		 * segunda ronda:
		 * 			
		 * 				audio: "mantene tal boton y sale super ataque"
		 *				- aparece target 1 
		 *
		 *
		 * 				audio: "ahora disparale a estas targets moviendose
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
	
	/*private void spawnTarget() {
		if (!targetSpawned) {
			targets[num].SetActive(true);
			targets[num].animation.Play("levantar");
			targetSpawned = true;	
		}	
	}
		
	private void checkTarget () {
		if (targets[num].GetComponent<HealthSystem>().isDead){
			targetSpawned = false;			
		}
		
	}
		
		*/
		
		
		
		/*
		if (firstWave) 
		{
			if (!lastTargetDead) {
				HealthSystem currentTarget = targets[num].GetComponent<HealthSystem>();
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
					targets[num].SetActive(true);
					targets[num].animation.Play("levantar");
					lastTargetDead = false;
				}
			}
			

			
			
			
		}
		*/
	
	
/*	private IEnumerator playSound() {
		
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
		return false;
	}*/
	
}
