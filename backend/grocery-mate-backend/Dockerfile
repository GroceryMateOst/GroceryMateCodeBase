FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["grocery-mate-backend.csproj", "."]
RUN dotnet restore "./grocery-mate-backend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "grocery-mate-backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "grocery-mate-backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "grocery-mate-backend.dll", "--server.urls", "https://+:5000"]
