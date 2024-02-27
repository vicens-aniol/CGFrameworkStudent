#include "mesh.h"
#include "image.h"
#include "framework.h"
#include "texture.h"
#include "shader.h"
#include "material.h"

class Entity
{

    float time;

public:
    bool occlusion = false; // Valor inicial de occlusion a falso

    std::vector<Vector3> lastTriangleVertices; // Almacenar la última posición de los vértices

    // Matriz de modelo
    Matrix44 modelMatrix;

    // Malla
    Mesh mesh;

    // Lab 3: Texture for the entity
    Image *texture;

    // Lab 4: Shader for the entity
    Shader *shader;

    // Constructor por defecto
    Entity();

    // Constructor que toma una matriz de modelo
    Entity(const Matrix44 &modelMatrix);

    // Constructor que toma una malla
    Entity(const Mesh &mesh);

    // Constructor que toma una matriz de modelo y una malla
    Entity(const Matrix44 &modelMatrix, const Mesh &mesh);

    // Método para establecer la matriz del modelo
    void setModelMatrix(const Matrix44 &modelMatrix);

    // Método para establecer la malla
    void setMesh(const Mesh &mesh);

    // Método para renderizar la entidad
    // void Render(Image *framebuffer, Camera *camera, Color c, FloatImage *zBuffer = nullptr);
    void Render(Material::sUniformData uniformData);

    void Update(float seconds_elapsed);
};
