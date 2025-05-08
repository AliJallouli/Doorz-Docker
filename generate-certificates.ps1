[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

# Définir les chemins
$basePath = "C:\Users\alija\Desktop\Doorz"
$backendCertsPath = Join-Path $basePath "Doors-BE\WebApi\certs"
$frontendPath = Join-Path $basePath "Doors-FE"
$gitBashPath = "C:\Program Files\Git\bin\bash.exe" # Ajustez si Git Bash est installé ailleurs

# Vérifier que Git Bash existe
if (-not (Test-Path $gitBashPath)) {
    Write-Host "Erreur : Git Bash non trouvé à $gitBashPath. Veuillez vérifier l'installation." -ForegroundColor Red
    exit 1
}

# Fonction pour exécuter des commandes via Git Bash
function Invoke-GitBashCommand {
    param (
        [string]$WorkingDirectory,
        [string[]]$Commands
    )

    try {
        Write-Host "Exécution des commandes dans $WorkingDirectory via Git Bash..." -ForegroundColor Cyan

        # Créer un script Bash temporaire
        $tempScript = [System.IO.Path]::GetTempFileName() + ".sh"
        $bashCommands = @"
#!/bin/bash
cd "$WorkingDirectory"
$($Commands -join "`n")
"@
        Set-Content -Path $tempScript -Value $bashCommands -Encoding UTF8

        # Exécuter le script via Git Bash
        $process = Start-Process -FilePath $gitBashPath -ArgumentList "--login -i -c `"bash '$tempScript'`"" -NoNewWindow -Wait -PassThru
        if ($process.ExitCode -ne 0) {
            Write-Host "Erreur : Échec de l'exécution des commandes Git Bash dans $WorkingDirectory. Code de sortie : $($process.ExitCode)" -ForegroundColor Red
        } else {
            Write-Host "✅ Commandes exécutées avec succès dans $WorkingDirectory" -ForegroundColor Green
        }

        # Supprimer le script temporaire
        Remove-Item -Path $tempScript -Force -ErrorAction SilentlyContinue
    } catch {
        Write-Host "Erreur lors de l'exécution des commandes Git Bash : $_" -ForegroundColor Red
    }
}

# 1. Certificat Backend
Write-Host "=== Gestion des certificats pour Doors-BE ===" -ForegroundColor Cyan

# Vérifier que le dossier certs existe
if (-not (Test-Path $backendCertsPath)) {
    Write-Host "Création du dossier $backendCertsPath..." -ForegroundColor Cyan
    New-Item -ItemType Directory -Path $backendCertsPath -Force | Out-Null
}

# Supprimer tous les fichiers dans le dossier certs
Write-Host "Suppression des fichiers dans $backendCertsPath..." -ForegroundColor Cyan
Get-ChildItem -Path $backendCertsPath -File -Force | ForEach-Object {
    $filePath = $_.FullName
    Write-Host "Suppression de : $filePath" -ForegroundColor Yellow
    try {
        Remove-Item -Path $filePath -Force -ErrorAction Stop
        Write-Host "✅ Supprimé : $filePath" -ForegroundColor DarkGreen
    } catch {
        Write-Host "⚠️ Erreur lors de la suppression de $filePath : $_" -ForegroundColor Red
    }
}

# Exécuter les commandes Git Bash pour le backend
$backendCommands = @(
    "mkcert -cert-file localhost.pem -key-file localhost-key.pem localhost",
    "openssl pkcs12 -export -out localhost.pfx -inkey localhost-key.pem -in localhost.pem -passout pass:password"
)
Invoke-GitBashCommand -WorkingDirectory $backendCertsPath -Commands $backendCommands

# 2. Certificat Frontend
Write-Host "=== Gestion des certificats pour Doors-FE ===" -ForegroundColor Cyan

# Vérifier que le dossier Doors-FE existe
if (-not (Test-Path $frontendPath)) {
    Write-Host "Erreur : Le dossier $frontendPath n'existe pas." -ForegroundColor Red
    exit 1
}

# Exécuter la commande Git Bash pour le frontend
$frontendCommands = @(
    "mkcert localhost"
)
Invoke-GitBashCommand -WorkingDirectory $frontendPath -Commands $frontendCommands

# Vérifier les fichiers générés
Write-Host "Vérification des fichiers générés..." -ForegroundColor Cyan
Write-Host "Contenu de $backendCertsPath :" -ForegroundColor Cyan
Get-ChildItem -Path $backendCertsPath -Force | ForEach-Object {
    Write-Host "  - $($_.FullName) (Dossier: $($_.PSIsContainer))" -ForegroundColor Gray
}
Write-Host "Contenu de $frontendPath (fichiers certificats) :" -ForegroundColor Cyan
Get-ChildItem -Path $frontendPath -Force | Where-Object { $_.Name -match "\.pem$|\.key$" } | ForEach-Object {
    Write-Host "  - $($_.FullName) (Dossier: $($_.PSIsContainer))" -ForegroundColor Gray
}

Write-Host "Script terminé avec succès !" -ForegroundColor Green