#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CloudTicTacToeBackend/CloudTicTacToe.API/CloudTicTacToe.API.csproj", "CloudTicTacToe.API/"]
COPY ["CloudTicTacToeBackend/CloudTicTacToe.Application/CloudTicTacToe.Application.csproj", "CloudTicTacToe.Application/"]
COPY ["CloudTicTacToeBackend/CloudTicTacToe.Domain/CloudTicTacToe.Domain.csproj", "CloudTicTacToe.Domain/"]
COPY ["CloudTicTacToeBackend/CloudTicTacToe.Infrastructure/CloudTicTacToe.Infrastructure.csproj", "CloudTicTacToe.Infrastructure/"]
RUN dotnet restore "CloudTicTacToe.API/CloudTicTacToe.API.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "CloudTicTacToe.API/CloudTicTacToe.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CloudTicTacToe.API/CloudTicTacToe.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CloudTicTacToe.API/CloudTicTacToe.API.dll"]