// This variables comes from the vertex shader
// They are baricentric interpolated by pixel according to the distance to every vertex
varying vec3 v_world_normal;
varying vec2 v_uv;

varying vec3 v_world_position;

uniform sampler2D u_texture;


void main()
{
	vec4 texColor = texture2D(u_texture, v_uv);

	gl_FragColor = vec4( texColor.rgb, texColor.a );
}
