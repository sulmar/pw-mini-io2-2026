# Order Management Service

**Order Management Service** to prosta usluga HTTP do obslugi zamowien klientow. System pozwala utworzyc zamowienie, przechowuje jego podstawowe dane oraz utrzymuje reguly biznesowe zwiazane z cyklem zycia zamowienia. Projekt laczy dokumentacje architektoniczna, model domenowy, implementacje aplikacyjna i testy, tak aby cala sciezka od wymagania do kodu byla czytelna i spojna.

Repozytorium pokazuje spojny punkt startowy dla projektu .NET:

- dokumentacja prowadzi od problemu do decyzji projektowych,
- kod zawiera minimalny pionowy wycinek funkcjonalnosci,
- testy sa elementem podstawowego workflow.

## Stos technologiczny

- `.NET 10`
- `ASP.NET Core Minimal API`
- `xUnit`
- repozytorium `in-memory`
- `GitHub Actions`

## Uruchomienie

Uruchamiaj polecenia z katalogu `project-template`:

```bash
dotnet restore ProjectTemplate.sln
dotnet build ProjectTemplate.sln
dotnet test ProjectTemplate.sln
dotnet run --project src/Api/ProjectTemplate.Api.csproj
```


## Struktura

```text
src/
  Api/             minimal API i kompozycja zaleznosci
  Application/     przypadki uzycia i kontrakty
  Domain/          model domenowy i reguly biznesowe
  Infrastructure/  szczegoly techniczne
docs/
  use-cases/       scenariusze funkcjonalne
  architecture/    opis struktury systemu
  domain/          model domenowy
  decisions/       ADR-y
tests/
  UnitTests/       szybkie testy logiki
  IntegrationTests/ testy endpointow i skladania aplikacji
```

## Sugerowany workflow dla zespolu

1. Opisz przypadek uzycia.
2. Ustal model domenowy i ogranicz zakres pierwszej implementacji.
3. Zapisz istotne decyzje w ADR.
4. Dodaj test jednostkowy lub integracyjny.
5. Dopiero potem rozwijaj kod produkcyjny.

## Autorzy

Do uzupelnienia.

## Licencja

Projekt jest udostepniany na licencji `MIT`, ktora pozwala na swobodne uzywanie, modyfikowanie i rozpowszechnianie oprogramowania przy zachowaniu informacji o autorstwie i licencji.
