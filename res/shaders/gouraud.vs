uniform vec3 u_La; 
uniform vec3 u_Ka; 
uniform vec3 u_Kd; 
uniform vec3 u_Ks; 
uniform float u_shininess; 

uniform vec3 u_lightposition; 
uniform vec3 u_Id; 
uniform vec3 u_Is; 

uniform mat4 u_model; 
uniform mat4 u_viewprojection; 

varying vec4 v_color;

varying vec2 v_texCoord;

varying vec2 v_uv;
varying vec3 v_world_position;
varying vec3 v_world_normal;

void main()
{
    v_uv = gl_MultiTexCoord0.xy;

    // de local a world
    vec3 world_position = (u_model * vec4( gl_Vertex.xyz, 1.0)).xyz;
    v_world_position = world_position;

    // de local a world normal
    vec3 world_normal = normalize((u_model * vec4(gl_Normal.xyz, 0.0)).xyz);
    v_world_normal = world_normal;

    // Direccion de la luz
    vec3 L = normalize(u_lightposition - world_position);

    // Dirección de la vista
    vec3 V = normalize(-world_position); // la camera està al centre de coordenades

    // calculo del reflejo sobre la normal
    vec3 R = reflect(-L, world_normal);

    // calculo de distancia para atenuacion
    float dist = distance(u_lightposition, world_position);

    // luz de ambiente calculo
    vec3 ambient = u_Ka * u_La;

    // iluminacion difusa calculo con atenuacion
    vec3 diffuse = (u_Kd * clamp(dot(L, world_normal), 0.0, 1.0) * u_Id) / (dist * dist);

    // specular iluminacion calculo con atenuacion
    vec3 specular = (u_Ks * pow(clamp(dot(R, V), 0.0, 1.0), u_shininess) * u_Is) / (dist * dist);

    // combinacion de todas y 1.0, asignando al varying para el fragment shader
    v_color = vec4(ambient + diffuse + specular, 1.0);

    gl_Position = u_viewprojection * vec4(world_position, 1.0);
}
