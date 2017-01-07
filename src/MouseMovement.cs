using UnityEngine; // MonoBehaviour

/// <summary>
/// Grabs the mouses movement and updates the camera as the mouse moves. This actually can be used on any object but
/// for this project it is used on a camera object.
/// </summary>
[DisallowMultipleComponent]
public class MouseMovement : MonoBehaviour {
	// Where we want our camera to focus
    Vector3 lookAt = Vector3.zero;
 
	// Current distance between lookAt and the camera
	float distance;
	// Camera movement speed in the x direction
	float xSpeed = 120.0f;
	// Camera movement speed in the y direction
	float ySpeed = 120.0f;
	// Camera zoom speed
    float scrollSpeed = 75.0f;
	
    float x = 0.0f, y = 0.0f;

    /// <summary>
    /// Used for initialization, this is called when the script is created before any Start functions.
    /// </summary>
    void Awake() {
        // Get the current camera angles in euler angles
        x = transform.eulerAngles.y;
        y = transform.eulerAngles.x;

        // Calculate the distance between lookAt and the camera
        distance = Vector3.Distance(transform.position, lookAt);
        return;
    }
    
    /// <summary>
    /// Called every frame, updates the camera position and rotation.
    /// </summary>
    void Update() {
		// Cameras current rotation
        Quaternion rotation = transform.rotation;
		// Cameras current positon
        Vector3 newPosition = transform.position;

        // Adjust the lookAt position
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            lookAt.y += 10;
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            lookAt.y -= 10;
        }

        // Adjust the distance to lookAt based on the mouse scroll wheel movement
        distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

        // If we are clicking the left mouse button
        if (Input.GetMouseButton(0)) {
            // Get the direction the mouse is moving and modify it by our speeds
            // Time.deltaTime is to keep speeds normal looking through frame lags
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

            // Convert the requested movements into a Quaternion rotation
            rotation = Quaternion.Euler(y, x, 0);
        }

        // Calculate the new position based on rotation and distance from lookAt
        newPosition = rotation * new Vector3(0.0f, 0.0f, -distance) + lookAt;

        // Update the cameras position and rotation
        transform.rotation = rotation;
        transform.position = newPosition;
		
        return;
    }
}