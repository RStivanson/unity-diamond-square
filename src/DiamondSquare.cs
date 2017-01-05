using UnityEngine; // MonoBehaviour, TerrainCollider, TerrainData

/// <summary>
/// When attached to a Terrain GameObject, it can be used to randomized the heights
/// using the Diamond-Square algorithm
/// </summary>
[RequireComponent(typeof(TerrainCollider))]
public class DiamondSquare : MonoBehaviour {
	// Data container for heights of a terrain
	private TerrainData data;
	// Size of the sides of a terrain
	private int size;
	// Flag to set random corner heights when terrain is reset
	private bool randomizeCornerValues = false;
	// 2D array of heights
	private float[,] heights;
	// Control variable to determine smoothness of heights
	private float roughness = 0.8f;

	/// <summary>
	/// Getters / Setters for the roughness value.
	/// </summary>
	public float Roughness {
		get { return roughness; }
		set { roughness = Mathf.Clamp(value, 0.001f, 1.999f); }
	}

	/// <summary>
	/// Used for initialization
	/// </summary>
	private void Awake() {
        	data = transform.GetComponent<TerrainCollider>().terrainData;
        	size = data.heightmapWidth;
		
		SetSeed((int)Random.value);
        	Reset();

		return;
	}
	
	/// <summary>
	/// Sets the seed of the random number generator
	/// </summary>
	/// <param name="seed">A value that influences the random number generator</param>
	public void SetSeed(int seed) {
		Random.InitState(seed);

		return;
	}

	/// <summary>
	/// Flips the value of the randomizeCornerValues flag
	/// </summary>
	public void ToggleRandomizeCornerValues() {
		randomizeCornerValues = !randomizeCornerValues;

		return;
	}

	/// <summary>
	/// Resets the values of the terrain. If randomizeCornerValues is true then the
	/// corner heights will be randomized, else it will be flat.
	/// </summary>
	public void Reset() {
		heights = new float[size, size];

		// If the corners need to be randomized
		if (randomizeCornerValues) {
			heights[0, 0] = Random.value;
			heights[size - 1, 0] = Random.value;
			heights[0, size - 1] = Random.value;
			heights[size - 1, size - 1] = Random.value;
		}

		// Update the terrain data
		data.SetHeights(0, 0, heights);

		return;
	}

	/// <summary>
	/// Executes the DiamondSquare algorithm on the terrain.
	/// </summary>
	public void ExecuteDiamondSquare() {
		heights = new float[size, size];
		float average, range = 0.5f;
		int sideLength, halfSide, x, y;

		// While the side length is greater than 1
		for (sideLength = size - 1; sideLength > 1; sideLength /= 2) {
			halfSide = sideLength / 2;

			// Run Diamond Step
			for (x = 0; x < size - 1; x += sideLength) {
				for (y = 0; y < size - 1; y += sideLength) {
					// Get the average of the corners
					average = heights[x, y];
					average += heights[x + sideLength, y];
					average += heights[x, y + sideLength];
					average += heights[x + sideLength, y + sideLength];
					average /= 4.0f;

					// Offset by a random value
					average += (Random.value * (range * 2.0f)) - range;
					heights[x + halfSide, y + halfSide] = average;
				}
			}

			// Run Square Step
			for (x = 0; x < size - 1; x += halfSide) {
				for (y = (x + halfSide) % sideLength; y < size - 1; y += sideLength) {
					// Get the average of the corners
					average = heights[(x - halfSide + size - 1) % (size - 1), y];
					average += heights[(x + halfSide) % (size - 1), y];
					average += heights[x, (y + halfSide) % (size - 1)];
					average += heights[x, (y - halfSide + size - 1) % (size - 1)];
					average /= 4.0f;

					// Offset by a random value
					average += (Random.value * (range * 2.0f)) - range;

					// Set the height value to be the calculated average
					heights[x, y] = average;

					// Set the height on the opposite edge if this is
					// an edge piece
					if (x == 0) {
						heights[size - 1, y] = average;
					}

					if (y == 0) {
						heights[x, size - 1] = average;
					}
				}
			}

			// Lower the random value range
			range -= range * 0.5f * roughness;
		}

		// Update the terrain heights
		data.SetHeights(0, 0, heights);

		return;
	}

	/// <summary>
	/// Returns the amount of vertices to skip using the given depth.
	/// </summary>
	/// <param name="depth">The vertice detail depth on the height array</param>
	/// <returns>Amount of vertices to skip</returns>
	public int GetStepSize(int depth) {
		// Return an invalid step size if the depth is invalid
		if (!ValidateDepth(depth)) {
			return -1;
		}

		// Return the amount of vertices to skip
		return (int)((size - 1) / Mathf.Pow(2, (depth - 1)));
	}

	/// <summary>
	/// Returns the maximum depth for this terrain's size.
	/// </summary>
	/// <returns>Maximum depth for this terrain</returns>
	public int MaxDepth() {
		// 0.69314718056f = Natural Log of 2
		return (int)((Mathf.Log(size - 1) / 0.69314718056f) + 1);
	}

	/// <summary>
	/// Returns false if the depth is above zero and below maximum depth, true otheriwse
	/// </summary>
	/// <param name="depth">The vertice detail depth on the height array</param>
	/// <returns></returns>
	private bool ValidateDepth(int depth) {
		if (depth > 0 && depth <= MaxDepth()) {
			return true;
		}

		return false;
	}
}
