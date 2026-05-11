# Testy architektury

Projekt wykorzystuje **testy architektury**, które automatycznie sprawdzają zgodność kodu z zasadami **Clean Architecture**.

Testy te pozwalają wykryć sytuacje, w których kod narusza ustaloną strukturę warstw systemu.

---

## 1. Utworzenie projektu testowego


```bash
dotnet new xunit -n ArchitectureTests -o tests/ArchitectureTests
```

Powstanie struktura:
```
tests/
└── ArchitectureTests
    └── ArchitectureTests.cs
```


---

## 2. Dodanie pakietu do testów architektury

```bash
dotnet add package NetArchTest.Rules
```


---    

3. Dodanie referencji do projektu domeny

Test musi znać projekt Domain, więc dodaj referencję:

```bash
dotnet add tests/ArchitectureTests reference src/Domain/Domain.csproj
```

Jeśli masz więcej projektów (Application, Infrastructure), można dodać je również.

---

4. Uruchomienie testów


```bash
dotnet test
```

---


## Cel

Celem testów architektury jest upewnienie się, że:

- warstwa **Domain** nie zależy od innych warstw
- zależności między warstwami są zgodne z zasadami Clean Architecture

Najważniejsza zasada:

API → Application → Domain  
Infrastructure → Application

Natomiast:

Domain ❌ nie zna API  
Domain ❌ nie zna Infrastructure

---

## Przykładowy test

Poniższy test sprawdza, czy warstwa **Domain** nie zależy od innych części systemu.

```csharp
using NetArchTest.Rules;
using Xunit;

public class ArchitectureTests
{
    [Fact]
    public void Domain_Should_Not_Depend_On_Other_Layers()
    {
        var result = Types
            .InAssembly(typeof(Domain.Order).Assembly)
            .Should()
            .NotHaveDependencyOnAny(
                "Api",
                "Application",
                "Infrastructure")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}