# Zarzadzanie zamowieniami

Ten katalog opisuje niewielki, ale spojny system. Celem nie jest kompletna realizacja wszystkich funkcji, tylko pokazanie uporzadkowanego fundamentu: od przypadku uzycia i modelu domenowego po implementacje i testy.

---

## Cel projektu

Szablon ma pokazac:

- podejscie **Use Case Driven Development**,
- podstawy **modelowania domenowego**,
- prosty podzial na warstwy w .NET,
- dokumentowanie decyzji projektowych,
- laczenie dokumentacji z kodem i testami.

---

## Aktualny zakres

Kod implementuje jeden referencyjny pionowy wycinek:

- **UC-01 Utworzenie zamowienia**

Drugi przypadek uzycia w `docs/use-cases/UC02-cancel-order.md` zostal pozostawiony jako przyklad dalszego rozwoju projektu.

---

## Jak czytac projekt

Najlepiej przejsc po nim w tej kolejnosci:

1. `docs/use-cases/UC01-create-order.md`
2. `docs/domain/domain-model.md`
3. `docs/architecture/overview.md`
4. `docs/decisions/`
5. `src/` i `tests/`

Dzieki temu latwo przejsc od dokumentacji do implementacji.

---

## Architektura

Projekt wykorzystuje prosty podzial na warstwy:

`API -> Application -> Domain`

Warstwa `Infrastructure` implementuje kontrakty potrzebne aplikacji, ale nie zawiera logiki biznesowej.

Szczegoly znajduja sie w:

`docs/architecture/overview.md`

---

## Model domenowy

Glowne elementy przykladu:

- `Order`
- `OrderItem`
- `OrderStatus`

Szczegoly znajduja sie w:

`docs/domain/domain-model.md`

---

## Decyzje architektoniczne

Kluczowe decyzje projektowe sa dokumentowane w katalogu:

`docs/decisions`

Dokumentacja obejmuje tylko decyzje bazowe. Bardziej zaawansowane elementy warto dodawac dopiero wtedy, gdy wynikaja z realnych potrzeb.

---

## Uruchomienie projektu

### Wymagania

- .NET 10 SDK

### Przykladowe polecenia

```bash
dotnet restore ../ProjectTemplate.sln
dotnet build ../ProjectTemplate.sln
dotnet test ../ProjectTemplate.sln
dotnet run --project ../src/Api/ProjectTemplate.Api.csproj
```

---

## Zasady pracy

Przy pierwszych iteracjach warto pilnowac zasady:

- najpierw use case,
- potem model i decyzje,
- potem test,
- na koncu implementacja.

