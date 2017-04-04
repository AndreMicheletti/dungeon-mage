using UnityEngine;
using System.Collections;

public class DestroyOnExit : MonoBehaviour {

	public LayerMask boundaryLayer;
	public Transform myCollider;

	void FixedUpdate() {
		if (!Physics2D.OverlapCircle(myCollider.position, 0.1f, boundaryLayer)) {
			Destroy (gameObject);
		}
	}
}
