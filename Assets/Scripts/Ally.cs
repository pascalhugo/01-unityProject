﻿// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections;

public class Ally : MonoBehaviour {

	private CharController charController;
	private Alive alive;
	private VisualController visualController;

	void Start() {
		charController = GetComponent<CharController>();
		alive = GetComponent<Alive>();
		visualController = GetComponent<VisualController> ();
		visualController.FaceRight ();
	}

	public bool IsDead() {
		return alive.IsDead;
	}

	#region SendMessage handlers
	void OnGotHit(Fighter attacker) {
		Alive attackersAlive = attacker.GetComponent<Alive>();
		if (!charController.CommandIssued && !attackersAlive.IsDead) {
			charController.Command_AttackMelee( attackersAlive );
		}
	}
	#endregion
}

