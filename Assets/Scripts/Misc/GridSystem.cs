using UnityEngine;
using System.Collections;

public class GridSystem : MonoBehaviour {
	
	public static Grid [] grids;
	public GUIText visualDebug;

	
	
	public class Grid {
		public int number;
		public int currentEnemies;		
		public float xPos;
		
	}
	
	
	public static void setGridLength(int length) {
		grids = new Grid[length];
		for (int i=0; i < grids.Length; i++) {
			grids [i] = new Grid();	
			grids [i].currentEnemies = 0;
		}		
		
	}
	
	
	// Use this for initialization
	void Awake () {
		
		//que asco loco

	}
	
	// Update is called once per frame
	void Update () {
		/*string gridString = "";
		for (int i=0; i<grids.Length; i++) {
			gridString += "grid" + i + ": " + grids[i].currentEnemies + "\n";	
		}
		visualDebug.text = gridString;*/
		
	}
}
