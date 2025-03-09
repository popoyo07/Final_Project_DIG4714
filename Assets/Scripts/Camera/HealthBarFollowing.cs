using UnityEngine;

public class HealthBarFollowing : MonoBehaviour
{
    private Camera m_Camera;

    void Start()
    {
        m_Camera = Camera.main;
    }

    void LateUpdate()
    {
        if (m_Camera == null) return;

        // Make the health bar face the camera
        transform.LookAt(transform.position + m_Camera.transform.forward);
    }
}
