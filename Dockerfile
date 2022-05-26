FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
ARG AWSREGION
ENV AWS_REGION=${AWSREGION}
WORKDIR /app
COPY LambdaFunctions/*.fsproj ./LambdaFunctions/
RUN mkdir -p /home/.aws
RUN dotnet restore ./LambdaFunctions/LambdaFunctions.fsproj
RUN ls
RUN dotnet publish \
        --configuration release \
        --output out \
        --no-restore \
        ./LambdaFunctions \
ENTRYPOINT ["dotnet", "LambdaFunctions.dll"]
