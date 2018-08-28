FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 52278
EXPOSE 44301

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["igaw-public/igaw-public.csproj", "igaw-public/"]
RUN dotnet restore "igaw-public/igaw-public.csproj"
COPY . .
WORKDIR "/src/igaw-public"
RUN dotnet build "igaw-public.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "igaw-public.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "igaw-public.dll"]