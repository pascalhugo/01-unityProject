using UnityEngine;
using System.Collections;

public class StopBox : MonoBehaviour {

	public Rigidbody2D rigi2D;
	public CharacterController charControl;

	public string AttackerTag;
	public string TargetTag;

	void Start() {
		rigi2D = GetComponentInParent<Rigidbody2D> ();
		charControl = GetComponentInParent<CharacterController> ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (AttackerTag == "Enemy") {
			if (col.isTrigger != true && (col.CompareTag ("Ally") || col.CompareTag ("Player"))) {
				Debug.Log ("Ally");
				col.SendMessageUpwards ("ClearCommands", charControl);
			}
		} else if (AttackerTag == "Ally") {
			if (col.isTrigger != true && col.CompareTag ("Enemy")) {
				Debug.Log ("Enemy");
				col.SendMessageUpwards ("ClearCommands", charControl);
			}
		}
	}
}
