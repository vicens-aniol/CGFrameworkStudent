
#include "mesh.h"
#include "image.h"
#include "framework.h"
#include "texture.h"
#include "shader.h"

class Material
{
    // Shader y textura
    Shader *shader;
    Texture *texture;

    // Color componentes
    Vector3 Ka;
    Vector3 Kd;
    Vector3 Ks;

    // Shininess
    float shininess;

public:
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

        sLight light; // Luz

        // Propiedades de material
        Vector3 Ka;
        Vector3 Kd;
        Vector3 Ks;

        float shininess;

        // luces de la escena
        // TODO: añadir luces de la escena
    };

    // Metodos enable y disable
    void Enable(const sUniformData &uniformData);
    void Disable();
};
