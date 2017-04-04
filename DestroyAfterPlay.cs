using UnityEngine;
using System.Collections;

public class DestroyAfterPlay : MonoBehaviour {

	private AudioSource audio;

	void Start() {
		audio = GetComponent<AudioSource> ();
		if (audio.isPlaying == false)
			audio.Play ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (audio.isPlaying == false) {
			Destroy (gameObject);
		}
	}
}
