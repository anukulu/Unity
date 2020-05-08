using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class CameraMMovement : MonoBehaviour
{
    public Transform target;
    public float RotateAmount = 15f;
    void LateUpdate()
    {
        OrbitCamera();
    }

    public void OrbitCamera()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 targetPos = target.position; //this is the center of the scene, you can use any point here
            float y_rotate = Input.GetAxis("Mouse X") * RotateAmount;
            float x_rotate = Input.GetAxis("Mouse Y") * RotateAmount;
            OrbitCamera(targetPos, y_rotate, x_rotate);
        }
    }

    public void OrbitCamera(Vector3 targetPos, float y_rotate, float x_rotate)
    {
        Vector3 angles = transform.eulerAngles;
        angles.z = 0;
        transform.eulerAngles = angles;
        transform.RotateAround(targetPos, Vector3.up, y_rotate);
        transform.RotateAround(targetPos, Vector3.left, x_rotate);

        transform.LookAt(target);
    }
}