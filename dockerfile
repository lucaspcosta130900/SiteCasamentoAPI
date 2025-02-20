# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia o arquivo de projeto e restaura as dependências
COPY SiteCasamentoAPI/SiteCasamentoAPI.csproj ./SiteCasamentoAPI/
RUN dotnet restore ./SiteCasamentoAPI/SiteCasamentoAPI.csproj

# Copia o restante dos arquivos e compila o projeto
COPY SiteCasamentoAPI/ ./SiteCasamentoAPI/
WORKDIR /app/SiteCasamentoAPI
RUN dotnet publish -c Release -o out

# Etapa 2: Configuração da imagem final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia os arquivos da build
COPY --from=build /app/SiteCasamentoAPI/out ./

# Expõe a porta 8080
EXPOSE 8080

# Comando para rodar a aplicação
CMD ["dotnet", "SiteCasamentoAPI.dll"]