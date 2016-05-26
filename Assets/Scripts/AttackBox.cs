using UnityEngine;
using System.Collections;

public class AttackBox : MonoBehaviour {

	public Fighter fighter;
	public Collider2D collider;

	public string AttackerTag;
	public string TargetTag;

	void Start() {
		fighter = GetComponentInParent<Fighter> ();
		collider = GetComponentInParent<Collider2D> ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (AttackerTag == "Enemy") {
			if (col.isTrigger != true && (col.CompareTag ("Ally") || col.CompareTag ("Player"))) {
				//col.SendMessageUpwards ("GetHit", fighter);
			}
		} else if (AttackerTag == "Ally") {
			if (col.isTrigger != true && col.CompareTag ("Enemy")) {
				//col.SendMessageUpwards ("GetHit", fighter);
			}
		}
	}
}
