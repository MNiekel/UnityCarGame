using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Motor : MonoBehaviour {

	public float enginePower = 400f;
	public float steerPower = 10f;

	public Wheel[] wheels;
	public GameObject centerOfMass;

	Rigidbody rigidBody;

	void Awake () {
		rigidBody = GetComponent<Rigidbody> ();
	}

	void Start () {
		rigidBody.centerOfMass = centerOfMass.transform.localPosition;
	}

	void FixedUpdate () {
		float torque = Input.GetAxis ("Vertical") * enginePower;
		float turnSpeed = Input.GetAxis ("Horizontal") * steerPower;

		wheels [0].Move (torque);
		wheels [1].Move (torque);

		wheels [0].Turn (turnSpeed);
		wheels [1].Turn (turnSpeed);
	}
}
