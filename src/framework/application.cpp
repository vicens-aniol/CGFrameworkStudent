#include "application.h"
#include "mesh.h"
#include "utils.h"

Application::Application(const char *caption, int width, int height)
{
	this->window = createWindow(caption, width, height);

	int w, h;
	SDL_GetWindowSize(window, &w, &h);

	this->mouse_state = 0;
	this->time = 0.f;
	this->window_width = w;
	this->window_height = h;
	this->keystate = SDL_GetKeyboardState(nullptr);

	this->tempbuffer.Resize(w, h);
	this->framebuffer.Resize(w, h);
}

Application::~Application()
{
}

void Application::Init(void)
{
	std::cout << "Initiating app..." << std::endl;


	// Crear un cuadrado
    mesh = new Mesh();
    mesh->CreateQuad(); // Asegúrate de tener este método en tu clase Mesh

    // Cargar el shader
	shader = Shader::Get("shaders/quad.vs", "shaders/quad.fs");
	
	// // Cargar las 3 mallas
	// Mesh *mesh_lee = new Mesh();
	// mesh_lee->LoadOBJ("meshes/cleo.obj");

	// // Asignar la malla a las entidades
	// entity1.mesh = *mesh_lee;

	// // Establecer las matrices de modelo para posicionar las entidades
	// entity1.modelMatrix.SetTranslation(0, -0.25, 0); // Posiciona entity1

	
	camera = new Camera();

	// Image *texture1 = new Image();

	// !texture1->LoadTGA("textures/cleo_color_specular.tga", true) ? printf("No se ha podido cargar la textura\n") : printf("Textura cargada correctamente\n");

	// entity1.texture = texture1;
	// zbuffer = new FloatImage(framebuffer.width, framebuffer.height);
	// zbuffer->Fill(1000000000.0f);

	// Configurar la vista de la cámara y la perspectiva
	camera->LookAt(Vector3(0, 0, 1), Vector3(0, 0, 0), Vector3::UP);
	camera->SetPerspective(fov, aspect, near_plane, far_plane); // Iniciamos Perpsective por defecto

	// // Añadir las entidades a la lista
	// entities.push_back(entity1);
	
	
}

void Application::Render(void)
{
	//framebuffer.Fill(Color::BLACK);

	// // Creamos un zbuffer para la pantalla	podemos borrar esot no??
	// zbuffer->Fill(1000000000.0f);
	// entity1.Render(&framebuffer, camera, Color::WHITE, zbuffer);

	// framebuffer.Render(); // Renderizamos el framebuffer

	shader->Enable();
	shader->SetFloat("u_time", time);
	shader->SetVector2("u_resolution", Vector2(window_width, window_height));
	shader->SetUniform1("u_subtask", subtask);
	printf("Subtask: %f\n", subtask);
	printf("Current Task: %d\n", currentTask);
    mesh->Render();
    shader->Disable();
}


void Application::Update(float seconds_elapsed)
{
}

// keyboard press event
void Application::OnKeyPressed(SDL_KeyboardEvent event)
{
	// KEY CODES: https://wiki.libsdl.org/SDL2/SDL_Keycode

	/* Keystoke Console Debug */
	std::cout << "Key pressed: " << event.keysym.sym << std::endl;
	std::cout << borderWidth << std::endl;

	switch (event.keysym.sym)
	{
	case SDLK_ESCAPE:
		exit(0);
		break; // ESC key, kill the app
	case SDLK_PLUS:
		if (propertyState == CAMERA_NEAR)
		{
			camera->near_plane = std::min(camera->near_plane + 0.15f, camera->far_plane - 0.15f); // Incrementa de a poco el near_plane, asegurándose de que sea siempre menor que far_plane
		}
		else if (propertyState == CAMERA_FAR)
		{
			camera->far_plane += camera->far_plane = std::max(camera->near_plane + 0.2f, camera->far_plane); // Incrementa de a poco el far_plane
		}
		else if (camera->type == Camera::PERSPECTIVE)
		{
			camera->fov = std::min(179.0f, camera->fov + 3.0f); // Incrementa el FOV, asegurándose de que no sea mayor a 179
		}
		camera->UpdateProjectionMatrix();
		break;
	case SDLK_MINUS:
		if (propertyState == CAMERA_NEAR)
		{
			camera->near_plane = std::max(0.15f, camera->near_plane - 0.15f); // Decrementa de a poco el near_plane, no permite que sea menor a 0.01
		}
		else if (propertyState == CAMERA_FAR)
		{
			camera->far_plane = std::max(camera->near_plane + 0.2f, camera->far_plane - 0.2f); // Decrementa de a poco el far_plane, asegurándose de que sea siempre mayor que near_plane
		}
		else if (camera->type == Camera::PERSPECTIVE)
		{
			camera->fov = std::max(1.0f, camera->fov - 3.0f); // Decrementa el FOV en incrementos más pequeños
		}
		camera->UpdateProjectionMatrix();
		break;

	case SDLK_o:
	{
		camera->type = Camera::ORTHOGRAPHIC;

		float aspect_ratio = static_cast<float>(window_width) / static_cast<float>(window_height);
		float ortho_size = 1.0f;

		camera->SetOrthographic(-ortho_size * aspect_ratio, ortho_size * aspect_ratio, -ortho_size, ortho_size, camera->near_plane, camera->far_plane);
		camera->UpdateProjectionMatrix();
		break;
	}
	case SDLK_p:
	{
		camera->type = Camera::PERSPECTIVE;
		propertyState = CAMERA_NONE;
		camera->SetPerspective(camera->fov, static_cast<float>(window_width) / static_cast<float>(window_height), camera->near_plane, camera->far_plane);
		camera->UpdateProjectionMatrix();
		break;
	}
	case SDLK_1:
        currentTask = 1;
        break;
	case SDLK_2:
		currentTask = 2;
		break;
	case SDLK_3:
		currentTask = 3;
		break;
	case SDLK_4:
		currentTask = 4;
		break;
	case SDLK_a:
		if (currentTask >= 1 && currentTask <= 4) {
			subtask = 1;
		}
		break;
	case SDLK_b:
		if (currentTask >= 1 && currentTask <= 4) {
			subtask = 2;
		}
		break;
	case SDLK_c:
		if (currentTask >= 1 && currentTask <= 3) {
			subtask = 3;
		}
		break;
	case SDLK_d:
		if (currentTask >= 1 && currentTask <= 3) {
			subtask = 4;
		}
		break;	
	case SDLK_e:
		if (currentTask >= 1 && currentTask <= 3) {
			subtask = 5;
		}
		break;
	case SDLK_f:
		if (currentTask >= 1 && currentTask <= 3) {
			subtask = 6;
		}
		break;
	}
}

void Application::OnMouseButtonDown(SDL_MouseButtonEvent event)
{
	// std::cout << "Mouse button pressed: " << (int)event.button << SDL_BUTTON_LEFT << std::endl;
}

void Application::OnMouseButtonUp(SDL_MouseButtonEvent event)
{
	// std::cout << "Mouse button released: " << (int)event.button << std::endl;
}

void Application::DrawCirclesDDA(Vector2 p0, Vector2 p1, int radius, const Color &color)
{

	// Algrotimo similar al de la linea con DDA, pero usando circulos para rellenar el espacio entre los puntos, no pixeles, para un trazo mas suave

	int dx = p1.x - p0.x;
	int dy = p1.y - p0.y;
	int d = std::max(abs(dx), abs(dy));

	float xInc = dx / (float)d;
	float yInc = dy / (float)d;

	float x = p0.x;
	float y = p0.y;

	for (int i = 0; i <= d; i++)
	{
		framebuffer.DrawCircle((int)(x), (int)(y), radius, color, 0, true, color);
		x += xInc;
		y += yInc;
	}
}

void Application::OnMouseMove(SDL_MouseButtonEvent event)
{
	if (mouse_state == SDL_BUTTON_RIGHT || mouse_state == SDL_BUTTON_X1)
	{
		// Calcular el cambio de posición del ratón
		int delta_x = event.x - last_mouse_x;
		int delta_y = event.y - last_mouse_y;

		// Calcular el vector de desplazamiento en el espacio del mundo
		Vector3 right = camera->GetLocalVector(Vector3::RIGHT);
		Vector3 up = camera->GetLocalVector(Vector3::UP);
		Vector3 delta_position = right * (float)delta_x * 0.001f - up * (float)delta_y * 0.001f;

		// Mover el centro de la cámara basándose en el desplazamiento calculada

		camera->center = camera->center + delta_position;

		camera->UpdateViewMatrix();
	}
	else if (mouse_state == SDL_BUTTON_LEFT)
	{
		int delta_x = event.x - last_mouse_x;
		int delta_y = event.y - last_mouse_y;

		float angle_y = delta_x * 0.08; // Escalamos valores para que no se rote muy rápido
		float angle_x = -delta_y * 0.05;

		// Calcular la nueva posición de 'eye' para la rotación horizontal
		Vector3 center_to_eye = camera->eye - camera->center;
		Vector3 horizontal_axis = Vector3::UP; // Eje de rotación horizontal (arriba global)

		// Rotar 'center_to_eye' alrededor del eje Y global
		Matrix44 rot_y;
		rot_y.SetIdentity();
		rot_y.Rotate(angle_y, horizontal_axis);
		center_to_eye = rot_y.RotateVector(center_to_eye);

		// Calcular eje de rotación vertical (perpendicular a 'center_to_eye' y 'UP')
		Vector3 vertical_axis = center_to_eye.Cross(Vector3::UP).Normalize();

		// Rotar 'center_to_eye' alrededor del nuevo eje vertical
		Matrix44 rot_x;
		rot_x.SetIdentity();
		rot_x.Rotate(angle_x, vertical_axis);
		center_to_eye = rot_x.RotateVector(center_to_eye);

		// Actualizar 'eye' con la nueva posición calculada
		camera->eye = camera->center + center_to_eye;

		camera->UpdateViewMatrix();
	}

	// Actualizar la posición anterior del ratón
	last_mouse_x = event.x;
	last_mouse_y = event.y;
}

void Application::OnWheel(SDL_MouseWheelEvent event)
{
	float dy = event.preciseY;

	// Añadimos limites para que no se pueda hacer infinitamente grande o pequeño, funcionalidad propia, para aumentar o disminuir el grosor del borde, en funcion de la rueda del ratón
	if (dy > 0)
	{
		camera->eye = camera->eye * 0.95;
	}
	else if (dy < 0)
	{
		camera->eye = camera->eye * 1.05;
	}
}

void Application::OnFileChanged(const char *filename)
{
	Shader::ReloadSingleShader(filename);
}
