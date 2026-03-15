# ADR-001 Wybor technologii warstwy API

## Status

Accepted

---

## Kontekst

System wymaga warstwy API umozliwiajacej komunikacje z klientem poprzez HTTP.

W projekcie wazne sa:

- prostota implementacji
- mala ilosc kodu konfiguracyjnego
- szybkie rozpoczecie pracy nad funkcjonalnosciami

Rozwazano dwa podejscia dostepne w platformie .NET:

- ASP.NET Core MVC (Controllers)
- ASP.NET Core Minimal API

---

## Decyzja

Projekt wykorzystuje **ASP.NET Core Minimal API** jako warstwe API.

Minimal API pozwala definiowac endpointy HTTP w prosty sposob, bez koniecznosci tworzenia kontrolerow i dodatkowej konfiguracji. Dobrze wspiera niewielki system, w ktorym liczy sie szybkie dostarczanie funkcjonalnosci przy zachowaniu czytelnosci.

---

## Alternatywy

### ASP.NET Core MVC

Klasyczne podejscie oparte na kontrolerach.

Plusy:

- dobrze znany wzorzec
- czytelna struktura kontrolerow
- szerokie wsparcie w ekosystemie

Minusy:

- wiecej kodu szablonowego
- wieksza zlozonosc dla malego projektu
- wolniejsze rozpoczecie implementacji

### ASP.NET Core Minimal API

Nowoczesne podejscie wprowadzone w .NET 6.

Plusy:

- bardzo mala ilosc kodu
- szybkie tworzenie endpointow
- dobre dopasowanie do niewielkich projektow i prostych uslug

Minusy:

- mniejsza strukturalnosc w duzych projektach
- przy rozbudowie systemu moze byc potrzebne przejscie do bardziej formalnej struktury

---

## Konsekwencje

Pozytywne:

- prostsza implementacja endpointow
- mniejsza ilosc kodu
- szybsze rozpoczecie pracy nad funkcjonalnosciami

Negatywne:

- w przyszlosci moze byc potrzebna refaktoryzacja przy rozbudowie systemu
- mniejsza strukturalnosc niz w przypadku MVC