using UnityEngine;
using System.Collections;


public class WaveGenerator : MonoBehaviour {
	
	// Game constants
	public const int waveCubeCount = 200;
	public const float waterWidth = 50;
	public const float waterHeight = 10;
	public const float halfWaterHeight = waterHeight/2F;
	
	public const int maxWaveCount = 50;
	
	
	// Class variables
	private GameObject wavecube_prefab;
	
	private GameObject[] wavecubes = new GameObject[waveCubeCount];
	public Wave[] waves = new Wave[maxWaveCount];
	public int waveCount = 0;
	
	
	
	// Use this for initialization
	void Start () {
		wavecube_prefab = (GameObject)Resources.Load("WaveCube");
		wavecube_prefab.transform.localScale = new Vector3(waterWidth/(float)waveCubeCount, waterHeight, 1);
		Vector3 cubeScale = wavecube_prefab.transform.localScale;
		
		for(int i = 0; i < waveCubeCount; i++) {
			wavecubes[i] = (GameObject)Instantiate(wavecube_prefab, new Vector3(i*cubeScale.x-(waterWidth/2), 0, 0), Quaternion.identity);
		}

		waves[0] = new Wave(50, -10, 10, 1);
		//waves[1] = new Wave(-68, 10, 10, -1);
		waveCount += 1;
		//waveCount += 2;
	}
	
	// Update is called once per frame
	void Update () {
		for(int j = 0; j < waveCount; j++) {
			waves[j].Update();
		}
		
		for(int i = 0; i < waveCubeCount; i++) {
			//y = WaveGenerator.halfWaterHeight;
			for(int j = 0; j < waveCount; j++) {
				//Debug.Log(i.ToString() + " " + wavecubes[i].transform.position.x.ToString());
				//Debug.Log(waves[j].limitLow.ToString() + " " + waves[j].limitHigh.ToString());
				//Debug.Log();
				if(wavecubes[i].transform.position.x > waves[j].limitLow &&
				   wavecubes[i].transform.position.x < waves[j].limitHigh) {
					var v = wavecubes[i].transform.position;
					v.y = waves[j].waveHeight(v.x);
					//Debug.Log(i.ToString() + " " + v.y.ToString());
					wavecubes[i].transform.position = v;
					//Debug.Log(j.ToString() + " " + waves[j].waveHeight(v.x).ToString());
				}
			}
			//wavecubes[i].transform.position = new Vector3(;
		}
	}
	
	public class Wave {
		public float position = 0;
		public float speed = 0;
		public float width = 0;
		public float height = 0;
		public float limitLow = 0;
		public float limitHigh = 0;
		
		public Wave(int position, int speed, int width, int height) {
			this.position = position;
			this.speed = speed;
			this.width = width;
			this.height = height;
				
			this.limitLow = this.position - this.width;
			this.limitHigh = this.position + this.width;
		}
				
		public void Update() {
			var dx = speed*Time.deltaTime;
			this.position += dx;
			this.limitLow += dx;
			this.limitHigh += dx;
		}
		
		public float waveHeight(float x) {
			if(x > this.limitLow &&
			   x < this.limitHigh) {
				return this.height*(Mathf.Cos(Mathf.PI*(this.position - x)/(this.width))+1);
			} else {
				return 0F;
			}
		}
		
		/*
		public Vector3 waveNormal(float x) {
			if(x > this.limitLow &&
			   x < this.limitHigh) {
				return new Vector3(-this.height*Mathf.PI/this.width*Mathf.Sin(Mathf.PI*(this.position - x)/(this.width)), 1, 0);
			} else {
				return new Vector3(0, 1, 0);
			}
		}
		*/
		public float waveHeightDerivative(float x) {
			if(x > this.limitLow &&
			   x < this.limitHigh) {
				return this.height*Mathf.PI/this.width*Mathf.Sin(Mathf.PI*(this.position - x)/(this.width));
			} else {
				return 0F;
			}
		}
		
		/*public Vector3 waveNormal(float x) {
			if(x > this.limitLow &&
			   x < this.limitHigh) {
				return Mathf.Atan2(-this.height*Mathf.PI/this.width*Mathf.Sin(Mathf.PI*(this.position - x)/(this.width)), 1);
			} else {
				return 0F;
			}
		}*/
	}
}
