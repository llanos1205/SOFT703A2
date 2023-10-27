dotnet ef migrations add initial -o ./Persistence/Migrations --project SOFT703A2.Infrastructure --startup-project SOFT703A2.WebApp

dotnet ef update --project SOFT703A2.Infrastructure --startup-project SOFT703A2.WebApp