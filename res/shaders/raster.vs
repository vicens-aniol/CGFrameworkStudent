// Global variables from the CPU
uniform mat4 u_model;
uniform mat4 u_viewprojection;

// Variables to pass to the fragment shader
varying vec2 v_uv;
varying vec3 v_world_position;
varying vec3 v_world_normal;

//here create uniforms for all the data we need here

void main()
{	
	v_uv = gl_MultiTexCoord0.xy;

	// De local a world
	vec3 world_position = (u_model * vec4( gl_Vertex.xyz, 1.0)).xyz;

	// De local a world
	vec3 world_normal = (u_model * vec4( gl_Normal.xyz, 0.0)).xyz;

	// Establecemos las variables varying para el fragment
	v_world_position = world_position;
	v_world_normal = world_normal;

	// Lo proyectamos en el mundo con la viewprojection_matrix
	gl_Position = u_viewprojection * vec4(world_position, 1.0); 
}