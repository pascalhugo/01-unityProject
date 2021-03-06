﻿using UnityEngine;
using System.Collections;

public class EngageTriggerEnemy : MonoBehaviour {

	private Fighter fighter;
	private MinionEnemy minionEnemy; 

	void Start() {
		fighter = GetComponentInParent<Fighter> ();
		minionEnemy = GetComponentInParent<MinionEnemy> ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.isTrigger != true && (col.CompareTag ("Ally") || col.CompareTag ("Player"))) {
			this.SendMessageUpwards("FightClosestTarget", minionEnemy);
		}
	}
}
