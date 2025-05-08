[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

# Définir les chemins sources
$backendSource = "C:\Users\alija\RiderProjects\Doors-BE"
$frontendSource = "C:\Users\alija\WebstormProjects\Doors-FE"

# Répertoire cible (où se trouve ce script)
$targetRoot = $PSScriptRoot
$backendTarget = Join-Path $targetRoot "Doors-BE"
$frontendTarget = Join-Path $targetRoot "Doors-FE"

# Exclusions (exclut certs et fichiers associés pour les deux projets)
$frontendExcludePatterns = @(
    ".angular", ".idea", ".vscode", "node_modules", "dist",
    ".git", ".gitignore", "*.crt", "*.key", "*.pem"
)
$backendExcludePatterns = @(
    "bin", "obj", ".vs", ".idea",
    ".git", ".gitignore", ".editorconfig",
    "publish", "TestResults", "*.user", "*.suo", "*.log",
    "*.crt", "*.key", "*.pem", "certs"
)

# Fonction pour copier avec filtres d’exclusion
function Copy-WithFilter {
    param (
        [string]$SourcePath,
        [string]$TargetPath,
        [string[]]$ExcludePatterns
    )

    try {
        Write-Host "Vérification du chemin source : $SourcePath" -ForegroundColor Cyan
        if (-not (Test-Path $SourcePath)) {
            Write-Host "Erreur : Le chemin source $SourcePath n'existe pas." -ForegroundColor Red
            return
        }

        # Vérifier le contenu du répertoire source
        Write-Host "Contenu de $SourcePath :" -ForegroundColor Cyan
        $sourceItems = Get-ChildItem -Path $SourcePath -Force -ErrorAction Stop
        Write-Host "Nombre d'éléments dans $SourcePath : $($sourceItems.Count)" -ForegroundColor Cyan
        if ($sourceItems.Count -eq 0) {
            Write-Host "Attention : Le répertoire $SourcePath est vide." -ForegroundColor Yellow
            return
        }
        $sourceItems | ForEach-Object { Write-Host "  - $($_.FullName) (Dossier: $($_.PSIsContainer))" -ForegroundColor Gray }

        # Supprimer tout le contenu du dossier cible
        Write-Host "Nettoyage contrôlé du dossier cible : $TargetPath" -ForegroundColor Cyan
        Write-Host "Valeur de TargetPath : $TargetPath" -ForegroundColor Magenta
        $targetDirName = Split-Path $TargetPath -Leaf
        Write-Host "Nom du dossier cible : $targetDirName" -ForegroundColor Magenta

        if (-not (Test-Path $TargetPath)) {
            Write-Host "Création du dossier cible : $TargetPath" -ForegroundColor Cyan
            New-Item -ItemType Directory -Path $TargetPath -Force -ErrorAction Stop | Out-Null
        } else {
            Write-Host "Contenu de $TargetPath avant nettoyage :" -ForegroundColor Cyan
            $targetItems = Get-ChildItem -Path $TargetPath -Force -Recurse
            $targetItems | ForEach-Object { Write-Host "  - $($_.FullName) (Dossier: $($_.PSIsContainer))" -ForegroundColor Gray }

            Write-Host "Suppression de tous les éléments dans $TargetPath..." -ForegroundColor Cyan
            $targetItems | ForEach-Object {
                $item = $_
                $fullPath = $item.FullName
                $itemName = $item.Name

                Write-Host "Analyse de l'élément pour suppression : $fullPath (Nom: $itemName, Dossier: $($item.PSIsContainer))" -ForegroundColor Gray
                Write-Host "Suppression de : $fullPath" -ForegroundColor Yellow

                try {
                    Remove-Item -Path $fullPath -Recurse -Force -ErrorAction Stop
                    Write-Host "✅ Supprimé : $fullPath" -ForegroundColor DarkGreen
                } catch {
                    Write-Host "⚠️ Erreur lors de la suppression de $fullPath : $_" -ForegroundColor Red
                }
            }
        }

        # Vérifier que le dossier cible est vide après nettoyage
        $remainingItems = Get-ChildItem -Path $TargetPath -Force -Recurse
        if ($remainingItems) {
            Write-Host "⚠️ Attention : Des éléments restent dans $TargetPath après nettoyage :" -ForegroundColor Red
            $remainingItems | ForEach-Object { Write-Host "  - $($_.FullName) (Dossier: $($_.PSIsContainer))" -ForegroundColor Red }
        } else {
            Write-Host "✅ $TargetPath est vide après nettoyage." -ForegroundColor Green
        }

        # Parcourir les fichiers et dossiers pour la copie
        Write-Host "Début de la copie des fichiers depuis $SourcePath..." -ForegroundColor Cyan
        $copiedCount = 0
        $excludedCount = 0

        Get-ChildItem -Path $SourcePath -Recurse -Force -ErrorAction Stop | ForEach-Object {
            $item = $_
            $fullPath = $item.FullName
            $relativePath = $fullPath.Substring($SourcePath.Length + 1)
            $dest = Join-Path $TargetPath $relativePath
            $itemName = $item.Name

            Write-Host "Analyse de l'élément pour copie : $fullPath (Nom: $itemName, Dossier: $($item.PSIsContainer), Chemin relatif: $relativePath)" -ForegroundColor Gray

            # Vérifier les exclusions
            $isExcluded = $false
            foreach ($pattern in $ExcludePatterns) {
                $patternRegex = [regex]::Escape($pattern).Replace("\*", ".*").Replace("\?", ".")
                if ($relativePath -match "([\\/]|^)" + $patternRegex + "([\\/]|$)" -or $itemName -like $pattern) {
                    Write-Host "Exclu : $relativePath (motif : $pattern)" -ForegroundColor Yellow
                    $excludedCount++
                    $isExcluded = $true
                    break
                }
            }

            # Copier si non exclu
            if (-not $isExcluded) {
                Write-Host "Copie : $relativePath" -ForegroundColor Blue
                try {
                    if ($item.PSIsContainer) {
                        New-Item -ItemType Directory -Path $dest -Force -ErrorAction Stop | Out-Null
                    } else {
                        $parentDir = Split-Path $dest -Parent
                        if (-not (Test-Path $parentDir)) {
                            New-Item -ItemType Directory -Path $parentDir -Force -ErrorAction Stop | Out-Null
                        }
                        Copy-Item -Path $fullPath -Destination $dest -Force -ErrorAction Stop
                    }
                    $copiedCount++
                    Write-Host "✅ Copié : $relativePath" -ForegroundColor DarkGreen
                } catch {
                    Write-Host "⚠️ Erreur lors de la copie de $relativePath : $_" -ForegroundColor Red
                }
            }
        }

        Write-Host "Résultat : $copiedCount élément(s) copié(s), $excludedCount élément(s) exclu(s)." -ForegroundColor Green
    } catch {
        Write-Host "Erreur dans Copy-WithFilter : $_" -ForegroundColor Red
    }
}

# Exécution
try {
    Write-Host "Copie de Doors-BE vers $backendTarget ..." -ForegroundColor Cyan
    Copy-WithFilter -SourcePath $backendSource -TargetPath $backendTarget -ExcludePatterns $backendExcludePatterns

    Write-Host "Copie de Doors-FE vers $frontendTarget ..." -ForegroundColor Cyan
    Copy-WithFilter -SourcePath $frontendSource -TargetPath $frontendTarget -ExcludePatterns $frontendExcludePatterns

    Write-Host "Copie terminée avec succès !" -ForegroundColor Green
} catch {
    Write-Host "Erreur lors de l'exécution du script : $_" -ForegroundColor Red
}