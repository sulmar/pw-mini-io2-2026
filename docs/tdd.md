
# Automatyzacja testów

## Wprowadzenie

W wielu zespołach proces weryfikacji działania aplikacji zaczyna się od testowania ręcznego. Programista uruchamia aplikację, klika kolejne scenariusze i sprawdza, czy wszystko działa zgodnie z oczekiwaniami. Na początku projektu może to wydawać się wystarczające, jednak wraz ze wzrostem złożoności systemu testowanie manualne staje się coraz bardziej czasochłonne i trudne do utrzymania.

W praktyce często kończy się to chodzeniem na skróty — nie wszystkie scenariusze są sprawdzane, regresja bywa pomijana, a testy wykonywane są tylko „na szybko”, tuż przed wdrożeniem. Presja czasu powoduje, że część przypadków brzegowych zostaje pominięta, co prowadzi do błędów trafiających na produkcję. Konsekwencją są nie tylko dodatkowe poprawki i stres w zespole, ale także frustracja końcowych użytkowników, którzy jako pierwsi odkrywają problemy.


Programiści, obawiają się zmian w kodzie wynikających z nowych wymagań biznesowych. Każda modyfikacja wydaje się ryzykowna, dlatego naturalną reakcją bywa zawyżanie estymacji i defensywne podejście do rozwoju systemu. Refaktoryzacja jest odkładana na później lub całkowicie pomijana — nawet gdy widać lepsze rozwiązanie architektoniczne — ponieważ pojawia się lęk, że zmiany mogą zdestabilizować aplikację i wprowadzić trudne do wykrycia błędy.


## Automatyzacja testów

Automatyczne testowanie kodu powstało właśnie jako odpowiedź na te wyzwania. Zamiast wielokrotnie ręcznie wykonywać te same kroki, zapisujemy scenariusze w postaci testów, które można uruchamiać wielokrotnie, szybko i w sposób powtarzalny. Testy stają się częścią procesu wytwarzania oprogramowania — działają jak siatka bezpieczeństwa, która natychmiast informuje nas o niepożądanych zmianach w zachowaniu systemu.

Dzięki automatyzacji:
- ograniczamy ryzyko regresji,
- skracamy czas weryfikacji zmian,
- zwiększamy pewność wdrożeń,
- odciążamy programistów od powtarzalnej pracy manualnej.

Automatyczne testy nie zastępują całkowicie testowania manualnego, ale przenoszą ciężar sprawdzania logiki biznesowej na poziom kodu. To fundament nowoczesnego podejścia do jakości oprogramowania.

## Podział testów
Testy mozemy podzielić według ich zakresu i poziomu abstrakcji. 

Najczęściej wyróżnia się:
- testy jednostkowe (unit tests), 
- testy integracyjne (integration tests) 
- testy end-to-end (E2E). 

Każdy poziom ma inne zadanie: 
- testy jednostkowe sprawdzają pojedyncze elementy systemu, 
- testy integracyjne weryfikują współpracę między komponentami, 
- testy E2E badają działanie całej aplikacji z perspektywy użytkownika. 

Świadome stosowanie różnych typów testów pozwala budować stabilną i łatwą w utrzymaniu architekturę testową.

## Testy jednostkowe (Unit Tests)
 są najmniejszym i najczęściej wykonywanym poziomem testowania. Skupiają się na pojedynczych klasach, metodach lub funkcjach, izolując je od zależności zewnętrznych poprzez stosowanie atrap (mocków, stubów lub fake’ów). 
 
 Ich głównym celem jest szybkie wykrywanie błędów logicznych oraz zapewnienie, że niewielkie fragmenty kodu działają zgodnie z oczekiwaniami. Testy jednostkowe nie powinny dotykać infrastruktury, takiej jak system plików, bazy danych, zewnętrzne API czy sieć — zamiast tego wykorzystują symulowane zależności, co pozwala zachować szybkość, deterministyczność i pełną izolację testów. Dzięki swojej precyzji stanowią fundament podejść takich jak TDD oraz zasad FIRST.


## Zasady FIRST

To zestaw zasad, które definiują dobre praktyki przy tworzeniu testów jednostkowych:

1. **Fast (Szybkie)**:
   Testy powinny być szybkie w swoim wykonaniu. Szybkie testy pozwalają programistom na częste uruchamianie ich podczas procesu programowania bez opóźnień. To umożliwia szybką informację zwrotną o stanie kodu i zmniejsza czas potrzebny na debugowanie.
2. **Independent (Niezależne)**:
   Każdy test powinien być niezależny od innych testów. Oznacza to, że wynik jednego testu nie powinien wpływać na wynik innych testów, co zapewnia bardziej stabilne i wiarygodne testowanie.
3. **Repeatable (Powtarzalne)**:
   Testy powinny być powtarzalne w różnych środowiskach i czasach. Oznacza to, że wyniki testów powinny być spójne i przewidywalne, bez względu na warunki, w których są wykonywane.
4. **Self-checking (Automatyczne)**:
   Testy powinny być automatyczne i nie wymagać interakcji użytkownika. Automatyzacja testów pozwala na szybsze i bardziej efektywne sprawdzanie poprawności kodu, a także ułatwia proces weryfikacji zmian.
5. **Timely (Pisane na bieżąco)**:
   Testy powinny być pisane na bieżąco, równolegle z procesem programowania. Pisane na bieżąco testy są łatwiejsze do utrzymania, a także pomagają w zapewnieniu, że kod działa zgodnie z oczekiwaniami od samego początku jego tworzenia.


## Nazewnictwo testów
Czytelne i konsekwentne nazewnictwo testów znacząco wpływa na zrozumiałość kodu testowego. Dobrze nazwana metoda testowa powinna jasno komunikować co jest testowane, w jakim scenariuszu oraz jakiego rezultatu oczekujemy.

### Nazewnictwo klas testowych
Nazwa klasy testowej powinna odpowiadać testowanej klasie z dopiskiem Tests.

```
{Class}Tests
```

Przykład:
```
 Calculator → CalculatorTests.
```

### Nazewnictwo metod testowych

Najczęściej stosowaną konwencją jest:
```
{Method}_{Scenario}_{ExpectedBehavior}
```

Dobra nazwa metody:
- opisuje zachowanie zamiast implementacji,
- nie zawiera skrótów biznesowych niezrozumiałych poza kontekstem,
- czyta się jak zdanie.

Przykłady
-	```Add_PositiveNumbers_ReturnsSummary```
-	```Add_NegativeNumbers_ThrowsException```

Unikaj nazw typu:
- ```Test1```
- ```ShouldWork```
- ```AddTest```


## Struktura Arrange-Act-Assert (AAA)

Jedną z najczęściej stosowanych konwencji zapisu testów jednostkowych jest wzorzec Arrange-Act-Assert, który porządkuje strukturę testu i zwiększa jego czytelność. Dzięki wyraźnemu podziałowi testy stają się łatwiejsze do zrozumienia, utrzymania oraz refaktoryzacji.

1.	**Arrange**
Przygotowanie danych wejściowych, konfiguracji testu oraz zależności (np. mocków). W tej sekcji tworzony jest kontekst potrzebny do wykonania operacji.
2.	**Act**
Wywołanie właściwej metody lub funkcji, która jest przedmiotem testu. Zaleca się, aby w tej części znajdowała się pojedyncza akcja biznesowa.
3.	**Assert**
Weryfikacja rezultatu działania — sprawdzenie, czy wynik jest zgodny z oczekiwaniami. W tej sekcji powinny znajdować się asercje określające zachowanie systemu.

## Szablon testu

```csharp
public class MyLibraryTests
{
  [Fact]
  public void Method_Scenario_ExpectedBehavior()
  {
      // Arrange

      // Act

      // Assert
  }
}
```

Wzorzec *Arrange-Act-Assert* zwiększa nie tylko czytelność testów, ale także sprawia, że mogą one pełnić rolę żywej dokumentacji systemu. Dobrze nazwane testy oraz przejrzysty podział na przygotowanie, wykonanie i asercję jasno pokazują, jak dana klasa powinna być używana i jakie zachowanie jest oczekiwane w konkretnych scenariuszach. W przeciwieństwie do tradycyjnej dokumentacji testy pozostają zawsze aktualne — jeśli opisane zachowanie przestaje być prawdziwe, test po prostu przestaje przechodzić.

## Parametryzacja testów

Aby uniknąć powielania wielu bardzo podobnych testów, które różnią się jedynie danymi wejściowymi, warto stosować parametryzację testów. W xUnit realizuje się ją poprzez atrybut [Theory], który pozwala uruchomić ten sam test wielokrotnie z różnymi zestawami danych.

Zamiast tworzyć kilka metod testowych:
```cs
[Fact]
public void Add_1_2_Returns3() { ... }

[Fact]
public void Add_2_2_Returns4() { ... }
```

lepiej zapisać jeden test:
```cs
[Theory]
[InlineData(1, 2, 3)]
[InlineData(2, 2, 4)]
public void Add_PositiveNumbers_ReturnsSummary(int x, int y, int expected)
{
    // Act
    var result = sut.Add(x, y);

    // Assert
    Assert.Equal(expected, result);
}
```

Korzyści z używania [Theory]:
-	redukcja duplikacji kodu testowego,
-	większa czytelność scenariuszy testowych,
-	łatwe rozszerzanie testów o nowe przypadki brzegowe,
-	zgodność z zasadą DRY.

## Testowanie wyjątków (Exception)

Częstym scenariuszem jest weryfikacja, czy metoda rzuca odpowiedni wyjątek w określonych warunkach. W xUnit wykorzystuje się do tego `Assert.Throws<T>()`.

```cs
[Fact]
public void Add_NegativeNumbers_ThrowsException()
{
    // Act
    Action act = () => sut.Add(-1, 0);

    // Assert
    Assert.Throws<ArgumentException>(act);
}
```

Dlaczego testujemy wyjątki:
- wyjątek jest częścią kontraktu metody,
- zabezpiecza reguły biznesowe,
- zapobiega cichym błędom logicznym.

---

## Testowanie metod `void` 

Metody void nie zwracają wartości, dlatego testujemy ich efekty uboczne (side effects), np.:
- zmianę stanu obiektu,
- wywołanie zależności (mocków),
- zapis do kolekcji.

Przykład — zmiana stanu:
```cs
sut.Reset();
Assert.Equal(0, sut.Value);
```

Przykład — weryfikacja współpracy z zależnością (mock):
```cs
mockLogger.Verify(x => x.Log("Saved"), Times.Once);
```

Kluczowa zasada: testujemy co się wydarzyło.

## Testowanie metod `private` — dlaczego nie testujemy

Metod prywatnych nie testujemy bezpośrednio. Testy jednostkowe powinny skupiać się na publicznym zachowaniu klasy.

Powody:
-	1.	**Encapsulation**
Metody private są szczegółem implementacyjnym — mogą się zmieniać bez wpływu na kontrakt klasy.
-	2.	**Stabilność testów**
Testowanie private powoduje kruche testy łamiące się przy refaktorze.

- 3. **Testujemy zachowanie, nie implementację**
Jeśli logika private jest istotna biznesowo, powinna zostać:
   -	wywołana przez metodę publiczną, albo
	-	wydzielona do osobnej klasy i testowana osobno.

Zasada praktyczna:

Jeśli czujesz potrzebę testowania private — to sygnał, że kod może wymagać refaktoryzacji.

## Kod niestowalny — problem i rozwiązanie

Jednym z częstych powodów trudności w testowaniu jest używanie elementów zależnych od środowiska lub czasu, takich jak:
-	DateTime.Now
-	Guid.NewGuid()
-	losowość (Random)
-	statyczne API systemowe

Kod korzystający bezpośrednio z bieżącego czasu staje się niedeterministyczny, co łamie zasadę Repeatable z FIRST.


### Problem

Klasa nietestowalna:
```cs
public class CalendarService
{
   public bool IsWeekend()
   {
       return DateTime.Now.DayOfWeek == DayOfWeek.Saturday
        || DateTime.Now.DayOfWeek == DayOfWeek.Sunday;
   }
```

Test takiej metody zależy od dnia tygodnia, w którym go uruchamiamy. Oznacza to, że wynik testu nie jest deterministyczny — raz przejdzie, a raz nie. W skrajnym przypadku musielibyśmy przyjść do pracy w weekend, aby upewnić się, że test działa poprawnie.

**Nie próbuj zmieniać czasu systemowego!**
Manipulowanie zegarem systemowym powoduje efekty uboczne w całym środowisku i prowadzi do bardzo niestabilnych testów.


### Rozwiązanie — wstrzykiwanie zależności (abstrakcja czasu)

Zamiast używać statycznego DateTime.Now, wprowadzamy interfejs:

```cs
public interface IClock
{
    DateTime Now { get; }
}
```

Implementacja produkcyjna:
```cs
public class SystemClock : IClock
{
    public DateTime Now => DateTime.Now;
}
```

Refaktoryzacja serwisu:
```cs
public class CalendarService
{
    private readonly IClock clock;

    public CalendarService(IClock clock)
    {
        this.clock = clock;
    }

    public bool IsWeekend()
    {
        var day = clock.Now.DayOfWeek;
        return day == DayOfWeek.Saturday || day == DayOfWeek.Sunday;
    }
}
```

FakeClock — implementacja testowa

```cs
public class FakeClock : IClock
{
    public DateTime Now { get; private set; }

    public FakeClock(DateTime now)
    {
        Now = now;
    }

    public void Set(DateTime newNow)
    {
        Now = newNow;
    }
}
```

Użycie w teście:
```cs
var fakeClock = new FakeClock(new DateTime(2024, 1, 6)); // sobota
var sut = new CalendarService(fakeClock);

Assert.True(sut.IsWeekend());
```

Dzięki temu:
- test jest w pełni powtarzalny,
- nie zależy od czasu systemowego,
- spełniamy zasadę Repeatable z FIRST.


### Wskazówka — gotowa abstrakcja w .NET 9+

Od .NET 9 nie trzeba tworzyć własnego `IClock`. Platforma udostępnia gotową abstrakcję czasu — **TimeProvider**.

Przykład użycia:
```cs
public class CalendarService
{
    private readonly TimeProvider timeProvider;

    public CalendarService(TimeProvider timeProvider)
    {
        this.timeProvider = timeProvider;
    }

    public bool IsWeekend()
    {
        var day = timeProvider.GetLocalNow().DayOfWeek;
        return day == DayOfWeek.Saturday || day == DayOfWeek.Sunday;
    }
}
```

Implementacja produkcyjna:
```cs
var sut = new CalendarService(TimeProvider.System);
```

Test
```cs
var fakeTime = new FakeTimeProvider();
fakeTime.SetLocalTime(new DateTimeOffset(2024, 1, 6, 0, 0, 0, TimeSpan.Zero));

var sut = new CalendarService(fakeTime);

Assert.True(sut.IsWeekend());
```


Korzyści z użycia `TimeProvider`:
- brak własnych interfejsów,
- standard platformy .NET,
- lepsza integracja z API frameworka.




# TDD (Test-Driven Development) 
to technika wytwarzania oprogramowania, w której testy powstają przed implementacją kodu produkcyjnego. Programista rozpoczyna od napisania testu opisującego oczekiwane zachowanie (Red), następnie tworzy minimalną implementację spełniającą wymagania testu (Green), a na końcu refaktoryzuje kod, poprawiając jego strukturę bez zmiany funkcjonalności (Refactor). Podejście to sprzyja tworzeniu luźno powiązanych komponentów, zwiększa testowalność kodu oraz pomaga utrzymać wysoką jakość projektu poprzez ciągłą weryfikację założeń biznesowych


## Cykl Red-Green-Refactor 
to iteracyjny proces pracy stosowany w podejściu TDD, którego celem jest rozwijanie kodu w małych, bezpiecznych krokach. Każda iteracja skupia się na dodaniu niewielkiej funkcjonalności oraz natychmiastowej weryfikacji jej poprawności poprzez testy.

1.	**Red (Czerwony)**
Na początku tworzony jest test opisujący nowe zachowanie systemu. Test powinien zakończyć się niepowodzeniem, ponieważ implementacja jeszcze nie istnieje. Ten etap wymusza precyzyjne zdefiniowanie oczekiwanego rezultatu przed napisaniem kodu produkcyjnego.

2.	**Green (Zielony)**
Następnie powstaje minimalna implementacja pozwalająca przejść testowi. Na tym etapie priorytetem nie jest idealna architektura, lecz osiągnięcie działającego rozwiązania spełniającego wymagania testu.

3.	**Refactor (Refaktoryzacja)**
Po uzyskaniu zielonych testów kod jest porządkowany — usuwane są duplikacje, poprawiana czytelność oraz struktura. Refaktoryzacja odbywa się przy zachowaniu pełnej funkcjonalności, a testy stanowią zabezpieczenie przed wprowadzeniem regresji.

Powtarzanie tego cyklu prowadzi do powstawania kodu o wysokiej jakości, dobrze pokrytego testami i łatwego w utrzymaniu.

---

## Kroki Red–Green–Refactor na przykładzie kalkulatora (AAA w praktyce)

### 1. RED — dopisujemy test, implementacja jeszcze nie działa

Na początku tworzymy test opisujący oczekiwane zachowanie. Test może być nawet celowo błędny — jego zadaniem jest wymuszenie implementacji.
```cs
[Fact]
public void Add_PositiveNumbers_ReturnsSummary()
{
    // Act
    var result = sut.Add(2, 3);

    // Assert
    Assert.Equal(3, result);
}
```

Przykładowa początkowa implementacja:
```cs
public int Add(int x, int y)
{
    throw new NotImplementedException();
}
```

Test jest **czerwony**, ponieważ metoda nie została jeszcze zaimplementowana.

---

### 2. GREEN — minimalna implementacja


Dodajemy najprostszy możliwy kod, który sprawi, że test przejdzie.

```cs
public int Add(int x, int y)
{
    return x + y;
}
```


Po tej zmianie test zaczyna przechodzić — osiągamy stan GREEN.

---

### 3. REFACTOR — poprawa jakości kodu

```cs
public int Add(int x, int y)
{
    if (x < 0) throw new ArgumentException(nameof(x));
    if (y < 0) throw new ArgumentException(nameof(y));

    return x + y;
}
```


---

Każda iteracja Red–Green–Refactor to mały, bezpieczny krok prowadzący do stabilnego i testowalnego kodu.

---

#	Architektura tworzona przez testy


Konsekwencją stosowania TDD jest naturalna poprawa jakości architektury kodu. Pisanie testów przed implementacją wymusza tworzenie klas hermetycznych, skupionych na logice biznesowej i oderwanych od szczegółów infrastruktury takich jak UI, baza danych czy operacje IO. Kod staje się bardziej modularny, ponieważ zależności muszą być wstrzykiwane z zewnątrz, co sprzyja stosowaniu Dependency Injection. W efekcie powstają komponenty luźno powiązane, łatwiejsze do testowania, rozwijania i refaktoryzacji, a sama architektura systemu staje się bardziej elastyczna i odporna na zmiany.

Podejście TDD bardzo naturalnie łączy się z zasadami SOLID oraz wzorcami projektowymi, ponieważ testowalność wymusza określony sposób projektowania kodu.

Przede wszystkim TDD sprzyja przestrzeganiu zasady **Single Responsibility Principle (SRP)**. Klasy, które mają wiele odpowiedzialności, są trudne do przetestowania, dlatego podczas pisania testów szybko pojawia się potrzeba ich rozdzielenia na mniejsze, wyspecjalizowane komponenty. Dzięki temu logika biznesowa zostaje oddzielona od UI, IO czy infrastruktury.

Zasada **Open/Closed Principle (OCP)** również wynika z TDD w sposób naturalny. Aby łatwo rozszerzać zachowanie bez łamania istniejących testów, kod zaczyna opierać się na abstrakcjach i polimorfizmie. Testy stają się wtedy bezpieczną siatką, która chroni istniejącą funkcjonalność podczas rozbudowy systemu.

TDD wzmacnia także **Dependency Inversion Principle (DIP)**. Ponieważ testy wymagają izolowania zależności, klasy zaczynają zależeć od interfejsów zamiast konkretnych implementacji. To prowadzi do stosowania Dependency Injection oraz wzorców takich jak Strategy, Adapter czy Factory.

Z perspektywy wzorców projektowych TDD często prowadzi do ich „organicznego” pojawienia się:
- **Strategy** — gdy chcemy łatwo podmieniać zachowanie w testach,
- **Adapter** — gdy oddzielamy kod domenowy od API zewnętrznych,
- **Factory** — gdy upraszczamy tworzenie obiektów w testach,
- **Facade** — gdy ukrywamy złożoność infrastruktury za prostym interfejsem.

W praktyce oznacza to, że TDD nie tylko weryfikuje poprawność działania kodu, ale także wpływa na jego architekturę. 
Kod pisany w ten sposób staje się bardziej hermetyczny, modularny i elastyczny, a wzorce projektowe przestają być teoretycznym narzędziem — zaczynają wynikać bezpośrednio z potrzeby utrzymania testowalności.

---


## Mutation Testing — sprawdzanie jakości testów

Samo posiadanie testów nie oznacza jeszcze, że są one dobre. Istnieje technika pozwalająca sprawdzić skuteczność testów poprzez **celowe wprowadzanie zmian w kodzie** i obserwowanie, czy testy wykryją błąd. Technika ta nazywa się **Mutation Testing** (testowanie mutacyjne).

Na czym polega:
-  narzędzie automatycznie modyfikuje kod produkcyjny,
-	wprowadza drobne zmiany (mutacje),
- uruchamia testy i sprawdza, czy któryś z nich nie przejdzie.

Przykłady mutacji:
-	negacja warunku: `>` → `<=`
-	zmiana operatora logicznego: `&&` → `||`
-	zmiana wartości zwracanej: `true` → `false`
-	usunięcie fragmentu kodu

Jeśli testy nadal przechodzą mimo zmiany logiki — oznacza to, że nie pokrywają rzeczywistego zachowania systemu.

Przykład:
```cs
return x > 10;
```

Mutacja
```cs
return x <= 10;
```

Dobre testy powinny „zabić mutanta” (ang. *kill the mutant*), czyli wykryć zmianę.

Korzyści z mutation testing:
-	mierzy realną jakość testów, nie tylko pokrycie kodu,
- pomaga wykryć fałszywie pozytywne testy,
- uczy pisać testy sprawdzające zachowanie, a nie implementację.

Popularne narzędzie w ekosystemie .NET: [**Stryker.NET**](https://stryker-mutator.io/docs/stryker-net/introduction/).



---

## Podsumowanie — dlaczego warto pisać testy

Testy jednostkowe to nie tylko narzędzie weryfikacji poprawności kodu. To przede wszystkim mechanizm projektowy, który pomaga tworzyć lepsze API, wymusza separację odpowiedzialności oraz zwiększa czytelność logiki biznesowej. Dzięki testom:
	-	szybciej wykrywamy regresje,
	-	możemy bezpiecznie refaktoryzować kod,
	-	dokumentujemy zachowanie systemu w sposób wykonywalny,
	-	skracamy czas debugowania,
	-	zwiększamy pewność wdrożeń.

Często pojawia się argument: ***„nie mam czasu na pisanie testów”***. W praktyce jest odwrotnie — **nie masz czasu na niepisanie testów**, bo brak testów prędzej czy później prowadzi do kosztownych błędów, regresji i długiego debugowania.

Dobrze napisane testy są proste i czytelne — nie stanowią bagażu spowalniającego rozwój kodu; wręcz przeciwnie, dodają pewności i działają jak skrzydła, które pozwalają rozwijać system szybciej i odważniej.


Programista, który ma testy, nie boi się zmian w kodzie, eksperymentów i refaktoringu — **jest odważny**.

---
