#pragma once

#include <queue>
#include <iostream>

class Telemetrador
{
	static Telemetrador* instance;
	int idSesion = -1;
	int contadorEventos;

	std::queue<std::string> colaEventos;

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
};

