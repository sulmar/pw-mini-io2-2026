# UC-02 Anulowanie zamowienia

## Aktor

Klient

## Opis

Klient anuluje istniejace zamowienie w systemie.

## Warunki wstepne

- zamowienie istnieje w systemie,
- status zamowienia nie jest rowny `Shipped`.

## Glowny scenariusz

1. Klient wybiera zamowienie.
2. Klient wysyla zadanie anulowania zamowienia.
3. System odczytuje zamowienie i sprawdza jego status.
4. System zmienia status na `Cancelled`.

## Rezultat

Status zamowienia zostaje zmieniony na `Cancelled`.

## API

`DELETE /orders/{id}`

## Uwagi

Ten przypadek uzycia pozostaje jako przyklad kolejnego kroku rozwoju projektu. Model domenowy zawiera juz regule biznesowa potrzebna do takiej funkcji, ale endpoint i przeplyw aplikacyjny nie sa jeszcze zaimplementowane.
