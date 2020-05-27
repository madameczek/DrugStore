# DrugStore

Aplikacja ma obsługiwać aptekę w części magazynowej oraz rejestracji zamówień i recept.

![Demonstracja menu dostawców](img/suppliers.gif)

## Geneza

Aplikacja powstała jako jeden z projektów wykonywanych za zakończenie modułu w szkole programistycznej. Ma na celu pokazanie opanowania materiału :). Nie jest jej założeniem realizacja super wygodnego UI i być gotowym produktem. Ale ma poprawnie obsłużyć logikę biznesową w założonym obszarze, tj:

- leki,
- dostawcy leków,
- recepty,
- zamówienia,
- szczegóły zamówień (pozycje zamówienia).

Możliwe jest:

- tworzenie wpisów,
- edycja,
- kasowanie,
- modyfikacja.

Aplikacja ma poprawnie obsługiwać logikę, w tym możliwe błędy i poprawnie informować użytkownika o zdarzeniach. Na przykład: próba usunięcia leku, na który są zamówienia skończy się niepowodzeniem i odpowiednim komunikatem.

## Technologia

- .NET Core 3.1 aplikacja konsolowa.
- MSSQL Express.

## Uruchomienie demo

Aplikacja wymaga serwera SQL. Aby uruchomić aplikację, wykonaj kroki:

- Sklonuj projekt na komputrer lokalny.
- Utwórz bazę danych.
  - Otwórz plik DbSchema.sql w edytorze Management Studio i wykonaj skrypt. Zostanie utworzona baza z przykładowymi danymi. Czasem skrypt przy pierwszym uruchomieniu daje komunikat o rollbacku. Dla pewności można skrypt uruchomić ponownie.
  - Alternatywnie załączam także plik `DrugStore.bak` wykonany w wersji SSMS 18.4.
- Skompiluj i uruchom solucję. Aplikacja sama sprawdza połączenie z bazą danych.

## To jest część pracy

Dane przykładowe obejmują leki, dostawców i zamówienia.  
Aplikacja obsługuje wydawanie leków z pomniejszewniem stanu magazynowogo.  
Niedługo nastąpi rozszerzenie o obsługę recept, tzn. niektóre leki recepturowe mogą być dodane do zamówienia tylko po wprowadzeniu danych recepty.
