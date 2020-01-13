using UnityEngine;
using System.Collections;

public class RotationAroundTarget : MonoBehaviour {
	public Transform origin;
	public float speed = 20;
	float ry, rz;

	// Use this for initialization
	void Start () {
		ry = Random.Range(1, 360);
		rz = Random.Range(1, 360);
	}

	// Update is called once per frame
	void Update () {
		Vector3 axis = new Vector3(0, ry, rz);
		//第一个是围绕的旋转点，在这里即太阳的位置，第二个是旋转的法向量，在这里法向量在（0，Y，Z）平面上，第三个是旋转的速度。
		this.transform.RotateAround(origin.position, axis, speed*Time.deltaTime);
	}
}
