 # Inżynieria Oprogramowania 2 (IO2)

## Wprowadzenie

Witaj w repozytorium z materiałami do przedmiotu **Inżynieria Oprogramowania 2** prowadzonym na Wydział Matematyki i Nauk Informacyjnych Politechniki Warszawskiej.

Do rozpoczęcia tego kursu potrzebujesz następujących rzeczy:

1. [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0).
2. Sklonuj repozytorium Git
```
git clone https://github.com/sulmar/pw-mini-io2-2026
```

## Zawartość repozytorium

### docs

Materiały do nauki i referencje:

- **git.md** — kontrola wersji w Git: wprowadzenie, model pracy (commity, branchy, merge), dobre praktyki oraz powiązanie z TDD.
- **tdd.md** — automatyzacja testów i TDD: wprowadzenie, podział testów (jednostkowe, integracyjne itd.), praktyki pracy z testami.

### src

Kod przykładowy i ćwiczeniowy:

- **Calculator** — przykładowy projekt .NET (solution z biblioteką i testami jednostkowymi):
  - `CalculatorLibrary` — biblioteka z klasą `Calculator` (np. metoda `Add`) i `NumberValidator`;
  - `CalculatorLibrary.UnitTests` — testy jednostkowe (xUnit) do biblioteki kalkulatora.
