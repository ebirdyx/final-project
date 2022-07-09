FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY "services/Cart/Cart" /src
WORKDIR /src/services/Cart/Cart
RUN dotnet restore "Cart.csproj"
RUN dotnet build "Cart.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cart.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cart.dll"]
