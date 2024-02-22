varying vec2 v_uv;

void main()
{

	// Define the start and end colors
	vec3 blue = vec3(0.0, 0.0, 1.0); // Blue color with 100% blue channel
	vec3 red = vec3(1.0, 0.0, 0.0);  // Red color with 100% red channel

	// Interpolate between blue and red based on the horizontal texture coordinate
	vec3 taskA = mix(blue, red, v_uv.x);

	// Set the final color of the pixel with full opacity
	gl_FragColor = vec4(taskA, 1.0);

	
    // Calculate the distance from the center of the texture
    // Assuming v_uv ranges from 0.0 to 1.0, with (0.5, 0.5) being the center of the texture
    float distance = distance(v_uv, vec2(0.5, 0.5));

    // Normalize the distance so it fits between 0.0 and 1.0 within the texture
    // If the distance is more than the radius, it will be clamped to 1.0
    float maxRadius = 0.5; // This can be adjusted if necessary
    distance = distance / maxRadius;
    distance = clamp(distance, 0.0, 1.0);

    // Define the start (center) and end (edge) colors
    vec3 black = vec3(0.0, 0.0, 0.0); // Black color
    vec3 white = vec3(1.0, 1.0, 1.0);   // White color

    // Interpolate between the center and edge colors based on the distance
    vec3 task2 = mix(black, white, distance);

    // Set the final color of the pixel with full opacity
    gl_FragColor = vec4(gradientColor, 1.0);
}