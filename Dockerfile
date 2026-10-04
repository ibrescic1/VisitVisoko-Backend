FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["VisitVisoko.API/VisitVisoko.API.csproj", "VisitVisoko.API/"]
COPY ["VisitVisoko.Domain/VisitVisoko.Domain.csproj", "VisitVisoko.Domain/"]
COPY ["VisitVisoko.Infrastructure/VisitVisoko.Infrastructure.csproj", "VisitVisoko.Infrastructure/"]
COPY ["VisitVisoko.Services/VisitVisoko.Services.csproj", "VisitVisoko.Services/"]

RUN dotnet restore "VisitVisoko.API/VisitVisoko.API.csproj"

COPY . .
WORKDIR "/src/VisitVisoko.API"
RUN dotnet publish "VisitVisoko.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "VisitVisoko.API.dll"]