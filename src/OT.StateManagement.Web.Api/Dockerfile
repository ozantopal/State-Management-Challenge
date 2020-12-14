#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/OT.StateManagement.Web.Api/OT.StateManagement.Web.Api.csproj", "src/OT.StateManagement.Web.Api/"]
COPY ["src/OT.StateManagement.DataAccess.EF.Repository/OT.StateManagement.DataAccess.EF.Repository.csproj", "src/OT.StateManagement.DataAccess.EF.Repository/"]
COPY ["src/OT.StateManagement.DataAccess.EF/OT.StateManagement.DataAccess.EF.csproj", "src/OT.StateManagement.DataAccess.EF/"]
COPY ["src/OT.StateManagement.Domain/OT.StateManagement.Domain.csproj", "src/OT.StateManagement.Domain/"]
COPY ["src/OT.StateManagement.Business.Service/OT.StateManagement.Business.Service.csproj", "src/OT.StateManagement.Business.Service/"]
RUN dotnet restore "src/OT.StateManagement.Web.Api/OT.StateManagement.Web.Api.csproj"
COPY . .
WORKDIR "/src/src/OT.StateManagement.Web.Api"
RUN dotnet build "OT.StateManagement.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OT.StateManagement.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OT.StateManagement.Web.Api.dll"]