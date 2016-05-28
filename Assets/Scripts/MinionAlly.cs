using System;
using UnityEngine;

public class MinionAlly : MonoBehaviour {

	private CharController charController;
	private Fighter fighter;
	private Alive alive;
	private SoldierType soldierType;
	private Walker walker;

	void Start() {
		charController = GetComponent<CharController>();
		fighter = GetComponent<Fighter>();
		alive = GetComponent<Alive>();
		soldierType = GetComponent<SoldierType>();
		walker = GetComponent<Walker> ();
	}

	public void Fight() {
		FightClosestTarget();
	}

	public bool IsDead() {
		return alive.IsDead;
	}

	private void FightClosestTarget() {
		TargetableByAIAlly closestTargetAlly = AI_ControllerAlly.Instance.GetClosestTargetAlly(transform.position, soldierType.GetSoldierType);
		if (closestTargetAlly != null) {
			if (soldierType.melee) {
				charController.Command_AttackMelee (closestTargetAlly.GetComponent<Alive> ());
			} else if (soldierType.range) {
				charController.Command_AttackRange (closestTargetAlly.GetComponent<Alive> ());
			} else if (soldierType.magic) {
				charController.Command_AttackMagic (closestTargetAlly.GetComponent<Alive> ());
			} else {
				Debug.Log ("Pas de type de combattant indiqué");
			}
		} else if (walker.IsInRallyPoint) {
			new WaitForSeconds (3f);
			StartCoroutine ("Fight");
		} else {
			// Debug.Log (walker.IsInRallyPoint);
			charController.OnWalkToRallyPoint ();
		}
	}

	#region SendMessage handlers
	void OnTargetDead(Alive target) {
		FightClosestTarget();
	}

	void OnGotHit(Fighter attacker) {
		SoldierType soldierTypeAttacker = attacker.GetComponent<SoldierType> ();
		if (soldierTypeAttacker.IsMelee && soldierTypeAttacker.IsEnemy) {
			Alive attackersAlive = attacker.GetComponent<Alive> ();
			if (!fighter.IsFighting && !attackersAlive.IsDead && !alive.IsDead) {
				// charController.Command_AttackMelee (attackersAlive);
			}
		}
	}
	#endregion
}