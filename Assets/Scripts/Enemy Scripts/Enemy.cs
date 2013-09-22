using UnityEngine;
using System.Collections;
using System.Xml;
using System.Threading;


public class Enemy : MonoBehaviour{
	
	
	protected string type;
	public int damage;
	public float moveSpeed;
	public float attackRange = 4f;
	public float baseAttInterval;
	
	
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
		Enemy.numerator++;	
		enemyInstance.GetComponent<AIPathfinding>().originalGrid = firstGrid;
		
		return enemyInstance;	
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
		       /* GO.AddComponent<BoxCollider>();
		        GO.AddComponent<Rigidbody>();	*/				
	
	            Destroy(GO, Random.Range(0.5f, 2.0f));
	        }
	    }
		Destroy(gameObject);
	    MR.enabled = false;

    }	
	
	

}
	
	
	
	
	
	
	
	
	#endregion	
	
	
	
	

