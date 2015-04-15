using UnityEngine;
using System.Collections;

public class djBoothMaterialCol : MonoBehaviour {

	public int color;

	// Use this for initialization
	void Start () {
	
		if(color == 0)
			renderer.materials [2].SetColor ("_Color", Color.cyan);
		if(color == 1)
			renderer.materials [2].SetColor ("_Color", Color.green);
		if(color == 2)
			renderer.materials [2].SetColor ("_Color", Color.red);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
