Projekt für Container vorbereiten:

dotnet restore test.csproj
dotnet build test.csproj
dotnet publish -c Release -o publish_output test.csproj


