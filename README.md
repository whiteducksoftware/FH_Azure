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

![Settings](https://github.com/whiteducksoftware/FH_Azure/blob/master/docs/imgs/func.png?raw=true "Settings")

Lokal ausführen und mit Bildupload testen.

**ÜBUNG**

- Bild prozessieren und in einen neuen BlobContainer ablegen
- Azure Function deployen

Tipps:

Bildbearbeitung: Nuget Package &quotImageSharp&quot; [https://github.com/SixLabors/ImageSharp](https://github.com/SixLabors/ImageSharp) (siehe API Beispiel)

Analog zum Controller in der Middleware das prozessierte Bild in einen neuen Container im Blob ablegen.

Die Appsetting &quotFileBlob&quot; muss nach dem deployen in die Appsettings der &quotonline&quot; Function App eingetragen werden.
