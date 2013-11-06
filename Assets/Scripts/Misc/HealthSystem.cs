using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour {
	public int totHp;
	public int currHp;	
	public static RoundManager roundManager;
	public PlayerBehave player;
	private Enemy enemyAttrs;
	
	
	public enum Type {Enemy = 0, Target = 1, Bullet = 2, Player = 3};
	public Type type = 0;
	
	public bool isDead= false;
	public void damageHp (int hit){	
		
		
		if(type == Type.Player) {
			player.imageEffectActive = true;
		}
		
		
		if (this.currHp-hit > 0){
			this.currHp-=hit;
		}
		else {
			
			if (type == Type.Enemy && !isDead) {
				enemyAttrs.markAsDead();
				player.addKill();
				roundManager.killOne();				
			}
			
			if (type == Type.Player && !isDead) {
				print("El jugador murio");	
			}
			
			if (type == Type.Bullet) {
				Destroy(gameObject);
			}
						
			
			this.currHp = 0;
			isDead = true;
			
			/*
			if (!isTarget) 
			{
				if(!isPlayer) {
					if (!this.isDead)
					{
						//para que no lo mate varias veces
						isDead = true;
						if (enemyAttrs != null) 
						{
							//es un enemigo
							enemyAttrs.markAsDead();
							player.addKill();
							roundManager.killOne();
						} 
						else 
						{
							//es una bala
							Destroy(gameObject);	
						}
					}
				}
				else 
				{
					print("el jugador se murio");	
				}
			} 
			else 
			{
				this.isDead = true;	
			}*/
		
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
		if (type == Type.Enemy) {
			enemyAttrs = gameObject.GetComponent<Enemy>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	}	
}
