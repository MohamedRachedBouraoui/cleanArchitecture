Ce projet se base sur la "Clean architecture" présentée par "Jayson.Taylor".
1- Les Tech utilisées:
  * ASP-CORE 3.1 (API)
  * EF.Core
  * Identity
  * Automapper
  * FluentValidaions
  * MediatR
  * JWT
  * Swagger
  * NUnit   (Unit/Integration & Subcutaneuos test)
  * Respawn (reset DB)
  * FluentAssertions

2- Les Patterns:
  * CQRS
  * UnitOfWork
  * Repository
  * Spécifications
 
3- Cette architecture favorise les avantages suivants:
  * Séparation des préoccupations (Concerns separation)
  * Indépendance du UI
  * Indépendance des SGBD
  * Éviter les CROSSCUTTINGS (Logging/Performance/Authentification/Validation)     
  * Remplacer les outils (Frameworks) utilisés facilement et sans avoir à modifier toutes les parties du codes
  * Application des principes SOLID
  * Faciliter les tests unitaires/intégrations
  * Minimiser les conflits (GIT) éventuels
