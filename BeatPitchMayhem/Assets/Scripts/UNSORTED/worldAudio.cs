using UnityEngine;
using System.Collections;

public class worldAudio : MonoBehaviour {

	private BeatObserver beatObserver;
	AudioSource source;

	// Use this for initialization
	void Start () {
	
		beatObserver = GetComponent<BeatObserver>();
		source = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
