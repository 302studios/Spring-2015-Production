using UnityEngine;
using System.Collections;

public class Singleton : MonoBehaviour {

	static Singleton s_Instance;

	public static Singleton get {
		get {
			return s_Instance;
		}
	}

	public virtual void Awake (){

		if (s_Instance != null)
			Debug.LogError ("There's already an instance of MyManager. Is it more than once in the scene?", this);
		s_Instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
