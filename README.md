# bettor [![Build Status](https://github.com/layereight/bettor/actions/workflows/main.yml/badge.svg)](https://github.com/layereight/bettor/actions)

Learn some C#/.NET with a Web API example.

The project implements gambling.

## Rules

* users bet on a random number between 0 and 9
* users start with an account balance of 10000
* users bet a stake within their account balance on the random number
* when users bet on the right number they earn 9 times their stake

## Implementation

* there are in-memory repositories for users and bets
* the user repository holds 2 users: Bob (id=1) and Alice (id=2)

## Endpoints/Resources

### Bets

* GET  **/bets** - collection of made bets
* POST **/bets** - create (place) a bet
* GET  **/bets/{id}** - get bet with a specific id
* GET  **/bets/{id}/result** - bet result sub resource returning the outcome of a bet

### User

* GET **/users** - collection of users
* GET **/users/{id}** - get user with a specific id
* GET **/users/{id}/account** - account sub resource for a user

## Swagger

When running the project you can open **Swagger UI** under `/swagger`.

## Run unit and integration tests

```
dotnet test
```

## Run project

```
dotnet run --project bettor
```
