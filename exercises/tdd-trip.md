# TDD — Plan trasy i obliczanie kosztu przejazdu

## Wprowadzenie

W systemie planowania podróży użytkownik może wyznaczyć trasę pomiędzy miastem początkowym a miastem docelowym. Długości tras pomiędzy miastami zapisane są w postaci macierzy odległości.

Na podstawie wybranej trasy należy obliczyć całkowitą długość przejazdu oraz jego koszt według określonej stawki.

System powinien reagować na niepoprawne dane wejściowe, np.:

- próba wyznaczenia trasy do tego samego miasta
- wskazanie miasta, które nie istnieje w danych

---

## Cel

Celem jest stworzenie modelu planowania trasy, który:

- pozwala wyznaczyć trasę z miasta A do miasta B
- umożliwia obliczenie długości trasy na podstawie macierzy
- pozwala obliczyć koszt przejazdu według stawki za kilometr
- uwzględnia możliwość ograniczenia maksymalnego kosztu
- pozostaje łatwy do testowania i rozwijania

---

## Zasady pracy (TDD)

Rozwijaj rozwiązanie zgodnie z techniką TDD:

- **Red** — napisz test, który nie przechodzi
- **Green** — napisz minimalny kod, aby test przeszedł
- **Refactor** — popraw strukturę kodu bez zmiany zachowania

Dodatkowo:

- Nie projektuj finalnej architektury z góry
- Pozwól, aby wymagania prowadziły do zmian w strukturze
- Rób małe commity po każdej iteracji

---

## Wymagania funkcjonalne (kolejność iteracji)

### 1. Wyznaczenie trasy

- System przyjmuje miasto początkowe i docelowe
- Długości tras zapisane są w macierzy odległości
- Jeśli początek i koniec to to samo miasto → wyjątek:
  - `ArgumentException`

---

### 2. Obliczanie długości trasy

- Długość trasy pobierana jest z macierzy odległości
- Jeśli miasto nie istnieje → wyjątek:
  - `ArgumentException`

---

### 3. Obliczanie kosztu przejazdu

Koszt liczony jest według wzoru:

```text
cost = distance * ratePerKm
```

- Stawka musi być większa od 0
- System powinien umożliwiać testowanie scenariuszy (np. 100km, 500km, 1000km)

---

### 4. Ograniczenie maksymalnego kosztu

- Można ustawić maksymalny limit kosztu
- Jeśli koszt przekracza limit — zwracany jest limit

---

## Wskazówki do pracy

- Zacznij od scenariusza: A → B → długość → koszt
- Dodawaj testy negatywne
- Refaktoryzuj dopiero po przejściu testów
- Jeśli kod się komplikuje — uprość model
- Nie rozbudowuj jednej metody o kolejne if-y

---

## Praca z Git

Każda iteracja TDD powinna kończyć się commitem:

- Red → Green → Refactor = jeden mały commit

### Sugestia pracy na gałęziach

- `feature/route-selection` — wyznaczanie trasy
- `feature/distance-calculation` — obliczanie długości
- `feature/cost-calculation` — obliczanie kosztu
- `refactor/cost-calculation` — refaktoryzacja
- `feature/max-cost-limit` — limit kosztu
- `refactor/limit-logic` — uproszczenie logiki

---

## Wskazówki końcowe

- Commituj małe zmiany
- Merge tylko gdy testy przechodzą
- Nie mieszaj feature + refactor w jednym branchu
- Traktuj historię Git jako dokumentację procesu myślowego
