using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	
	// Constants
	public const float shipHeight = 3F;
	public const float optimalDepth = 0.2F;
	
	
	// References
	private WaveGenerator waveGen;
	
	
	// Use this for initialization
	void Start () {
		//WaveGenerator waveGen = (WaveGenerator)Resources.Load("WaveGenerator");
		transform.position = new Vector3(0, 2F + WaveGenerator.waterHeight/2F, 0);
		waveGen = (WaveGenerator)Object.FindObjectOfType(typeof(WaveGenerator));
		//var waves = waveGen.waves;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow)) {
			//var zrot = transform.rotation.z;
			transform.Rotate(0, 0, 1);
		} else if(Input.GetKey(KeyCode.RightArrow)) {
			transform.Rotate(0, 0, -1);
		}
		
		
		
		
		var waveHeight = WaveGenerator.halfWaterHeight;
		var waveDerivative = 0F;
		for(int i = 0; i < waveGen.waveCount; i++) {
			waveHeight += waveGen.waves[i].waveHeight(transform.position.x);
			waveDerivative += waveGen.waves[i].waveHeightDerivative(transform.position.x);
		}
		var waveAngle = Mathf.Atan2(waveDerivative, 1)*180/Mathf.PI;
		
		
		//Debug.Log(waveAngle);
		//Debug.Log(transform.rotation.eulerAngles.z-90);
		Debug.Log(waveAngle - (transform.rotation.eulerAngles.z-90));
		//transform.rotation.eulerAngles.z;
		//transform.rotation.z
		
		
		/*** Bouyancy
		var depth = Mathf.Min(waveHeight - transform.position.y, shipHeight) + 0.6F;
		
		if(depth >= optimalDepth) {
			//this.rigidbody.AddForce(new Vector3(0, depth*20, 0));
			//this.rigidbody.AddForce(-Physics.gravity*rigidbody.mass*(1+depth-optimalDepth));
			//this.rigidbody.AddForceAtPosition
		}
		***/
		
		// Locked to waterlevel (wave height)
		this.transform.position = new Vector3(0, waveHeight + optimalDepth, 0);
		
		//Debug.Log(transform.rotation.z);
	}
}
