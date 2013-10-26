using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour {
	public int totHp;
	public int currHp;	
	public bool isPlayer;
	public static RoundManager roundManager;
	public PlayerBehave player;
	private Enemy enemyAttrs;
	
	public bool isTarget;	
	public bool isDead= false;
	
	public void damageHp (int hit){	
		
		if (this.currHp-hit > 0){
			this.currHp-=hit;
		}
		
		else {
			
			isDead = true;
			this.currHp = 0;

			
			if (!isTarget) 
			{
				if(!isPlayer) {
					if (!enemyAttrs.isDead){
						enemyAttrs.markAsDead();
						player.addKill();
						roundManager.killOne();
					}
				}
				else {
					print("el jugador se murio");	
				}
			}
			else {
				//es target aca y se murio	(o es una bala)
				if (!isTarget) {
					Destroy(gameObject);
				}
				else {
					isDead = true;	
				}
				
			}
		
		
		}
	}

	
	public void calcHp (int baseHp){
		this.totHp = baseHp + Random.Range(10,40) * Round.number;
		this.currHp = this.totHp;
	}	
	
	
	
	
	
	// Use this for initialization
	void Start () {
		this.currHp = this.totHp;
		player = (PlayerBehave) Enemy.player.GetComponent<PlayerBehave>();
		roundManager = (RoundManager) GameObject.Find("WaveManagerGO").GetComponent<RoundManager>();
		if (!isPlayer) {
			enemyAttrs = gameObject.GetComponent<Enemy>();	
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
