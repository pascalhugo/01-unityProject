using UnityEngine;
using System.Collections;

public class RallyPointMelee : MonoBehaviour {

	private Walker walker; 

	void Start () {
		
	}
	
	void OnTriggerStay2D(Collider2D col) {
		if (col.isTrigger != true && col.CompareTag ("Ally")) {
			col.GetComponent<Walker> ().GoToRallyPoint = false;
			col.GetComponent<Walker> ().IsInRallyPoint = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.isTrigger != true && col.CompareTag ("Ally")) {
			col.GetComponent<Walker> ().IsInRallyPoint = false;
		}
	}
}
