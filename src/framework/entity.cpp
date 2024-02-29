#include "entity.h"

// Constructor por defecto
Entity::Entity()
{
}

// Constructor que toma una matriz de modelo
Entity::Entity(const Matrix44 &modelMatrix) : modelMatrix(modelMatrix)
{
}

// Constructor que toma una malla
Entity::Entity(const Mesh &mesh) : mesh(mesh)
{
}

// Constructor que toma una matriz de modelo y una malla
Entity::Entity(const Matrix44 &modelMatrix, const Mesh &mesh) : modelMatrix(modelMatrix), mesh(mesh)
{
}

// Método para establecer la matriz del modelo
void Entity::setModelMatrix(const Matrix44 &modelMatrix)
{
    this->modelMatrix = modelMatrix;
}

// Método para establecer la malla
void Entity::setMesh(const Mesh &mesh)
{
    this->mesh = mesh;
}

// Método para renderizar unicamente el mesh desde Enitity.
void Entity::Render(Material::sUniformData uniformData)
{
    // Update model matrix
    uniformData.model = modelMatrix;
    material->Enable(uniformData);
    mesh.Render();
    material->Disable();
    // mesh.Render();
}

void Entity::Update(float seconds_elapsed)
{
    Vector3 axis(0, 1, 0); // Eje Y
    modelMatrix.Rotate(5.0 * DEG2RAD, axis);
}
