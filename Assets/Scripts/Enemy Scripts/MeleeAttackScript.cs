using UnityEngine;
using System.Collections;

public class MeleeAttackScript : MonoBehaviour {
	Enemy enemyAttrs;
	public float attackInterval;
	
	
	private void followPlayer(){
	
		Vector3 playerPos = new Vector3(Enemy.player.transform.position.x,
										gameObject.transform.position.y
										,Enemy.player.transform.position.z);
		
		gameObject.transform.LookAt(playerPos);
		enemyAttrs.moveForward();
	}
	
	
	
	void Start () {
		enemyAttrs = gameObject.GetComponent<Enemy>();
	}
	
	void Update () {
		if(enemyAttrs.insideAttZone) {
			followPlayer();	
		}
	
	}
}
