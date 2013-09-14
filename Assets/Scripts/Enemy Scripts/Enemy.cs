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
	
	public static GameObject enemyContainer = GameObject.Find("Enemies");
	
	
	//El jugador y el controlador de las rondas
	public static GameObject player = GameObject.FindGameObjectWithTag("Player");
	public static Rigidbody ghostProjectile;
		
	
	#region Stats
	
	public static Enemy createEnemy(string type, int round, Transform currSpawn, int firstGrid) {		
		
		//if (round>0 && round<10){
		GameObject enemyGameObject;
		Enemy enemyInstance;

		switch(type){ 
			
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
			enemyInstance.GetComponent<AIPathfinding>().originalGrid = firstGrid;
			
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
			enemyInstance.GetComponent<AIPathfinding>().originalGrid = firstGrid;
	
			
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
			enemyInstance.GetComponent<AIPathfinding>().originalGrid = firstGrid;

			
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
			enemyInstance.GetComponent<AIPathfinding>().originalGrid = firstGrid;
		
			
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
			enemyInstance.GetComponent<AIPathfinding>().originalGrid = firstGrid;
		
			
			return enemyInstance;
			
			}	
		
		
	}	
	
	#endregion
		
	
	#region behaviour	
		
	public void moveForward() {
		gameObject.transform.localPosition += transform.forward * (moveSpeed * Time.deltaTime);	
	}	
	
	
	
	public void modifyRoute() {
		this.insideAttZone=true;
	}
	
	
	
	public void markAsDead() {
		this.isDead = true;	
		explodeMesh();
	}
	
	
	private void explodeMesh () {
		
		MeshFilter MF = gameObject.GetComponentInChildren<MeshFilter>();
		MeshRenderer MR = gameObject.GetComponentInChildren<MeshRenderer>();
	    Mesh M = MF.mesh;
	    Vector3[] verts = M.vertices;
	    Vector3[] normals = M.normals;
	    Vector2[] uvs = M.uv;
	    for (int submesh = 0; submesh < M.subMeshCount; submesh++)
	    {
	        int[] indices = M.GetTriangles(submesh);
	        for (int i = 0; i < indices.Length; i += 3)
	        {
	            Vector3[] newVerts = new Vector3[3];
	            Vector3[] newNormals = new Vector3[3];
	            Vector2[] newUvs = new Vector2[3];
	            for (int n = 0; n < 3; n++)
	            {
	                int index = indices[i + n];
	                newVerts[n] = verts[index];
	                newUvs[n] = uvs[index];
	                newNormals[n] = normals[index];
	            }
	            Mesh mesh = new Mesh();
	            mesh.vertices = newVerts;
	            mesh.normals = newNormals;
	            mesh.uv = newUvs;
	
	            mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };
	
	            GameObject GO = new GameObject("Triangle " + (i / 3));
				GO.transform.parent = enemyContainer.transform;
	            GO.transform.position = MF.transform.position;
	            GO.transform.rotation = MF.transform.rotation;
	            GO.AddComponent<MeshRenderer>().material = MR.materials[submesh];
	            GO.AddComponent<MeshFilter>().mesh = mesh;
	            GO.AddComponent<BoxCollider>();
	            GO.AddComponent<Rigidbody>().AddExplosionForce(100, transform.position, 30);
	
	            Destroy(GO, 2 + Random.Range(0.0f, 2.0f));
	        }
	    }
		Destroy(gameObject);
	    MR.enabled = false;

    }	


}
	
	
	
	
	
	
	
	
	#endregion	
	
	
	
	

