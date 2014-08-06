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
	
	
	private static GameObject dieParticles = (GameObject) Resources.Load("Prefabs/Particles/EnemyDieParticles");
	public static GameObject enemyContainer = GameObject.Find("Enemies");
	public static Material flubberTrisMat = (Material) Resources.Load("Materials/Glow_OrangeAlt1");
	
	//El jugador y el controlador de las rondas
	public static GameObject player = GameObject.FindGameObjectWithTag("Player");	
	
	#region Stats
	
	
	public static Enemy createEnemy(string type, int round, Transform currSpawn, int firstGrid) {	
		 
		//cuando reinicio el juego, las variables estaticas se rompen, aca checkeo que no sean null, y si lo son, les asigno un valor 
		if (dieParticles == null) {
			dieParticles = (GameObject) Resources.Load("Prefabs/Particles/EnemyDieParticles");
		}
		if (enemyContainer == null) {
			enemyContainer = GameObject.Find("Enemies");
		}
		if (flubberTrisMat == null) {
			flubberTrisMat  = (Material) Resources.Load("Materials/Glow_OrangeAlt1");
		}
		if (player == null) {
			player = GameObject.FindGameObjectWithTag("Player");	
		}


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
		
		if(enemyInstance.type=="spider") {
			dieParticles = (GameObject) Resources.Load("Prefabs/Particles/EnemyDieParticles2");
		}
		
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
		if (gameObject.GetComponent<RangedAttackScript>() != null) {
			anim.Play(type + "Idle");
			if(type=="flubber") {
				float [] lengthArray = {anim[type + "AttackCharge"].length, anim[type + "Attack"].length};
				gameObject.GetComponent<RangedAttackScript>().animationLength = lengthArray;
			}
		}
	}
	
	
	
	public void markAsDead() {
		
		if(type=="spider") {
			Destroy(Instantiate(dieParticles, transform.position, transform.rotation),1.2f);
			Destroy(gameObject);
		}
		else {
			
		
		
			if(type != "ghost" && type != "zombie") {
				Destroy(Instantiate(dieParticles, transform.position, transform.rotation),1.2f);
				explodeMesh();
			}
			else {
				dissolveMesh();	
			}
		}
	}
	
	private void dissolveMesh() {
		if(type == "ghost") {
			/*GameObject man = gameObject.transform.FindChild("Model").transform.FindChild("Man").gameObject;
			man.SendMessage("disableYourself");
			Texture newTex = (Texture) Resources.Load("Materials/Textures/DissolveAfter");
			man.renderer.material.mainTexture = newTex;*/
		}
		GameObject tmp = gameObject.transform.FindChild("Model").gameObject;
		tmp.animation.clip = tmp.animation[type + "Dissolve"].clip;
		tmp.animation.Play();
		Destroy(gameObject, tmp.animation.clip.length+0.2f);
		
		
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
					if(type == "flubber"){
		            	GO.AddComponent<MeshRenderer>().material = flubberTrisMat;	
					}
				}
				else {
	           		GO.transform.position = MF.transform.position;
		            GO.transform.rotation = MF.transform.rotation;
		            GO.AddComponent<MeshRenderer>().material = MR.materials[submesh];					
				}
	            GO.AddComponent<MeshFilter>().mesh = mesh;
		        //GO.AddComponent<BoxCollider>();
		        GO.AddComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-200,200),Random.Range(-100,-30),Random.Range(-200, 200)));	
				GO.AddComponent<CheapPhysics>();
				Destroy(model);
	            Destroy(GO, Random.Range(0.5f, 1.2f));
	        }
	    }
		Destroy(gameObject);

    }	
	
	

}
	
	
	#endregion	
	
	
	
	

