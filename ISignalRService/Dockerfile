FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["ISignalRService/ISignalRService.csproj", "ISignalRService/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "ISignalRService/ISignalRService.csproj"
COPY . .
WORKDIR "/src/ISignalRService"
RUN dotnet build "ISignalRService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ISignalRService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ISignalRService.dll"]