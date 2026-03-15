# Model domenowy

Ten dokument warto uzupelnic po opisaniu pierwszych przypadkow uzycia. Ma pomagac uchwycic obiekty domenowe i reguly biznesowe, a nie byc tylko lista klas.

## Opis domeny

Krotki opis problemu biznesowego, ktory rozwiazuje system.

Przyklad:

System sluzy do zarzadzania zamowieniami klientow w sklepie internetowym.

---

## Elementy modelu

### EntityName

Opis odpowiedzialnosci elementu domenowego.

Glowne atrybuty:

- `Id`
- `Name`
- `Status`

Najwazniejsze reguly biznesowe:

- regula 1
- regula 2

---

### AnotherEntity

Opis odpowiedzialnosci kolejnego elementu.

Glowne atrybuty:

- `Id`
- `OrderId`
- `ProductId`
- `Quantity`

---

## Relacje

Opisz zaleznosci pomiedzy encjami, value objects i enumami.

Przyklad:

- `Order` zawiera wiele `OrderItem`.
- `OrderItem` nalezy do jednego `Order`.

## Uwagi

Jesli model jest jeszcze niepelny, warto to wprost zaznaczyc. Dokument ma odzwierciedlac aktualne rozumienie domeny, a nie udawac kompletne rozwiazanie.