FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["SW.Products.WebApi/SW.Products.WebApi.csproj", "SW.Products.WebApi/"]
RUN dotnet restore "SW.Products.WebApi/SW.Products.WebApi.csproj"
COPY . .
WORKDIR "/src/SW.Products.WebApi"
RUN dotnet build "SW.Products.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SW.Products.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SW.Products.WebApi.dll"]