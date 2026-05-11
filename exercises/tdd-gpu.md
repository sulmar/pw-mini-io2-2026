# TDD — GPU i naliczanie kosztów

## Wprowadzenie

W systemie obliczeniowym karta graficzna GPU może zostać uruchomiona na określony czas. Każda karta posiada swój status, który określa aktualny stan urządzenia:

- **Idle** — karta jest dostępna i nie pracuje
- **IsRunning** — karta została uruchomiona i wykonuje zadania

Użytkownik rozpoczyna pracę operacją Start, a kończy ją operacją Stop. Na podstawie czasu działania należy obliczyć koszt użytkowania według stawki godzinowej.

Dodatkowo koszt użytkowania GPU może być ograniczony maksymalnym limitem, którego nie można przekroczyć.

---

## Cel

Celem jest stworzenie modelu GPU, który:

- pozwala rozpocząć i zakończyć pracę karty
- umożliwia określenie czasu działania
- pozwala obliczyć koszt użytkowania
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

### 1. Uruchomienie GPU

- GPU można uruchomić metodą `Start()`
- Status zmienia się z Idle na IsRunning
- Rozpoczyna się pomiar czasu pracy
- Próba ponownego uruchomienia powoduje wyjątek:
  - `InvalidOperationException`
  - komunikat: `"GPU already running."`

---

### 2. Zatrzymanie GPU

- GPU można zatrzymać metodą `Stop()`
- Status zmienia się z IsRunning na Idle
- Kończy się pomiar czasu pracy
- Próba zatrzymania nieuruchomionej karty powoduje wyjątek:
  - `InvalidOperationException`
  - komunikat: `"GPU is not running"`

---

### 3. Obliczanie kosztu

Koszt liczony jest według wzoru:

```text
cost = hourlyRate * totalHours
```

- Stawka godzinowa musi być większa od 0
- System powinien umożliwiać testowanie czasu (np. 1h, 5h, 10h) bez realnego czekania

---

### 4. Ograniczenie maksymalnego kosztu

- Można ustawić maksymalny limit kosztu
- Jeśli koszt przekracza limit — zwracany jest limit

---

## Wskazówki do pracy

- Zacznij od scenariusza: Start → Stop → koszt
- Dodawaj testy negatywne
- Refaktoryzuj dopiero po przejściu testów
- Jeśli kod się komplikuje — uprość model
- Nie rozbudowuj jednej metody o kolejne if-y

---

## Praca z Git

Każda iteracja TDD powinna kończyć się commitem:

- Red → Green → Refactor = jeden mały commit

### Sugestia pracy na gałęziach

- `feature/start-stop` — uruchamianie i zatrzymywanie
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
