#include "material.h"

// Constructor por defecto
Material::Material()
{
}

// Métodos enable y disable

void Material::Enable(const sUniformData &uniformData)
{ // constante porque no cambiará durante el ciclo de vida del objeto
    shader->Enable();

    // ? establecemos el uniformData a la estructura de material

    // Upload material properties
    // shader->SetVector3("u_La", uniformData.La);
    // shader->SetVector3("u_Ka", uniformData.Ka);
    // shader->SetVector3("u_Kd", uniformData.Kd);
    // shader->SetVector3("u_Ks", uniformData.Ks);
    // shader->SetFloat("u_shininess", uniformData.shininess);

    shader->SetMatrix44("u_model", uniformData.model);
    shader->SetTexture("u_texture", texture);
    shader->SetMatrix44("u_viewprojection", uniformData.viewprojection);
}

void Material::Disable()
{
    shader->Disable();
}
