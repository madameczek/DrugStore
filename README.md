# DrugStore

Aplikacja demo przeznaczona dla konsoli i wykonana w .NET Core 3.1.
Oparta jest o bazę danych MSSQL Express.
W założeniu aplikacja ma obsługiwać aptekę w części magazynowej oraz rejestracji zamówień i recept.

## Inicjalizacja bazy danych

Otwórz plik DbSchema.sql w edytorze Management Studio i wykonaj skrypt. Zostanie utworzona baza z przykładowymi danymi.
Czasem skrypt przy pierwszym uruchomieniu daje komunikat o rollbacku. Dla pewności można skrypt uruchomić ponownie.

## To jest część pracy

Aplikacja obsługuje obecnie tabele:

- leki,
- dostawcy leków,
- zamówienia
- szczegóły zamówień (pozycje zamówienia)

Dane przykładowe obejmują leki i dostawców. Można dodawać samodzielnie zamówienia i leki do zamówienia.
Aplikacja obsługuje także wydawanie leków z pomniejszewniem stanu magazynowogo. Niedługo nastąpi rozszerzenie o obsługę recept, tzn. niektóre leki recepturowe mogą być dodane do zamówienia tylko po wprowadzeniu danych recepty.
