varying vec4 v_color; 
uniform sampler2D u_texture;
varying vec2 v_uv;

void main()
{
    gl_FragColor = v_color;
}