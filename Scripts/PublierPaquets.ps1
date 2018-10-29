# Variables.
$version = (Read-Host -Prompt "Entrez le n° de version").ToString()
$cleApi = (Read-Host -Prompt "Entrez la clé d'API").ToString()
$source = "https://api.nuget.org/v3/index.json"

# Projets.
$cheminProjetInterfaces = "..\ESRI.NetCore.Interfaces\ESRI.NetCore.Interfaces.csproj"
$cheminProjetClient = "..\ESRI.NetCore\ESRIConnect.NetCore.csproj"

# Paquets.
$cheminPaquetInterfaces = "..\ESRI.NetCore.Interfaces\bin\Release\netcoreapp2.0\ESRIConnect.Interfaces." + $version + ".nupkg"
$cheminPaquetClient = "..\ESRI.NetCore\bin\Release\netcoreapp2.0\ESRIConnect." + $version + ".nupkg"

# Paquet ESRIConnect.Interfaces.
Write-Information -MessageData "Création du paquet ESRIConnect.Interfaces..."
dotnet pack -c Release $cheminProjetInterfaces
Write-Information -MessageData "Publication du paquet ESRIConnect.Interfaces..."
dotnet nuget push $cheminPaquetInterfaces -k $cleApi -s $source

# Paquet ESRIConnect.
Write-Information -MessageData "Création du paquet ESRIConnect..."
dotnet pack -c Release $cheminProjetClient
Write-Information -MessageData "Publication du paquet ESRIConnect..."
dotnet nuget push $cheminPaquetClient -k $cleApi -s $source