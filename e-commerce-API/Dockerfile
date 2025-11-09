# Use official .NET runtime as base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["e-commerce-API.csproj", "./"]
RUN dotnet restore "./e-commerce-API.csproj"

# Copy the rest of the files and build
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Final stage: runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "e-commerce-API.dll"]
