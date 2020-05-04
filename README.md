# cleanArchitecture
- Ce projet se base sur la "Clean architecture" présentée par "Jayson.Taylor".
- Tech utilisées:
  * ASP-CORE 3.1 (API)
  * EF.Core
  * Identity
  * Automapper
  * MediatR
  * JWT
  * Swagger
  * NUnit
  
- Patterns:
 * CQRS
 * UnitOfWork
 * Repository
 * Spécifications
 
- Avantages:
  * Cette architecture favorise les avantages suivants:
    * Séparation des préoccupations (Concerns separation)
    * Éviter les CROSSCUTTINGS (Logging/Performance/Authentification/Validation)     
    * Remplacer les outils (Frameworks) utilisés facilement et sans avoir à modifier toutes les parties du codes
    * Application des principes SOLID
    * Faciliter les tests unitaires/intégrations
    * Minimiser les conflits (GIT) éventuels
