using UnityEngine;
using System.Collections;

public class randomColor : MonoBehaviour {

	public Color[] colors;
	public int selectedCol;

	// Use this for initialization
	void Start () {

		selectedCol = Random.Range (0, colors.Length);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
