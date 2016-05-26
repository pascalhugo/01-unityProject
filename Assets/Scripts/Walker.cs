// ------------------------------------------------------------------------------
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

public class Walker : MonoBehaviour {
				
	public float walkSpeed = 1f;
	public bool huntRandomAttackSpot = true;

	private Transform myTr;
	private float t;
	private Vector3 walkStart;
	private Vector3 walkEnd;
	private float walkSpeedModificator;

	public bool goToRallyPoint = false;
	public bool isInRallyPoint = false;
	private bool hunting = false;

	private Vector3 personalOffset;

	private Alive alive;

	// Use this for initialization
	void Start () {
		myTr = transform;
		alive = GetComponent<Alive>();
		personalOffset = new Vector3(UnityEngine.Random.Range(-.1f, .1f), UnityEngine.Random.Range(-.1f, .1f), 0f);

		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime * walkSpeedModificator;
		if (t < 1) {
			myTr.position = Vector3.Lerp(walkStart, walkEnd, t);
		} else {
			myTr.position = walkEnd;
			StopWalk();
		}
	}

	public void StopWalk() {
		enabled = false;
		SendMessage("OnWalkDone", SendMessageOptions.DontRequireReceiver);
	}

	public void WalkTo(Vector3 position) {
		if (alive.IsDead) {
			return;
		}
		walkStart = myTr.position;
		walkEnd = new Vector3(position.x, position.y, myTr.position.z);
		float angle = Vector3.Angle(Vector3.right, walkEnd - walkStart);
		float angleModificator = ( Mathf.Abs( Mathf.Cos( Mathf.Deg2Rad * angle ) ) * .4f) + .6f;//walking vertically is slowed down to 60% of horizonal walk speed. It looks like character mantains the same speed no matter which direction it goes on our background.
		walkSpeedModificator = walkSpeed / Vector3.Distance(walkStart, walkEnd) * angleModificator;
		t = 0f;
		enabled = true;

		SendMessage("OnWalkStarted", walkEnd.x < walkStart.x, SendMessageOptions.DontRequireReceiver);
	}

	public void Hunt(Alive target) {
		hunting = true;
		StartCoroutine ("HuntCoroutine", target);
	}

	public void StopHunt() {
		hunting = false;
		StopCoroutine( "HuntCoroutine" );
	}

	private IEnumerator HuntCoroutine(Alive target) {
		Transform attackableSpot;
		if (huntRandomAttackSpot) {
			attackableSpot = target.GetRandomAttackableSpot();
		} else {
			attackableSpot = target.GetClosestAttackableSpot(myTr.position);
		}
		//while ( hunting && !IsCloseEnough(attackableSpot.position + personalOffset) ) {
		while ( hunting && (target != null) && !IsCloseToFight(target.transform.position) ) {
			WalkTo( attackableSpot.position + personalOffset );
			yield return new WaitForSeconds(.5f);
		}

		StopWalk();

		if (hunting) {
			SendMessage("OnHuntDone", target, SendMessageOptions.DontRequireReceiver);
			hunting = false;
		}
	}
	
	private bool IsCloseEnough(Vector3 target) {
		bool isCloseX = Mathf.Abs(myTr.position.x - target.x) < 2.15f;
		bool isCloseY = Mathf.Abs(myTr.position.y - target.y) < .2f;
		return isCloseX && isCloseY;
	}

	private bool IsCloseTooMuch(Vector3 target) {
		bool isCloseMuchX = Mathf.Abs(myTr.position.x - target.x) < 1.3f;
		return isCloseMuchX;
	}

	private bool IsCloseToFight(Vector3 target) {
		return IsCloseEnough(target) && !IsCloseTooMuch(target);
	}

	public bool GoToRallyPoint {
		get { return goToRallyPoint; }
		set { goToRallyPoint = value; }
	}

	public bool IsInRallyPoint {
		get { return isInRallyPoint; }
		set { isInRallyPoint = value; }
	}
}

