using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraData : MonoBehaviour
{
    [HideInInspector]
    public Vector2 mouseWorldPosition;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
