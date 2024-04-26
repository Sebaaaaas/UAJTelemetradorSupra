########################################
### Import section
########################################
import json
import pandas as pd
import matplotlib.pyplot as plt


if __name__ == '__main__':
    """Main function
    """
    # Specify the path to the JSON file
    file_path = './data/prueba.json'

    # Open the JSON file and load its contents
    with open(file_path, 'r') as file:
        data = json.load(file)
    events = data["datos"]; 

    # # Sort by timestamp

    sorted_events = sorted(events, key=lambda x: x['timestamp'])


    """Let's compute a metric: game session length 
    Approach 1 (Naive): We have all events loaded in memory so we can
    look for specific events and compute the metric
    """
    #lista para guardar 
    player_positions = []
    # Compute the session length for each session (in ms)
    duracionJuego = []
    duracionSesion = []

    for currentEvent in sorted_events:
        if currentEvent['name'] == "Empiece_Partida":
            tsSessionStart = currentEvent['timestamp']
        if currentEvent['name'] == "Salida_Jugador":
            tsSessionEnd = currentEvent['timestamp']
            tsSessionStart = float(tsSessionStart.replace(',', '.'))
            tsSessionEnd = float(tsSessionEnd.replace(',', '.'))
            duracionSesion.append(tsSessionEnd-tsSessionStart)
            
        if currentEvent['name'] == "Empiece_Nivel":
            tsGameStart = currentEvent['timestamp']
        if currentEvent['name'] == "Fin":
            tsGameEnd = currentEvent['timestamp']
            tsGameStart = float(tsGameStart.replace(',', '.'))
            tsGameEnd = float(tsGameEnd.replace(',', '.'))
            duracionJuego.append(tsGameEnd-tsGameStart)
            gameWon = currentEvent['win']
        if currentEvent['name'] == "Movimiento":
            # Guardar la posición del jugador en la lista
            aux = {"x": float(currentEvent['playerX'].replace(',', '.'))+ 100, "y": float(currentEvent['playerY'].replace(',', '.'))+ 100}
            player_positions.append(aux)

    print("duracion partida: ")
    print(duracionJuego)
    print("duracion sesion: ")
    print(duracionSesion)
    
    # Convertir la lista de posiciones del jugador a un DataFrame de pandas
    dfPlayerPositions = pd.DataFrame(player_positions)

    # Guardar las posiciones del jugador en un archivo CSV (opcional)
    # dfPlayerPositions.to_csv('player_positions.csv', index=False)

    fig, ax = plt.subplots(figsize=(27, 8)) 

    # Dibujar el mapa en el fondo
    img = plt.imread("bg.png")
    ax.imshow(img, extent=[0, 1107, 0, 472])  

    # Invertir los datos del eje y antes de graficar el heatmap
    dfPlayerPositions['x'] = 1107 - dfPlayerPositions['x']
    dfPlayerPositions['y'] = 472 - dfPlayerPositions['y']

    # Dibujar un mapa de calor transparente utilizando pandas.plot.hexbin
    heatmap  = dfPlayerPositions.plot.hexbin(
        fig=fig,
        ax=ax,
        x="x",
        y="y",
        reduce_C_function=sum,
        gridsize=(50,25),  # Ajustar gridsize al tamaño del mapa
        extent=[0, 1107, 0, 472],  # Ajustar extent al tamaño del mapa
        alpha=0.5,
        cmap='Reds'
    )

    ax.set_xticks(range(0, 1107, 100))
    ax.set_yticks(range(0, 472, 100))
    fig.savefig('player_positions_heatmap.png')

    # # Aggregate game session lenghts and compute some stats using pandas.describe
    # s = pd.Series(duracionJuego)
    # print(s.describe())

