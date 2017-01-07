using UnityEngine; // MonoBehaviour
using UnityEngine.UI; // Slider

/// <sumary>
/// This class is to be attached to a slider object and given an object with the
/// DiamondSqaure script attached.
/// </sumary>
[RequireComponent(typeof(Slider))]
public class RoughnessSlider : MonoBehaviour {
	// The DiamondSquare script to be adjusted
    public DiamondSquare diamondSquare;
	// The slider to get the values from
    private Slider slider;
    
    /// <summary>
    /// Used for initialization.
    /// </summary>
    public void Start() {
        slider = GetComponent<Slider>();
        slider.value = diamondSquare.Roughness;
		
        return;
    }

    /// <summary>
    /// Sets the roughness in the DiamondSquare script to the value of the slider
    /// </summary>
    public void SetRoughness() {
        diamondSquare.Roughness = slider.value;
		
        return;
    }
}
