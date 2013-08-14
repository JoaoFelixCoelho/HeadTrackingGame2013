using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour {
	public int totHp;
	public int currHp;	
	public bool isPlayer;
	public static RoundManager roundManager;
	public PlayerBehave player;
	
	
	public void damageHp (int hit){
		if (this.currHp-hit > 0){
			this.currHp-=hit;
		}
		else if(!isPlayer) {
			Destroy(gameObject);
			player.GetComponent<PlayerBehave>().addKill();
			roundManager.killOne();
		}
		else {
			print("el jugador se murio guachiturro");	
		}
	}

	
	public void calcHp (int baseHp){
		this.totHp = baseHp + Random.Range(10,40) * Round.number;
		this.currHp = this.totHp;
	}	
	
	
	
	
	
	// Use this for initialization
	void Start () {
		this.currHp = this.totHp;
		roundManager = (RoundManager) GameObject.Find("SpawnManager").GetComponent("RoundManager");	
		player = (PlayerBehave) GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehave>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
