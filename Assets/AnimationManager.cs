using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {
	
	private Enemy enemyAttrs;
	private RangedAttackScript rangedAttrs;
	
	// Use this for initialization
	void Start () {
		
		enemyAttrs = transform.parent.GetComponent<Enemy>();
		
		
		if (enemyAttrs.type == "flubber") {
			rangedAttrs = transform.parent.GetComponent<RangedAttackScript>();	
			rangedAttrs.animationLength = animation.GetClip(enemyAttrs.type + "AttackCharge").length;
		}
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(enemyAttrs.insideAttZone) {
			animation.Play((enemyAttrs.type) +  "Idle");
		}
		
		
		if (rangedAttrs.readyToAttack) {
			animation.Play((enemyAttrs.type) + "AttackCharge");
		}
		
		if (rangedAttrs.isAttacking) {
			animation.Play((enemyAttrs.type) +  "Attack");
			
		}
		
		
	
	}
}
