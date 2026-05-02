# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project file first (for better caching)
COPY *.csproj ./
RUN dotnet restore

# Copy everything else
COPY . ./

# Publish the app
RUN dotnet publish -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app .

# Expose port (Render uses 10000 by default internally)
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# Start app
ENTRYPOINT ["dotnet", "RestaurantAPI.dll"]