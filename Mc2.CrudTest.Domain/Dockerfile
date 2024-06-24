# Use the official Microsoft ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official Microsoft ASP.NET Core SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Mc2.CrudTest.DomainModel.csproj", "."]
RUN dotnet restore "Mc2.CrudTest.DomainModel.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "Mc2.CrudTest.DomainModel.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mc2.CrudTest.DomainModel.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mc2.CrudTest.DomainModel.dll"]
