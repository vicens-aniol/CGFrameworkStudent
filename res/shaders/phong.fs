uniform vec3 u_La; 
uniform vec3 u_Ka; 
uniform vec3 u_Kd; 
uniform vec3 u_Ks; 
uniform float u_shininess; 
uniform vec3 u_Id; 
uniform vec3 u_Is; 
uniform vec3 u_lightposition; 
uniform mat4 u_model; 
uniform sampler2D u_texture;
uniform sampler2D u_texture_normal;
uniform vec3 u_texture_flags;

varying vec2 v_uv;
varying vec3 v_world_position;
varying vec3 v_world_normal;

void main()
{
    vec4 texColor = texture2D(u_texture, v_uv);
    vec3 texNormal = texture2D(u_texture_normal, v_uv).rgb;
    texNormal = texNormal * 2.0 - 1.0;

    float dist = distance(u_lightposition, v_world_position);

    // ifs ternarios en funcion del vector 3 que activa o desactiva características del shader con las teclas
    vec3 temp_Kd = (u_texture_flags.x == 1.0) ? texColor.rgb : u_Kd;
    vec3 temp_Ka = (u_texture_flags.x == 1.0) ? mix(u_Ka, texColor.rgb, 0.5) : u_Ka;
    vec3 temp_Ks = (u_texture_flags.y == 1.0) ? vec3(texColor.a) : u_Ks;
    vec3 temp_world_normal = (u_texture_flags.z == 1.0) ? (u_model * vec4(texNormal, 0.0)).xyz : v_world_normal;

    vec3 L = normalize(u_lightposition - v_world_position);
    vec3 V = normalize(-v_world_position); 
    vec3 R = reflect(-L, temp_world_normal);
    
    vec3 ambient = temp_Ka * u_La;
    vec3 diffuse = (temp_Kd * clamp(dot(L, temp_world_normal), 0.0, 1.0) * u_Id) / (dist * dist);
    vec3 specular = (temp_Ks * pow(clamp(dot(R, V), 0.0, 1.0), u_shininess) * u_Is) / (dist * dist);
    vec4 color = vec4(ambient + diffuse + specular, 1.0);

    gl_FragColor = color;
}