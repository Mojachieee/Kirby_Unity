using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirbyController : MonoBehaviour {

	public float moveSpeed;
	public float rotateSpeed;

	public float pullForce;

	public int pullDistance;
	public ParticleSystem ps;
	public Animator animator;

	// Use this for initialization
	void Start () {
		Debug.Assert(ps != null, "Particle system missing");
		Debug.Assert(animator != null, "Animator missing");
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		if (horizontal != 0 || vertical != 0) {
		
			transform.Rotate(new Vector3(0, 1, 0) * horizontal * rotateSpeed * Time.deltaTime);
			transform.Translate(Vector3.forward * vertical * moveSpeed * Time.deltaTime);

			animator.SetBool("Walking", true);
		} else {
			animator.SetBool("Walking", false);
		}
		


		if (Input.GetKey(KeyCode.Space)) {
			PullTowards();
		}
	}

	void OnDrawGizmosSelected() {
        Gizmos.color = new Color(50, 100, 100, 0.3f);

        Gizmos.DrawSphere(transform.position + transform.forward * pullDistance, pullDistance);

    }

	private void PullTowards() {
		ps.Emit(1);
		int layerMask = (1 << 8);
		Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward * pullDistance, pullDistance, layerMask);
		foreach(Collider collider in colliders) {
			if (collider.attachedRigidbody != null) {
				Vector3 forceDirection = transform.position - collider.attachedRigidbody.position;
				collider.attachedRigidbody.AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
				collider.attachedRigidbody.AddTorque(-0.1f, 0, 0.1f);
				// RaycastHit hit;
				// if (Physics.Linecast(transform.position, collider.transform.position, out hit, layerMask)) {
				// 	Debug.DrawLine (transform.position, collider.transform.position, Color.red);
				// 	if (hit.collider.gameObject == collider.gameObject) {
				// 		Debug.DrawLine (transform.position, hit.point, Color.green);
				// 		Vector3 forceDirection = transform.position - hit.transform.position;
				// 		hit.rigidbody.AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
				// 		hit.rigidbody.AddTorque(-0.1f, 0, 0.1f);
				// 	}
				// }
			}
		}
	}
}
