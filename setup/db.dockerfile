ARG mssql_password
FROM mcr.microsoft.com/mssql/server:2022-latest

ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=$mssql_password
ENV MSSQL_TCP_PORT=1433

EXPOSE 1433
