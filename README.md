# BokningssystemGrupp6


# Inlämningsuppgift: Bokningssystem för skolans lokaler

## Introduktion
I denna inlämningsuppgift ska ni utveckla ett bokningssystem för skolans olika lokaler.
Systemet ska hantera bokning av både salar och grupprum. Applikationen ska byggas med
objektorienterade principer och moderna C#-funktioner. Viss data ska persisteras mellan
körningar genom att sparas i filer(detta kommer Fredrik att gå igenom senare).

## Tekniska krav
Följande koncept ska implementeras i lösningen:

### 1. Objektorienterad programmering
  - Använd inkapsling med properties
  - Implementera konstruktorer på lämpligt sätt

### 3. Arv
  - Skapa en basklass Lokal med gemensamma egenskaper och metoder
  - Skapa minst två ärvande klasser: Sal och Grupprum med specifika egenskaper
  - Använd override för att anpassa metoder i ärvande klasser

### 5. Interface
  - Skapa ett IBookable interface med metoder för bokning
  - Implementera interfacet i relevanta klasser
  - Använd interface som returtyp där lämpligt

### 7. DateTime
  - Hantera start- och sluttid för bokningar
  - Formatera datum och tid på användarvänligt sätt

### 8. TimeSpan
  - Längd på bokning

### 10. Listor och Collections
  - Använd List<T> för att lagra bokningar och lokaler när programmet körs.
  - Implementera operationer för filtrering och sökning
  - Hantera sortering av bokningar

### 12. Filhantering
  - Implementera persistens genom att spara lokalerna i fil.
  - Läs in data när programmet startar


## Kundkrav

### Bokningar
  1. Som användare vill jag kunna skapa skapa ny bokningar
  2. Som användare vill jag kunna lista alla bokningar
  3. Som användare vill jag kunna uppdatera en bokning
  4. Som användare vill jag kunna ta bort en bokning
  5. Som användare vill jag kunna lista bokningar från ett specifikt år.

### Lokaler
  6. Som användare vill jag kunna lista alla salar, med lämpliga egenskaper listade
  7. Som användare vill jag förhindra att salar har samma namn
  8. Som användare vill jag att alla salar ska lagras i en fil mellan programstarter
  9. Som användare vill jag kunna skapa nya salar.

## Bedömningskriterier
  * Korrekt implementering av alla tekniska krav
  * Implementering av alla kundkrav
  * Väl strukturerad och läsbar kod
  * Väl strukturerad mappstruktur
  * Effektiv felhantering och validering
  * Dokumentation och kommentarer
### Inlämning
  * Vecka 46 Tisdag 23:59
  * Kompletta källkodsfiler på Omniway
    - En per grupp lämnar in fil
    - De andra skriver in gruppnamn och vem som skickat in uppgiften.
    - Kör “Clean Solution” under “Build” i menyn av Visual Studio. Innan zippning av filer. Detta för att undvika att ni skickar med buildfiler som inte behövs.
  * Länk till publikt github repo
  * Kort dokumentation som beskriver:
    - Hur man startar och använder programmet
    - Eventuella kända begränsningar
    - Val och motiveringar för implementation
    - Beskrivning av filformat och struktur
    - Vilken student har huvudansvaret för vilka delar
    - Detta kan även läggas till i Readme som komplement.

## Tips
Om ni vill prova på att ha er planering av projektet i trello. https://trello.com/ Det är vanligt att man jobbar med en board i arbetslivet. Gustav visar DevOps en mer avancerad variant av Trello. https://dev.azure.com/berggustav/Planera/_boards/board/t/Planera%20Team/Stories
Tips Git
- Använd .gitignore(Visual Studio template) för att exkludera onödiga filer
- Bestäm branch-namngivningskonvention
- Best practice: Använd beskrivande branch-namn
- T.ex: `feature/login-system` eller `bugfix/validation-error`
- Bestäm commit-meddelandekonvention
- T.ex. `#{kundkravsnummer} - {Vilka ändringar har skett}`

## Git flöde
1. Ägaren skapar ett repository på github.
2. Ägaren klonar repot.
3. Ägaren lägger till ett konsolprojekt i samma mapp som projektet.
4. Ägaren gör en commit och push.
5. Ägaren till teamet som collaborators.
6. Teamet klonar repot.
7. Alla skapar en branch för sina uppgift.
8. Alla gör regelbundna commits/push med beskrivande meddelande till sina egna
brancher.
9. När en uppgift är klar.
-
1. Pull på senaste main.
2. Merge på main in i egen branch i Visual Studio. Lös eventuella
mergekonflikter.
3. Commit och push.
4. Skapa PR egen branch into main.
5. Meddela alla att PR finns att kolla på.
6. Alla kollar på koden.
7. Sedan approve PR.
8. Pull på main testa att allt funkar i denna.
9. Börja om på steg 7.


---

## Filformat och kodstruktur
- `**BokningssystemGrupp6/`** Huvudmap med alla filer och dokument i projektet. 
  - `**BokningssystemGrupp6/- **` Mapp med projektets kodfiler som används för att köra projektet.
    - `**Classes/- **` Mapp med klasser som utför olika funktioner i bokningssystemet
      - `**LokalClasses/- **` Mapp med klasser som utför saker som har med rum och lokaler att göra.
        - `**Classroom.cs- **` Innehåller kod för klassrum, ärver från Rooms-klassen.
        - `**GroupRoom.cs- **` Innehåller kod för grupprum, ärver från Rooms-klassen.
        - `**Hall.cs- **` Innehåller kod för salar, ärver från Rooms-klassen.
        - `**Rooms.cs- **` Bas-klass med egenskaper och metoder för rum (Rooms), så som att skapa rum, visa rum, etcetera. Rummens data sparas i en JSON-fil
        - `**RoomsListAndSort.cs- **` Kod för att sortera och visa rummen i olika ordningar, som antal platser, rummens egenskaper.
      - `**Bookings.cs- **` Innehåller metoder för att boka, ändra, ta bort och visa bokningar. Sparar bokningar i JSON-fil
      - `**InputValidation.cs- **` Metoder för inputvalidering, ex. kontroll av siffervärden, input format, etc.
      - `**Menu.cs- **` Huvudmenyn och den som först visas när man kör programmet och visar de funktioner som man kan utföra.
      - `**RoomsConverter.cs- **` Utför hanteringen av polymorf serialisering och deserialisering för att kunna skapa en lista där olika typer av underklasser där de ärver från en gemensam klass med där de kan innehålla olika mycket information. Den används för att både läsa och skriva till JSON-filerna.
      - `**Save.cs- **` Hantering av att läsa och spara objektlistor (rum och bokningar) i JSON-filerna.
    - `**Interfaces/- **` Mapp för interface-filer.
      - `**IListable.cs- **` Interface som definierar metoder som används i flera klasser.
      - `**IRoom.cs- **` Interface som definierar egenskaper för rum.
    - `**BokningssystemGrupp6.csproj- **` Projektfil för byggprocessen.
    - `**Programs.cs- **` Startfil, skapar objekt och listor som används i de andra filerna. Deserialiserar JSON-filerna och startar sedan MainMenu metoden.
  - `**.gitignore- **` Gitignore-fil med grundinställningar för Visual Studio-projekt.
  - `**BokningssystemGrupp6.sln- **` Lösningsfil för hela C#-projektet.
  - `**README.md- **` Projektbeskrivning och instruktioner för att köra programmet.

---


