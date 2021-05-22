# Słowo wstępu:
W nawiasach są podsumowania poszczególnych punktów (to ile punktów można dostać za każde, w mojej ocenie trudność zaimplemmentowania, oraz "tytuł"). Ocena trudności jest 4 punktowa: I - proste, II - będzie wymagać troche googlowania by zrobić dobrze, III - będzie wymagać sporo pracy by zaimplementować dobrze, V - zajmie spoooro czasu, dodatkowa ? - trudno ocenić. Tytuł to także proponowana nazwa karty kanbanowej.



# Spotkania grup
1. __(1 pkt - <|> Autoryzacja)__ Profil użytkownika :
- jako Niezalogowany Użytkownik mam możliwość rejestracji w serwisie
- jako Niezalogowany Użytkownik mam możliwość zresetowania hasła do własnego profilu; procedura odzyskiwania hasła powinna być bezpieczna (np. przez wysłanie linku aktywacyjnego na adres email)
- jako Zalogowany Użytkownik mogę edytować moje dane
- jako Niezalogowany Użytkownik mogę się zalogować i wylogować z
serwisu.
2. __(2 pkt - <|> Tworzenie grup (raczej mało zaawansowane))__ Zalogowany użytkownik ma możliwość tworzenia grup. Grupa ma nazwę, opis, miasto oraz listę uczestników. Użytkownik, który tworzy grupę jest jej właścicielem. Jeden użytkownik może być właścicielem maksymalnie 10 grup. Właściciel grupy może dowolnie ją modyfikować - usuwać i edytować.

3. __(1,5 pkt -<||> Kategorie grup (do pkt.2))__ Zalogowany użytkownik może dodawać, usuwać i edytować kategorie. Właściciel grupy może ją przypisać do jednej z kategorii (np. IT) , a dowolny użytkownik może wyszukiwać grupy według kategorii. Usunięcie kategorii nie powinno spowodować usunięcia grupy.
4. __(1 pkt -<|> Przeszukiwanie bazy danych)__ Użytkownik (zalogowany i niezalogowany) ma możliwość wyszukania interesującej go grupy po nazwie lub mieście. Wyszukane grupy są wyświetlane według najnowszej daty utworzenia.
5. __(1 pkt -<|>)__ Dowolny zalogowany użytkownik może zapisać się do dowolnej grupy i wypisać się z grupy. Zapisany użytkownik może przeglądać listę grup, do których jest zapisany.
6. __(1 pkt -<|>)__ Właściciel grupy może organizować spotkania grupy. Spotkanie ma temat, opis, datę, godzinę, lokalizację oraz link do spotkania online i limit osób. Historia spotkań jest dostępna na profilu grupy.
7. __(1 pkt -<|>)__ Uczestnik grupy może zapisać się lub wypisać się do/ze spotkania grupy, do której należy. Do spotkania może dołączyć określona w “limicie osób” liczba uczestników.
8. __(1,5 pkt -<||> Generowanie pdf'ów i kodów QR)__ Uczestnik spotkania może wygenerować i pobrać bilet wstępu na spotkanie w postaci pliku PDF. Bilet wstępu powinien zawierać kod QR, numer biletu, nazwę grupy, temat oraz miejsce spotkania, a także datę i godzinę spotkania.
9. __(1 pkt -<?> Styl)__ Aplikacja powinna mieć estetyczny wygląd.



# Przepisy
1. __(1 pkt -<|> Autoryzacja)__ Profil użytkownika :
- jako Niezalogowany Użytkownik mam możliwość rejestracji w serwisie
- jako Niezalogowany Użytkownik mam możliwość zresetowania hasła do własnego profilu; procedura odzyskiwania hasła powinna być bezpieczna (np. przez wysłanie linku aktywacyjnego na adres email)
- jako Zalogowany Użytkownik mogę edytować moje dane
- jako Niezalogowany Użytkownik mogę się zalogować i wylogować z serwisu

2. __(1 pkt -<|> Tworzenie i edytowanie przepisów)__ Zalogowany użytkownik ma możliwość opublikowania przepisu na posiłek. Przepis powinien zawierać nazwę, listę składników, opis wykonania oraz datę publikacji. Użytkownik może edytować i usuwać przepisy, które stworzył.

3. __(1 pkt -<|> Lista ulubionych przepisów)__ Zalogowany użytkownik ma możliwość dodawania/usuwania przepisu do/z listy ulubionych. Może dowolnie przeglądać listę ulubionych przepisów.
4. __(1 pkt -<|> Wyszukiwanie przepisów)__ Użytkownik (zalogowany i niezalogowany) ma możliwość wyszukania przepis po nazwie lub nazwie użytkownika. Wyszukane przepisy są wyświetlane według najnowszej daty publikacji.
5. __(1 pkt -<|> Przeglądanie profilów innych użytkowników)__ Użytkownik (zalogowany i niezalogowany) może przeglądać przepisy opublikowane na profilu autora. Przepisy są sortowane według najnowszej daty publikacji. 
6. __(2 pkt -<|> Kategorie przepisów)__ Zalogowany użytkownik może dodawać, usuwać i edytować kategorie. Usunięcie kategorii nie powinno spowodować usunięcia przepisu. Autor przepisu może go przypisać do kilku kategorii (np. śniadania, vege), a dowolny użytkownik może wyszukiwać przepisy po kategorii.  
    >Jak to widzę to do tego będą służyć 2 tabele, jedna przechowująca same kategorie w różnych postach (zawiera 3 pozycje, [ID, PostCount, Name]), oraz tabelę zawierającą to jakie kategorie są przypisane postom [ID, PostID, CategoryID].
7. __(1 pkt -<||> Głosowanie na przepisach)__ Zalogowani użytkownicy mogą głosować na przepis (na “+” i “-”). Jeden zalogowany użytkownik może tylko raz zagłosować na wybrany przepis (na + lub na -).
    >Problemem tutaj jest zaimplementowanie drugiej części punktu, czyli tego by użytkownicy głosowali tylko raz na post. Trzeba by przechowywać tablicę postów upvote'owanych przez użytkownika najpewniej w strukturze [ID, UserID, Rating].
- 1. __(0,5 pkt -<|> Ranking)__ Na stronie głównej aplikacji powinny być prezentowane 10 najlepiej ocenianych przepisów.
    >Po zaimplementowaniu głównej części punktu to już jest łatwizną.
8. __(1,5 pkt -<||> Przechowywanie plików użytkowników)__ Przepis może zawierać galerię zdjęć podanych jako linki do grafik z zewnętrznych serwisów lub zdjęć przesłanych bezpośrednio z komputera i przechowywanych w aplikacji.
    >To może okazać się zdradliwe, trzebaby tutaj zaimplementować pobieranie plików od użytkowników i jakąś formę normalizacji tych plików.

9. __(1 pkt -<?> Styl)__ Aplikacja powinna mieć estetyczny wygląd.



# Budżet domowy/firmowy
1. __(1 pkt -<|> Autoryzacja)__ Profil użytkownika :
- jako Niezalogowany Użytkownik mam możliwość rejestracji w serwisie
- jako Niezalogowany Użytkownik mam możliwość zresetowania hasła do własnego profilu; procedura odzyskiwania hasła powinna być bezpieczna (np. przez wysłanie linku aktywacyjnego na adres email)
- jako Zalogowany Użytkownik mogę edytować moje dane
- jako Niezalogowany Użytkownik mogę się zalogować i wylogować z serwisu
2. __(1 pkt -<|> Zakładanie profilów)__ Aplikacja umożliwia założenie profilu firmowego lub osoby prywatnej. Profil firmowy oprócz adresu email i hasła wymaga podania przy rejestracji: nazwy firmy, numeru NIP oraz adresu siedziby. Profil osoby prywatnej wymaga podania dodatkowych pól takich jak: imię, nazwisko oraz numeru telefonu. 
3. __(2 pkt -<|> Tworzenie budżetów)__ Użytkownik może tworzyć dowolną liczbę budżetów. Budżet ma nazwę oraz datę utworzenia. Każdy budżet może mieć listę planów i listę faktycznych wydatków i oszczędności. Plan, wydatek, oszczędności określają pola: nazwa, data utworzenia, kwota (domyślna waluta to złotówki). Użytkownik może edytować i usuwać plany, wydatki, oszczędności oraz budżety, które utworzył. Użytkownik nie może tworzyć budżetów, wydatków, oszczędności z datą w przeszłości 
4. __(0,5 pkt)__ Użytkownik może śledzić stan realizacji wybranego budżetu i prezentować dane z wybranych przedziałów dat (np. od 01.05.2021 do 15.06.2021 r). 
5. __(1,5 pkt -<||> Generowanie pdf'ów)__ Użytkownik ma możliwość wyeksportowania stan wydatków i realizacji budżetu do pliku PDF.
    >Podobne do zadania 1 pkt 8, też jest za 1.5 pkt, śliska sprawa bo nie wiem jak to robić i bym się musiał uczyć.
6. __(1pkt -<||> Tworzenie wykresów)__ Użytkownik może śledzić stan realizacji wybranego budżetu i prezentować dane za pomocą wykresów.
    >Wytworzenie wykresu jako .png i wysłanie go - ez, zrobienie by był interaktywny i zoptymalizowany, to właśnie to co sprawia, że wyceniłem na ||.
7. __(1 pkt)__ Plany, wydatki, oszczędności można przypisać do jednej kategorii. Użytkownik może usuwać i edytować kategorie. Usunięcie kategorii nie powinno spowodować usunięcia planów, wydatków, oszczędności.
8. __(1 pkt)__ Użytkownik powinien móc wyświetlać faktyczny stan realizowania budżety według kategorii.
9. __(1 pkt)__ Użytkownik ma możliwość udostępniania swoich budżetów innym użytkownikom. Użytkownik, który otrzymał dostęp do budżetu może go dowolnie modyfikować, ale nie może usunąć. 
10. __(1 pkt -<?> Styl)__ Aplikacja powinna mieć estetyczny wygląd.

<!-- <style>
body{
    font-family: consolas;
}
</style> -->