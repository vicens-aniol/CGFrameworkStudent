varying vec4 v_color; 
uniform sampler2D u_texture;
varying vec2 v_uv;

void main()
{
    vec4 texColor = texture2D(u_texture, v_uv);
    
    gl_FragColor = texColor* v_color;
}