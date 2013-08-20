using UnityEngine;
using System.Collections;

public class RoundManager : MonoBehaviour {
	
	
	/*
	 * Enemy tiene los prefabs, desde aca solo se instansean los enemigos, los stats y todo, allá (en Enemy)
	 * Quiero que los pibes se copen y comenten todo bien piola ok ?
	 * 
	 * */
		
	public static float interval; 
	public bool isSpawning;
	
	public Transform [] spawnPoints;
	public GUIText roundAlert;
	public GameObject spawnParticlePrefab;
	
	
	public PlayerBehave player;
	
	float waveInterval;
	float waveDeltaTime = 0f;	
	bool roundStarted = false;	
	
	
	
	void Start () {	
		startNewRound();
		//lo tuve que poner aca porque no andaba, alta negrada :/
		Stats.read();
	}	
	
	public void startNewRound(){
		Enemy.numerator = 0;
		Enemy.current = 0;
		player.killsInRound = 0;
		waveDeltaTime = 0f;
		Round.next();	
		updateEnemiesLeft();
		this.roundAlert.enabled = true;
		roundStarted = false;
	}	
	
	
	public void updateEnemiesLeft() {
		player.enemyCounter.text = "Restantes: " + (Round.destinyQuant - player.killsInRound);
	}	

	void Update () {
		
		waveDeltaTime+=Time.deltaTime;	
		
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
		
		
	}
	
	private void newRoundWarning() {
		if(waveDeltaTime>2f) {
			this.roundAlert.enabled = false;	
			roundStarted = true;
		}
		this.roundAlert.text = "pasando desde \n la ronda " + (Round.number-1) + "\n a la " +Round.number;
	}	
	
	
	public void spawnEnemy() {
		if(waveDeltaTime>=RoundManager.interval) {
			int randomSpawn = Random.Range(0,spawnPoints.Length);
			Enemy.createEnemy(Round.type, Round.number, spawnPoints[randomSpawn]);
			Instantiate (
							spawnParticlePrefab, 
							new Vector3(spawnPoints[randomSpawn].transform.position.x, 0, spawnPoints[randomSpawn].transform.position.z),
							spawnPoints[randomSpawn].transform.rotation
						);
			
			waveDeltaTime = 0;
			Enemy.current++;
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
