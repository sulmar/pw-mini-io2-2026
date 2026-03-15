# UC-01 Utworzenie zamowienia

## Aktor

Klient

## Opis

Klient tworzy nowe zamowienie w systemie.

## Warunki wstepne

- klient przekazuje poprawny `CustomerId`,
- zamowienie zawiera co najmniej jedna pozycje,
- kazda pozycja ma `ProductId`, dodatnia ilosc i dodatnia cene.

## Glowny scenariusz

1. Klient wybiera produkty do zamowienia.
2. Klient wysyla zadanie utworzenia zamowienia.
3. System waliduje dane wejsciowe.
4. System tworzy agregat zamowienia.
5. System zapisuje zamowienie przez repozytorium.
6. System zwraca identyfikator i status nowego zamowienia.

## Rezultat

Zamowienie zostaje zapisane w systemie ze statusem `Created`.

## API

`POST /orders`

Przykladowe pola zadania:

- `customerId`
- `items[].productId`
- `items[].quantity`
- `items[].unitPrice`

## Uwagi

To jest przypadek uzycia zaimplementowany w aktualnej wersji systemu.