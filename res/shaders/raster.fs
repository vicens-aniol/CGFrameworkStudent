// This variables comes from the vertex shader
// They are baricentric interpolated by pixel according to the distance to every vertex
varying vec3 v_world_normal;
varying vec2 v_uv;

varying vec3 v_world_position;

uniform sampler2D u_texture;


void main()
{

	// Calculamos y establecemos el color de la textura al fragment color
	vec4 texColor = texture2D(u_texture, v_uv);

	// el texColor.a es el canal alpha de la textura
	gl_FragColor = vec4( texColor.rgb, texColor.a );
}
