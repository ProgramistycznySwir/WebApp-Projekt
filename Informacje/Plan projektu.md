# Plan projektu

1. __(1 pkt -<|> Autoryzacja)__ Profil użytkownika :
- [x] jako Niezalogowany Użytkownik mam możliwość rejestracji w serwisie
- jako Niezalogowany Użytkownik mam możliwość zresetowania hasła do własnego profilu; procedura odzyskiwania hasła powinna być bezpieczna (np. przez wysłanie linku aktywacyjnego na adres email)
- jako Zalogowany Użytkownik mogę edytować moje dane
- jako Niezalogowany Użytkownik mogę się zalogować i wylogować z serwisu


2. __(1 pkt -<|> Tworzenie i edytowanie przepisów)__ Zalogowany użytkownik ma możliwość opublikowania przepisu na posiłek. Przepis powinien zawierać nazwę, listę składników, opis wykonania oraz datę publikacji. Użytkownik może edytować i usuwać przepisy, które stworzył.
    > Tabela w bazie [RecipeID, UserID, RecipeName, IngredientsList, InstructionText, PublicationDate, SummaryPoints].

3. __(1 pkt -<|> Lista ulubionych przepisów)__ Zalogowany użytkownik ma możliwość dodawania/usuwania przepisu do/z listy ulubionych. Może dowolnie przeglądać listę ulubionych przepisów.
    > Tabela [UserID, RecipeID].
4. __(1 pkt -<|> Wyszukiwanie przepisów)__ Użytkownik (zalogowany i niezalogowany) ma możliwość wyszukania przepis po nazwie lub nazwie użytkownika. Wyszukane przepisy są wyświetlane według najnowszej daty publikacji.
    > Użytkownik wysyła formularz z stringiem wyszukiwania, aplikacja go parsuje i przekierowuje użytkownika na wygenerowaną stronę (LINQ > context.Where(a.RecipeName => a.ToLower().Contains(searchedPhrase:string), 25)).
5. __(1 pkt -<|> Przeglądanie profilów innych użytkowników)__ Użytkownik (zalogowany i niezalogowany) może przeglądać przepisy opublikowane na profilu autora. Przepisy są sortowane według najnowszej daty publikacji.
    > Przeszukujemy po UserID.
6. __(2 pkt -<|> Kategorie przepisów)__ Zalogowany użytkownik może dodawać, usuwać i edytować kategorie. Usunięcie kategorii nie powinno spowodować usunięcia przepisu. Autor przepisu może go przypisać do kilku kategorii (np. śniadania, vege), a dowolny użytkownik może wyszukiwać przepisy po kategorii.
    >> Jako dodawanie usuwanie i edytowanie kategorii rozumiane jest edytowanie kategorii na postcie.
    > Jak to widzę to do tego będą służyć 2 tabele, jedna przechowująca same kategorie w różnych postach (zawiera 3 pozycje, [ID, PostCount, Name]), oraz tabelę zawierającą to jakie kategorie są przypisane postom [ID, PostID, CategoryID].
7. __(1 pkt -<||> Głosowanie na przepisach)__ Zalogowani użytkownicy mogą głosować na przepis (na “+” i “-”). Jeden zalogowany użytkownik może tylko raz zagłosować na wybrany przepis (na + lub na -).
    >> Nic nie pisze o zmienianiu głosów.
    > Problemem tutaj jest zaimplementowanie drugiej części punktu, czyli tego by użytkownicy głosowali tylko raz na post. Trzeba by przechowywać tablicę postów upvote'owanych przez użytkownika najpewniej w strukturze [ID, UserID, Rating].
    > Propozycja oddzielnej tabeli z podwójnym kluczem głównym UserID + RecipeID i Rating. Podwójny klucz główny wykluczy możliwość głosowania dwa razy na ten sam przepis (tylko jedna unikalna kombinacja ID użytkownika i ID przepisu może istnieć w tabeli).
- 1. __(0,5 pkt -<|> Ranking)__ Na stronie głównej aplikacji powinny być prezentowane 10 najlepiej ocenianych przepisów.
    >Po zaimplementowaniu głównej części punktu to już jest łatwizną.
8. __(1,5 pkt -<||> Przechowywanie plików użytkowników)__ Przepis może zawierać galerię zdjęć podanych jako linki do grafik z zewnętrznych serwisów lub zdjęć przesłanych bezpośrednio z komputera i przechowywanych w aplikacji.
    >To może okazać się zdradliwe, trzebaby tutaj zaimplementować pobieranie plików od użytkowników, jakąś formę normalizacji tych plików i ich przechowywania.

9. __(1 pkt -<?> Styl)__ Aplikacja powinna mieć estetyczny wygląd.


## Pytania do Pani X:
- 6pkt - Jak do końca mają działać kategorie?
    - Mamy pomysł by przechowywać je 2 odrębnych tabelach Kategorie i Przepisy_Kategorie
- 7pkt - Jak przechowywać czy ktoś już zagłosował na daną kategorię?
    - Czy można to przechowywać w dużej tabelii.
+ 8pkt - Jak przechowywać te pliki?
>A: W bazie danych zapisać referencję do pliku zwyczajnie zapisanego za pomocą System.IO.
- Co jeśli ktoś robi sam design bazy?
+ Jak nadpisać kontekst User tak bo, przydałoby się to do:
    - zmniejszenia UserName do bardziej sensownej liczby 32 znaków,
    - dodania do UserName flagi UNIQUE,
    - skrócenia UserID do bardziej sensownych długości (450 znaków to troche overkill)
>A: Własny profil


- Co do bazy danych, czy można w tabeli AspNetUsers zmniejszyć rozmiar id do bardziej sensownych rozmiarów załóżmy 64.

## Łukasz:
- Projekt bazy danych.
- Ogarnąć jak ładnie zrobić wpisywanie listy składników.
## Daniel:
- Ogarnąć autoryzację (pkt.1).


<!-- <style>
body{
    font-family: Comic Sans MS, Comic Sans, cursive;
    /* font-family: Impact, fantasy; */
}
</style> -->
