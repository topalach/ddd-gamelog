# GameLog - DDD in practice

The purpose of this project is to practice various tactical DDD patterns.

The keyword here is *overkill*.

The business requirements simplicity does not justify the complexity of the implementation. However, it serves as a good training ground.

## Business requirements

There is only a handful of rules here:

1. The application is a video game gameplay logging service. Users can browse game profiles, rate the games and record the gameplay time.

2. There are three types of users:

- Guest (no account, can only browse game profiles).
- Gamer (account required, can rate games and log gameplay time).
- Librarian (can add and maintain game profiles).

3. A Gamer has a rank that depends on the number of games they have played.

## Architecture

Onion (ports and adapters) architecture layers:

1. GameLog.Domain - business logic.
2. GameLog.Application - domain services + repository interfaces.
3. GameLog.Infrastructure - infrastructure layer (persistence implementation, external services etc.).
4. GameLog.Web - thin web application layer.
