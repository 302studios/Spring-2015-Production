using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class gadgetIcon : MonoBehaviour {

	public enum TheGadgets{
		
		Metronome, 
		LightController, 
		SonicCompressor
		
	};

	public TheGadgets thisGadget;
	public worldInfo theWorld;
	public playerInfo thePlayer;
	public metronome theMet;
	public lightController theLC;
	public sonicCompressor theSC;
	public Image theBlur;
	public Image background;
	public Color blackedOutColor;
	public Color activeColor;
	public Color coolingDownColor;
	public Color backgroundColor;
	public Color availableColor;


	// Use this for initialization
	void Start () {

		theWorld = GameObject.Find ("World").GetComponent<worldInfo>();
		thePlayer = GameObject.Find ("First Person Controller").GetComponent<playerInfo>();
		theMet = GameObject.Find ("Metronome Gadget").GetComponent<metronome>();
		theLC = GameObject.Find ("Light Controller").GetComponent<lightController>();
		theSC = GameObject.Find ("Sonic Compressor Gadget").GetComponent<sonicCompressor>();
		background = this.GetComponent<Image> ();
		backgroundColor = theWorld.theColor;
		background.color = activeColor;

	}
	
	// Update is called once per frame
	void Update () {
	
		if(thePlayer.hasMetronome == true && 
		   thisGadget == TheGadgets.Metronome && 
		   theMet.active){
			theBlur.color = availableColor;
			background.color = activeColor;
		}
		if(thePlayer.hasMetronome == true && thisGadget == TheGadgets.Metronome && theMet.cooling && !theMet.active){
			theBlur.color = coolingDownColor;
			background.color = backgroundColor;
		}
		if(thePlayer.hasMetronome == true && thisGadget == TheGadgets.Metronome && !theMet.cooling && !theMet.active){
			theBlur.color = availableColor;
			background.color = backgroundColor;
		}



		if(thePlayer.hasLightController == true && thisGadget == TheGadgets.LightController && theLC.active){
			theBlur.color = availableColor;
			background.color = activeColor;
		}
		if(thePlayer.hasLightController == true && thisGadget == TheGadgets.LightController && theLC.cooling && !theSC.active){
			theBlur.color = coolingDownColor;
			background.color = backgroundColor;
		}
		if(thePlayer.hasLightController == true && thisGadget == TheGadgets.LightController && !theLC.cooling && !theSC.active){
			theBlur.color = availableColor;
			background.color = backgroundColor;
		}



		if(thePlayer.hasSonicCompressor == true && thisGadget == TheGadgets.SonicCompressor && theSC.active){
			theBlur.color = availableColor;
			background.color = activeColor;
		}
		if(thePlayer.hasSonicCompressor == true && thisGadget == TheGadgets.SonicCompressor && theSC.cooling && !theSC.active){
			theBlur.color = coolingDownColor;
			background.color = backgroundColor;
		}
		if(thePlayer.hasSonicCompressor == true && thisGadget == TheGadgets.SonicCompressor && !theSC.cooling && !theSC.active){
			theBlur.color = availableColor;
			background.color = backgroundColor;
		}




	}
}
