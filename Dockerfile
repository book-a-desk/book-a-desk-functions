FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
ARG AWSREGION
ENV AWS_REGION=${AWSREGION}
WORKDIR /app
COPY . .
RUN mkdir -p /home/.aws
RUN dotnet restore
RUN dotnet publish --configuration release --output out --no-restore

# Build runtime image
FROM public.ecr.aws/lambda/dotnet:6
#WORKDIR /app
#COPY --from=build-env /app/out .
COPY publish/* ${LAMBDA_TASK_ROOT}
CMD ["LambdaFunctions.Notifications::LambdaFunctions.AppHandlers::webApp"]