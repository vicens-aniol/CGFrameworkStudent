#include "material.h"

// Constructor por defecto
Material::Material()
{
}

// Métodos enable y disable

void Material::Enable(const sUniformData &uniformData, int light_index)
{ // constante porque no cambiará durante el ciclo de vida del objeto

    shader->Enable();
    shader->SetTexture("u_texture", texture);
    shader->SetTexture("u_texture_normal", texture_normal);

    shader->SetVector3("u_texture_flags", uniformData.texture_flags);

    if (light_index == 0)
        shader->SetVector3("u_La", uniformData.La);

    // Subir las propiedades del material al shader
    shader->SetVector3("u_Ka", uniformData.Ka);
    shader->SetVector3("u_Kd", uniformData.Kd);
    shader->SetVector3("u_Ks", uniformData.Ks);
    shader->SetFloat("u_shininess", uniformData.shininess);

    // Subir las propiedades de la luz al shader
    shader->SetVector3("u_lightposition", uniformData.lights[light_index].position);
    shader->SetVector3("u_Id", uniformData.lights[light_index].Id);
    shader->SetVector3("u_Is", uniformData.lights[light_index].Is);

    // Subir las matrices y la luz ambiental al shader
    shader->SetMatrix44("u_model", uniformData.model);
    shader->SetMatrix44("u_viewprojection", uniformData.viewprojection);
}

void Material::Disable()
{
    shader->Disable();
}
