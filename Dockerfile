FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["SW.Checkout.WebApi/SW.Checkout.WebApi.csproj", "SW.Checkout.WebApi/"]
RUN dotnet restore "SW.Checkout.WebApi/SW.Checkout.WebApi.csproj"
COPY . .
WORKDIR "/src/SW.Checkout.WebApi"
RUN dotnet build "SW.Checkout.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SW.Checkout.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SW.Checkout.WebApi.dll"]