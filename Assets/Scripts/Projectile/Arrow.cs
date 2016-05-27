using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public float speed = 10;
	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		rigidBody.AddForce (transform.up*speed, ForceMode2D.Impulse);
		Debug.Log (transform);
		transform.Rotate (Vector3.forward * 270);


		// rigidBody.transform.rotation.SetLookRotation(transform.position + rigidBody.velocity);
	}

	void FixedUpdate () {
		// transform.LookAt (transform.position + rigidBody.velocity);
		/*var dir = rigidBody.transform.position;
		var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		rigidBody.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);*/
	}
}
