using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {

	public GameObject myCamera;

	private Vector3 realPosition;
	private Quaternion realRotation;

	private bool isAlive = true;
	float lerpSmoothing = 10f;

	void Start () {
		if (photonView.isMine) {
			gameObject.name = "Me";

			myCamera.SetActive (true);

			GetComponent<Motor> ().enabled = true;
			WheelCollider[] wheelColliders = GetComponentsInChildren<WheelCollider> ();
			foreach (WheelCollider wheelCollider in wheelColliders) {
				wheelCollider.enabled = true;
			}
			GetComponent<Rigidbody> ().useGravity = true;
		} else {
			gameObject.name = "NetworkPlayer";
			StartCoroutine("Alive");
		}
	}

	/*
	void Update () {
		if (!photonView.isMine) {
			transform.position = Vector3.Lerp (transform.position, realPosition,
			                                   Time.deltaTime * lerpSmoothing);
			transform.rotation = Quaternion.Lerp (transform.rotation, realRotation,
			                                      Time.deltaTime * lerpSmoothing);
		}
	}
	*/

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// We own this player: send the others our data
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else
		{
			// Network player, receive data
			realPosition = (Vector3) stream.ReceiveNext();
			realRotation = (Quaternion) stream.ReceiveNext();
		}
	}

	IEnumerator Alive () {
		while (isAlive) {
			this.transform.position = Vector3.Lerp (this.transform.position, realPosition,
			                                   Time.deltaTime * lerpSmoothing);
			this.transform.rotation = Quaternion.Lerp (this.transform.rotation, realRotation,
			                                      Time.deltaTime * lerpSmoothing);

			yield return null;
		}
	}
}
