FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "services/Discount" .
WORKDIR /src/Discount.gRPC
RUN dotnet restore "Discount.gRPC.csproj"
RUN dotnet build "Discount.gRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.gRPC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.gRPC.dll"]
