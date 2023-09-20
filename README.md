# HiLoGame

The first idea for this game implementation came out for it to be used as a simple couch game, where the players can share the keyboard to play it by turns.

The solution was structured with evolvability and scalability in mind. For challenge purposes, the selected user interface is a simple console application and the persistence layer implemented in memory, but the stateless system design and the abstraction provided by the application layer and the repository pattern enables us to easily plug in a different frontend, API, etc. without the need to change the core application and domain models. Same if we want to move into a different data storage. If for instance, we would need to move into a web based solution, this design would enable us to do it with ease.

The structure of this solution is Clean Architecture based, in which on the external layers we have the Presentation and Infrastructure layer, in the middle the Application layer and inside the Domain layer.
For the sake of simplicity, the Presentation uses a simple console application and in the Infrastructure a repository is being used to store data in an in memory generic list. For this repository the Generic Repository Pattern is being used for basic operations (CRUD) to promote code reuse and high scalability, so in the future if the rules change and other entities are created, the same contract of the repository will be used.
The Application layer is being used like an orchestrator between layers and the Domain is responsible for the rules of the game, representing the core of the game!

Taking into account this is a challenge some unit tests were made to cover the most important methods of the game, however it was decided to not make unit tests for the UI (console application).
