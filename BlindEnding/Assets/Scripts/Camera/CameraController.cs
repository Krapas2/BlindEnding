using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public Rigidbody2D target;

	[Header("Movement")]
	public float leadingDistance = 5;
	public float smoothMoveSpeed = 5;

	[Header("Zoom")]
	public float zoomOutPower;
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
		Vector2 pixelMousePositionFromCenter = (Vector2)Input.mousePosition - camData.pixelCenterOfScreen;
		Vector2 worldMousePositionFromCenter = camData.pixelToWorldPosition(pixelMousePositionFromCenter);

		Vector2 clampedMouseVector = Vector2.ClampMagnitude(worldMousePositionFromCenter, cam.orthographicSize);

		Move(clampedMouseVector);
		Zoom(clampedMouseVector.magnitude / camData.worldCenterOfScreen.magnitude);
	}

	void Move(Vector2 vector)
	{
		Vector2 leadingOffset = vector * leadingDistance;
		Vector2 desiredPosition = (Vector2)target.transform.position + leadingOffset;
		Vector2 position = Vector2.Lerp(desiredPosition, transform.position, Mathf.Pow(.5f, smoothMoveSpeed * Time.deltaTime));

		transform.position = new Vector3(position.x, position.y, -10);
	}

	void Zoom(float intensity)
	{
		float desiredSize = Mathf.Lerp(origSize, origSize * zoomOutPower, intensity);

		cam.orthographicSize = Mathf.Lerp(desiredSize, cam.orthographicSize, Mathf.Pow(.5f, smoothZoomSpeed * Time.deltaTime));
	}
}
