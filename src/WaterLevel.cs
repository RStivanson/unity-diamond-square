using UnityEngine; // MonoBehaviour, Transform, and Vector3
using UnityEngine.UI; // Slider

/// <summary>
/// This class is to be attached to a slider object and given an object that
/// represents the water.
/// </summary>
[RequireComponent(typeof(Slider))]
public class WaterLevel : MonoBehaviour {
	// The object to be adjusted
    public Transform water;
	// The slider to get the values from
    private Slider slider;

    /// <summary>
    /// Used for initialization.
    /// </summary>
    public void Start() {
        // Get the slider component and sync it with the water object
        slider = GetComponent<Slider>();
        slider.value = water.position.y;
		
        return;
    }

    /// <summary>
    /// Adjusts the Y position of the water object.
    /// </summary>
    public void ChangeWaterLevel() {
        // Get the waters current position and change its y value
        Vector3 newPosition = water.position;
        newPosition.y = slider.value;

        // Update the waters position
        water.position = newPosition;
		
        return;
    }
}