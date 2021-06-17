# Plan projektu

1. __(1 pkt -<|> Autoryzacja)__ Profil użytkownika :
- [x] jako Niezalogowany Użytkownik mam możliwość rejestracji w serwisie
- [ ]  jako Niezalogowany Użytkownik mam możliwość zresetowania hasła do własnego profilu; procedura odzyskiwania hasła powinna być bezpieczna (np. przez wysłanie linku aktywacyjnego na adres email)
- [ ]  jako Zalogowany Użytkownik mogę edytować moje dane
- [x]  jako Niezalogowany Użytkownik mogę się zalogować i wylogować z serwisu


2. [x]  __(1 pkt -<|> Tworzenie i edytowanie przepisów)__ Zalogowany użytkownik ma możliwość opublikowania przepisu na posiłek. Przepis powinien zawierać nazwę, listę składników, opis wykonania oraz datę publikacji. Użytkownik może edytować i usuwać przepisy, które stworzył.

3. [x]  __(1 pkt -<|> Lista ulubionych przepisów)__ Zalogowany użytkownik ma możliwość dodawania/usuwania przepisu do/z listy ulubionych. Może dowolnie przeglądać listę ulubionych przepisów.
4. [x]  __(1 pkt -<|> Wyszukiwanie przepisów)__ Użytkownik (zalogowany i niezalogowany) ma możliwość wyszukania przepis po nazwie lub nazwie użytkownika. Wyszukane przepisy są wyświetlane według najnowszej daty publikacji.
5. [x]  __(1 pkt -<|> Przeglądanie profilów innych użytkowników)__ Użytkownik (zalogowany i niezalogowany) może przeglądać przepisy opublikowane na profilu autora. Przepisy są sortowane według najnowszej daty publikacji.
6. [x]  __(2 pkt -<|> Kategorie przepisów)__ Zalogowany użytkownik może dodawać, usuwać i edytować kategorie. Usunięcie kategorii nie powinno spowodować usunięcia przepisu. Autor przepisu może go przypisać do kilku kategorii (np. śniadania, vege), a dowolny użytkownik może wyszukiwać przepisy po kategorii.
    > Jeszcze ewentualnie można by podpiąć jakiego ajax'a by użytkownik mógł przeszukiwać kategorie, aleeee...
7. [x]  __(1 pkt -<||> Głosowanie na przepisach)__ Zalogowani użytkownicy mogą głosować na przepis (na “+” i “-”). Jeden zalogowany użytkownik może tylko raz zagłosować na wybrany przepis (na + lub na -).
- 1. [x]  __(0,5 pkt -<|> Ranking)__ Na stronie głównej aplikacji powinny być prezentowane 10 najlepiej ocenianych przepisów.
8. [ ]  __(1,5 pkt -<||> Przechowywanie plików użytkowników)__ Przepis może zawierać galerię zdjęć podanych jako linki do grafik z zewnętrznych serwisów lub zdjęć przesłanych bezpośrednio z komputera i przechowywanych w aplikacji.
    >Nie udało nam się tego kompletnie zrealizować.

9. [?]  __(1 pkt -<?> Styl)__ Aplikacja powinna mieć estetyczny wygląd.