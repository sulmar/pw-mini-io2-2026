# Przeglad architektury

Ten dokument uzupelnij, gdy zespol rozumie juz pierwsze przypadki uzycia i podjal podstawowe decyzje o strukturze projektu.

## Styl architektury

Opisz wybrane podejscie, np. prosty podzial warstwowy lub lekka Clean Architecture.

Warstwy:

- API
- Application
- Domain
- Infrastructure

## Diagram zaleznosci

Pokaz, kto od kogo zalezy. W szczegolnosci dopilnuj, aby opis i diagram nie byly ze soba sprzeczne.

Przyklad:

`Client -> Api -> Application -> Domain`

`Api -> Infrastructure`

`Infrastructure -> Application`

## Opis warstw

### API

Odpowiada za komunikacje z klientem i mapowanie zadan na przypadki uzycia.

### Application

Zawiera logike aplikacyjna i przeplywy use case.

Przyklady:

- handlery,
- komendy,
- modele odpowiedzi,
- kontrakty repozytoriow.

### Domain

Zawiera model domenowy i reguly biznesowe.

Przyklady:

- encje,
- value objects,
- enumy,
- metody pilnujace invariantow.

### Infrastructure

Zawiera szczegoly techniczne.

Przyklady:

- baza danych,
- repozytoria,
- integracje zewnetrzne.

## Uwagi

Jesli projekt jest dopiero na poczatku, opisz architekture na tyle, by prowadzila zespol, ale nie dokladaj elementow, ktorych jeszcze realnie nie potrzebujecie.
