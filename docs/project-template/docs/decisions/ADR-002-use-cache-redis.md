# ADR-002 Wykorzystanie Redis jako warstwy cache

## Status

Proposed

---

## Kontekst

W miare rozwoju projektu odczyt danych moze stawac sie coraz czestszy, a glowna baza danych moze zaczac obslugiwac zbyt duzo zapytan.

Taka sytuacja moze dotyczyc miedzy innymi:

- list zamowien,
- szczegolow zamowienia,
- danych tymczasowych wykorzystywanych przez aplikacje.

Na obecnym etapie nie ma jeszcze potrzeby wprowadzania dodatkowej infrastruktury, ale taka decyzja moze pojawic sie w dalszym rozwoju systemu.

Rozwazano dwa podejscia:

- brak warstwy cache,
- wykorzystanie Redis jako warstwy cache.

---

## Decyzja

Na obecnym etapie **nie wdrazamy Redis**, ale traktujemy go jako sensowna **propozycje** dla bardziej rozbudowanej wersji systemu.

Jesli w kolejnych iteracjach pojawia sie problemy wydajnosciowe lub potrzeba przechowywania danych tymczasowych, Redis moze zostac zaakceptowany jako osobna warstwa cache.

---

## Alternatywy

### Brak cache

System korzysta wylacznie z glownej bazy danych.

Plusy:

- prostsza architektura,
- brak dodatkowej infrastruktury,
- latwiejsze utrzymanie rozwiazania.

Minusy:

- wieksze obciazenie bazy danych,
- wolniejsze odczyty przy duzej liczbie zapytan.

### Redis

Dodatkowa warstwa cache oparta o Redis.

Plusy:

- bardzo szybki dostep do danych,
- zmniejszenie obciazenia bazy danych,
- dobry przyklad rozwiazania spotykanego w wiekszych systemach.

Minusy:

- dodatkowa infrastruktura do utrzymania,
- wieksza zlozonosc architektury,
- koniecznosc przemyslenia strategii wygaszania i synchronizacji danych.

---

## Konsekwencje

Pozytywne:

- obecna architektura pozostaje prosta,
- decyzja jest udokumentowana z wyprzedzeniem,
- zespol ma gotowy punkt wyjscia do rozmowy o skalowaniu systemu.

Negatywne:

- obecna wersja systemu nie pokazuje implementacji cache,
- moze byc potrzebny osobny ADR lub aktualizacja tego dokumentu po rzeczywistym wdrozeniu Redis.
