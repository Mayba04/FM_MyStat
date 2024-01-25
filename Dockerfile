FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base

COPY . .


# Install all dependencies
RUN dotnet restore "FM_MyStat.Web/FM_MyStat.Web.csproj"

# Install the dotnet-ef tools
RUN dotnet tool install -g dotnet-ef --version 7.0
ENV PATH $PATH:/root/.dotnet/tools

RUN dotnet publish "FM_MyStat.Web/FM_MyStat.Web.csproj" -c Release -o /app/build
WORKDIR /app/build
EXPOSE 80

ENTRYPOINT ["dotnet", "FM_MyStat.Web.dll", "--urls=http://0.0.0.0:80"]
