Innhold

Vi lager en backend med c# og .NET rammeverk. Vi har da lagd et prosjekt med ASP.NET med MVC som vi bruker heksagonal arkitektur i.

I mappen Backend vil du finne:

Core (kjerne mappen)

-- Domain (kjerne)
Dette er her vi skriver klasser som vi bygger videre på i ports og adaptere.

-- Ports (interface)
Her skriver vi interface til repository og services. Repository er der logikk for lagring skal skje mens Service er det som skal bli brukt på klient siden.

-- Service
Det er her vi lager en service klasse som imlementerer et interface fra ports. Her lager vi funksjoner som for eksempel å lage, slette eller oppdatere ting.
Service er det som vi kommer til å bruke i Controller i MVC.

-- DTO (Data Transfer Object)
Her skal DTO klasser lage som kun skal sende ting fra serveren til klienten.

--------------------------------------

Infrastructure (Repository)
Det er her vi lager metoder som vil lagre ting i serveren.

-- FileStorage
Her lager vi klasser som skal lagre json-filer for de forskjellige klassene.

--------------------------------------

Tests
Det er her vi kommer til å lage tester for prosjektet vårt.

--------------------------------------------------------------------------------

Nå kommer vi til MVC strukturen

Controller ()
Det er her vi lager api til nettsiden og funksjoner den kan bruke.

Views 
Her lager vi cshtml til den controlleren viewmappen tilhører. 
For eksempel Views/Device hører til Controller/DeviceController.

wwwroot
Her kan vi lage css og javascrip til nettsiden, og vi kan også legge til bilder som vi kan bruke.

--------------------------------------

Hvordan starte prosjektet:

Først må du starte Program.cs i Backend som starter serveren. Dette kan du gjøre gjennom terminalen eller om du bruker Visual Studio 2022 kan du trykke på start om du er i Program.cs. Etter det må du finne mappen gui og finne Form1.cs og starte den.

Etter du har startet serveren kan du åpne en nettleser og lime inn http://localhost:5013 eller hvis det står en annen port i terminalen så bruker du den. Her vil du kunne se hvilken IoT-enheter du kan koble deg til. Da har du muligheten til å legge disse i en gruppe eller flere. Etter du har lagd grupper kan du så sende disse over til GUIen som vi har laget.

Når du har trykket på send vil du kunne se i GUIen en bekreftelse at den har fått filen. Nå kan du gå inn på "nettsiden som har testlys" for å teste GUIen og se om du kan skru av og på de forskjellige lysene.

Dette vil simulere produktet vårt på en best mulig måte der GUIen er fjernkontrollen. Enhetene du ser på nettsiden skal være enheter som er koblet på det samme nettverket fjernkontrollen er koblet til, og så har du muligheten til å lage grupper så du slipper å skru av og på hver enkel enhet.

