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
    file_path = './data/telemetry.json'

    # Open the JSON file and load its contents
    with open(file_path, 'r') as file:
        data = json.load(file)

    # Sort by timestamp
    sorted_data = sorted(data, key=lambda x: x['timestamp'])


    """Let's compute a metric: game session length 
    Approach 1 (Naive): We have all events loaded in memory so we can
    look for specific events and compute the metric
    """
    #lista para guardar 
    player_positions = []
    # Compute the session length for each session (in ms)
    gameSessionLengthMs = []
    for currentEvent in sorted_data:
        if currentEvent['eventType'] == "startGame":
            tsSessionStart = currentEvent['timestamp']
        if currentEvent['eventType'] == "endGame":
            tsSessionEnd = currentEvent['timestamp']
            gameSessionLengthMs.append(tsSessionEnd-tsSessionStart)
        if currentEvent['eventType'] == "playerPosition":
            # Guardar la posición del jugador en la lista
            player_positions.append({"x": currentEvent['x'], "y": currentEvent['y']})

    #----------------------------------------------------------------------CHATGPT-----------------
    # # Crear una lista para almacenar todas las posiciones del jugador

    for currentEvent in sorted_data:
        # Verificar si el evento es el inicio de un juego
        if currentEvent['eventType'] == "startGame":
            tsSessionStart = currentEvent['timestamp']
        # Verificar si el evento es el final de un juego
        elif currentEvent['eventType'] == "endGame":
            tsSessionEnd = currentEvent['timestamp']
            gameSessionLengthMs.append(tsSessionEnd - tsSessionStart)
        # Verificar si el evento es una posición del jugador
        el

    # Convertir la lista de posiciones del jugador a un DataFrame de pandas
    dfPlayerPositions = pd.DataFrame(player_positions)

    # Guardar las posiciones del jugador en un archivo CSV (opcional)
    dfPlayerPositions.to_csv('player_positions.csv', index=False)

    # Escena dimensiones: 27x22 tiles = 864x705px
    fig, ax = plt.subplots(figsize=(27, 22))  # en pulgadas

    # Dibujar el mapa en el fondo
    img = plt.imread("bg.png")
    ax.imshow(img)

    # Dibujar un mapa de calor transparente utilizando pandas.plot.hexbin
    dfPlayerPositions.plot.hexbin(
        fig=fig,
        ax=ax,
        x="x",
        y="y",
        reduce_C_function=sum,
        gridsize=(27,8),
        extent=[0, 1107, 0, 472],
        alpha=0.5,
        cmap='Reds'
    )
    ax.set_xticks(range(0, 1107, 100))
    ax.set_yticks(range(0, 472, 100))
    fig.savefig('player_positions_heatmap.png')






#     dfmovements = pd.DataFrame(movements)

#     # Scene dimensions: 27x8 tiles = 1107, 472
#     fig,ax = plt.subplots(figsize=(27,8)) #inches

#     # We draw the map in the background
#     img = plt.imread("bg.png")
#     ax.imshow(img)
#     # Then, we draw a transparent heatmap using pandas.plot.hexbin
#     dfmovements.plot.hexbin(fig = fig, ax = ax,x="x", y="y", reduce_C_function=sum, gridsize=(27,8), extent=[0,1107,0,472], alpha=0.5, cmap='Reds')
#     ax.set_xticks(range(0, 1107, 100)) #-100 10
#     ax.set_yticks(range(0, 472, 100)) # 0 40
#     fig.savefig('heatmap.png')
# #--------------------------------


#------------------------------------------------------------------------------------------------

    # Aggregate game session lenghts and compute some stats using pandas.describe
    s = pd.Series(gameSessionLengthMs)
    print(s.describe())
    





    """Approach 2 (Advances): Events are streamed so they are procesed sequentially.
    Every event is processed depending on the current context (defined by the events that
    we have already processed). Contexts are stacked so the event is processed using the top
    context. Events can trigger a pop operation on a stack. 

    In this case, we have a context sessions and levels, and a root context that stores the 
    games (in this example, we don't have sessions). Each context stores metrics and/or other contexts 
    """

#     contextStack = []
#     currentEventIx = 0
#     totalEvents = len(sorted_data)
#     contextStack.append(RootContextData(contextStack))

#     while currentEventIx<totalEvents:
#         currentEvent = sorted_data[currentEventIx]
#         if len(contextStack) == 0:
#             # If the context stack is empty, stop processing
#             break
#         else:
#             # Use the context at the top of the stack
#             currentContext = contextStack[-1]
#             # Parse event using the current context
#             consumeEvent = currentContext.parseEvent(currentEvent)
#             # Some contexts update the stack and leave the event for the next/previous context
#             # If the context does not consume the event, we do not increment the index
#             if consumeEvent:
#                 currentEventIx+=1

#     # In this case, context stack contains all game contexts created
#     # after parsing all the events
#     gameSessionLengthMs = [ game.gameSessionLengthMs for game in contextStack[-1].games ]
#     s = pd.Series(gameSessionLengthMs)
#     # Aggregate game session lenghts and compute some stats using pandas.describe
#     print(s.describe())
 
    
#     dfmovements = pd.DataFrame(movements)

#     # Scene dimensions: 27x8 tiles = 1107, 472
#     fig,ax = plt.subplots(figsize=(27,8)) #inches

#     # We draw the map in the background
#     img = plt.imread("bg.png")
#     ax.imshow(img)
#     # Then, we draw a transparent heatmap using pandas.plot.hexbin
#     dfmovements.plot.hexbin(fig = fig, ax = ax,x="x", y="y", reduce_C_function=sum, gridsize=(27,8), extent=[0,1107,0,472], alpha=0.5, cmap='Reds')
#     ax.set_xticks(range(0, 1107, 100)) #-100 10
#     ax.set_yticks(range(0, 472, 100)) # 0 40
#     fig.savefig('heatmap.png')
# #------------------------------------------------------------------------------------
#     # Scene dimensions: 27x22 tiles = 864x705px
#     fig,ax = plt.subplots(figsize=(27,22)) #inches

#     # We draw the map in the background
#     img = plt.imread("bg.png")
#     ax.imshow(img)
#     # Then, we draw a transparent heatmap using pandas.plot.hexbin
#     dfDeaths.plot.hexbin(fig = fig, ax = ax,x="x", y="y", reduce_C_function=sum, gridsize=(27,22), extent=[0,865,0,705], alpha=0.5, cmap='Reds')
#     ax.set_xticks(range(0, 865, 100))
#     ax.set_yticks(range(0, 705, 100))
#     fig.savefig('heatmap.png')