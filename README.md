```
Dependencies:
├── com.unity.nuget.newtonsoft-json
└── Unity 2022.3.22f1
    └── BIRP
--------
│Program Flow:
└── class Brain : MonoBehaviour
    ├── Start()
    │   ├── │ Initilisation of globals
    │   └── │ Enter initial GameplayState
    ├── Update()
    │   ├── │ Poll user inputs
    │   └── │ Update shader properties
    └── FixedUpdate()
        ├── │ Tick GameplayState logic
        └── │ Execute action queue
--------
│Mini-games:
└── class _____ : GameplayState
    ├── OnEnter(Brain b)
    │   ├── │ Store ref to Brain
    │   ├── │ Initilisation of game state
    │   └── │ Load assets to AssetLibrary
    ├── OnUpdate()
    │   ├── │ Execute minigame logic
    │   └── │ ProcessAIs()
    └── OnLeave()
        ├── │ Unload assets
        └── │ Destroy minigame-specific AIs
--------
│AIs:
└── class AIController : MonoBehaviour
    ├── ChangeState(AIState)
    │   ├── │ OnLeave() current state
    │   └── │ OnEnter() new state
    └── class AIState
        ├── │ OnEnter(AIController)
        │   ├── │ Store ref to Brain
        │   └── │ Initilisation of AI state
        ├── OnUpdate()
        │   └── │ Update AI logic
        └── OnLeave()
            └── │ Destroy AI sub-objects
```
