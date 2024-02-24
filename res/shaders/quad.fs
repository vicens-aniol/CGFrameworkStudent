varying vec2 v_uv;

uniform vec2 u_resolution; // La resolución de la ventana
uniform int u_currentTask; // La tarea actual
uniform int u_subtask;
uniform float u_time;
uniform sampler2D u_texture; 

void main()
{
	if (u_currentTask == 1)
	{// SUBTASCA A DEL EJERCICIO 1
	if (u_subtask == 1)
	{
		vec3 color = mix(vec3(0.0, 0.0, 1.0), vec3(1.0, 0.0, 0.0), v_uv.x);
		gl_FragColor = vec4(color, 1.0);
	}

	// SUBTASCA B DEL EJERCICIO 1
	else if (u_subtask == 2)
	{
		// Calcular la distancia desde el centro de la pantalla
		vec2 center = vec2(0.5, 0.5);
		vec2 normalizedUV = (v_uv - center) * u_resolution / u_resolution.y; // Ajustar las coordenadas UV según la relación de aspecto y centrar el círculo
		float distance = distance(normalizedUV, vec2(0.0));

		// Calcular el color del círculo
		vec3 color = mix(vec3(0.0), vec3(1.0), distance);

		// Salida del color final
		gl_FragColor = vec4(color, 1.0);
	}

	// SUBTASCA C DEL EJERCICIO 1
	else if (u_subtask == 3)
	{
		// Definir el ancho de las rayas
		float stripeWidth = 0.06;
		float edgeWidth = stripeWidth / 2.0; // El ancho de los bordes para el suavizado

		// Crear el patrón de rayas verticales para el color rojo con un gradiente suave
		float redStripe = smoothstep(edgeWidth, stripeWidth, mod(v_uv.x, stripeWidth * 2.0)) * (1.0 - smoothstep(stripeWidth, stripeWidth + edgeWidth, mod(v_uv.x, stripeWidth * 2.0)));

		// Crear el patrón de rayas horizontales para el color azul con un gradiente suave
		float blueStripe = smoothstep(edgeWidth, stripeWidth, mod(v_uv.y, stripeWidth * 2.0)) * (1.0 - smoothstep(stripeWidth, stripeWidth + edgeWidth, mod(v_uv.y, stripeWidth * 2.0)));

		// Calcular la intensidad del color rojo y azul basado en las rayas
		vec3 redColor = vec3(1.0, 0.0, 0.0) * redStripe;
		vec3 blueColor = vec3(0.0, 0.0, 1.0) * blueStripe;

		// Mezclar los dos colores
		vec3 color = redColor + blueColor - redColor * blueColor; 
		
		// Asegurarse de que el color no exceda el valor máximo de 1.0 en ninguna componente
		color = clamp(color, 0.0, 1.0);

		// Salida del color final
		gl_FragColor = vec4(color, 1.0);
	}

	// SUBTASCA D DEL EJERCICIO 1
	//TODO: Poner los cuadrados para dar el efecto de mosaico.
	else if (u_subtask == 4)
	{
		// Calcular la posición de cuadrícula basada en las coordenadas UV
		vec2 gridPos = floor(v_uv * 16.0);

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

		// Interpolar linealmente entre bottomColor y topColor
		vec3 color = mix(bottomColor, topColor, v_uv.y);

		// Salida del color final
		gl_FragColor = vec4(color, 1.0);
	}

	// SUBTASCA E DEL EJERCICIO 1
	// FIXME: Arreglar y quitar el if
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
	// else if (u_subtask == 6)
	// {
	// 	gl_FragColor = vec3(0.0,1.0,0.0);
	// }
	} else if (u_currentTask == 2) {
		vec4 texture_color = texture2D(u_texture, v_uv);
		gl_FragColor = texture_color;
		if (u_subtask == 1) {
			// SUBTASCA A DEL EJERCICIO 2
			vec3 grayscale_color = vec3(dot(texture_color.rgb, vec3(0.299, 0.587, 0.114)));
			gl_FragColor = vec4(grayscale_color, texture_color.a);

		}
		else if (u_subtask == 2) {
			// SUBTASCA B DEL EJERCICIO 2
			vec3 sepia_color = vec3(texture_color.r * 0.393 + texture_color.g * 0.769 + texture_color.b * 0.189);
			gl_FragColor = vec4(sepia_color, texture_color.a);
		}
		//FIXME: Arreglar el tono amarillo
		else if (u_subtask == 3) {
			// SUBTASCA C DEL EJERCICIO 2
			vec3 grayscale_color = vec3(dot(texture_color.rgb, vec3(0.299, 0.587, 0.114)));
			vec3 yellow_tint = vec3(1.0, 1.0, 0.0);
			float tint_factor = 0.5; 
			vec3 final_color = mix(grayscale_color, yellow_tint * grayscale_color, tint_factor);
			gl_FragColor = vec4(final_color, texture_color.a);
		}
		else if (u_subtask == 4) {
			// SUBTASCA D DEL EJERCICIO 2
			vec3 color = texture_color.rgb;
			float gray = dot(color, vec3(0.299, 0.587, 0.114));
			float threshold = 0.5;
			vec3 blackAndWhite = vec3(step(threshold, gray));
			gl_FragColor = vec4(blackAndWhite, texture_color.a);
		}
		// else if (u_subtask == 5) {
		// 	// SUBTASCA E DEL EJERCICIO 2
		// 	// Aplicar efecto difuminado en los bordes
		// 	float blur_radius = 0.1;
		// 	float blur_factor = smoothstep(0.0, blur_radius, v_uv.x) * smoothstep(1.0 - blur_radius, 1.0, v_uv.x) * smoothstep(0.0, blur_radius, v_uv.y) * smoothstep(1.0 - blur_radius, 1.0, v_uv.y);
		// 	vec4 blurred_color = mix(negative_color, vec3(1.0), blur_factor);
			
		// 	// Aclarar el centro
		// 	float center_radius = 0.3;
		// 	float center_factor = smoothstep(0.5 - center_radius, 0.5 + center_radius, v_uv.x) * smoothstep(0.5 - center_radius, 0.5 + center_radius, v_uv.y);
		// 	vec4 final_color = mix(blurred_color, vec3(1.0), center_factor);
			
		// 	gl_FragColor = vec4(final_color, texture_color.a);
		// }
		// else if (u_subtask == 6 ){
		// // SUBTASCA F DEL EJERCICIO 1
		// // Aplicar efecto de desenfoque a la textura
		// float blur_radius = 0.1;
		// float blur_factor = smoothstep(0.0, blur_radius, v_uv.x) * smoothstep(1.0 - blur_radius, 1.0, v_uv.x) * smoothstep(0.0, blur_radius, v_uv.y) * smoothstep(1.0 - blur_radius, 1.0, v_uv.y);
		// vec4 blurred_color = texture2D(u_texture, v_uv) * blur_factor;
		
		// gl_FragColor = blurred_color;
		// }
	}
}
