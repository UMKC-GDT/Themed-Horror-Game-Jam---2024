using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX = 2f, sensY = 2f, snappiness = 10f, upDownRange = 90f;
    public Transform orientation;

    private float xRotation = 0f;
    private float yRotation = 0f;

    public bool lockMouseMovement;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (lockMouseMovement) return;

        // Get the raw mouse input for rotation
        float rotX = Input.GetAxis("Mouse X") * sensX;
        float rotY = Input.GetAxis("Mouse Y") * sensY;

        // Accumulate the rotation values (smooth rotation)
        yRotation += rotX;
        xRotation -= rotY;

        // Clamp the vertical rotation to avoid flipping
        xRotation = Mathf.Clamp(xRotation, -upDownRange, upDownRange);

        // Smoothly interpolate between the current and desired rotation using LerpAngle
        float smoothX = Mathf.LerpAngle(transform.localEulerAngles.y, yRotation, snappiness * Time.deltaTime);
        float smoothY = Mathf.LerpAngle(transform.localEulerAngles.x, xRotation, snappiness * Time.deltaTime);

        // Apply the smoothed rotation
        transform.localRotation = Quaternion.Euler(smoothY, smoothX, 0);
        orientation.localEulerAngles = new Vector3(0, smoothX, 0);
    }
}
