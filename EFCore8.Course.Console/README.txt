Migration commands (dotnet CLI):
dotnet tool install dotnet-ef

.ajouter une migration:
add-migration MigrationName

.appliquer la migration a la base de donnée
dotnet ef database update

.appliquer la migration dans un script
dotnet ef migrations script

.générer la migartion sql 
(options: -idempotent: vérifie qu'un élément existe avant de le créer
MigrationName, commencera sa migration après la migration MigrationName):
script-migration -idempotent
script-migration MigrationName
