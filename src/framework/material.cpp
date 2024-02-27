#include "material.h"

// Constructor por defecto
Material::Material()
{
}

// Métodos enable y disable

void Material::Enable(const sUniformData &uniformData)
{ // constante porque no cambiará durante el ciclo de vida del objeto
    shader->Enable();

    this->Ka = uniformData.Ka;
    this->Kd = uniformData.Kd;
    this->Ks = uniformData.Ks;
    this->shininess = uniformData.shininess;

    // Upload material properties
    shader->SetVector3("u_La", uniformData.La);
    shader->SetVector3("u_Ka", uniformData.Ka);
    shader->SetVector3("u_Kd", uniformData.Kd);
    shader->SetVector3("u_Ks", uniformData.Ks);
    shader->SetFloat("u_shininess", uniformData.shininess);
}

void Material::Disable()
{
    shader->Disable();
}
