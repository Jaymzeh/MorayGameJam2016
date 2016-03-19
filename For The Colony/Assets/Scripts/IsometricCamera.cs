using UnityEngine;
using System.Collections;

/*
This Camera class allows the player to move an isometric camera
in the scene. This class is not limited to a strictly isometric view.
The class moves along the world's X & Z axis so any camera configuration should work.
*/
public class IsometricCamera : MonoBehaviour {

    public float maxOrthographicZoom, minOrthographicZoom;
    public float maxFOV, minFOV;
    public float zoomSpeed = 0;
    public float moveSpeed = 5;
    public float rotateSpeed = 0.75f;
    
    void Start() {
        Camera  = GetComponentInChildren<Camera>();
    }

    public static Camera Camera { get; private set; }
    
    /*
    If player input is enabled, move the camera with the WASD keys and control the zoom with
    the scrollwheel.
    */
    void LateUpdate() {

        if (GameControl.instance.InputEnabled) {
            //if (Input.GetMouseButton(1)) {
            //    float dx = -Input.GetAxis("Mouse X");
            //    transform.Rotate(Vector3.up, dx);
            //}

            if (Input.GetMouseButton(2)) {
                float dx = -Input.GetAxis("Mouse X");
                float dy = -Input.GetAxis("Mouse Y");
                transform.Translate(transform.forward * (dy) * (moveSpeed * 2), Space.World);
                transform.Translate(transform.right * (dx) * (moveSpeed * 2), Space.World);
                Cursor.lockState = CursorLockMode.Confined;

                return;
            }
            else
                Cursor.lockState = CursorLockMode.None;

            transform.Translate(transform.forward * (Input.GetAxis("Vertical") * moveSpeed), Space.World);
            transform.Translate(transform.right * (Input.GetAxis("Horizontal") * moveSpeed), Space.World);

            Camera.main.orthographicSize += -(Input.GetAxis("Mouse ScrollWheel") * 10);
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minOrthographicZoom, maxOrthographicZoom);

            zoomSpeed = Input.GetAxis("Mouse ScrollWheel") * 10;
            if (zoomSpeed != 0 && !Camera.orthographic) {
                transform.Translate(Camera.transform.forward * zoomSpeed, Space.World);
            }

            if (Input.GetKey("q"))
                transform.Rotate(Vector3.up, -rotateSpeed);

            if (Input.GetKey("e"))
                transform.Rotate(Vector3.up, rotateSpeed);

            if (Input.GetKey("r"))
                transform.Translate(transform.up);
            if (Input.GetKey("f"))
                transform.Translate(-transform.up);
        }
    }
}
