FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
ARG AWSREGION
ENV AWS_REGION=${AWSREGION}
WORKDIR /app
COPY LambdaFunctions.Api/*.fsproj ./LambdaFunctions.Api/
RUN mkdir -p /home/.aws
RUN dotnet restore ./LambdaFunctions.Api/LambdaFunctions.fsproj
RUN ls
RUN dotnet publish \
        --configuration release \
        --output out \
        --no-restore \
        ./LambdaFunctions.Api \
ENTRYPOINT ["dotnet", "LambdaFunctions.Api.dll"]