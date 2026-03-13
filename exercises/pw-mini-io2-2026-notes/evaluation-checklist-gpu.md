# Checklist — ocena zadania TDD  
**Zadanie:** GPU i naliczanie kosztów

Maksymalna liczba punktów: **40**

---

# 1. Poprawność funkcjonalna (0–10 pkt)

### Happy path

- [ ] metoda `Start()` uruchamia GPU
- [ ] status zmienia się z `Idle` na `IsRunning`
- [ ] metoda `Stop()` zatrzymuje GPU
- [ ] status zmienia się z `IsRunning` na `Idle`
- [ ] czas działania GPU jest poprawnie wyznaczany
- [ ] koszt liczony jest jako `hourlyRate * totalHours`
- [ ] limit maksymalnego kosztu działa poprawnie

### Obsługa błędów

- [ ] uruchomienie już działającej karty kończy się `InvalidOperationException`
- [ ] komunikat wyjątku dla ponownego uruchomienia to `"GPU already running."`
- [ ] zatrzymanie nieuruchomionej karty kończy się `InvalidOperationException`
- [ ] komunikat wyjątku dla zatrzymania nieuruchomionej karty to `"GPU is not running"`
- [ ] walidacja stawki godzinowej (`hourlyRate > 0`)

**Punkty:** ___ / 10

---

# 2. Zgodność z zasadami SOLID (0–10 pkt)

### SRP — Single Responsibility Principle

- [ ] logika zmiany statusu GPU jest oddzielona od logiki naliczania kosztu
- [ ] odpowiedzialność za pomiar czasu nie jest niepotrzebnie mieszana z inną logiką
- [ ] klasy mają jedną, czytelną odpowiedzialność

### OCP — Open/Closed Principle

- [ ] rozwiązanie można rozszerzyć bez modyfikowania istniejącego kodu
- [ ] ograniczenie maksymalnego kosztu nie jest dopisane jako kolejny trudny do utrzymania `if`
- [ ] struktura pozwala dodać kolejne reguły kosztowe bez psucia istniejących klas

### Struktura kodu

- [ ] brak „God Class” lub „God Method”
- [ ] kod ma czytelny podział odpowiedzialności
- [ ] rozwiązanie jest proste i łatwe do rozwijania

**Punkty:** ___ / 10

---

# 3. Jakość testów (0–10 pkt)

### Pokrycie scenariuszy

- [ ] test uruchomienia GPU (`Start()`)
- [ ] test zatrzymania GPU (`Stop()`)
- [ ] test zmiany statusu po uruchomieniu
- [ ] test zmiany statusu po zatrzymaniu
- [ ] test obliczania kosztu
- [ ] test ograniczenia maksymalnego kosztu
- [ ] test uruchomienia już działającej karty
- [ ] test zatrzymania nieuruchomionej karty
- [ ] test walidacji niepoprawnej stawki godzinowej
- [ ] testy pozwalają symulować 1h, 5h lub 10h bez rzeczywistego czekania

### Czytelność testów

- [ ] dobre nazwy testów
- [ ] testy stosują strukturę AAA (`Arrange / Act / Assert`)
- [ ] testy są małe, czytelne i skupione na jednym zachowaniu

**Punkty:** ___ / 10

---

# 4. Zastosowanie TDD i historia Git (0–10 pkt)

### TDD

- [ ] testy powstają przed implementacją
- [ ] widoczny cykl **Red → Green → Refactor**
- [ ] rozwój rozwiązania odbywa się małymi krokami
- [ ] po przejściu testów pojawia się refaktoryzacja

### Git

- [ ] małe commity
- [ ] czytelne komunikaty commitów
- [ ] nowe funkcjonalności i refaktoryzacja nie są mieszane w jednym kroku
- [ ] historia Git pokazuje kolejne iteracje rozwiązania

**Punkty:** ___ / 10

---

# Podsumowanie

| Kryterium | Punkty |
|----------|-------|
| Poprawność funkcjonalna | ___ / 10 |
| SOLID | ___ / 10 |
| Jakość testów | ___ / 10 |
| TDD + Git | ___ / 10 |

**Suma:** ___ / 40

---

# Uwagi prowadzącego