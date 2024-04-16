#pragma once

#include <queue>
#include <iostream>

#include "evento.h"

class Telemetrador
{
	static Telemetrador* instance;
	int idSesion = -1;
	int contadorEventos = 0;

	float frecuenciaRecogidaEventos = 0.1f; //cada cuanto tiempo, medido en segundos, recogemos eventos tipo posicion jugador

	std::queue<evento> colaEventos; //igual no es necesario y sólo debería estar en el juego?
	int tamMaxCola = 100; //cantidad de eventos guardados en cola antes de guardarlos

public:

	static Telemetrador* GetInstance() {
		if (instance == nullptr) {
			instance = new Telemetrador();
		}

		return instance;
	}

	Telemetrador() {

	}

	~Telemetrador() {}

	void recibeEvento(const std::queue<evento>& ev) {


		//recibimos de uniry de alguna forma...
		contadorEventos++;

		//guardamos en disco o enviamos a servidor
		if (contadorEventos >= tamMaxCola) {
			contadorEventos = 0;
		}
	}
};

