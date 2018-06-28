using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject target;

	public Vector3 targetOffset = new Vector3(0, 0, -5);

	// Use this for initialization
	void Start () {
		Debug.Assert(target != null, "Camera Target is null");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void LateUpdate () {
		transform.LookAt(target.transform);
		
		transform.position = target.transform.position + targetOffset;
	}
}
