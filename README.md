To setup the db do the following:

1. Create a database called `SOFT703A2` in sqlserver
2. Run from the root of the project:

``` 
dotnet ef database update --project SOFT703A2.Infrastructure --startup-project SOFT703A2.WebApp
``` 

To create new features:

1. Create a new branch from main with the pattern `feature/<feature-name>`
2. Create a pull request to merge back into main
3. Once approved, merge into main
4. Delete the branch
5. DO NOT PUSH TO MAIN DIRECTLY
