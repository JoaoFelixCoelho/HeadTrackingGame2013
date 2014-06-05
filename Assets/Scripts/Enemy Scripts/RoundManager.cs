using UnityEngine;
using System.Collections;

public class RoundManager : MonoBehaviour {
		
	public static float interval; 
	public bool isSpawning;
	
	public Transform [] spawnPoints;
	
	
	public GameObject secondaryWeaponPrefab, weaponSpawn;
	public PlayerBehave player;
	public static HSController dbController = new HSController();
	
	float waveInterval;
	float waveDeltaTime = 0f;	
	public bool roundStarted = false;	
	
	
	
	void Start () {	
		startNewRound();
		//lo tuve que poner aca porque no andaba, alta negrada :/
		Stats.read();
		Configuration.readConfig();
		GridSystem.setGridLength(spawnPoints.Length);
		for (int i=0; i<spawnPoints.Length; i++) {
			GridSystem.grids[i].xPos = spawnPoints[i].transform.position.x;	
		}
		
		if (Round.isTargetRound) {
			gameObject.GetComponent<TargetManager>().enabled = true;
		}

		
	}	
	
	public void startNewRound(){
		Enemy.numerator = 0;
		Enemy.current = 0;
		player.killsInRound = 0;
		if(Round.number > 0) {
			int randomRestore = Round.number*10 + Random.Range(20,50);
			if(player.GetComponent<HealthSystem>().currHp + randomRestore <= player.GetComponent<HealthSystem>().totHp) {
				player.GetComponent<HealthSystem>().currHp += randomRestore;
				player.msgGUI.showMsg("Health restored partially");
			}
		}
		else {
			Camera.main.animation.Play("playerSpawn");	
		}
		
		waveDeltaTime = 0f;
		Round.next();	
		updateEnemiesLeft();
		
		//roundAlert.GetComponent<FadeGUI>().startAnim();
		//roundAlert.transform.position = new Vector3(0.5f, 0.5f, 0.5f);

		roundStarted = false;
		if(Round.weaponSpawned && !Round.weaponPicked) {
			weaponSpawn.SetActive(true);
			Instantiate(secondaryWeaponPrefab);
		}
	}	
	
	
	public void updateEnemiesLeft() {
		if (!Round.isTargetRound) {
			player.enemyCounter.text = "Restantes: " + (Round.destinyQuant - player.killsInRound);
		}
	}	

	void FixedUpdate () {
		
		waveDeltaTime+=Time.deltaTime;	
		if (!Round.isTargetRound && !Round.gameOver) {
			if(roundStarted) {
				if (Enemy.current + player.killsInRound < Round.destinyQuant) {
					spawnEnemy();
				}
				else if(player.killsInRound==Round.destinyQuant) {
					startNewRound();
				}
			}
			
			else{
				newRoundWarning();
			}
		} else if (Round.gameOver) {
			StartCoroutine(dbController.PostScores(PlayerBehave.playerName, PlayerBehave.score));	
		}

		
		
	}
	
	private void newRoundWarning() {
		
		roundStarted = true;
	
		if (!Round.gameOver) {
			player.sendMessageToPlayer("round " + (Round.number-1), "RoundWarning");
		}
		else {
			player.sendMessageToPlayer("Game Over!", "RoundWarning");
		}
		
	}	
	
	
	public void spawnEnemy() {
		if(waveDeltaTime>=RoundManager.interval) {
			int randomSpawn = Random.Range(0,spawnPoints.Length);
			if (Round.isTargetRound) {
				gameObject.GetComponent<TargetManager>().enabled = true;
			}
			else {
				gameObject.GetComponent<TargetManager>().enabled = false;
				int spawnRate  = Random.Range(0,101);
				int randomType = Random.Range(0,Round.type.Length);		
				Enemy.createEnemy(Round.type[randomType], Round.number, spawnPoints[randomSpawn], randomSpawn);
				GridSystem.grids[randomSpawn].currentEnemies ++;
				waveDeltaTime = 0;
				Enemy.current++;				
				
			}
			
			/*Instantiate (
							spawnParticlePrefab, 
							new Vector3(spawnPoints[randomSpawn].transform.position.x, 0, spawnPoints[randomSpawn].transform.position.z),
							spawnParticlePrefab.transform.rotation
						);*/
			

		}
	}
	
	public void killOne() {
		Enemy.current--;
		updateEnemiesLeft(); 
		if((Round.destinyQuant-player.killsInRound)==0) {
			this.startNewRound();
		}
		
	}	
	
	
	
	
}
