using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public Rigidbody2D target;

	[Header("Movement")]
	public float leadingDistance = 5;
	public float maxTargetSpeed = 20;
	public float smoothMoveSpeed = 5;

	[Header("Zoom")]
	public float zoomPower;
	public float smoothZoomSpeed;

	private float origSize;

	private Camera cam;
	private CameraData camData;

	void Start()
	{
		cam = GetComponent<Camera>();
		camData = GetComponent<CameraData>();

		origSize = cam.orthographicSize;
	}

	void LateUpdate()
	{
		Vector2 targetSpeed = target.velocity;

		Move(targetSpeed);
		Zoom(targetSpeed);
	}

	void Move(Vector2 targetSpeed)
	{
		Vector2 leadingOffset = targetSpeed / maxTargetSpeed * leadingDistance;
		Vector2 desiredPosition = (Vector2)target.transform.position + leadingOffset;
		Vector2 position = Vector2.Lerp(desiredPosition, transform.position, Mathf.Pow(.5f, smoothMoveSpeed * Time.deltaTime));

		transform.position = new Vector3(position.x, position.y, -10);
	}

	void Zoom(Vector2 targetSpeed)
	{
		float desiredSize = Mathf.Lerp(origSize, origSize / zoomPower, target.velocity.magnitude / maxTargetSpeed);
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, desiredSize, Mathf.Pow(.5f, smoothZoomSpeed * Time.deltaTime));
	}
}
