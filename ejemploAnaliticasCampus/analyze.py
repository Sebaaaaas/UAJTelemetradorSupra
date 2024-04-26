class ContextData:
    """Abstract class with the main structure of a context data class
    A context data has:
    - A reference to the context stack to push new states
    or pop itself
    - A reference to the parent context, the context that created this.
    It is useful to notify the parent that the child context is removed
    from the context stack

    ContextData contains two important methods:
    - parseEvent: it decides what to do with the current event. This event can
    be consumed (or not)
    - onPopChildContext: what to do when a child context has been removed
    from the context stack
    """

    def __init__(self, contextStack, parentContext = None) -> None:
        """Constructor
        A parent context can be None
        """
        self.contextStack = contextStack
        self.parentContext = parentContext

    def parseEvent(self, event) -> bool:
        """Parses the event.
        Return True if the event must be consumed
        """
        pass

    def popContext(self) -> None:
        """It is the best way to remove itself from the context stack
        because this method notifies the paren context (if exists) that
        the top context has been removed
        """
        childContext = self.contextStack.pop()
        if self.parentContext is not None:
            self.parentContext.onPopChildContext(childContext)        
    
    def onPopChildContext(self,childContextData) -> None:
        """What to do when a child context has been removed from the stack
        
        Keyword arguments:
        childContextData -- The context removed from the stack 
        """
        pass


class RootContextData(ContextData):
    """This is the first context data in the context stack
    It creates game contexts and stores them 
    """

    def __init__(self, contextStack, parentContext=None) -> None:
        """Constructor
        """
        super().__init__(contextStack, parentContext)
        self.games = []
    
    def parseEvent(self, event) -> bool:
        """Creates a new Session context on GAME:START event
        """
        if event['eventType'] == "GAME:START":
           self.contextStack.append(GameContextData(self.contextStack, self))
           # The event is not consumed because it will be used by the game context
           return False
        return True

    def onPopChildContext(self,childContextData) -> None:
        """Stores the session data when removed from the stack
        """
        self.games.append(childContextData)


class GameContextData(ContextData):
    """Represents a game session (when the user starts a new game until wins or fails)
    It stores when a game starts and finishes, its length and the information 
    extracted from the levels played during the game session
    """
    
    def __init__(self, contextStack, parentContext=None) -> None:
        """Constructor
        """
        super().__init__(contextStack, parentContext)
        self.tsGameStart = None
        self.tsGameEnd = None
        self.gameSessionLengthMs = 0
        self.levels = []

    def parseEvent(self, event) -> bool:
        """It stores information on GAME:START and GAME:END events.
        It also creates a LevelContext when a level starts
        """
        if event['eventType'] == "LEVEL:START":
           # Create a level context and don't consume the event
           self.contextStack.append(LevelContextData(self.contextStack, self))
           return False
        elif event['eventType'] == "GAME:START":
           self.tsGameStart = event['timestamp']
        elif (event['eventType'] == "GAME:END"):
            self.tsGameEnd = event['timestamp'] 
            self.gameSessionLengthMs = self.tsGameEnd - self.tsGameStart
            self.popContext()
        return True
    
    def onPopChildContext(self,childContextData) -> None:
        """Stores a level context when it is removed from the stack
        """
        self.levels.append(childContextData)


class LevelContextData(ContextData):
    """It stores information about level events:
    - When the level starts and ends and its lenght
    - The event result
    - Player deaths during this level
    """
    def __init__(self, contextStack, parentContext=None) -> None:
        """Constructor
        """
        super().__init__(contextStack, parentContext)
        self.id = None
        self.tsLevelStart = None
        self.tsLevelEnd = None
        self.levelLengthMs = 0
        self.levelResult = None    
        self.deaths = []    

    def parseEvent(self, event) -> bool:
        """It stores data on LEVEL:START and LEVEL:END
        Additionally, it stores death positions in PLAYER:DEATH events
        """
        if event['eventType'] == "LEVEL:START":
            self.id = event["levelId"]
            self.tsLevelStart = event['timestamp']       
        elif (event['eventType'] == "LEVEL:END") and (self.id == event["levelId"]):
            self.tsLevelEnd = event['timestamp'] 
            self.levelLengthMs = self.tsLevelEnd - self.tsLevelStart
            self.levelResult = event["result"]
            self.popContext()
        elif (event['eventType'] == "PLAYER:DEATH"):
            self.deaths.append( dict( x=event['x'], y=event['y']))
    
        return True


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

    # Compute the session length for each session (in ms)
    gameSessionLengthMs = []
    for currentEvent in sorted_data:
        if currentEvent['eventType'] == "GAME:START":
            tsSessionStart = currentEvent['timestamp']
        if currentEvent['eventType'] == "GAME:END":
            tsSessionEnd = currentEvent['timestamp']
            gameSessionLengthMs.append(tsSessionEnd-tsSessionStart)

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

    contextStack = []
    currentEventIx = 0
    totalEvents = len(sorted_data)
    contextStack.append(RootContextData(contextStack))

    while currentEventIx<totalEvents:
        currentEvent = sorted_data[currentEventIx]
        if len(contextStack) == 0:
            # If the context stack is empty, stop processing
            break
        else:
            # Use the context at the top of the stack
            currentContext = contextStack[-1]
            # Parse event using the current context
            consumeEvent = currentContext.parseEvent(currentEvent)
            # Some contexts update the stack and leave the event for the next/previous context
            # If the context does not consume the event, we do not increment the index
            if consumeEvent:
                currentEventIx+=1

    # In this case, context stack contains all game contexts created
    # after parsing all the events
    gameSessionLengthMs = [ game.gameSessionLengthMs for game in contextStack[-1].games ]
    s = pd.Series(gameSessionLengthMs)
    # Aggregate game session lenghts and compute some stats using pandas.describe
    print(s.describe())

    # Store all deaths in a list. 
    deaths = []
    for game in contextStack[-1].games:
        for level in game.levels:
            deaths.extend(level.deaths)    
 
    
    dfDeaths = pd.DataFrame(deaths)

    # Scene dimensions: 27x22 tiles = 864x705px
    fig,ax = plt.subplots(figsize=(27,22)) #inches

    # We draw the map in the background
    img = plt.imread("bg.png")
    ax.imshow(img)
    # Then, we draw a transparent heatmap using pandas.plot.hexbin
    dfDeaths.plot.hexbin(fig = fig, ax = ax,x="x", y="y", reduce_C_function=sum, gridsize=(27,22), extent=[0,865,0,705], alpha=0.5, cmap='Reds')
    ax.set_xticks(range(0, 865, 100))
    ax.set_yticks(range(0, 705, 100))
    fig.savefig('heatmap.png')