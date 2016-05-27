using UnityEngine;
using System.Collections;

public class VisualController : MonoBehaviour {

	public Animator anim;
	public Transform swappingFaceTransform;
	public Transform transform;

	public GameObject projectile;
	private Fighter fighter;

	private int walkID = Animator.StringToHash("walk");
	private int hitID = Animator.StringToHash("hit");
	private int attackID = Animator.StringToHash("attack");
	private int deathID = Animator.StringToHash("death");
	private int castID = Animator.StringToHash("cast");
	private int attackTypeID = Animator.StringToHash("attackType");
	private int deathTypeID = Animator.StringToHash("deathType");
	private int castTypeID = Animator.StringToHash("castType");

	private bool isFacingLeft = true;

	void Start () {
		transform = GetComponent<Transform> ();
		fighter   = GetComponent<Fighter> ();
	}

	#region Facing
	public void FaceLeft() {
		Face( true );
	}
	public void FaceRight() {
		Face( false );
	}

	public void Face(bool faceLeft) {
		if (faceLeft == isFacingLeft) {
			return;
		}

		SwapFace();
		isFacingLeft = faceLeft;
	}
	private void SwapFace() {
		swappingFaceTransform.localScale = new Vector3(-swappingFaceTransform.localScale.x, swappingFaceTransform.localScale.y, swappingFaceTransform.localScale.z);
	}
	#endregion


	#region Play animations
	public void PlayWalk() {
		anim.SetBool( walkID, true );
	}
	public void StopWalk() {
		anim.SetBool( walkID, false );
	}
	
	public void PlayAttack() {
		anim.SetFloat( attackTypeID, UnityEngine.Random.Range(0, 2) );
		anim.SetTrigger( attackID );
	}

	public void PlayRangeAttack() {
		anim.SetFloat( attackTypeID, UnityEngine.Random.Range(0, 2) );
		anim.SetTrigger( attackID );

		Debug.Log (fighter.Target);
		Vector3 dir = fighter.Target.transform.position - transform.position;
		dir = fighter.Target.transform.InverseTransformDirection(dir);
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		Debug.Log (angle);

		GameObject arrow = Instantiate (projectile, new Vector3 (transform.position.x, transform.position.y + 0.9f, transform.position.z), Quaternion.AngleAxis (angle + 270, Vector3.forward)) as GameObject;
	}
	
	public void PlayCast() {
		anim.SetFloat( castTypeID , UnityEngine.Random.Range(0, 2) );
		anim.SetTrigger( castID );
	}
	
	public void PlayDeath() {
		anim.SetFloat( deathTypeID, UnityEngine.Random.Range(0, 2) );
		anim.SetTrigger( deathID );
	}
	
	public void PlayHit() {
		anim.SetTrigger( hitID );
	}
	#endregion
}
