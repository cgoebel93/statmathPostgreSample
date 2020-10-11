FROM mcr.microsoft.com/dotnet/core/sdk:3.1
 
EXPOSE 5432

WORKDIR /app
 
# Alles kopieren
COPY . ./
 
# Alles im out Ordner bauen
RUN dotnet publish -c Release -o out
ENTRYPOINT ["dotnet", "out/statmathPostgreSample.dll"]