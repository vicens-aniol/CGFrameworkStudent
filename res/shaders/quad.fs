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
	{
			// SUBTASCA A DEL EJERCICIO 1
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
		else if (u_subtask == 5)
		{
			vec2 grid_pos = floor(aspect_corrected_uv * 16.0);

			// Calculo del color
			float checker = step(1.0, mod(grid_pos.x + grid_pos.y, 2.0));
			
			vec3 white = vec3(1.0, 1.0, 1.0);
			vec3 black = vec3(0.0, 0.0, 0.0);
			vec3 color = mix(white, black, checker);

			gl_FragColor = vec4(color, 1.0);
		} else if (u_subtask == 6){
				// Variables de configuración del seno
				float amplitude = 0.25; // La amplitud del seno
				float frequency = 1.0; // La frecuencia del seno

				// Calcular la posición y del seno para la coordenada x actual
				float PI = 3.141592;

				float sine_y = amplitude * sin(v_uv.x * frequency * 2.0 * PI) + 0.5;

				vec3 green = vec3(0.0, 1.0, 0.0);
				vec3 black = vec3(0.0, 0.0, 0.0);

				float t = mix(amplitude, 1.0, v_uv.y); // Establecemos el rango de interpolación
				vec3 color_above = mix(green, black, t);

				float t2 = mix(0.0, 1.0- amplitude, v_uv.y); // Establecemos el rango de interpolación
				vec3 color_below = mix(black, green, t2);

				float above_sine = step(sine_y, aspect_corrected_uv.y);

				vec3 final_color = mix(color_below, color_above, above_sine);
				gl_FragColor = vec4(final_color, 1.0);
		}
	} else if (u_currentTask == 2) {
		vec4 texture_color = texture2D(u_texture, v_uv);
		gl_FragColor = texture_color;

		// SUBTASCA A DEL EJERCICIO 2
		if (u_subtask == 1) {
			// Creamos una escala de grises haciendo un producto punto entre el color de la textura y un vector con los valores estandar utilizados para la escala de grises
		vec3 grayscale_color = vec3(dot(texture_color.rgb, vec3(0.299, 0.587, 0.114)));
		gl_FragColor = vec4(grayscale_color, texture_color.a);
		}
		else if (u_subtask == 2) {
		// SUBTASCA B DEL EJERCICIO 2
		// Crea el negativo de la imagen restando el color de la textura a 1, invirtiendo así los colores.
		vec3 negative_color = vec3(1.0 - texture_color.r, 1.0 - texture_color.g, 1.0 - texture_color.b);
		gl_FragColor = vec4(negative_color, texture_color.a);
		}
		else if (u_subtask == 3) {
		// SUBTASCA C DEL EJERCICIO 2
		// Convierte la textura a escala de grises utilizando la luminancia y mezcla el valor de escala de grises con dos colores para obtener el efecto de duotono.
		float grayscale = dot(texture_color.rgb, vec3(0.299, 0.587, 0.114));
		vec3 darkColor = vec3(0.0, 0.0, 0.0); // Negro
		vec3 lightColor = vec3(1.0, 1.0, 0.0); // Amarillo
		vec3 duotoneColor = mix(darkColor, lightColor, grayscale);
		gl_FragColor = vec4(duotoneColor, texture_color.a);
		}
		else if (u_subtask == 4) {
		// SUBTASCA D DEL EJERCICIO 2
				float gray = dot(texture_color.rgb, vec3(0.299, 0.587, 0.114));
		float threshold = 0.5;
		vec3 blackAndWhite = vec3(step(threshold, gray));
		gl_FragColor = vec4(blackAndWhite, texture_color.a);
		}
		else if (u_subtask == 5) {
		// SUBTASCA E DEL EJERCICIO 2
		// Oscurece el color de la textura basado en la distancia desde el centro de la textura.
		vec2 center = vec2(0.5, 0.5);
		float distance = length(v_uv - center);
		float darknessFactor = 1.0 - distance;
		vec3 color = texture_color.rgb * darknessFactor;
		gl_FragColor = vec4(color, texture_color.a);
		}
		else if (u_subtask == 6) 
		{
		// Tamaño del desenfoque
		float blurSize = 3.0 / u_resolution.x;
		vec4 sum = texture2D(u_texture, v_uv);
		sum += texture2D(u_texture, v_uv + vec2(-blurSize, -blurSize));
		sum += texture2D(u_texture, v_uv + vec2(blurSize, -blurSize));
		sum += texture2D(u_texture, v_uv + vec2(-blurSize, blurSize));
		sum += texture2D(u_texture, v_uv + vec2(blurSize, blurSize));
		sum /= 5.0;
		gl_FragColor = sum;
		}
	} else if (u_currentTask == 3){
		// SUBTASCA A DEL EJERCICIO 3
		if (u_subtask == 1)
		{
			// Rota la textura en función del tiempo
			float rotationAngle = u_time * 0.5 * 3.14159;
			float cosAngle = cos(rotationAngle);
			float sinAngle = sin(rotationAngle);
			vec2 rotatedCoords = vec2((v_uv.x - 0.5) * cosAngle - (v_uv.y - 0.5) * sinAngle + 0.5, (v_uv.x - 0.5) * sinAngle + (v_uv.y - 0.5) * cosAngle + 0.5);

			vec4 color = texture2D(u_texture, rotatedCoords);
			gl_FragColor = color;
		}
		// SUBTASCA B DEL EJERCICIO 3
    	else if (u_subtask == 2)
		{
			// Tamaño de los píxeles
			float pixelSize = 1.0 / 25.0;

			// Calcular las coordenadas del píxel correspondiente
			vec2 pixelCoords = vec2(floor(v_uv.x / pixelSize) * pixelSize, floor(v_uv.y / pixelSize) * pixelSize);

			// Obtener el color del píxel correspondiente
			vec4 pixelColor = texture2D(u_texture, pixelCoords);

			// Aplicar movimiento en función de u_time
			float movement = sin(u_time) * 0.1;
			pixelCoords += vec2(movement, movement);

			// Obtener el color del píxel movido
			vec4 movedPixelColor = texture2D(u_texture, pixelCoords);

			// Salida del color final
			gl_FragColor = movedPixelColor;
		}
	}
}
