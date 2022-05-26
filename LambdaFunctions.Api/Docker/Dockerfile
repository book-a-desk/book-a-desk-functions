FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
ARG AWSREGION
ENV AWS_REGION=${AWSREGION}
WORKDIR /app
COPY . .
COPY *.csproj ./
RUN mkdir -p /home/.aws
RUN ls
CMD ls
RUN dotnet restore
RUN dotnet publish --configuration release --output out --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "LambdaFunctions.Api.dll"]