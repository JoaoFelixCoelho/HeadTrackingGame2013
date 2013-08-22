using UnityEngine;
using System.Collections;
using System.Xml;

public class Enemy : MonoBehaviour{
	
	
	protected string type;
	public int damage;
	public float moveSpeed;
	public int id;
	
	public static float standByTime = 1f;
	private float timer = 0f;
	private bool started = false;
	public static int numerator;
	public static int current;
	public bool insideAttZone = false;
	public bool isDead = false;
	
	

	//los prefabs de los enemigos
	
	public static GameObject zombiePrefab  = (GameObject) Resources.Load("Prefabs/Enemies/zombie");
	public static GameObject flubberPrefab = (GameObject) Resources.Load("Prefabs/Enemies/flubber");
	public static GameObject spiderPrefab  = (GameObject) Resources.Load("Prefabs/Enemies/spider");
	public static GameObject ghostPrefab   = (GameObject) Resources.Load("Prefabs/Enemies/ghost");
	
	//El jugador y el controlador de las rondas
	public static GameObject player = GameObject.FindGameObjectWithTag("Player");
	public static Rigidbody ghostProjectile;
		
	
	#region Stats
	
	public static Enemy createEnemy(string type, int round, Transform currSpawn) {		
		
		//if (round>0 && round<10){
		GameObject enemyGameObject;
		Enemy enemyInstance;

		switch(type){ 
			
		/*
		 * Instanseo un gameObject con el prefab del chobi que corresponde
		 * le agrego el Script "Enemy", la clase Enemy, esta misma
		 * agarro a el componente Enemy del gameobject y le seteo todo
		 * devuelvo al enemigo, para que desde donde llamaron a este metodo puedan hacer lo que quieran
		 * el gameobject ya está relacionado con el Enemy
		 * 
		 * 
		 * */
			
		case "zombie":
			
			enemyGameObject = (GameObject)Instantiate(zombiePrefab,currSpawn.position, zombiePrefab.transform.rotation);
			enemyGameObject.AddComponent("Enemy");
			enemyInstance = (Enemy) enemyGameObject.GetComponent("Enemy");

			enemyInstance.type = type;
			enemyInstance.damage = Stats.ZombieStats.damage;	
			enemyInstance.GetComponent<HealthSystem>().calcHp(Stats.ZombieStats.baseHP);
			enemyInstance.moveSpeed = Stats.ZombieStats.moveSpeed;
			enemyInstance.id = Enemy.numerator;
			enemyInstance.name = enemyInstance.type + "-" + enemyInstance.id + "-" + Round.number;
			Enemy.numerator++;	
			//cambiar
			//enemyGameObject.transform.GetChild(0).renderer.enabled = false;
			
			return enemyInstance;
				
		case "flubber":
			
			enemyGameObject = (GameObject)Instantiate(flubberPrefab,currSpawn.position, flubberPrefab.transform.rotation);	
			enemyGameObject.AddComponent("Enemy");
			enemyInstance = (Enemy) enemyGameObject.GetComponent("Enemy");

			
			enemyInstance.type = type;
			enemyInstance.damage = Stats.FlubberStats.damage;	
			enemyInstance.GetComponent<HealthSystem>().calcHp(Stats.FlubberStats.baseHP);
			enemyInstance.moveSpeed = Stats.FlubberStats.moveSpeed;
			enemyInstance.id = Enemy.numerator;
			enemyInstance.name = enemyInstance.type + "-" + enemyInstance.id + "-" + Round.number;
			Enemy.numerator++;	
			//cambiar
			//enemyGameObject.transform.GetChild(0).renderer.enabled = false;			
			
			return enemyInstance;
			
		case "spider":
			
			enemyGameObject = (GameObject)Instantiate(spiderPrefab,currSpawn.position, spiderPrefab.transform.rotation);
			enemyGameObject.AddComponent("Enemy");
			enemyInstance = (Enemy) enemyGameObject.GetComponent("Enemy");

			
			enemyInstance.type = type;
			enemyInstance.damage = Stats.SpiderStats.damage;	
			enemyInstance.GetComponent<HealthSystem>().calcHp(Stats.SpiderStats.baseHP);
			enemyInstance.moveSpeed = Stats.SpiderStats.moveSpeed;
			enemyInstance.id = Enemy.numerator;
			enemyInstance.name = enemyInstance.type + "-" + enemyInstance.id + "-" + Round.number;
			Enemy.numerator++;	
			//cambiar
			//enemyGameObject.transform.GetChild(0).renderer.enabled = false;
			
			return enemyInstance;
			
		case "ghost":
			
			enemyGameObject = (GameObject)Instantiate(ghostPrefab,currSpawn.position, ghostPrefab.transform.rotation);
			enemyGameObject.AddComponent("Enemy");
			enemyInstance = (Enemy) enemyGameObject.GetComponent("Enemy");

			
			enemyInstance.type = type;
			enemyInstance.damage = Stats.GhostStats.damage;	
			enemyInstance.GetComponent<HealthSystem>().calcHp(Stats.GhostStats.baseHP);
			enemyInstance.moveSpeed = Stats.GhostStats.moveSpeed;
			
			int randomScript = Random.Range(0,2);
			
			if (randomScript==0) {
				Destroy(enemyInstance.GetComponent<RangedAttackScript>());
				enemyInstance.gameObject.AddComponent<MeleeAttackScript>();		
			}
			
			enemyInstance.id = Enemy.numerator;
			enemyInstance.name = enemyInstance.type + "-" + enemyInstance.id + "-" + Round.number;
			Enemy.numerator++;	
			//cambiar
			//enemyGameObject.transform.GetChild(0).renderer.enabled = false;			
			
			return enemyInstance;
		
		default:
			
			enemyGameObject = (GameObject)Instantiate(zombiePrefab,currSpawn.position, currSpawn.rotation);
			enemyGameObject.AddComponent("Enemy");
			enemyInstance = (Enemy) enemyGameObject.GetComponent("Enemy");

			enemyInstance.type = type;
			enemyInstance.damage = Stats.ZombieStats.damage;	
			enemyInstance.GetComponent<HealthSystem>().calcHp(Stats.ZombieStats.baseHP);
			enemyInstance.moveSpeed = Stats.ZombieStats.moveSpeed;
			enemyInstance.id = Enemy.numerator;
			enemyInstance.name = enemyInstance.type + "-" + enemyInstance.id + "-" + Round.number;
			Enemy.numerator++;	
			print("Se creo el default Enemy, fijate si no escribiste algo mal!! ");
			//cambiar
			//enemyGameObject.transform.GetChild(0).renderer.enabled = false;			
			
			return enemyInstance;
			
			}	
		
		
	}	
	
	#endregion
		
	
	#region behaviour	
	
	private bool clearFront(){
		Vector3 startV3 = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
		Vector3 endV3 = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z + 10);
		
		Vector3 fwd = transform.dir;
		Debug.DrawRay(transform.localPosition, fwd);
        if (Physics.Raycast(transform.position, fwd, 10)) {
			print("pegando");	
		}
		
		
		Debug.DrawLine(startV3, endV3);
		print(Physics.Raycast(startV3, endV3));
        return true;
	}	
	
	public void modifyRoute() {
		this.insideAttZone=true;
	}
	
	public void moveForward(){
		gameObject.transform.position += transform.forward * Time.deltaTime * moveSpeed;	
	}
	
	
	public void markAsDead() {
		this.isDead = true;	
	}
	
	
	
	void Update () {
		if(clearFront()) {
			print("clearfront");
		}
		/*if (clearFront()) {
			if(!insideAttZone) {
				moveForward();
			}
		} else {
			print("not clear front");
			gameObject.transform.position -= transform.forward * Time.deltaTime * moveSpeed;	
		}*/

		}

	}
	
	
	
	
	
	
	
	
	#endregion	
	
	
	
	

