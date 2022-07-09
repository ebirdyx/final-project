FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "services/Discount" .
WORKDIR /src/Discount
RUN dotnet restore "Discount.csproj"
RUN dotnet build "Discount.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.dll"]
