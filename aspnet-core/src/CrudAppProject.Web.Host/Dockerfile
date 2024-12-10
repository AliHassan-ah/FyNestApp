FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/CrudAppProject.Web.Host/CrudAppProject.Web.Host.csproj", "src/CrudAppProject.Web.Host/"]
COPY ["src/CrudAppProject.Web.Core/CrudAppProject.Web.Core.csproj", "src/CrudAppProject.Web.Core/"]
COPY ["src/CrudAppProject.Application/CrudAppProject.Application.csproj", "src/CrudAppProject.Application/"]
COPY ["src/CrudAppProject.Core/CrudAppProject.Core.csproj", "src/CrudAppProject.Core/"]
COPY ["src/CrudAppProject.EntityFrameworkCore/CrudAppProject.EntityFrameworkCore.csproj", "src/CrudAppProject.EntityFrameworkCore/"]
WORKDIR "/src/src/CrudAppProject.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/CrudAppProject.Web.Host", "src/CrudAppProject.Web.Host"]
COPY ["src/CrudAppProject.Web.Core", "src/CrudAppProject.Web.Core"]
COPY ["src/CrudAppProject.Application", "src/CrudAppProject.Application"]
COPY ["src/CrudAppProject.Core", "src/CrudAppProject.Core"]
COPY ["src/CrudAppProject.EntityFrameworkCore", "src/CrudAppProject.EntityFrameworkCore"]
WORKDIR "/src/src/CrudAppProject.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "CrudAppProject.Web.Host.dll"]
