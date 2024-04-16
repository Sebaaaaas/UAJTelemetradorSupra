#include "Telemetrador.h"

//Telemetrador* Telemetrador::instance = nullptr;

Telemetrador* createTelemetrador() {
	return new Telemetrador();
}

int test(int a) {
	return a + 5;
}