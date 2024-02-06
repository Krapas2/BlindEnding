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

	private Vector3 offset;
	private float origSize;

	private Camera cam;

	void Start ()
	{
		cam = GetComponent<Camera> ();
        
		origSize = GetComponent<Camera> ().orthographicSize;
		offset = transform.position - target.transform.position;
	}
	
	void Update ()
	{
        Vector2 targetSpeed = target.velocity;

        Move(targetSpeed);
        Zoom(targetSpeed);
	}

    void Move(Vector2 targetSpeed){
        Vector2 leadingOffset = targetSpeed/maxTargetSpeed * leadingDistance;
        Vector2 desiredPosition = target.position + leadingOffset;
        Vector2 position = Vector2.Lerp(transform.position, desiredPosition, smoothMoveSpeed * Time.deltaTime);

        transform.position = new Vector3(position.x, position.y, -10);
    }

    void Zoom(Vector2 targetSpeed){
		float desiredSize = Mathf.Lerp(origSize, origSize/zoomPower, target.velocity.magnitude/maxTargetSpeed);
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, desiredSize, smoothZoomSpeed * Time.deltaTime);
    }
}
