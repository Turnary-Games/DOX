using UnityEngine;
using System.Collections;

public class PratilceController : MonoBehaviour {

	[HideInInspector] public bool active;

	private SphereCollider col;
	private ParticleSystem par;

	void Start() {
		col = GetComponent<SphereCollider>();
		par = GetComponent<ParticleSystem>();

		Deactivate();
	}

	// executed from PlayerController.OnTriggerEnter
	public bool PickedUp() {
		Deactivate();

		// Returns true if it was the last pickup
		return PratilcesLeft() == 0;
	}

	#region Static methods
	// executed from PartilceController.PickedUp
	public static int PratilcesLeft() {
		int amount = 0;

		// Count the number of active pratilces
		foreach(var pratilce in FindObjectsOfType<PratilceController>()) {
			if (pratilce.active)
				amount++;
		}

		return amount;
	}

	// executed from PlayerController.UseShockwave upon using the shockwave
	// - and within AngelAI upon activation
	public static void ActivateAll() {
		foreach(var pratilce in FindObjectsOfType<PratilceController>()) {
			pratilce.Activate();
		}
	}
	#endregion

	#region Activate/Deactivate
	// executed from PratilceController.PickedUp when the player picks it up
	public void Deactivate() {
		col.enabled = false;
		var em = par.emission;
		em.enabled = false;
		active = false;
	}
	
	// executed from PratilceController.Reset
	public void Activate() {
		col.enabled = true;
		var em = par.emission;
		em.enabled = true;

		par.Play();
		active = true;
	}
	#endregion

}
