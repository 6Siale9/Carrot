using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 15f;
    public Vector2 panLimit;
    private Vector3 pos;
    
    private float zoom;
    private float zoomMultiplier = 4f;
    private float minZoom = 6f;
    private float maxZoom = 15f;
    private float zoomVelocity = 0f;
    private float smoothTime = 0.25f;

    [SerializeField] private Camera cam;

    void Start()
    {
        zoom = cam.orthographicSize;
        pos.z = -20;
    }

    // Update is called once per frame
    void Update()
    {

        pos = transform.position;
        
        if (Input.GetKey("w"))
        {
            pos.y += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("s"))
        {
            pos.y -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("a"))
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("d"))
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Math.Clamp(zoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref zoomVelocity, smoothTime);

        pos.x = Math.Clamp(pos.x, -10, 40);
        pos.y = Math.Clamp(pos.y, -panLimit.y, panLimit.y);

        transform.position = pos;
    }
}
