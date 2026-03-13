# Checklist — ocena zadania TDD  
**Zadanie:** Planowanie trasy i obliczanie kosztu przejazdu

Maksymalna liczba punktów: **40**

---

# 1. Poprawność funkcjonalna (0–10 pkt)

### Happy path

- [ ] trasa A → B działa
- [ ] odległość pobierana z macierzy
- [ ] koszt liczony jako `distance * rate`
- [ ] limit kosztu działa poprawnie

### Obsługa błędów

- [ ] wyjątek dla trasy do tego samego miasta
- [ ] wyjątek dla nieistniejącego miasta
- [ ] walidacja stawki (`rate > 0`)

**Punkty:** ___ / 10

---

# 2. Zgodność z zasadami SOLID (0–10 pkt)

### SRP — Single Responsibility Principle

- [ ] klasy mają jedną odpowiedzialność
- [ ] logika planowania trasy oddzielona od kalkulacji kosztu

### OCP — Open/Closed Principle

- [ ] rozwiązanie można rozszerzyć bez modyfikacji istniejącego kodu
- [ ] brak wielu instrukcji `if` dla różnych wariantów logiki

### Struktura kodu

- [ ] czytelny podział klas
- [ ] brak „God Class” lub „God Method”

**Punkty:** ___ / 10

---

# 3. Jakość testów (0–10 pkt)

### Pokrycie scenariuszy

- [ ] test happy path
- [ ] test dla tego samego miasta
- [ ] test dla nieistniejącego miasta
- [ ] test dla obliczania kosztu
- [ ] test limitu kosztu

### Czytelność testów

- [ ] dobre nazwy testów
- [ ] struktura AAA (Arrange / Act / Assert)

**Punkty:** ___ / 10

---

# 4. Zastosowanie TDD i historia Git (0–10 pkt)

### TDD

- [ ] testy powstają przed implementacją
- [ ] widoczny cykl **Red → Green → Refactor**

### Git

- [ ] małe commity
- [ ] czytelne komunikaty commitów
- [ ] kolejne funkcjonalności rozwijane iteracyjnie

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