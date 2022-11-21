$ErrorActionPreference = 'Stop'

. .\env_vars.ps1

if ($null -eq $env:MSSQL_PASSWORD)
{
    Write-Error "Please set 'MSSQL_PASSWORD' environment variable."
}

& docker build -t "gamelog-mssql:db.dockerfile" --build-arg mssql_password=$env:MSSQL_PASSWORD
