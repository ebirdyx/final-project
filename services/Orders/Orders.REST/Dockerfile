FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["services/Orders/Orders.REST/Orders.REST.csproj", "Orders.REST/"]
RUN dotnet restore "services/Orders/Orders.REST/Orders.REST.csproj"
COPY . .
WORKDIR "/src/Orders.REST"
RUN dotnet build "Orders.REST.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Orders.REST.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Orders.REST.dll"]
