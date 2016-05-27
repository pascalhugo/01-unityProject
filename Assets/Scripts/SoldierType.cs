using UnityEngine;
using System.Collections;

public class SoldierType : MonoBehaviour {

	public bool melee;
	public bool range;
	public bool magic;
	public bool enemy;
	public bool ally;


	public string GetSoldierType {
		get {
			string type = "";
			if (melee) {
				type = "melee";
			} else if (range) {
				type = "range";
			} else if (magic) {
				type = "range";
			}
			return type;
		}
	}

	public bool IsMelee {
		get { return melee; }
	}

	public bool IsRange {
		get { return range; }
	}

	public bool IsMagic {
		get { return magic; }
	}

	public bool IsAlly {
		get { return ally; }
	}
		
	public bool IsEnemy {
		get { return enemy; }
	}
}
