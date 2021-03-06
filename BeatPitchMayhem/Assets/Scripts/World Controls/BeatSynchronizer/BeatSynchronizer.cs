﻿using UnityEngine;
using System.Collections;

/// <summary>
/// This class should be attached to the audio source for which synchronization should occur, and is 
/// responsible for synching up the beginning of the audio clip with all active beat counters and pattern counters.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class BeatSynchronizer : MonoBehaviour {

	public float bpm = 120f;		// Tempo in beats per minute of the audio clip.
	public float startDelay = 1f;	// Number of seconds to delay the start of audio playback.
	public delegate void AudioStartAction(double syncTime);
	public static event AudioStartAction OnAudioStart;
	private double initTime;
	
	void Start ()
	{
		initTime = AudioSettings.dspTime;
		//audio.enabled = true;
		//audio.Stop ();
		//audio.PlayScheduled(initTime + startDelay);
		StartCoroutine(audioStartDelay());
		//if (OnAudioStart != null) {
		//	OnAudioStart(initTime + startDelay);
		//}
	}

	void audioGo(){

		audio.enabled = true;
		audio.Stop ();
		audio.PlayScheduled(initTime + (startDelay-1));
		if (OnAudioStart != null) {
			OnAudioStart(initTime + (startDelay-1));
		}
	}

	IEnumerator audioStartDelay(){

		yield return new WaitForSeconds (1);
		audioGo();
		
	}

}
