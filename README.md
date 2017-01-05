# Unity-Diamond-Square
A lightweight Diamond-Square component for Unity. Last updated for Unity 5.4.2f2.

# Diamond-Square
The Diamond-square algorithm is a two-dimensional version of the Midpoint displacement algorithm. The algorithm is used to manipluate heightmaps to produce a natural-esk terrain.

More details can be found at the following links:
https://en.wikipedia.org/wiki/Diamond-square_algorithm
http://www.paulboxley.com/blog/2011/03/terrain-generation-mark-one
http://stevelosh.com/blog/2016/06/diamond-square/

# How To Use
  1. Import the script into an existing Unity project.
  2. Attach the script to a GameObject either in the inspector or during runtime.
  3. From a seperate script, during runtime, call the Reset function to flatten the terrain.
  4. Call the ExecuteDiamondSquare function to execute the Diamond-Square algorithm.

# Results
The following images show the result of this algorithm. The water depicted in the photos are NOT a result of the algorithm; this was added seperatly to show height.

![Diamond Square Result](http://www.robertstivanson.com/images/DiamondSquare1.png)
![Diamond Square Result](http://www.robertstivanson.com/images/DiamondSquare2.png)
![Diamond Square Result](http://www.robertstivanson.com/images/DiamondSquare3.png)
![Diamond Square Result](http://www.robertstivanson.com/images/DiamondSquare4.png)
![Diamond Square Result](http://www.robertstivanson.com/images/DiamondSquare5.png)
