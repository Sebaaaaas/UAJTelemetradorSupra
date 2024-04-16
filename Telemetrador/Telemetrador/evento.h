#pragma once

enum tipoEvento {
	comienzoPartida,
	finPartida
};

class evento
{
	tipoEvento tipo;
	long timestamp;

public:
	
	evento(const tipoEvento tipo_, long timestamp_) : tipo(tipo_), timestamp(timestamp_) {

	}
};

