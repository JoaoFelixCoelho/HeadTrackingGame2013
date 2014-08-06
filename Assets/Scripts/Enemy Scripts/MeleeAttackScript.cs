using UnityEngine;
using System.Collections;

public class MeleeAttackScript : MonoBehaviour {
	Enemy enemyAttrs;
	public float attackInterval;
	private float auxTime = 0f;
	bool stay = false;
	
	
	private void followPlayer(){
		if(!stay) {
			if (!gameObject.GetComponent<AIPathfinding>().hittingPlayer){
			
				Vector3 playerPos = new Vector3(Enemy.player.transform.position.x,
												gameObject.transform.position.y
												,Enemy.player.transform.position.z);
				
				gameObject.transform.LookAt(playerPos);
				enemyAttrs.moveForward();
				
				if(transform.position.z > Enemy.player.transform.position.z) {
					gameObject.GetComponent<HealthSystem>().damageHp(9999);	
				}			
				
			}
		}
	}
	
	
	
	void Start () {
		enemyAttrs = gameObject.GetComponent<Enemy>();
		attackInterval = Random.Range(enemyAttrs.baseAttInterval - (enemyAttrs.baseAttInterval/8f), enemyAttrs.baseAttInterval + (enemyAttrs.baseAttInterval/4f));
	}
	
	private void checkAttack() {
		auxTime += Time.deltaTime;
		if (auxTime >= attackInterval) {
			if (gameObject.GetComponent<AIPathfinding>().hittingPlayer) {
				enemyAttrs.anim.Play(enemyAttrs.type + "Melee");
				Enemy.player.GetComponent<HealthSystem>().damageHp(enemyAttrs.damage);
				stay=true;
				auxTime = 0f;
			}			
		}		
	}
	
	
	void Update () {
		if(enemyAttrs.insideAttZone) {
			followPlayer();	
		}
		
		checkAttack();

	
	}
}
