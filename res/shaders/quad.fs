varying vec2 v_uv;

uniform vec2 u_resolution; // La resolución de la ventana
uniform int u_subtask;


void main()
{
	// SUBTASCA A DEL EJERCICIO 1
	if (u_subtask == 1)
	{
		vec3 color = mix(vec3(0.0, 0.0, 1.0), vec3(1.0, 0.0, 0.0), v_uv.x);
		gl_FragColor = vec4(color, 1.0);
	}

	// TODO: QUE SIEMPRE ESTE CENTRADO EL CIRCULO, QUE NO SE CONVIERTA EN UNA ELIPSE AL CAMBIAR EL TAMAÑO DE LA PANTALLA, PERO QUE ESTE CENTRADO EN EL CENTRO DE LA PANTALLA
	// SUBTASCA B DEL EJERCICIO 1
	else if (u_subtask == 2)
	{
		float distance = distance(v_uv, vec2(0.5, 0.5));
		vec3 subTaskB = mix(vec3(0.0), vec3(1.0), distance);
		gl_FragColor = vec4(subTaskB, 1.0);
	}

	// SUBTASCA C DEL EJERCICIO 1
	else if (u_subtask == 3)
	{
		// Definir el ancho de las rayas
		float stripeWidth = 0.06; // El ancho de cada raya, ajustar según sea necesario
		float edgeWidth = stripeWidth / 2.0; // El ancho de los bordes para el suavizado

		// Crear el patrón de rayas verticales para el color rojo con un gradiente suave
		float redStripe = smoothstep(edgeWidth, stripeWidth, mod(v_uv.x, stripeWidth * 2.0)) * (1.0 - smoothstep(stripeWidth, stripeWidth + edgeWidth, mod(v_uv.x, stripeWidth * 2.0)));

		// Crear el patrón de rayas horizontales para el color azul con un gradiente suave
		float blueStripe = smoothstep(edgeWidth, stripeWidth, mod(v_uv.y, stripeWidth * 2.0)) * (1.0 - smoothstep(stripeWidth, stripeWidth + edgeWidth, mod(v_uv.y, stripeWidth * 2.0)));

		// Calcular la intensidad del color rojo y azul basado en las rayas
		vec3 redColor = vec3(1.0, 0.0, 0.0) * redStripe;
		vec3 blueColor = vec3(0.0, 0.0, 1.0) * blueStripe;

		// Mezclar los dos colores, el resultado será una mezcla suave donde las rayas se cruzan
		vec3 color = redColor + blueColor - redColor * blueColor; // Utilizar multiplicación en lugar de resta

		// Asegurarse de que el color no exceda el valor máximo de 1.0 en ninguna componente
		color = clamp(color, 0.0, 1.0);

		// Salida del color final
		gl_FragColor = vec4(color, 1.0);
	}

	// SUBTASCA D DEL EJERCICIO 1
	// TODO: Poner los cuadrados para dar el efecto de mosaico.
	else if (u_subtask == 4)
	{
		// Calcular la posición de cuadrícula basada en las coordenadas UV
		vec2 gridPos = floor(v_uv * 16.0); // Multiplicar por el número deseado de cuadrados

		// Calcular la interpolación basada en la posición de la cuadrícula para crear un efecto de mosaico
		float mixFactor = mod(gridPos.x + gridPos.y, 3.0);

		// Calcular la interpolación gradiente desde la esquina inferior izquierda negro a la esquina inferior derecha roja
		vec3 color1 = vec3(0.0, 0.0, 0.0); // negro
		vec3 color2 = vec3(1.0, 0.0, 0.0); // rojo
		vec3 bottomColor = mix(color1, color2, v_uv.x);

		// Calcular la interpolación gradiente desde la esquina superior izquierda verde a la esquina superior derecha amarillo
		vec3 color3 = vec3(0.0, 1.0, 0.0); // verde
		vec3 color4 = vec3(1.0, 1.0, 0.0); // amarillo
		vec3 topColor = mix(color3, color4, v_uv.x);

		// Interpolar linealmente entre bottomColor y topColor basado en el factor de mezcla
		vec3 color = mix(bottomColor, topColor, v_uv.y);

		// Salida del color final
		gl_FragColor = vec4(color, 1.0);
	}

	// SUBTASCA E DEL EJERCICIO 1
	else if (u_subtask == 5)
	{
	// Ajustar las coordenadas UV basadas en la relación de aspecto de la ventana
    vec2 uv = v_uv;
    uv.x *= u_resolution.x / u_resolution.y;

    // Calcular la posición de la cuadrícula
    vec2 gridPosition = floor(uv * 8.0); // Ajustar el número para cambiar el tamaño del tablero de ajedrez
    
    // Calcular la coloración del tablero de ajedrez
    float checker = mod(gridPosition.x + gridPosition.y, 2.0);
    vec3 color = checker < 1.0 ? vec3(0.0, 0.0, 0.0) : vec3(1.0, 1.0, 1.0);

    // Salida del color final
    gl_FragColor = vec4(color, 1.0);
	}

	//TODO: FALTA POR APROXIMAR EL GRADIENTE DE LA ONDA
	// SUBTASCA F DEL EJERCICIO 1
	else if (u_subtask == 6)
	{
	// Asumiendo que la relación de aspecto ya está ajustada en v_uv.
	vec2 uv = v_uv;

	// Definir la onda sinusoidal.
	float wave = sin(uv.x * 3.14159 * 2.0); // 2 ciclos a lo largo del eje X.

	// Definir la intensidad del color en el punto más alto de la onda.
	float waveHeight = 0.5 + 0.5 * wave; // La onda oscila entre 0 y 1.

	// Calcular el gradiente basado en la distancia vertical al centro de la onda.
	float distanceToWaveCenter = abs(uv.y - waveHeight);
	float gradient = 1.0 - distanceToWaveCenter * 4.0; // Multiplicador para ajustar la atenuación.

	// Asegurar que el gradiente no sea negativo.
	gradient = max(gradient, 0.0);

	// Intensificar el color verde cerca del centro de la onda.
	vec3 greenColor = vec3(0.0, gradient, 0.0); // El verde se hace más intenso en el centro de la onda.

	// Aplicar el color final con una base negra y el gradiente verde.
	vec3 color = max(vec3(0.0, 0.0, 0.0), greenColor);

	// Suavizar la transición hacia los bordes superiores e inferiores.
	float fade = smoothstep(0.0, 0.1, uv.y) * smoothstep(1.0, 0.9, uv.y);
	color *= fade;

	// Salida del color final.
	gl_FragColor = vec4(color, 1.0);
	}
}
