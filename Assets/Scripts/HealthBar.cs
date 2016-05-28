using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	private Scrollbar healthBar;
	private Alive alive;
	private Renderer renderer;

	void Start() {
		healthBar = GetComponent<Scrollbar> ();
		alive = GetComponentInParent<Alive> ();
	}

	void Update () {
		healthBar.size = (float) alive.HP / (float) alive.MaxHP;
	}

	public void DestroyAll() {
		Destroy (this.gameObject);
	}
}
