using UnityEngine;
using System.Collections;

public class GridSystem : MonoBehaviour {
	
	public static Grid [] grids = new Grid[4];
	public GUIText visualDebug;

	
	
	public class Grid {
		public int number;
		public int currentEnemies;		
		
	}
	
	
	// Use this for initialization
	void Awake () {
		
		//que asco loco
		for (int i=0; i<grids.Length; i++) {
			grids [i] = new Grid();	
			grids [i].currentEnemies = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		visualDebug.text = ("grid 0: " + grids[0].currentEnemies + "\n" +
						   "grid 1: " + grids[1].currentEnemies + "\n" +
						   "grid 2: " + grids[2].currentEnemies + "\n");
		
	}
}
