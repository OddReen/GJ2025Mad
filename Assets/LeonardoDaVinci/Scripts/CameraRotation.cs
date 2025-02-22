using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField, Range(0, 500)]
    public float sensibility = 25;

    public Transform orientation;

    float Pitch;
    float Yaw;

    public void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        Rotation();
    }
    void Rotation()
    {
        float moveX = Input.GetAxis("Mouse X") * Time.deltaTime * sensibility;
        float moveY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensibility;

        Yaw += moveX;

        Pitch -= moveY;

        Pitch = Mathf.Clamp(Pitch, -90f, 90f);

        transform.rotation = Quaternion.Euler(0, Yaw, 0);

        orientation.localRotation = Quaternion.Euler(Pitch, 0, 0);
    }
}
