uniform vec3 u_La; 
uniform vec3 u_Ka; 
uniform vec3 u_Kd; 
uniform vec3 u_Ks; 
uniform float u_shininess; 
uniform vec3 u_Id; 
uniform vec3 u_Is; 
uniform vec3 u_lightposition; 
uniform sampler2D u_texture;

varying vec2 v_uv;
varying vec3 v_world_position;
varying vec3 v_world_normal;

void main()
{
    vec4 texColor = texture2D(u_texture, v_uv);

    // Direccion de la luz
    vec3 L = normalize(u_lightposition - v_world_position);

    // Dirección de la vista
    vec3 V = normalize(-v_world_position); 

    // calculo del reglejo sobre la normal
    vec3 R = reflect(-L, v_world_normal);

    // Luz de ambiente calculo
    vec3 ambient = u_Ka * u_La;

    // iluminacion difusa calculo
    vec3 diffuse = u_Kd * max(dot(L, v_world_normal), 0.0) * u_Id;

    // specular iluminacion calculo
    vec3 specular = u_Ks * pow(max(dot(R, V), 0.0), u_shininess) * u_Is;

    // combinacion de todas y 1.0, asignando al varying para el fragment shader
    vec4 color = vec4(ambient + diffuse + specular, 1.0);

    gl_FragColor = texColor * color;
}