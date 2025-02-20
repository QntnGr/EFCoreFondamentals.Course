Migration commands (dotnet CLI):
dotnet tool install dotnet-ef

.ajouter une migration:
dotnet ef migrations add

.appliquer la migration a la base de donnée
dotnet ef database update

.appliquer la migration dans un script
dotnet ef migrations script

.générer la migartion sql:
script-migration