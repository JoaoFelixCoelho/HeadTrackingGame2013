using UnityEngine;
using System.Collections;
using System.Xml;
using System.Threading;


public class Enemy : MonoBehaviour{
	
	
	public string type;
	public int damage;
	public float moveSpeed;
	public float attackRange = 4f;
	public float baseAttInterval;
	public Animation anim;
	
	
	public int id;
	
	public static float standByTime = 1f;
	private float timer = 0f;
	private bool started = false;
	public static int numerator;
	public static int current;
	public bool insideAttZone = false;
	public bool isDead = false;
	

	

	
	public static GameObject enemyContainer = GameObject.Find("Enemies");
	
	
	//El jugador y el controlador de las rondas
	public static GameObject player = GameObject.FindGameObjectWithTag("Player");
	public static Rigidbody ghostProjectile;
		
	
	#region Stats
	
	public static Enemy createEnemy(string type, int round, Transform currSpawn, int firstGrid) {	
		
		GameObject enemyGameObject;
		Enemy enemyInstance;
		
		Stats enemyStats = new Stats(type);
		
		enemyGameObject = (GameObject)Instantiate(enemyStats.prefab, currSpawn.position, enemyStats.prefab.transform.rotation);
		enemyGameObject.AddComponent<Enemy>();
		enemyInstance = (Enemy) enemyGameObject.GetComponent<Enemy>();

		enemyInstance.type = type;
		enemyInstance.damage = enemyStats.damage;	
		enemyInstance.GetComponent<HealthSystem>().calcHp(enemyStats.baseHP);
		enemyInstance.moveSpeed = enemyStats.moveSpeed;
		enemyInstance.baseAttInterval = enemyStats.interval;
		enemyInstance.id = Enemy.numerator;
		enemyInstance.name = enemyInstance.type + "-" + enemyInstance.id + "-" + Round.number;
		enemyInstance.anim = enemyGameObject.transform.FindChild("Model").GetComponent<Animation>();
		Enemy.numerator++;	
		enemyInstance.GetComponent<AIPathfinding>().originalGrid = firstGrid;
		
		/*if (type=="flubber") {
			int a = Mathf.RoundToInt(Random.Range(0,1));
			Material mat;
			switch (a) {
				case 0:
					mat = (Material) Resources.Load("Materials/Glow_Orange");
					enemyGameObject.renderer.material = mat;
				break;
				case 1:
					mat = (Material) Resources.Load("Materials/Glow_OrangeAlt1");
					enemyGameObject.renderer.material = mat;
				break;
			}
			
		}*/
		
		
		return enemyInstance;	
	}

		
			
	#endregion
		

	
	#region behaviour	
		
	public void moveForward() {
		gameObject.transform.localPosition += transform.forward * (moveSpeed * Time.deltaTime);	
		
	}	
	
	
	
	public void modifyRoute() {
		this.insideAttZone=true;
		anim.Play(type + "Idle");
		if (gameObject.GetComponent<RangedAttackScript>() != null) {
			float [] lengthArray = {anim[type + "AttackCharge"].length, anim[type + "Attack"].length};
			gameObject.GetComponent<RangedAttackScript>().animationLength = lengthArray;
		}
	}
	
	
	
	public void markAsDead() {
		this.isDead = true;	
		anim.Play(type + "Die");
		explodeMesh();
	}
	
	
	private void explodeMesh () {
		
		bool hasMF = true;
		Mesh M;
		SkinnedMeshRenderer SKMR = new SkinnedMeshRenderer();
		MeshFilter MF = new MeshFilter();
		MeshRenderer MR = new MeshRenderer();
		
		//negrada
		
		GameObject model = gameObject;
		
		for (int i=0; i< gameObject.transform.childCount; i++) {
			if (gameObject.transform.GetChild(i).tag == "Model") {
					model = (GameObject) gameObject.transform.GetChild(i).gameObject;
			}
			
		}
				
		
		if (model.GetComponent<MeshFilter>() == null) 
		{
			hasMF = false;
			SKMR = model.GetComponentInChildren<SkinnedMeshRenderer>();
	    	M = SKMR.sharedMesh;
			
		}
		
		else 
		{
			MF = model.GetComponentInChildren<MeshFilter>();
			MR = model.GetComponentInChildren<MeshRenderer>();
	   		M  = MF.mesh;
		}
		
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
				if (!hasMF) {
	           		GO.transform.position = new Vector3(SKMR.transform.position.x, SKMR.transform.position.y , SKMR.transform.position.z);
		            GO.transform.rotation = SKMR.transform.rotation;
		            GO.AddComponent<MeshRenderer>().material = SKMR.materials[submesh];										
				}
				else {
	           		GO.transform.position = MF.transform.position;
		            GO.transform.rotation = MF.transform.rotation;
		            GO.AddComponent<MeshRenderer>().material = MR.materials[submesh];					
					
				}
	            GO.AddComponent<MeshFilter>().mesh = mesh;
		       /* GO.AddComponent<BoxCollider>();
		        GO.AddComponent<Rigidbody>();	*/				
				Destroy(model);
	            Destroy(GO, Random.Range(0.5f, 1.2f));
	        }
	    }
		Destroy(gameObject);

    }	
	
	

}
	
	
	
	
	
	
	
	
	#endregion	
	
	
	
	

