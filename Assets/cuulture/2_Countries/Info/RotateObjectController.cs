using UnityEngine;

public class RotateObjectController : MonoBehaviour
{
    public float PCRotationSpeed = 10f;
    public float MobileRotationSpeed = 0.4f;
    public Camera cam;

    void OnMouseDrag()
    {
        Vector3 right = Vector3.Cross(cam.transform.up, transform.position - cam.transform.position);
        Vector3 up = Vector3.Cross(transform.position - cam.transform.position, right);
    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            Ray camRay = cam.ScreenPointToRay(touch.position);
            if (touch.phase == TouchPhase.Began)
            {
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                transform.Rotate(0 * MobileRotationSpeed,
                    -touch.deltaPosition.x * MobileRotationSpeed, 0, Space.World);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
            }
        }
    }
}
