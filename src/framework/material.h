#ifndef MATERIAL_H
#define MATERIAL_H

#include "mesh.h"
#include "image.h"
#include "framework.h"
#include "texture.h"
#include "shader.h"

#define MAX_LUCES 5

class Material
{

public:
    // Shader y textura
    Shader *shader;
    Texture *texture;
    Texture *texture_normal;

    Material();

    struct sLight
    {
        Vector3 position;
        Vector3 Id;
        Vector3 Is;
    };

    struct sUniformData
    {
        Matrix44 model;          // Matriz del modelo
        Matrix44 viewprojection; // Matriz de vista y proyeccion de la camara
        Vector3 La;              // Luz ambiente

        sLight lights[MAX_LUCES]; // Luces
        int num_lights;           // Numero de luces

        // Propiedades de material
        Vector3 Ka;
        Vector3 Kd;
        Vector3 Ks;

        float shininess;

        Vector3 texture_flags;
    };

    // Metodos enable y disable
    void Enable(const sUniformData &uniformData, int light_index = 0);
    void Disable();
};

#endif
