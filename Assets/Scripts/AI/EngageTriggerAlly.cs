using UnityEngine;
using System.Collections;

public class EngageTriggerAlly : MonoBehaviour {

	private Fighter fighter;
	private MinionAlly minionAlly; 

	void Start() {
		fighter = GetComponentInParent<Fighter> ();
		minionAlly = GetComponentInParent<MinionAlly> ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.isTrigger != true && col.CompareTag ("Enemy")) {
			// Debug.Log ("OnTriggerEnter2D");
			this.SendMessageUpwards("FightClosestTarget", minionAlly);
		}
	}
}
