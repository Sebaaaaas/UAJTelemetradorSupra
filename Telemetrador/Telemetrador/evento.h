#pragma once

enum tipoEvento {
	comienzoPartida,
	finPartida,
	posicionJugador
};

class evento //debemos poder adaptar los eventos para recoger cosas distintas(posicion, p.ej., solo para el eventos posicionJugador, no para comienzoPartida)
{
	tipoEvento tipo;
	long timestamp;

public:
	
	evento(const tipoEvento tipo_, long timestamp_) : tipo(tipo_), timestamp(timestamp_) {

	}
};

