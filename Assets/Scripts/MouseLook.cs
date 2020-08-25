using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
	public float mouseSensitivity = 100f;
	public Transform playerBody;

	private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
		// Lock and hide the mouse
		Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		xRotation -= mouseY;
		// Clamp the vertical camera movement so the player can't look behind them
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		// Rotate the camera on the x axis
		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		// Rotate the player object on the y axis
		playerBody.Rotate(Vector3.up * mouseX);
	}
}
