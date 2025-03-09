using UnityEngine;
using Cinemachine;

public class following : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public Transform player;
    public float moveSpeed = 20f;
    public float zoomSpeed = 5f;
    public float minZoom = 5f;
    public float maxZoom = 10f;


    private bool isFollowing = true;
    private Vector3 freeCameraPosition;
    private float currentZoom;

    void Start()
    {
        freeCameraPosition = vcam.transform.position;
        currentZoom = vcam.m_Lens.OrthographicSize; // Assuming an orthographic camera
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isFollowing = !isFollowing;
            vcam.Follow = isFollowing ? player : null;

            if (!isFollowing)
            {
                freeCameraPosition = vcam.transform.position;
            }
        }

        if (!isFollowing)
        {
            HandleFreeCameraMovement();
            HandleZoom();
        }
    }

    void HandleFreeCameraMovement()
    {
        if (Input.GetMouseButton(1)) // Right Mouse Button is held
        {
            float moveX = Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime;
            float moveZ = Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime;

            // Move the camera based on mouse movement
            freeCameraPosition += new Vector3(moveX, 0, moveZ);
            vcam.transform.position = freeCameraPosition;
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        vcam.m_Lens.OrthographicSize = currentZoom;
    }
}
