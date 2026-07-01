# aspnet-e05 &mdash; Programmation d’une application Web (CRUD)

## Création des fichiers ASP.NET Web API
À partir du dossier `aspnet-e05`, exécuter les commandes suivantes :
```sh
cd aspnet-e05
dotnet new webapi -n aspnet05
cd aspnet05
dotnet new gitignore
```

## Port réservé à l’application aspnet-e05
> 5462

## Installation de la librairie _EF Core In-Memory Database Provider_
La librairie _EF Core In-Memory Database Provider_ ne fonctionnera pas en environnement de production seulement avec le cadre d’applications **.NET Runtime** qui est installé sur le serveur de production.
```sh
dotnet --version
dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 8.0.12
dotnet list package
```

## Sous-répertoires et fichiers supplémentaires générés pour réaliser l’application
```
/aspnet05/Controllers/WeatherControllers.cs
/aspnet05/Data/AppDbContext.cs
/aspnet05/Models/Weather.cs
```

## Sous-répertoires reliés à l’application
Voici tous les sous-répertoires reliées à l’application :
```
/home/dev2601/Documents/XD01/aspnet-e05/
/etc/apache2/sites-available/
/etc/systemd/system/
/var/www/aspnet05/
/var/www/html/d003/aspnet-e05/
```

## Création des fichiers Angular 20
À partir du dossier `aspnet-e05`, exécuter les commandes suivantes :
```sh
npx @angular/cli@20 new angular05
```
Au cours de la création des fichiers, sélectionner les options par défaut.

## Fichiers Angular générés pour réaliser l’exercice
```sh
ng generate service services/weather --type=service
ng generate component components/weather --type=component
```

## Activation des applications ASP.NET et Angular
À partir d’un premier terminal, exécuter les commandes suivantes :
```sh
cd aspnet05
dotnet run --urls="http://localhost:5462"
```
L’application ASP.NET est disponible à partir de l’adresse URL suivante :
http://localhost:5462/api/weather

À partir d’un deuxième terminal, exécuter les commandes suivantes :
```sh
cd angular05
ng serve
```
L’application Angular est disponible à partir de l’adresse URL suivante :
http://localhost:4200

## Appel de l’application ASP.NET à partir d’Angular
Dans l’application Angular, il faut absolument appeler l’application ASP.NET à partir d’un chemin relatif. Dans le fichier `/src/app/services/weather.service.ts`, la variable `api` doit contenir le chemin relatif suivant :
```ts
private api="/api/weather";
```

## Publication de l’application ASP.NET sur un serveur Web
À partir du terminal, saisir la commande suivante :
```sh
cd aspnet-e05/aspnet05
dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true
```
Les fichiers de publication sont générés dans le sous-répertoire suivant :
```sh
/aspnet-e05/aspnet05/bin/Release/net8.0/linux-x64/publish
```
Copier les fichiers dans le dossier suivant :
```sh
/var/www/aspnet05/
```
Appliquer les permissions suivantes :
```sh
sudo chown -R www-data:www-data /var/www/aspnet05
```
Tester l’activation de l’application :
```sh
cd /var/www/aspnet
./aspnet05
```
L’application est disponible à partir de l’adresse URL suivante :
```
http://localhost:5462/api/weather
```

## Publication de l’application sur un serveur Web en tant que service
Les fichiers compilés `ASP.NET` doivent être localisés dans le sous-répertoire suivant :
```sh
/var/www/aspnet05/
```
À partir du terminal, saisir la commande suivante :
```sh
sudo nano /etc/systemd/system/aspnet05.service
```
Dans le fichier `aspnet05.service`, intégrer le code suivant :
```conf
[Unit]
Description=ASP.NET 8.0 -- aspnet-e05
After=network.target

[Service]
WorkingDirectory=/var/www/aspnet05
ExecStart=/var/www/aspnet05/aspnet05
Restart=always
RestartSec=10
SyslogIdentifier=aspnet05
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Development
Environment=ASPNETCORE_URLS=http://localhost:5462

[Install]
WantedBy=multi-user.target
```
À partir du terminal, saisir les commandes suivantes :
```sh
sudo systemctl daemon-reload
sudo systemctl enable aspnet05
sudo systemctl start aspnet05
sudo systemctl status aspnet05
```

## Accès à l’application ASP.NET à partir de Apache
Il ne faut pas que le serveur Web Kestrel (celui qui est intégré à ASP.NET Core) soit accessible directement depuis l’extérieur, comme un serveur Web public. Les fichiers doivent être localisés dans le sous-répertoire `/var/www/aspnet05`, et non dans le sous-répertoire `/var/www/html/aspnet05`.