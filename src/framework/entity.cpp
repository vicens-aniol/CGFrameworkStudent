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

void Entity::Render(Image *framebuffer, Camera *camera, Color c, FloatImage *zBuffer)
{

    // Obtener los vértices de la malla
    const std::vector<Vector3> &vertices = mesh.GetVertices();

    // Crear un puntero condicional para el zBuffer y la textura, para que puedan ser nulos en caso de activarlo con los keybindings
    FloatImage *conditionalZBuffer = occlusion ? nullptr : zBuffer;
    Image *conditionalTexture = (mode == eRenderMode::POINTCLOUD) ? texture : nullptr;

    // Iterar a través de todos los triángulos en la malla
    for (int i = 0; i < vertices.size(); i += 3)
    {
        Vector3 triangleVertices[3];
        bool skipTriangle = false;

        // Iterar a través de los vértices del triángulo
        for (int j = 0; j < 3; j++)
        {
            Vector3 vertex = modelMatrix * vertices[i + j];
            bool negZ;

            // Proyectar el vértice al espacio de recorte
            vertex = camera->ProjectVector(vertex, negZ);

            // Si el vértice está fuera del frustum, descartar el triángulo entero
            if (negZ)
            {
                skipTriangle = true;
                break; // Usamos break aquí para salir del ciclo de vértices
            }

            // Convertir las posiciones del espacio de recorte a espacio de pantalla
            vertex.x = (vertex.x + 1) / 2 * 1280;
            vertex.y = (vertex.y + 1) / 2 * 720;
            triangleVertices[j] = vertex;
        }

        // Si se debe omitir el triángulo, continúa con el siguiente
        if (skipTriangle)
        {
            continue;
        }

        // Cojemos los UVs de la malla
        std::vector<Vector2> uvs = mesh.GetUVs();
        // TODO: AQUI SE TIENEN QUE PONER LOS IFS

        // printf("Modo: %d\n", mode);

        // occlusion ? zBuffer = nullptr : zBuffer = zBuffer;
        // mode == eRenderMode::POINTCLOUD ? texture = texture : texture = nullptr;

        // printf("Occlusion: %d\n", occlusion);
        // printf("ZBuffer: %d\n", zBuffer);
        // printf("Texture: %d\n", texture);

        // Cambiar entre mesh texture o el plain color

        if (mode == eRenderMode::TRIANGLES)
        {
            framebuffer->DrawTriangle(Vector2(triangleVertices[0].x, triangleVertices[0].y), Vector2(triangleVertices[1].x, triangleVertices[1].y), Vector2(triangleVertices[2].x, triangleVertices[2].y), Color::BLUE, true, Color::BLUE);
        }
        else
        {
            framebuffer->DrawTriangleInterpolated(triangleVertices[0], triangleVertices[1], triangleVertices[2], Color::RED, Color::GREEN, Color::BLUE, conditionalZBuffer, conditionalTexture, uvs[i], uvs[i + 1], uvs[i + 2]);
        }

        // Dibujar las líneas del triángulo (Labs anteriores)
        // framebuffer->DrawLineDDA(static_cast<int>(triangleVertices[0].x), static_cast<int>(triangleVertices[0].y), static_cast<int>(triangleVertices[1].x), static_cast<int>(triangleVertices[1].y), c);
        // framebuffer->DrawLineDDA(static_cast<int>(triangleVertices[1].x), static_cast<int>(triangleVertices[1].y), static_cast<int>(triangleVertices[2].x), static_cast<int>(triangleVertices[2].y), c);
        // framebuffer->DrawLineDDA(static_cast<int>(triangleVertices[2].x), static_cast<int>(triangleVertices[2].y), static_cast<int>(triangleVertices[0].x), static_cast<int>(triangleVertices[0].y), c);
    }
}

void Entity::Update(float seconds_elapsed)
{
    Vector3 axis(0, 1, 0); // Eje Y
    modelMatrix.Rotate(5.0 * DEG2RAD, axis);
}
