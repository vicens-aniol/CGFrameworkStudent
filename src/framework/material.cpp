#include "material.h"

// Constructor por defecto
Material::Material()
{
}

// Métodos enable y disable

void Material::Enable(const sUniformData &uniformData)
{ // constante porque no cambiará durante el ciclo de vida del objeto
    // Habilitar el shader
    shader->Enable();

    // Subir las propiedades del material al shader
    shader->SetVector3("u_Ka", uniformData.Ka);
    shader->SetVector3("u_Kd", uniformData.Kd);
    shader->SetVector3("u_Ks", uniformData.Ks);
    shader->SetFloat("u_shininess", uniformData.shininess);

    // Subir las propiedades de la luz al shader
    shader->SetVector3("u_lightposition", uniformData.light.position);
    shader->SetVector3("u_Id", uniformData.light.Id);
    shader->SetVector3("u_Is", uniformData.light.Is);

    // Subir las matrices y la luz ambiental al shader
    shader->SetMatrix44("u_model", uniformData.model);
    shader->SetMatrix44("u_viewprojection", uniformData.viewprojection);
    shader->SetVector3("u_La", uniformData.La);

    //shader->SetTexture("u_texture", texture);
    
}

void Material::Disable()
{
    shader->Disable();
}
