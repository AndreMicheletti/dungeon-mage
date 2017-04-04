using UnityEngine;
using System.Collections;

public class LightFlickering : MonoBehaviour {

	public new Light light;
	public float flickeringTime;
	public float flickeringIntensity;

	private float timer = 0;
	private float baseIntensity;


	// Use this for initialization
	void Start () {
		if (light == null) {
			Destroy (gameObject);
			return;
		}
		baseIntensity = light.intensity;
	}

	void FixedUpdate() {
		timer += 1;
		if (timer >= flickeringTime) {
			float rand = (flickeringIntensity / 2.0f)  - Random.Range(0, flickeringIntensity);
			light.intensity = (baseIntensity + rand);
			timer = 0;
		}
	}
}
