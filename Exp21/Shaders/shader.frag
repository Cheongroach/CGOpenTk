#version 330 core

out vec4 outputColor;

uniform vec3 uColor;

void main()
{
    outputColor = vec4(uColor, 1.0);
}