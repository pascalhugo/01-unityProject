using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public float speed = 10;
	private Rigidbody2D rigidBody;
	public GameObject attacker;

	public string AttackerTag;
	public bool hasHit = false;

	// Use this for initialization
	void Start () {
		// gameObject = GetComponent<GameObject> ();
		rigidBody = GetComponent<Rigidbody2D> ();
		rigidBody.AddForce (transform.up*speed, ForceMode2D.Impulse);
		transform.Rotate (Vector3.forward * 270);

		// rigidBody.transform.rotation.SetLookRotation(transform.position + rigidBody.velocity);
	}

	/*void FixedUpdate () {
		// transform.LookAt (transform.position + rigidBody.velocity);
		var dir = rigidBody.transform.position;
		var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		rigidBody.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
	}*/

	void OnTriggerEnter2D(Collider2D col) {
		if (hasHit != true) {
			if (AttackerTag == "Enemy") {
				if (col.isTrigger != true && (col.CompareTag ("Ally") || col.CompareTag ("Player"))) {
					hasHit = true;
					col.GetComponent<Alive> ().GetHit(attacker.GetComponent<Fighter>());
					StartCoroutine ("DestroyArrow");
				}
			} else if (AttackerTag == "Ally") {
				if (col.isTrigger != true && col.CompareTag ("Enemy")) {
					hasHit = true;
					col.GetComponent<Alive> ().GetHit(attacker.GetComponent<Fighter>());
					StartCoroutine ("DestroyArrow");
				}
			}
		}
	}

	private IEnumerator DestroyArrow() {
		yield return new WaitForSeconds (0.02f);
		Destroy (this.gameObject);
	}
}
