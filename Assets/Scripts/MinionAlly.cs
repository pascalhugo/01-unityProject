using System;
using UnityEngine;

public class MinionAlly : MonoBehaviour {

	private CharController charController;
	private Fighter fighter;
	private Alive alive;
	private SoldierType soldierType;

	void Start() {
		charController = GetComponent<CharController>();
		fighter = GetComponent<Fighter>();
		alive = GetComponent<Alive>();
		soldierType = GetComponent<SoldierType>();
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
				Debug.Log ("Pas de classe déterminé");
			}
		} else {
			charController.OnWalkToRallyPoint ();
		}
	}

	#region SendMessage handlers
	void OnTargetDead(Alive target) {
		FightClosestTarget();
	}

	void OnGotHit(Fighter attacker) {
		if (attacker.GetComponent<SoldierType> ().IsMelee && attacker.GetComponent<SoldierType> ().IsEnemy) {
			Alive attackersAlive = attacker.GetComponent<Alive> ();
			if (!fighter.IsFighting && !attackersAlive.IsDead && !alive.IsDead) {
				charController.Command_AttackMelee (attackersAlive);
			}
		}
	}
	#endregion
}