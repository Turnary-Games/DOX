using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
	public float acceleration;
	public float speed;
	public float KillY;
	public int lives;
	public int winCount;
	[Range(0.0f,1.0f)]
	public float drag;
	public Material invinincinableMaterial;
	public float invisitimer;
	public float jumpspeed;
	[Space(12)]
	public GameObject shockwavePrefab;
	public GameObject[] activeWithShockwave; // becomes active when shockwave is active, and vice versa
	
	public Image shockwaveButton;
	public Color shockwaveEnabledColor = Color.white;
	public Color shockwaveDisabledColor = new Color(0.3f,0.3f,0.3f,0.3f);

	private Vector3 origin;
	private Quaternion rotation;
	private Rect lifeCounter;
	private AudioSource source;
	private Material originalMaterial;
	private Coroutine invinicbleCoroutine;

	#region Init (Awake, Start)
	void Awake ()
	{
		source = GetComponent<AudioSource> ();
	}
	
	void Start ()
	{
		origin = transform.position;
		rotation = transform.rotation;
		originalMaterial = GetComponent<Renderer> ().material;

		MakeShockwaveUnavailable();
	}
	#endregion

	#region Updates (Update, FixedUpdate)
	void Update() {
		if (gameObject.layer == 9) {
			Color color = invinincinableMaterial.color;
			color.a = Mathf.Sin(Time.time*4)/3f+2f/3f;
			invinincinableMaterial.color = color;
		}

		if (CrossPlatformInputManager.GetButtonDown("Fire")) {
			UseShockwave();
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = CrossPlatformInputManager.GetAxis ("Horizontal");
		float moveVertical = CrossPlatformInputManager.GetAxis ("Vertical");
		
		if (GetComponent<Rigidbody> ().velocity.magnitude < acceleration) {
		
			Vector3 Movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

			GetComponent<Rigidbody> ().AddForce (Movement * speed * Time.fixedDeltaTime / Time.timeScale);

			if (transform.position.y <= KillY)
				ReturnToOrigin ();
		}

		
		Vector3 vel = GetComponent<Rigidbody> ().velocity;
		vel.x *= drag;
		vel.z *= drag;

		GetComponent<Rigidbody> ().velocity = vel;

	}
	#endregion

	#region collisions (OnTriggerEnter, OnCollisionEnter)
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "pickup")
		{
			other.gameObject.SetActive(false);
			lives++;
			source.Play ();

			foreach (LifeCounter lifeCounter in FindObjectsOfType<LifeCounter>()) {
				lifeCounter.LifeUpdate();
			}
		}
		
		if (other.gameObject.tag == "bullet") {
			Destroy(other.gameObject);
			ReturnToOrigin ();
		}

		if (other.gameObject.tag == "Pratilce") {
			var pratilce = other.gameObject.GetComponent<PratilceController>();
			if (pratilce.PickedUp())
				MakeShockwaveAvailable();
		}
	} 

	void OnCollisionEnter(Collision collider) {
		if (collider.gameObject.tag == "pulse") {
			ReturnToOrigin ();

		}
	}
	#endregion

	#region Reset + Invincibiliteh (ReturnToOrigin, Invinciciblel, ResetTrails)
	public void ReturnToOrigin ()
	{

		// CHIF NOIT INVININICCBLE
		if (gameObject.layer != 9) {
				lives--;

				foreach (LifeCounter lifeCounter in FindObjectsOfType<LifeCounter>()) {
					lifeCounter.LifeUpdate();
				}

			TurnInvincablele();

		} 
		else {
			TurnInvincablele();
		}

		if (lives > 0) {
			GetComponent<Rigidbody> ().position = origin+new Vector3(0,2);
			GetComponent<Rigidbody> ().rotation = rotation;
			GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		} else if (lives == 0) {
			lives = -666;
			FindObjectOfType<ChangeScene>().StartAnimation("mainMenu");
		}

		ResetTrails ();
		Invoke ("ResetTrails", 0.1f);
	}

	public void StopInvinainbCoroutine() {
		if (invinicbleCoroutine != null) {
			StopCoroutine (invinicbleCoroutine);
		}
	}

	public void TurnInvincablele() {
		StopInvinainbCoroutine();
		invinicbleCoroutine = StartCoroutine (Invinciciblel ());
	}

	IEnumerator Invinciciblel() {
		gameObject.layer = 9;
		GetComponent<Renderer> ().material = invinincinableMaterial;

		//ChangeTrailRenderer.ChangeAlpha(GetComponent<TrailRenderer>(), 0);


		yield return new WaitForSeconds (invisitimer);


		gameObject.layer = 11;
		GetComponent<Renderer> ().material = originalMaterial;

		//ChangeTrailRenderer.ChangeAlpha(GetComponent<TrailRenderer>(), 0);
	}

	public void ResetTrails() {
		GetComponent<TrailRenderer> ().time *= -1;
	}
	
	#endregion

	#region Jump (OnCollisionStay, Jump, JumpCooldown)

	private bool canJump = true;
	void OnCollisionStay(Collision collider) {
		if (CrossPlatformInputManager.GetButton ("Jump") && collider.gameObject.tag == "ground" && canJump) {
			Jump();
		}
	}

	void Jump(){
		GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpspeed / Time.timeScale);
		StartCoroutine(JumpCooldown());
	}

	IEnumerator JumpCooldown() {
		canJump = false;
		yield return new WaitForSeconds(0.5f);
		canJump = true;
	}

	#endregion

	#region Shockwave (UseShockwave, MakeShockwaveAvailable, MakeShockwaveUnavailable)
	private bool shockwaveAvailable;

	void UseShockwave() {
		if (shockwaveAvailable) {
			MakeShockwaveUnavailable();
			PratilceController.ActivateAll();

			// EXPLOSION
			SpawnShockwave();
			TurnInvincablele();
		}
	}

	void SpawnShockwave() {
		Instantiate(shockwavePrefab, transform.position, Quaternion.identity);
	}
	
	void MakeShockwaveAvailable() {
		shockwaveAvailable = true;

		foreach(var obj in activeWithShockwave) {
			obj.SetActive(true);
		}

		shockwaveButton.color = shockwaveEnabledColor;
		shockwaveButton.raycastTarget = true;
	}

	void MakeShockwaveUnavailable() {
		shockwaveAvailable = false;

		foreach(var obj in activeWithShockwave) {
			obj.SetActive(false);
		}

		shockwaveButton.color = shockwaveDisabledColor;
		shockwaveButton.raycastTarget = false;
	}
	#endregion

}