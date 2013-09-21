using UnityEngine;
using System.Collections;

public class AIPathfinding : MonoBehaviour {
	
	private Enemy enemyAttrs;
	bool isRotating = false;
	public  int originalGrid;
	private int currentGrid;
	string rotation;
	public bool hittingPlayer = false;
	private float rayDistance = 5f;
	
	// Use this for initialization
	void Start () {
		enemyAttrs = gameObject.GetComponent<Enemy>();
		this.currentGrid = originalGrid;
	}
	
	
	public void moveForwardRect(){
		gameObject.transform.localPosition += transform.forward * (enemyAttrs.moveSpeed * Time.deltaTime);	
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
	}
	
	
	private bool clearFront(){
		
		Ray rayo = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
		Debug.DrawRay(rayo.origin, rayo.direction * rayDistance);
	
		
		if (isRotating) {
			if (Mathf.Round(transform.position.x) == Mathf.Round(GridSystem.grids[currentGrid].xPos)) {
				isRotating = false;
				animation.Play("EnemyReturnFrom" + rotation);
			}
			
			
		}
		
		
		RaycastHit hit;
        if (Physics.Raycast(rayo, out hit, rayDistance)) {
            if (hit.collider.tag == "Enemy") {
				if (!enemyAttrs.insideAttZone) {
					
					
					if (!isRotating) {
						
						int leastCrowdedAdjacent;
						
						if (currentGrid >= 1 && currentGrid < GridSystem.grids.Length-1) {
						
							if (GridSystem.grids[currentGrid+1].currentEnemies > GridSystem.grids[currentGrid-1].currentEnemies) {
								leastCrowdedAdjacent = currentGrid-1;
								rotation = "Left";
								
							} 
							else {
								leastCrowdedAdjacent = currentGrid+1;
								rotation = "Right";
							}
						} 
						else if(currentGrid == 0) {
							leastCrowdedAdjacent = currentGrid + 1;	
							rotation = "Right";
						}
						else {
							leastCrowdedAdjacent = currentGrid - 1;	
							rotation = "Left";
						}
						
						if (GridSystem.grids[currentGrid].currentEnemies > GridSystem.grids[leastCrowdedAdjacent].currentEnemies) {
							isRotating = true;
							enemyAttrs.animation.Play("EnemyTurn" + rotation);
							GridSystem.grids[currentGrid].currentEnemies -- ;
							GridSystem.grids[leastCrowdedAdjacent].currentEnemies ++;	
							currentGrid = leastCrowdedAdjacent;
						}
						else {
							isRotating = false;
						}
						
					}
						
						
						
					
				}
				return false;
			}
			else {
				rayDistance = enemyAttrs.attackRange;
			
				if(hit.collider.tag == "Player") {
					hittingPlayer = true;
					return false;
				}
				
				else {
					hittingPlayer = false;
					return true;	
				}				
			}
			

    	}
		else {
			hittingPlayer = false;
			return true;	
		}
		
	}	
	
	
	
	void Update () {
		
		
		if (clearFront()) {
			if(!enemyAttrs.insideAttZone) {
				moveForwardRect();
			}
		} 
		
		


	}
}
