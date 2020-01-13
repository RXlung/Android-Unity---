using UnityEngine;
using System.Collections;

public class SelfRotation : MonoBehaviour {
	public float FloRotationSpeed = 1F;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (Vector3.up*FloRotationSpeed,Space.World);
	}
}
