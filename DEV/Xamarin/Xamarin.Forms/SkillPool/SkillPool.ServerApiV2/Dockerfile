#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 5003
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["Xamarin/Xamarin.Forms/SkillPool/SkillPool.ServerApiV2/SkillPool.ServerApiV2.csproj", "Xamarin/Xamarin.Forms/SkillPool/SkillPool.ServerApiV2/"]
COPY ["Xamarin/Xamarin.Forms/SkillPool/SkillPool.Common/SkillPool.Common.csproj", "Xamarin/Xamarin.Forms/SkillPool/SkillPool.Common/"]
COPY ["Xamarin/Xamarin.Forms/SkillPool/SkillPool.Server/SkillPool.Server.csproj", "Xamarin/Xamarin.Forms/SkillPool/SkillPool.Server/"]
COPY ["Xamarin/Xamarin.Forms/SkillPool/SkillPool.Model/SkillPool.Model.csproj", "Xamarin/Xamarin.Forms/SkillPool/SkillPool.Model/"]
RUN dotnet restore "Xamarin/Xamarin.Forms/SkillPool/SkillPool.ServerApiV2/SkillPool.ServerApiV2.csproj"
COPY . .
WORKDIR "/src/Xamarin/Xamarin.Forms/SkillPool/SkillPool.ServerApiV2"
RUN dotnet build "SkillPool.ServerApiV2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SkillPool.ServerApiV2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkillPool.ServerApiV2.dll"]