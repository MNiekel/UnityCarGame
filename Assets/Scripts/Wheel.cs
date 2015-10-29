using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WheelCollider))]
public class Wheel : MonoBehaviour {

	WheelCollider wc;
	Transform tyre;

	void Awake () {
		wc = GetComponent<WheelCollider> ();
		tyre = this.transform.FindChild ("Tyre");
	}

	public void Move(float value) {
		if (value == 0)
			return;

		if (value < 0 && wc.rpm > 0) {
			Break ();
		} else {
			wc.brakeTorque = 0f;
			wc.motorTorque = value;
		}
	}

	private void Break () {
		wc.brakeTorque = 50000;
		wc.motorTorque = 0;
	}

	public void Turn(float value) {
		wc.steerAngle = value;
		//tyre.localEulerAngles = new Vector3 (0f, wc.steerAngle, 90f);

		float rotationX = 360f * wc.rpm * Time.deltaTime / 60f + tyre.localEulerAngles.x;
		tyre.localEulerAngles = new Vector3 (rotationX, wc.steerAngle, 90f);
	}
}
