# Zadanie — Kalkulator kosztu dostawy (rozgrzewka TDD)

## Wprowadzenie

System logistyczny oblicza koszt dostawy paczki na podstawie wybranego typu transportu oraz wagi przesyłki. Początkowo obsługiwane są tylko dwie metody dostawy **Standard** i **Express**, jednak wraz z rozwojem wymagań pojawia się dodatkowa reguła biznesowa wpływająca na końcową cenę.

Zadanie należy realizować iteracyjnie zgodnie z metodyką TDD, pozwalając aby kolejne wymagania naturalnie prowadziły do potrzeby refaktoryzacji kodu.

---

## Cel
- zaimplementowanie kalkulatora kosztu dostawy,
- rozwijanie rozwiązania krok po kroku poprzez testy,
- zauważenie momentu, w którym struktura kodu zaczyna wymagać uproszczenia,
- przeprowadzenie refaktoryzacji dopiero wtedy, gdy stanie się ona uzasadniona.

## Wymagania funkcjonalne (kolejność iteracji)

**1. Standard**
- System powinien obliczać koszt dostawy **Standard** wg wzoru:
```cs
cost = 10 + weight * 1.5
```
- Waga musi być większa od 0. W przeciwnym razie powinien rzucać ArgumentException z komunikatem `Weight must be greater than zero.`
- Nieznany typ dostawy powinien rzucać ArgumentException.

**2. Express**
- Dodaj metodę dostawy **Express** wg wzoru:
```cs
cost = 20 + weight * 2.5
```

**3. Weekend +50%**
- W weekend cena każdej dostawy rośnie o 50%.
- Reguła weekendowa dotyczy wszystkich metod dostawy.

---

## Zasady pracy
- Rozwijaj rozwiązanie zgodnie z cyklem **Red → Green → Refactor**.
- Zacznij od najprostszej implementacji.
- Nie projektuj docelowej struktury na początku.
- Refaktoryzuj dopiero wtedy, gdy kod zacznie się komplikować.
- Każdą metodę testową zapisuj zgodnie ze strukturą **AAA (Arrange – Act – Assert)**:
    - **Arrange** — przygotuj dane i obiekty potrzebne do testu,
    - **Act** — wykonaj jedną operację, którą testujesz,
    - **Assert** — sprawdź oczekiwany rezultat.

---

## Rozwiązanie

### Iteracja 1 — Standard

#### RED — minimalna implementacja Standard (kod jeszcze nie działa)

Na etapie Red piszemy test i dopisujemy absolutnie minimalny kod tylko po to, aby projekt się kompilował. Logika nie jest jeszcze zaimplementowana — test ma być czerwony.
```cs
using Xunit;
using Shipping.Domain;

namespace Shipping.UnitTests;

public class DeliveryCostCalculatorTests
{
    [Fact]
    public void Calculate_StandardDelivery_ReturnsCorrectCost()
    {
        // Arrange
        var sut = new DeliveryCostCalculator();

        // Act
        var result = sut.Calculate("Standard", weight: 2m);

        // Assert
        Assert.Equal(13m, result); // 10 + 2*1.5
    }
}
```

```cs
namespace Shipping.Domain;

public class DeliveryCostCalculator
{
    public decimal Calculate(string deliveryType, decimal weight)
    {
        throw new NotImplementedException();
    }
}
```

Na tym etapie:
- test się uruchamia,
- metoda istnieje,
- ale implementacja celowo nie działa.

To pozwala przejść świadomie do kroku **Green**, w którym dopiero zaczynamy implementować minimalne zachowanie wymagane przez test.


#### GREEN — minimalna implementacja (hardcoding)

W kroku Green implementujemy absolutnie najprostszy kod, który sprawi, że test przejdzie. Na tym etapie nie dbamy jeszcze o elastyczność ani ostateczną strukturę — celem jest tylko przejście z czerwonego na zielony.

```cs
namespace Shipping.Domain;

public class DeliveryCostCalculator
{
    public decimal Calculate(string deliveryType, decimal weight)
    {
        return 13m;
    }
}
```

#### RED — test ujawniający hardcoding
Test z inną wagą.

```cs
[Fact]
public void Calculate_StandardDelivery_DifferentWeight_ReturnsCorrectCost()
{
    // Arrange
    var sut = new DeliveryCostCalculator();

    // Act
    var result = sut.Calculate("Standard", weight: 4m);

    // Assert
    // 10 + 4 * 1.5 = 16
    Assert.Equal(16m, result);
}
```
Nowy test odkrywa hardcoding i wymusza przejście od „stałej wartości" do prawdziwej logiki obliczeniowej w kolejnym kroku Green.


#### GREEN — implementacja usuwająca hardcoding

```cs
namespace Shipping.Domain;

public class DeliveryCostCalculator
{
    public decimal Calculate(string deliveryType, decimal weight)
    {
        if (deliveryType == "Standard")
        {
            return 10m + weight * 1.5m;
        }

        throw new ArgumentException("Invalid delivery type.");
    }
}
```

#### RED — test na wyjątek wagi

```cs
[Fact]
public void Calculate_WeightIsZero_ThrowsException()
{
    // Arrange
    var sut = new DeliveryCostCalculator();

    // Act
    var ex = Assert.Throws<ArgumentException>(() => sut.Calculate("Standard", 0m));

    // Assert
    Assert.Equal("Weight must be greater than zero.", ex.Message);
}
```


- Implementacja nadal jest minimalna — obsługuje tylko Standard, bo tylko tego wymagają aktualne testy.

#### GREEN — dodajemy walidację

```cs
namespace Shipping.Domain;

public class DeliveryCostCalculator
{
    public decimal Calculate(string deliveryType, decimal weight)
    {
        if (weight <= 0)
            throw new ArgumentException("Weight must be greater than zero.");

        if (deliveryType == "Standard")
            return 10m + weight * 1.5m;

        throw new ArgumentException("Invalid delivery type.");
    }
}
```

### Iteracja 2 — Express

#### RED — test dla Express

```cs
[Fact]
public void Calculate_ExpressDelivery_ReturnsCorrectCost()
{
    // Arrange
    var sut = new DeliveryCostCalculator();

    // Act
    var result = sut.Calculate("Express", weight: 2m);

    // Assert
    // 20 + 2 * 2.5 = 25
    Assert.Equal(25m, result);
}
```

Ten test nie przejdzie, ponieważ aktualna implementacja obsługuje tylko Standard.

#### GREEN — minimalna implementacja

Dodajemy najprostszy możliwy kod, aby test przeszedł. Nadal nie refaktoryzujemy — tylko rozszerzamy istniejące warunki.

```cs
namespace Shipping.Domain;

public class DeliveryCostCalculator
{
    public decimal Calculate(string deliveryType, decimal weight)
    {
        if (weight <= 0)
            throw new ArgumentException("Weight must be greater than zero.");

        if (deliveryType == "Standard")
        {
            return 10m + weight * 1.5m;
        }
        else if (deliveryType == "Express")
        {
            return 20m + weight * 2.5m;
        }

        throw new ArgumentException("Invalid delivery type.");
    }
}
```

### Iteracja 3 — Weekend +50% (intuicyjnie, przed refaktorem)

#### RED — test weekendowy

```cs
[Fact]
public void Calculate_StandardDelivery_OnWeekend_IncreasesCostBy50Percent()
{
    // Arrange
    var sut = new DeliveryCostCalculator();

    // Act
    var result = sut.Calculate("Standard", weight: 2m, isWeekend: true);

    // Assert
    Assert.Equal(19.5m, result); // 13 * 1.5
}
```

#### GREEN — dodajemy parametr i mnożnik

```cs
namespace Shipping.Domain;

public class DeliveryCostCalculator
{
    public decimal Calculate(string deliveryType, decimal weight, bool isWeekend = false)
    {
        if (weight <= 0)
            throw new ArgumentException("Weight must be greater than zero.");

        decimal cost;

        if (deliveryType == "Standard")
            cost = 10m + weight * 1.5m;
        else if (deliveryType == "Express")
            cost = 20m + weight * 2.5m;
        else
            throw new ArgumentException("Invalid delivery type.");

        if (isWeekend)
            cost *= 1.5m;

        return cost;
    }
}
```

W tym momencie metoda zaczyna łamać SRP: coraz więcej odpowiedzialności, dodatkowy parametr itd. To dobry moment na refaktor.

#### Iteracja 4 — REFACTOR (bez zmiany zachowania)

Cel: usunąć rozrastające się if/else i przenieść logikę do osobnych klas, a regułę weekendową dodać jako warstwę.

#### Abstrakcyjna Strategia

```cs
namespace Shipping.Domain;

public interface IDeliveryPricing
{
    decimal Calculate(decimal weight);
}
```

#### Konkretne Strategie

```cs
namespace Shipping.Domain;

public sealed class StandardPricing : IDeliveryPricing
{
    public decimal Calculate(decimal weight) => 10m + weight * 1.5m;
}

public sealed class ExpressPricing : IDeliveryPricing
{
    public decimal Calculate(decimal weight) => 20m + weight * 2.5m;
}
```

#### Warstwa weekendowa (dekorator)

```cs
namespace Shipping.Domain;

public sealed class WeekendPricingDecorator : IDeliveryPricing
{
    private readonly IDeliveryPricing inner;

    public WeekendPricingDecorator(IDeliveryPricing inner)
    {
        this.inner = inner;
    }

    public decimal Calculate(decimal weight)
        => inner.Calculate(weight) * 1.5m;
}
```

#### Kalkulator (nie zna weekendu, dostaje gotowe strategie)

```cs
namespace Shipping.Domain;

public class DeliveryCostCalculator
{
    private readonly Dictionary<string, IDeliveryPricing> pricingByType;

    public DeliveryCostCalculator(Dictionary<string, IDeliveryPricing> pricingByType)
    {
        this.pricingByType = pricingByType;
    }

    public decimal Calculate(string deliveryType, decimal weight)
    {
        if (weight <= 0)
            throw new ArgumentException("Weight must be greater than zero.");

        if (!pricingByType.TryGetValue(deliveryType, out var pricing))
            throw new ArgumentException("Invalid delivery type.");

        return pricing.Calculate(weight);
    }
}
```

#### Testy po refaktoryzacji

**Test — Standard (dzień powszedni)**

```cs
[Fact]
public void Calculate_StandardDelivery_ReturnsCorrectCost()
{
    // Arrange
    var strategies = new Dictionary<string, IDeliveryPricing>
    {
        ["Standard"] = new StandardPricing(),
        ["Express"]  = new ExpressPricing()
    };

    var sut = new DeliveryCostCalculator(strategies);

    // Act
    var result = sut.Calculate("Standard", weight: 2m);

    // Assert
    Assert.Equal(13m, result);
}
```

**Test — Express (dzień powszedni)**
```cs
[Fact]
public void Calculate_ExpressDelivery_ReturnsCorrectCost()
{
    // Arrange
    var strategies = new Dictionary<string, IDeliveryPricing>
    {
        ["Standard"] = new StandardPricing(),
        ["Express"]  = new ExpressPricing()
    };

    var sut = new DeliveryCostCalculator(strategies);

    // Act
    var result = sut.Calculate("Express", weight: 2m);

    // Assert
    Assert.Equal(25m, result);
}
```

**Test — Weekend +50% (Standard)**
```cs
[Fact]
public void Calculate_StandardDelivery_OnWeekend_IncreasesCostBy50Percent()
{
    // Arrange
    var strategies = new Dictionary<string, IDeliveryPricing>
    {
        ["Standard"] = new WeekendPricingDecorator(new StandardPricing()),
        ["Express"]  = new WeekendPricingDecorator(new ExpressPricing())
    };

    var sut = new DeliveryCostCalculator(strategies);

    // Act
    var result = sut.Calculate("Standard", weight: 2m);

    // Assert
    Assert.Equal(19.5m, result);
}
```

**Test — Walidacja wagi**

```cs
[Fact]
public void Calculate_WeightIsZero_ThrowsException()
{
    // Arrange
    var sut = new DeliveryCostCalculator(new Dictionary<string, IDeliveryPricing>
    {
        ["Standard"] = new StandardPricing()
    });

    // Act
    var ex = Assert.Throws<ArgumentException>(() => sut.Calculate("Standard", 0m));

    // Assert
    Assert.Equal("Weight must be greater than zero.", ex.Message);
}
```

**Test — Nieznany typ dostawy**
```cs
[Fact]
public void Calculate_InvalidDeliveryType_ThrowsException()
{
    // Arrange
    var sut = new DeliveryCostCalculator(new Dictionary<string, IDeliveryPricing>
    {
        ["Standard"] = new StandardPricing()
    });

    // Act
    var ex = Assert.Throws<ArgumentException>(() => sut.Calculate("Drone", 1m));

    // Assert
    Assert.Equal("Invalid delivery type.", ex.Message);
}
```

### Iteracja 5 — testowanie strategii i dekoratora w izolacji

Po refaktoryzacji warto uprościć testy i przenieść ciężar weryfikacji logiki do poziomu poszczególnych strategii oraz dekoratora. Zamiast testować złożony obiekt `DeliveryCostCalculator`, testujemy mniejsze, niezależne elementy.

Dzięki temu:
- testy są prostsze i bardziej czytelne,
- łatwiej zrozumieć odpowiedzialność każdej klasy,
- zmiany w kompozycji nie wpływają na testy logiki biznesowej.

**Test — StandardPricing**

```cs
public class StandardPricingTests
{
    [Fact]
    public void Calculate_ReturnsCorrectCost()
    {
        // Arrange
        var sut = new StandardPricing();

        // Act
        var result = sut.Calculate(2m);

        // Assert
        Assert.Equal(13m, result);
    }
}
```

**Test — ExpressPricing**
```cs
public class ExpressPricingTests
{
    [Fact]
    public void Calculate_ReturnsCorrectCost()
    {
        // Arrange
        var sut = new ExpressPricing();

        // Act
        var result = sut.Calculate(2m);

        // Assert
        Assert.Equal(25m, result);
    }
}
```

**Test — WeekendPricingDecorator**
Dekorator testujemy niezależnie, przekazując dowolną strategię bazową.
```cs
public class WeekendPricingDecoratorTests
{
    [Fact]
    public void Calculate_IncreasesCostBy50Percent()
    {
        // Arrange
        IDeliveryPricing basePricing = new StandardPricing();
        var sut = new WeekendPricingDecorator(basePricing);

        // Act
        var result = sut.Calculate(2m);

        // Assert
        Assert.Equal(19.5m, result);
    }
}
```

**Test — Kalkulator (delegacja)**

Po wydzieleniu testów strategii kalkulator testujemy już tylko jako mechanizm delegujący:

```cs
public class DeliveryCostCalculatorTests
{
    [Fact]
    public void Calculate_DelegatesToStrategy()
    {
        // Arrange
        var strategies = new Dictionary<string, IDeliveryPricing>
        {
            ["Standard"] = new StandardPricing()
        };

        var sut = new DeliveryCostCalculator(strategies);

        // Act
        var result = sut.Calculate("Standard", 2m);

        // Assert
        Assert.Equal(13m, result);
    }
}
```

#### Dlaczego to dobre podejście?
- Każda klasa ma własne, proste testy.
- Testy są bardziej stabilne przy kolejnych refaktoryzacjach.
- Złożony obiekt przestaje być „ciężarem" testowym — jego rola sprowadza się do kompozycji i delegacji.

To naturalny etap ewolucji testów w TDD — wraz z rozbiciem odpowiedzialności rozbijamy również testy na mniejsze, bardziej precyzyjne jednostki.
