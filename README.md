# Azure WebApps + Storage + Functions Hands On

Folgender UseCase wird implementiert:

- Angular Frontend mit einfachem ImageUpload
- ImageUpload in BlobStorage über ASP.NET Core Middleware
- Azure Function
  - Trigger auf Änderungen im Blob Storage
  - Resizen hochgeladener Bilder auf Thumbnail Größe
  - Ablegen im Thumbnail Container im Blob Storage

**Requirements:**

- Node.js

[https://nodejs.org/en/](https://nodejs.org/en/)

- .NET Core

[https://www.microsoft.com/net/download](https://www.microsoft.com/net/download)

- Angular CLI
```
npm install -g @angular/cli
```

- Azure Resources (Web App + Storage + Function App)
- Azure SDK (Visual Studio / Tools / Get Tools and Features)
- Azure Functions and WebJob Tools (Visual Studio / Tools / Extension and Updates)



## 1 Project scaffolding

Anlegen eines Angular Projekts
```
dotnet new angular --name file-upload
```

Hinzufügen des Azure Storage Package
Im Ordner &quot;file-upload&quot;

```
dotnet add package WindowsAzure.Storage
```
Hinzufügen und Erstellen von benötigten Komponenten und Modulen
Im Ordner &quot;file-upload\ClientApp&quot;
```
npm install
ng g component fileUpload
npm install primeng --save
npm install primeicons --save
```

Projekt mit Visual Studio öffnen und ausführen.

## 2 Middleware Controller

appsettings.json:
```
"ConnectionStrings": {
    "StorageAccount": "XXX"
  },
```

Im Ordner &quot;Contollers&quot; neuen &quot;FileUploadController.cs&quot; erstellen:

![Controller](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs/controller.png?raw=true "Controller")

## 3 Frontend

Bereitstellen des IconSets und Styles in &quot;.angular-cli.json&quot;

![Styles](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs/styles.png?raw=true "Styles")

Bereitstellen benötigter Module und Komponenten in &quot;app.module.ts&quot;

![Modules](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs/appmodule.png?raw=true "Modules")

Implementierung eines Menüverweis zum FileUpload in &quot;nav-menu.component.html&quot;

![Navigation](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs/nav.png?raw=true "Modules")

Implementierung des FileUpload Controls in &quot;file-upload.component.html&quot;

![Upload](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs/nav.png?raw=true "Upload")

## 4 Deployment der WebApp

Projektkontextmenü - Publish

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs/publish.png?raw=true "Publish")

## 5 Azure Functions

Neues Azure Functions Projekt erstellen

![Azure Function](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs/func.png?raw=true "Azure Function")

In &quot;local.settings.json&quot; ConnectionString hinzufügen

![Settings](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs/funcsettings.png?raw=true "Settings")

Lokal ausführen und mit Bildupload testen.

**ÜBUNG**

- Bild prozessieren und in einen neuen BlobContainer ablegen
- Azure Function deployen

Tipps:

Bildbearbeitung: Nuget Package &quotImageSharp&quot; [https://github.com/SixLabors/ImageSharp](https://github.com/SixLabors/ImageSharp) (siehe API Beispiel)

Analog zum Controller in der Middleware das prozessierte Bild in einen neuen Container im Blob ablegen.

Die Appsetting &quotFileBlob&quot; muss nach dem deployen in die Appsettings der &quotonline&quot; Function App eingetragen werden.


# Monitor Web App und Azure Function Hands On

Für den im Azure WebApps + Storage + Functions Hands On implementierten UseCase wird ein Monitoring implementiert.

- Hinzufügen von Application Insights zur Middleware
- Hinzufügen von Application Insights für Azure Functions
  - Auswerten eines fehlerhaften Uploads
- Custom Logging
  - Hinzufügen von Custom Monitoring
    - In der Middleware mit dem TelemetryClient
  - In der UI
    - Tracken von Page Views mit einem custom monitoring service
- Anzeigen/Auswerten im Portal

**Requirements:**

- Application Insights Resourcen (3 Stück) in eigener RG erstellen und sprechend benennen.
  - Middleware
    - Application Insights für die ASP.NET web application erstellen
  - Function
    - Application Insights für die ASP.NET web application erstellen
  - UI
    - Application Insights für Node.js application erstellen (UI)

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs2/CreateAppinsights.png?raw=true "Publish")

## 1 Implementieren von Application Insights in die Middleware

In Visual Studio in die File Upload App wechseln

Unter Manage NuGet Packages App Insights hinzufügen und auf aktuelle Version (2.5.1) prüfen

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs2/AddNuGetPackage.png?raw=true "Publish")

Startup.cs

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs2/Startup.png?raw=true "Publish")

Program.cs

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs2/Program.png?raw=true "Publish")

Appsettings.json (Instrumentation Key der richtigen App Insights Resource eintragen)

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs2/Appsettings.png?raw=true "Publish")

Ausführen

- Lokal ausführen und prüfen ob in der Cloud App Insights resource etwas ankommt (Kann ein wenig Zeit dauern)
- Wenn es funktioniert die App wieder Publishen
- Dann Online testen

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs2/Publish.png?raw=true "Publish")

## 2 Implementieren von Application Insights in die Middleware

Kopieren des Instrumentation Key der Azure Function Application Insights resource

In der Azure Function --> Application Settings

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs2/func1.png?raw=true "Publish")

Add new setting

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs2/func2.png?raw=true "Publish")

**ÜBUNG**

Zu der Azure Function Application Insights resource wechseln

- Bilder hochladen und prüfen ob App Insights etwas registriert
- Eine Datei (Kein Bild!) hochladen z.B .pdf
- Die Ursache für einen Fehler in der function mithilfe von App Insights suchen (Wo steht wieso der Dateiupload fehlgeschlagen ist)


## 3 Implementieren von Custom Events mit dem Telemetry Client

FileUploadController.cs (Den Filenamen und die Filegröße mit einem Upload Event tracken)

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs2/FileUploadController.png?raw=true "Publish")

## 4 Implementieren von Application Insights für die UI (Custom Logging)

Environment.ts

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs2/environment.png?raw=true "Publish")

Environment.prod.ts

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs2/environment-prod.png?raw=true "Publish")

Under Ordner app --> neuen Ordner services erstellen und monitoring service files anlegen

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs2/services.png?raw=true "Publish")

Monitoring.service.spec.ts

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs2/monitoring-service-spec.png?raw=true "Publish")

monitoring.service.ts

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs2/monitoring-service.png?raw=true "Publish")

App.module.ts

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs2/appmodule.png?raw=true "Publish")

Counter.components.ts

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs2/counter-component.png?raw=true "Publish")

Fetch-data.component.ts

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs2/fetch-data-component.png?raw=true "Publish")

File-upload.components.ts

![Publish](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs2/file-upload-component.png?raw=true "Publish")

Ausführen

- Lokal und prüfen in App Insights was angezeigt wird
- Publishen und nochmals prüfen



