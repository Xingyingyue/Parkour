using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {
	public Transform playerView;
	public float cameraDistance;
	public float cameraHeight;
	private readonly float smooth = 5f;

	void LateUpdate(){
		Vector3 position = playerView.position;
		position = position - playerView.rotation*Vector3.forward*cameraDistance ;
		position.y += cameraHeight;
		transform.position = Vector3.Lerp (transform.position, position, Time.deltaTime*smooth);
		transform.LookAt (playerView);
	}
}
