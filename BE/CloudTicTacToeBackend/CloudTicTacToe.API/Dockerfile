# cd BE
# docker build -t macioszczykmikolaj/tictactoe-api -f CloudTicTacToeBackend\CloudTicTacToe.API\Dockerfile .
# docker push macioszczykmikolaj/tictactoe-api

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CloudTicTacToeBackend/CloudTicTacToe.API/CloudTicTacToe.API.csproj", "CloudTicTacToeBackend/CloudTicTacToe.API/"]
COPY ["CloudTicTacToeBackend/CloudTicTacToe.Infrastructure/CloudTicTacToe.Infrastructure.csproj", "CloudTicTacToeBackend/CloudTicTacToe.Infrastructure/"]
RUN dotnet restore "CloudTicTacToeBackend/CloudTicTacToe.API/CloudTicTacToe.API.csproj"
COPY . .
WORKDIR "/src/CloudTicTacToeBackend/CloudTicTacToe.API"
RUN dotnet build "CloudTicTacToe.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CloudTicTacToe.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=password
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/etc/ssl/aspnetapp.pfx
ENTRYPOINT ["dotnet", "CloudTicTacToe.API.dll"]

COPY ./CloudTicTacToeBackend/CloudTicTacToe.API/SSL/aspnetapp.pfx /etc/ssl/