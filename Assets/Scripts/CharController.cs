using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {

	private VisualController visualController;
	private Walker walker;
	private Fighter fighter;
	private Alive alive;

	private Alive currentTarget = null;
	private Transform myTr;
	private BoxCollider2D bCollider;
	private SoldierType soldierType;

	public bool CommandIssued { get; private set; }
	public float rallyDistanceMin = 0.1f;
	public float rallyDistanceMax = 1f;

	void Start() {
		myTr = transform;
		bCollider 		 = GetComponent<BoxCollider2D>();
		visualController = GetComponent<VisualController>();
		walker 			 = GetComponent<Walker>();
		fighter 		 = GetComponent<Fighter>();
		alive 			 = GetComponent<Alive>();
		soldierType 	 = GetComponent<SoldierType> ();

		CommandIssued = false;
	}

	public void Command_WalkTo(Vector3 position) {
		ClearCommands();
		walker.WalkTo( position );
		CommandIssued = true;
	}

	public void Command_AttackMelee(Alive target) {
		if (target.Equals(currentTarget)) {
			return;
		}
		ClearCommands();
		currentTarget = target;
		walker.Hunt(target);
		CommandIssued = true;
	}

	public void Command_AttackRange(Alive target) {
		if (target.Equals(currentTarget)) {
			return;
		}
		ClearCommands();
		currentTarget = target;
		fighter.Fight(target);
		CommandIssued = true;
	}

	public void Command_AttackMagic(Alive target) {
		if (target.Equals(currentTarget)) {
			return;
		}
		ClearCommands();
		currentTarget = target;
		fighter.Fight(target);
		CommandIssued = true;
	}

	public bool IsDead() {
		return alive.IsDead;
	}

	private void ClearCommands() {
		currentTarget = null;
		fighter.StopFight();
		walker.StopHunt();
		walker.StopWalk();
		CommandIssued = false;
	}

	public void OnWalkToRallyPoint() {
		if (walker.IsInRallyPoint != true) {
			walker.GoToRallyPoint = true;
			StartCoroutine ("CheckRallyPoint");
		}
	}

	private IEnumerator CheckRallyPoint() {
		walker.WalkTo(new Vector3(transform.position.x - 100, transform.position.y, transform.position.z));
		while (walker.GoToRallyPoint) {
			yield return new WaitForSeconds(UnityEngine.Random.Range(rallyDistanceMin, rallyDistanceMax));
		}
		walker.WalkTo(new Vector3(transform.position.x, transform.position.y, transform.position.z));
		walker.GoToRallyPoint = false;
	}

	#region SendMessage handlers
	void OnWalkStarted(bool facingLeft) {
		if (!IsDead()) {
			visualController.Face(facingLeft);
			visualController.PlayWalk();
		}
	}

	void OnWalkDone() {
		if (!IsDead()) {
			visualController.StopWalk();
			CommandIssued = false;
		}
	}

	void OnHuntDone(Alive target) {
		if (!IsDead() && (target != null)) {
			visualController.Face( target.transform.position.x < myTr.position.x );
			fighter.Fight( target );
		}
	}

	void OnAttack(Alive target) {
		if (!IsDead() && (target != null)) {
			visualController.Face( target.transform.position.x < myTr.position.x );
			if (soldierType.IsMelee) {
				visualController.PlayAttack ();
			} else if (soldierType.IsRange) {
				visualController.PlayRangeAttack ();
			} else if (soldierType.IsMagic) {
				visualController.PlayCast ();
			} else {
				visualController.PlayAttack ();
			}
		}
	}

	void OnTargetLost(Alive target) {
		if (!IsDead () && (target != null)) {
			walker.Hunt (target);
		} else {
			SearchTarget ();
		}
	}

	void OnTargetDead(Alive target) {
		CommandIssued = false;
		SearchTarget ();
	}

	void OnGotHit(Fighter attacker) {
		if (!IsDead()) {
			visualController.PlayHit();
		}
	}

	void OnDead() {
		ClearCommands();
		bCollider.enabled = false;
		visualController.PlayDeath();
	}

	void SearchTarget() {
		if (soldierType.IsEnemy) {
			soldierType.GetComponent<MinionEnemy> ().Fight();
		} else if (soldierType.IsAlly) {
			soldierType.GetComponent<MinionAlly> ().Fight();
		}
	}
	#endregion
}
