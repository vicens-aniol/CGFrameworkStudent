varying vec2 v_uv;

uniform vec2 u_resolution; // La resolución de la ventana
uniform int u_currentTask; // La tarea actual
uniform int u_subtask;
uniform float u_time;
uniform sampler2D u_texture;

void main()
{
	float aspect_ratio = u_resolution.x / u_resolution.y; // Calculamos el aspect ratio de la ventana
	vec2 aspect_corrected_uv = vec2(v_uv.x * aspect_ratio, v_uv.y); // Adaptamos las coordenadas al aspect
	vec2 aspect_corrected_center = vec2(0.5 * aspect_ratio, 0.5); // Lo mismo con las coordenadas del centro de la pantalla

	if (u_currentTask == 1)
	{
		// SUBTASCA A DEL EJERCICIO 1
		if (u_subtask == 1)
		{
			vec3 color = mix(vec3(0.0, 0.0, 1.0), vec3(1.0, 0.0, 0.0), v_uv.x); // Hacemos mix en funcion de las coordenadas x y los colores rojo y azul
			gl_FragColor = vec4(color, 1.0);
		}

		// SUBTASCA B DEL EJERCICIO 1
		else if (u_subtask == 2)
		{
			float distance = distance(aspect_corrected_uv, aspect_corrected_center); // Calculamos la distancia entre al centro
			vec3 color = mix(vec3(0.0), vec3(1.0), distance); // Hacemos un mix entre negro y blanco en funcion de la distancia
			gl_FragColor = vec4(color, 1.0);
		}

		// SUBTASCA C DEL EJERCICIO 1
		else if (u_subtask == 3)
		{
			// Definimos los parámetros de las rayas en floats
			float tamano_linea = 0.1;
			float ancho_borde = tamano_linea / 2.0; 

			// Calculamos el modulo de las coordenadas UV para crear el patrón de las lineas
			float mod_X = mod(aspect_corrected_uv.x, tamano_linea * 2.0);
			float mod_Y = mod(aspect_corrected_uv.y, tamano_linea * 2.0);

			// Creamos el suavizado rojo
			float red_stripe = smoothstep(ancho_borde, tamano_linea, mod_X) * (1.0 - smoothstep(tamano_linea, tamano_linea + ancho_borde, mod_X));

			// Creamos el suavizado azul
			float blue_stripe = smoothstep(ancho_borde, tamano_linea, mod_Y) * (1.0 - smoothstep(tamano_linea, tamano_linea + ancho_borde, mod_Y));

			// Cáculo de la intensidad con cada uno de los colores
			vec3 red = vec3(1.0, 0.0, 0.0) * red_stripe;
			vec3 blue = vec3(0.0, 0.0, 1.0) * blue_stripe;

			// Mezclamos ambos colores
			vec3 color = red + blue - red * blue;
			// Clampeamos para asegurarnos de que no se sobre pase el 0 ni el 1
			color = clamp(color, 0.0, 1.0);

			gl_FragColor = vec4(color, 1.0);
		}
		
		// SUBTASCA D DEL EJERCICIO 1
		else if (u_subtask == 4)
		{
			// Calcular la posición de la cuadrícula basada en las coordenadas UV
			vec2 grid_pos = floor(aspect_corrected_uv * 16.0);
			vec2 cell_center = (grid_pos + 0.5) / 16.0; // Calculo del interior de la celda

			// Gradiente de esquina inferior izquierda a la  derecha
			vec3 negro = vec3(0.0, 0.0, 0.0);
			vec3 rojo = vec3(1.0, 0.0, 0.0);

			// Calculamos el color del gradiente en el centro de la celda
			vec3 bottom_color = mix(negro, rojo, cell_center.x);

			// Gradiente de esquina izquierda a derecha
			vec3 verde = vec3(0.0, 1.0, 0.0); 
			vec3 amarillo = vec3(1.0, 1.0, 0.0);

			// Calculamos el color del gradiente en el centro de la celda
			vec3 top_color = mix(verde, amarillo, cell_center.x);

			// Interpolamos los colores en cada celda, para obtener el color uniforme
			vec3 color = mix(bottom_color, top_color, cell_center.y);

			gl_FragColor = vec4(color, 1.0);
		}
		
		// SUBTASCA E DEL EJERCICIO 1
		else if (u_subtask == 5)
		{
			// Calculo de los cuadrados (16 en nuestro caso)
			vec2 grid_pos = floor(aspect_corrected_uv * 16.0);

			// Cálculo del color en funcion de la celda y el step para determinar en que celda nos econtramos (mod 2)
			float checker = step(1.0, mod(grid_pos.x + grid_pos.y, 2.0));
			
			// Cálculo de los colores negro y blanco en función del step anterior
			vec3 white = vec3(1.0, 1.0, 1.0);
			vec3 black = vec3(0.0, 0.0, 0.0);
			vec3 color = mix(white, black, checker);

			gl_FragColor = vec4(color, 1.0);
		} 

		// SUBTASCA F DEL EJERCICIO 1
		else if (u_subtask == 6)
		{
			// Parámetros de la onda
			float amplitude = 0.25;
			float frequency = 1.0;
			float PI = 3.141592;

			// Onda Seno Formula
			float sine_y = amplitude * sin(aspect_corrected_uv.x * frequency * 2.0 * PI) + 0.5;

			// Colores verde y negro
			vec3 green = vec3(0.0, 1.0, 0.0);
			vec3 black = vec3(0.0, 0.0, 0.0);
			
			// Computo del punto máximo (amplitud) del degradado para la parte superior
			float t = mix(amplitude, 1.0, aspect_corrected_uv.y); 

			// Calculo del color en funcion del máximo, determinado por el mix anterior
			vec3 color_arriba = mix(green, black, t);

			// Repetimos el proceso pero para la parte inferior y cambiando las coordenadas de amplitud para que sean las de abajo
			float t2 = mix(0.0, 1.0- amplitude, v_uv.y);
			vec3 color_abajo = mix(black, green, t2);

			// Checkeamos que esté arriba o abajo del seno para determinar un degradado o otro.
			float arriba_seno = step(sine_y, aspect_corrected_uv.y);
			vec3 color = mix(color_abajo, color_arriba, arriba_seno);

			gl_FragColor = vec4(color, 1.0);
		}
	} 
	else if (u_currentTask == 2) 
	{
		vec4 texture_color = texture2D(u_texture, v_uv);
		gl_FragColor = texture_color;

		// SUBTASCA A DEL EJERCICIO 2
		if (u_subtask == 1) 
		{
			// Creamos una escala de grises haciendo un producto punto entre el color de la textura y un vector con los valores estandar utilizados para la escala de grises
			vec3 color = vec3(dot(texture_color.rgb, vec3(0.299, 0.587, 0.114)));
			gl_FragColor = vec4(color, texture_color.a);
		}
		
		// SUBTASCA B DEL EJERCICIO 2
		else if (u_subtask == 2) 
		{
			// Creamos el negativo de la imagen restando el color de la textura a 1 de cada componente ya que esto hara el efecto de invertir los colores. 
			vec3 color = vec3(1.0 - texture_color.r, 1.0 - texture_color.g, 1.0 - texture_color.b);
			gl_FragColor = vec4(color, texture_color.a);
		}

		// SUBTASCA C DEL EJERCICIO 2
		else if (u_subtask == 3) 
		{
			// Calculamos la escala de grises como lo hemos hecho antes, realizamos una interpolacion entre el negro y el amarillo basandose en la escala de grises.
			float gray_scale = dot(texture_color.rgb, vec3(0.299, 0.587, 0.114));
			vec3 color = mix(vec3(0.0), vec3(1.0, 1.0, 0.0), gray_scale);
			gl_FragColor = vec4(color, texture_color.a);
		}
		
		// SUBTASCA D DEL EJERCICIO 2
		else if (u_subtask == 4) 
		{
			// Con la escala de grises calculada, comparamos el valor con un umbral y si es mayor, el color sera blanco y sino negro
			float gray_scale = dot(texture_color.rgb, vec3(0.299, 0.587, 0.114));
			float umbral = 0.5;
			vec3 color = vec3(step(umbral, gray_scale));
			gl_FragColor = vec4(color, texture_color.a);
		}
		
		// SUBTASCA E DEL EJERCICIO 2
		else if (u_subtask == 5) 
		{
			// Oscurece el color de la textura basado en la distancia desde el centro de la textura, en el centro la oscuridad sera 0 ya que la distancia es 0 y a medida que se aleje sera ira oscureciendo.
			vec2 center = vec2(0.5, 0.5);
			float distance = length(v_uv - center);
			float factor_oscuro = 1.0 - distance;
			vec3 color = texture_color.rgb * factor_oscuro;
			gl_FragColor = vec4(color, texture_color.a);
		}
		
		// SUBTASCA F DEL EJERCICIO 2
		else if (u_subtask == 6) 
		{
			// Primero determinamos cuanto nos queremos alejar del pixel actual para obetener los pixeles de al lado.
			float blur_size = 3.0 / u_resolution.x;
			vec4 sum = texture2D(u_texture, v_uv);
			
			// Cada una de las sumas obtendrá el color de un pixel que esta a una distancia de blur_size del pixel actual.
			sum += texture2D(u_texture, v_uv + vec2(-blur_size, -blur_size));
			sum += texture2D(u_texture, v_uv + vec2(blur_size, -blur_size));
			sum += texture2D(u_texture, v_uv + vec2(-blur_size, blur_size));
			sum += texture2D(u_texture, v_uv + vec2(blur_size, blur_size));

			// Esto calculara el promedio de los colores obtenidos.
			sum /= 5.0;
			gl_FragColor = sum;
		}
	} 
	else if (u_currentTask == 3)
	{
		// SUBTASCA A DEL EJERCICIO 3
		if (u_subtask == 1)
		{
			// Calcular el ángulo de rotación en función de u_time
			float rotation_angle = u_time * 0.5 * 3.14159;

			// Estos valores nos ayudarán a calcular las coordenadas UV de la textura 
			float cos_angulo = cos(rotation_angle);
			float sin_angulo = sin(rotation_angle);

			// Formula de rotacion de coordenadas 2D para rotar en el punto 0.5, 0.5
			vec2 rotated_coords = vec2((v_uv.x - 0.5) * cos_angulo - (v_uv.y - 0.5) * sin_angulo + 0.5, (v_uv.x - 0.5) * sin_angulo + (v_uv.y - 0.5) * cos_angulo + 0.5);

			vec4 color = texture2D(u_texture, rotated_coords);
			gl_FragColor = color;
		}

		// SUBTASCA B DEL EJERCICIO 3
    	else if (u_subtask == 2)
		{
			// Calculamos el tamaño de los píxeles
			float pixel_size = 1.0 / 25.0;

			// Dividimos las coordenadas UV por el tamaño del píxel y redondeamos hacia abajo con la funcion floor, esto dara el efecto de pixelado
			vec2 pixel_coords = vec2(floor(v_uv.x / pixel_size) * pixel_size, floor(v_uv.y / pixel_size) * pixel_size);

			// Obtenemos el color del píxel correspondiente
			vec4 pixel_color = texture2D(u_texture, pixel_coords);

			// Aplicamos movimiento en función de u_time que oscilará entre -0.1 y 0.1
			float movement = sin(u_time) * 0.1;
			pixel_coords += vec2(movement, movement);

			// Cogemos el color del píxel movido
			vec4 moved_pixel_color = texture2D(u_texture, pixel_coords);
			gl_FragColor = moved_pixel_color;
		}
	}
}
