# Kontrola wersji

## Wprowadzenie

Na wczesnym etapie pracy nad projektem często spotykamy się z chaotycznym sposobem zarządzania kodem — pliki są przesyłane mailem, kopiowane między katalogami, a kolejne wersje oznaczane nazwami w stylu `projekt_final_v3_poprawiony`. Takie podejście szybko okazuje się kruche i trudne do utrzymania. Brakuje historii zmian, nie wiadomo kto i dlaczego wprowadził daną modyfikację, a powrót do wcześniejszej wersji kodu bywa praktycznie niemożliwy.

Wraz z rozwojem projektu rośnie liczba zmian, eksperymentów i osób pracujących nad kodem. Bez systemu kontroli wersji współpraca zespołowa staje się problematyczna, a każda większa modyfikacja niesie ryzyko nadpisania czyjejś pracy lub wprowadzenia trudnych do wykrycia błędów. Kontrola wersji powstała właśnie po to, aby uporządkować proces rozwoju oprogramowania i dać programistom pełną kontrolę nad historią kodu.

## Kontrola wersji Git

Git jest rozproszonym systemem kontroli wersji, który pozwala śledzić zmiany w projekcie, pracować równolegle nad różnymi funkcjonalnościami oraz bezpiecznie wracać do wcześniejszych stanów aplikacji. Każda zmiana zapisywana jest w postaci commita, który dokumentuje intencję autora oraz kontekst modyfikacji.

Dzięki Git:
-	historia projektu jest przejrzysta i możliwa do odtworzenia,
-	wiele osób może pracować jednocześnie nad tym samym repozytorium,
-	eksperymentowanie z kodem nie wiąże się z ryzykiem utraty stabilnej wersji,
-	branchowanie umożliwia izolowanie nowych funkcjonalności i refaktoryzacji.

Git bardzo dobrze wspiera sposób pracy w TDD. Krótkie iteracje Red-Green-Refactor naturalnie prowadzą do małych, częstych commitów, a czytelna historia zmian pokazuje ewolucję rozwiązania krok po kroku — od testu, przez implementację, aż po refaktoryzację. W praktyce testy zapewniają bezpieczeństwo zmian, a Git daje kontrolę nad ich historią, co razem tworzy stabilne środowisko do rozwoju oprogramowania.

## Podstawowy model pracy z Git

Podstawowy model pracy z Git opiera się na wprowadzaniu małych, kontrolowanych zmian oraz ich świadomym integrowaniu z główną linią rozwoju projektu. Zamiast traktować kod jako jedną całość modyfikowaną „od czasu do czasu”, Git zachęca do pracy iteracyjnej — każda zmiana powinna mieć jasno określony cel i być zapisana w postaci commita.

**Commit** to najmniejsza jednostka historii projektu. Powinien reprezentować jedną logiczną zmianę — na przykład dodanie testu, implementację funkcjonalności lub refaktoryzację. Małe i częste commity sprawiają, że historia repozytorium staje się czytelna i pozwala łatwo zrozumieć, jak ewoluował kod.

**Branch** (gałąź) umożliwia pracę nad nową funkcjonalnością lub eksperymentem w izolacji od stabilnej wersji systemu. Dzięki temu można rozwijać kod bez ryzyka destabilizacji głównej gałęzi projektu. Branchowanie szczególnie dobrze współgra z podejściem TDD, gdzie nowe pomysły można rozwijać iteracyjnie i bezpiecznie.

**Merge** lub **Pull Request** służą do łączenia zmian z główną linią rozwoju. To moment, w którym kod jest przeglądany, testy weryfikują poprawność działania, a zespół podejmuje świadomą decyzję o integracji.

W praktyce model pracy z Git można podsumować w kilku krokach:
- tworzysz branch dla nowej zmiany,
- wprowadzasz małe commity opisujące kolejne etapy pracy,
- testujesz i refaktoryzujesz kod,
- integrujesz zmiany poprzez merge.

Taki sposób pracy wprowadza porządek w projekcie, ułatwia współpracę zespołową i pozwala rozwijać system w sposób przewidywalny — bez chaosu i bez obawy o utratę stabilnej wersji aplikacji.

## Przykładowa praca z repozytorium — pierwsze kroki

Poniżej znajduje się przykładowy scenariusz rozpoczęcia pracy z nowym repozytorium Git. Celem jest stworzenie uporządkowanej struktury projektu oraz pierwszej rewizji, która stanie się punktem wyjścia do dalszego rozwoju aplikacji.

1. Utworzenie repozytorium

Na początku inicjalizujemy repozytorium w katalogu projektu:
```bash
git init
```

2. Utworzenie początkowej rewizji

Dobrym zwyczajem jest rozpoczęcie pracy od pierwszego commita, nawet jeśli projekt jest jeszcze pusty. Pozwala to jasno określić punkt startowy historii:

```bash
git commit --allow-empty -m "Początkowa rewizja"
```

3. Dodanie pliku README

Plik README.md pełni rolę dokumentacji startowej projektu. Można w nim opisać cel repozytorium, sposób uruchomienia aplikacji lub strukturę katalogów.

```bash
touch README.md
```

4. Utworzenie struktury katalogów

Dobrą praktyką jest uporządkowanie projektu już na początku pracy. Najczęściej wydziela się katalog na kod źródłowy oraz dokumentację.

```bash
mkdir src
mkdir docs
cd src
```

💡 **Wskazówka — puste katalogi w Git**
Git nie śledzi pustych katalogów. Jeśli chcesz, aby struktura folderów była widoczna w repozytorium, dodaj do nich plik pomocniczy, np. `.gitkeep`:

```bash
touch ../docs/.gitkeep
```

Dzięki temu katalog zostanie uwzględniony w historii projektu, nawet jeśli na początku nie zawiera jeszcze właściwych plików.



5. Dodanie pierwszego pliku i pojęcie Stage

Po przygotowaniu struktury katalogów możemy dodać pierwszy plik do projektu — na przykład `Program.cs` w katalogu `src`.

```bash
cd src
touch Program.cs
```

Na tym etapie plik istnieje w katalogu roboczym, ale Git jeszcze go nie śledzi. Aby zrozumieć, co dzieje się dalej, trzeba poznać jedno z kluczowych pojęć — **Stage** (obszar przygotowania zmian).

**Czym jest Stage (obszar przygotowania zmian)?**
Git nie zapisuje zmian automatycznie. Zanim wykonasz commit, musisz świadomie wskazać, które pliki mają znaleźć się w kolejnej rewizji. Proces ten nazywa się stage’owaniem.

Można powiedzieć, że Stage to „poczekalnia” dla zmian:
-	katalog roboczy — miejsce, gdzie edytujesz pliki,
-	**stage** — lista zmian przygotowanych do commita,
-	repozytorium — historia zapisanych commitów.

Dodanie pliku do stage:
```bash
git add Program.cs
```

Od tego momentu Git wie, że plik ma zostać uwzględniony w następnym commicie.

Sprawdzenie statusu:
```bash
git status
```

Zobaczysz informację, że plik znajduje się w sekcji *Changes to be committed*.


**Dlaczego Stage jest ważny?**
Stage daje pełną kontrolę nad tym, co trafia do historii projektu. Dzięki temu możesz:
- dodać tylko część zmian,
- rozdzielić duże modyfikacje na mniejsze commity,
- zachować czytelną historię rozwoju kodu.

Na koniec zapisujemy zmiany w repozytorium:

```bash
git commit -m "Dodanie pliku Program.cs"
```

Zrozumienie różnicy między katalogiem roboczym, stage a commitami jest kluczowe — to właśnie ten model sprawia, że Git pozwala pracować w sposób świadomy i uporządkowany.

## 6. Praca z gałęziami (branch) — poprawny model pracy

Jedną z najważniejszych koncepcji w Git jest praca na gałęziach. Gałąź pozwala rozwijać nowe funkcjonalności w izolacji od stabilnej wersji aplikacji, dzięki czemu zmiany można wprowadzać bez ryzyka destabilizacji głównej linii projektu.

**Dlaczego używamy gałęzi?**
Praca bez branchy często prowadzi do chaosu — wiele zmian trafia bezpośrednio do głównej gałęzi, co utrudnia kontrolę jakości i zwiększa ryzyko konfliktów. Branchowanie wprowadza uporządkowany proces:
-	główna gałąź (main lub master) pozostaje stabilna,
-	nowe funkcjonalności rozwijane są w osobnych gałęziach,
-	integracja następuje dopiero po zakończeniu pracy i weryfikacji testów.

**Utworzenie nowej gałęzi**
Przykład utworzenia gałęzi dla nowej funkcjonalności:

```bash
git checkout -b feature/hello-world
```

Polecenie tworzy nową gałąź i automatycznie na nią przełącza.

W nowszych wersjach Git zalecane jest jednak użycie bardziej czytelnej komendy switch, która została wprowadzona, aby uprościć pracę z gałęziami.

```bash
git switch -c feature/hello-world
```


Można sprawdzić aktualną gałąź:
```bash
git branch
```

## Konwencje nazewnictwa gałęzi

- **feature/…** — nowa funkcjonalność  
  np. `feature/user-login`, `feature/hello-world`
- **fix/…** — poprawki błędów  
  np. `fix/null-reference`, `fix/order-validation`
- **refactor/…** — zmiany strukturalne bez zmiany zachowania  
  np. `refactor/extract-service`, `refactor/cleanup-tests`
- **docs/…** — zmiany w dokumentacji  
  np. `docs/update-readme`, `docs/add-git-workflow`
- **test/…** — dodanie lub poprawa testów  
  np. `test/add-calculator-tests`, `test/improve-coverage`
- **chore/…** — zmiany techniczne i porządkowe (bez wpływu na logikę biznesową)  
  np. `chore/update-dependencies`, `chore/cleanup-solution`