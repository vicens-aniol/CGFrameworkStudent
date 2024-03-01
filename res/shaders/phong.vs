uniform vec3 u_lightposition; 
uniform mat4 u_model; 
uniform mat4 u_viewprojection; 

varying vec2 v_uv;
varying vec3 v_world_position;
varying vec3 v_world_normal;

void main()
{
    v_uv = gl_MultiTexCoord0.xy;

    // de local a world
    vec3 world_position = (u_model * vec4( gl_Vertex.xyz, 1.0)).xyz;

    // de local a world normal
    vec3 world_normal = normalize((u_model * vec4(gl_Normal.xyz, 0.0)).xyz);

    // Pasar a fragment shader
    v_world_position = world_position;
    v_world_normal = world_normal;

    gl_Position = u_viewprojection * vec4(world_position, 1.0);
}