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

void main()
{
    vec3 P = vec3(0.0);
    vec3 Eye = vec3(0.0, 0.0, 1.0);

    vec3 N = vec3(0.0, 0.0, 1.0);
    vec3 L = normalize(u_lightposition - P);
    vec3 V = normalize(Eye - P); 
    vec3 R = reflect(-L, N);

    vec3 ambient = u_Ka * u_La;
    vec3 diffuse = u_Kd * max(dot(L, N), 0.0) * u_Id;
    vec3 specular = u_Ks * pow(max(dot(R, V), 0.0), u_shininess) * u_Is;

    vec4 color = vec4(ambient + diffuse + specular, 1.0);
    gl_Position = u_viewprojection * u_model * vec4(P, 1.0);
}