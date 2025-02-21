Migration commands (dotnet CLI):
dotnet tool install dotnet-ef

.ajouter une migration:
add-migration MigrationName

.supprimer une migration pas encore appliqué a la bdd:
remove-migration

.appliquer la migration a la base de donnée
update-database

.générer la migartion sql 
(options: -idempotent: vérifie qu'un élément existe avant de le créer
MigrationName, commencera sa migration après la migration MigrationName):
script-migration -idempotent
script-migration MigrationName
