WORKDIR /src
COPY ["EcommerceAPI.csproj", "."]
RUN dotnet restore "./EcommerceAPI.csproj"
COPY . .
RUN dotnet build "EcommerceAPI.csproj" -c Release -o /app/build
RUN dotnet publish "EcommerceAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "EcommerceAPI.dll"]