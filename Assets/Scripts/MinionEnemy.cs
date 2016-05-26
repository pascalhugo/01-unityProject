using System;
using UnityEngine;

public class MinionEnemy : MonoBehaviour {

	private CharController charController;
	private Fighter fighter;
	private Alive alive;

	void Start() {
		charController = GetComponent<CharController>();
		fighter = GetComponent<Fighter>();
		alive = GetComponent<Alive>();
	}

	public void Fight() {
		FightClosestTarget();
	}

	public bool IsDead() {
		return alive.IsDead;
	}

	private void FightClosestTarget() {
		TargetableByAIEnemy closestTargetEnemy = AI_ControllerEnemy.Instance.GetClosestTargetEnemy(transform.position);
		if (closestTargetEnemy != null) {
			charController.Command_Attack (closestTargetEnemy.GetComponent<Alive> ());
		}
	}

	#region SendMessage handlers
	void OnTargetDead(Alive target) {
		FightClosestTarget();
	}

	void OnGotHit(Fighter attacker) {
		Alive attackersAlive = attacker.GetComponent<Alive>();
		if (!fighter.IsFighting && !attackersAlive.IsDead && !alive.IsDead) {
			charController.Command_Attack( attackersAlive );
		}
	}
	#endregion
}

