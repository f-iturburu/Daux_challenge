# Use the official ASP.NET Core 8 runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5143
EXPOSE 443

# Use the .NET Core SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY ["Francisco_Iturburu_Daux_Challenge/Francisco_Iturburu_Daux_Challenge.csproj", "Francisco_Iturburu_Daux_Challenge/"]
COPY ["Services/Services.csproj", "Services/"]
RUN dotnet restore "Francisco_Iturburu_Daux_Challenge/Francisco_Iturburu_Daux_Challenge.csproj"

# Copy the entire solution and build the application
COPY . .
WORKDIR "/src/Francisco_Iturburu_Daux_Challenge"
RUN dotnet build -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Final stage to run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Francisco_Iturburu_Daux_Challenge.dll"]
