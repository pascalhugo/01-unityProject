using UnityEngine;
using System.Collections;

public class RallyPointMagic : MonoBehaviour {

	void OnTriggerStay2D(Collider2D col) {
		if (col.isTrigger != true && col.CompareTag ("Ally") && col.GetComponent<SoldierType> ().IsMagic) {
			col.GetComponent<Walker> ().GoToRallyPoint = false;
			col.GetComponent<Walker> ().IsInRallyPoint = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.isTrigger != true && col.CompareTag ("Ally") && col.GetComponent<SoldierType> ().IsMagic) {
			col.GetComponent<Walker> ().IsInRallyPoint = false;
		}
	}
}