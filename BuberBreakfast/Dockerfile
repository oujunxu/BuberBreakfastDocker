FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /App

COPY . ./

RUN dotnet restore
RUN dotnet publish -c Release -o out
RUN dotnet tool install --global dotnet-ef

ENV PATH="$PATH:/root/.dotnet/tools"

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build /App/out .
EXPOSE 80/tcp
ENTRYPOINT [ "dotnet", "BuberBreakfast.dll" ]
