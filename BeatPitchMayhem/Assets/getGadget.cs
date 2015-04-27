using UnityEngine;
using System.Collections;

public class getGadget : MonoBehaviour {

	public enum TheGadgets{

		Metronome, 
		LightController, 
		SonicCompressor

	};
	
	public TheGadgets thisPickup;
	public bool destroyOnGet;
	public bool attachedToForcefield;
	public GameObject forceField1;
	public GameObject forceField2;
	public pauseScreen pause;
	public mouseLookBPM camRotX;
	public mouseLookBPM camRotY;

	// Use this for initialization
	void Start () {

		pause = GameObject.Find ("Paused").GetComponent<pauseScreen> ();
		camRotX = GameObject.Find ("First Person Controller").GetComponent<mouseLookBPM> ();
		camRotY = GameObject.Find ("Main Camera").GetComponent<mouseLookBPM> ();

	}
	
	// Update is called once per frame
	void Update () {
	


	}

	void OnTriggerStay(Collider col){

		if (col.tag == "Player") {
			//if(Input.GetMouseButton(0)){

				switch (thisPickup){

				case TheGadgets.Metronome:
					PlayerPrefs.SetInt ("Metronome", 1);
					col.gameObject.GetComponent<playerInfo>().gadgetCheck();
					pause.metDetails.enabled = true;
					pause.thePlayer.canControl = false;
					camRotX.canControl = false;
					camRotY.canControl = false;
					if(destroyOnGet)
						Destroy(this.gameObject);
					break;
				case TheGadgets.LightController:
					PlayerPrefs.SetInt ("Light Controller", 1);
					col.gameObject.GetComponent<playerInfo>().gadgetCheck();
					pause.lcDetails.enabled = true;
					pause.thePlayer.canControl = false;
					camRotX.canControl = false;
					camRotY.canControl = false;
					if(destroyOnGet)
						Destroy(this.gameObject);
					break;
				case TheGadgets.SonicCompressor:
					PlayerPrefs.SetInt ("Sonic Compressor", 1);
					col.gameObject.GetComponent<playerInfo>().gadgetCheck();
					pause.scDetails.enabled = true;
					pause.thePlayer.canControl = false;
					camRotX.canControl = false;
					camRotY.canControl = false;
					if(destroyOnGet)
						Destroy(this.gameObject);
					break;
				default: 
					break;

				}

				if(attachedToForcefield){

					forceField1.SetActive(false);
					forceField2.SetActive(false);

				}

			//}
		}

	}
}
