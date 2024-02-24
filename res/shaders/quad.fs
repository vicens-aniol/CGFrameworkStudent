varying vec2 v_uv;

uniform vec2 u_resolution; // La resolución de la ventana
uniform int u_currentTask; // La tarea actual
uniform int u_subtask;
uniform float u_time;
uniform sampler2D u_texture; 

void main()
{
	float aspect_ratio = u_resolution.x / u_resolution.y;
	vec2 aspect_corrected_uv = vec2(v_uv.x * aspect_ratio, v_uv.y);
	vec2 aspect_corrected_center = vec2(0.5 * aspect_ratio, 0.5);

	aspect_corrected_uv = (aspect_corrected_uv - aspect_corrected_center) / max(aspect_ratio, 1.0 / aspect_ratio) + aspect_corrected_center;

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

		float distance = distance(aspect_corrected_uv, aspect_corrected_center);
		vec3 color = mix(vec3(0.0), vec3(1.0), distance);
		gl_FragColor = vec4(color, 1.0);
	}

	// SUBTASCA C DEL EJERCICIO 1
	else if (u_subtask == 3)
	{
		// Definir el ancho de las rayas
		float tamano_linea = 0.1;
		float ancho_borde = tamano_linea / 2.0; // El ancho de los bordes para el suavizado

		// Calculamos el modulo de las coordenadas UV para crear el patrón de rayas
		float mod_X = mod(aspect_corrected_uv.x, tamano_linea * 2.0);
		float mod_Y = mod(aspect_corrected_uv.y, tamano_linea * 2.0);

		// Creamos el patrón de rayas verticales para el color rojo con un gradiente suave
		float red_stripe = smoothstep(ancho_borde, tamano_linea, mod_X) * (1.0 - smoothstep(tamano_linea, tamano_linea + ancho_borde, mod_X));

		// Creamos el patrón de rayas horizontales para el color azul con un gradiente suave
		float blue_stripe = smoothstep(ancho_borde, tamano_linea, mod_Y) * (1.0 - smoothstep(tamano_linea, tamano_linea + ancho_borde, mod_Y));

		// Calculamos la intensidad del color rojo y azul basado en las rayas
		vec3 red = vec3(1.0, 0.0, 0.0) * red_stripe;
		vec3 blue = vec3(0.0, 0.0, 1.0) * blue_stripe;

		// Mezclar los dos colores
		vec3 color = red + blue - red * blue;
		// Asegurarse de que el color no exceda el valor máximo de 1.0 en ninguna componente
		color = clamp(color, 0.0, 1.0);

		// Salida del color final
		gl_FragColor = vec4(color, 1.0);
	}

	// SUBTASCA D DEL EJERCICIO 1
	//TODO: Poner los cuadrados para dar el efecto de mosaico.
	else if (u_subtask == 4)
	{
		// Calcular la posición de la cuadrícula basada en las coordenadas UV
		vec2 grid_pos = floor(aspect_corrected_uv * 16.0);
		vec2 cell_center = (grid_pos + 0.5) / 16.0; // El centro de la celda en términos de coordenadas UV

		// Calcular el color del gradiente en el centro de la celda
		// Desde la esquina inferior izquierda (negro) a la inferior derecha (rojo)
		vec3 color1 = vec3(0.0, 0.0, 0.0); // negro
		vec3 color2 = vec3(1.0, 0.0, 0.0); // rojo
		vec3 bottom_color = mix(color1, color2, cell_center.x);

		// Desde la esquina superior izquierda (verde) a la superior derecha (amarillo)
		vec3 color3 = vec3(0.0, 1.0, 0.0); // verde
		vec3 color4 = vec3(1.0, 1.0, 0.0); // amarillo
		vec3 top_color = mix(color3, color4, cell_center.x);

		// Interpolar linealmente entre bottom_color y top_color en el centro de la celda
		vec3 color = mix(bottom_color, top_color, cell_center.y);

		// Salida del color final
		gl_FragColor = vec4(color, 1.0);
	}

	// SUBTASCA E DEL EJERCICIO 1
	// FIXME: Arreglar y quitar el if
	else if (u_subtask == 5)
	{
		vec2 grid_pos = floor(aspect_corrected_uv * 16);

		// Calculo del color
		float checker = step(1.0, mod(grid_pos.x + grid_pos.y, 2.0));
		
		vec3 white = vec3(1.0, 1.0, 1.0);
		vec3 black = vec3(0.0, 0.0, 0.0);
		vec3 color = mix(white, black, checker);

		gl_FragColor = vec4(color, 1.0);
	} else if (u_subtask == 6){
    		// Variables de configuración del seno
			float amplitude = 0.25; // La amplitud del seno
			float frequency = 1; // La frecuencia del seno

			// Calcular la posición y del seno para la coordenada x actual
			const float PI = 3.1415926535897932384626433832795;

			float sine_y = amplitude * sin(v_uv.x * frequency * 2.0 * PI) + 0.5;

			vec3 green = vec3(0.0, 1.0, 0.0);
			vec3 black = vec3(0.0, 0.0, 0.0);

			float t = mix(amplitude, 1, v_uv.y); // Establecemos el rango de interpolación
			vec3 color_above = mix(green, black, t);

			float t2 = mix(0, 1- amplitude, v_uv.y); // Establecemos el rango de interpolación
			vec3 color_below = mix(black, green, t2);

			float above_sine = step(sine_y, aspect_corrected_uv.y);

			vec3 final_color = mix(color_below, color_above, above_sine);
			gl_FragColor = vec4(final_color, 1.0);
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
	}
}
