$ErrorActionPreference = 'Stop'

. .\env_vars.ps1

if ($null -eq $env:MSSQL_PASSWORD)
{
    Write-Error "Please set 'MSSQL_PASSWORD' environment variable."
}

$mssqlPort = 1433

Write-Host "Building image..."
& docker build -t gamelog-mssql -f mssql.dockerfile --build-arg mssql_password=$env:MSSQL_PASSWORD .

Write-Host "Starting container..."
$containerId = & docker run -p $mssqlPort`:1433 -d gamelog-mssql
Write-Host "Container $containerId started."

Write-Host "Waiting until the container starts..."
Start-Sleep -Seconds 10

Write-Host "Creating database..."
& docker exec $containerId /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "$env:MSSQL_PASSWORD" -Q "CREATE DATABASE GameLog"

Write-Host "Done."
