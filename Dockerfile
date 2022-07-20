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
WORKDIR /app
RUN mkdir -p ${LAMBDA_TASK_ROOT}
COPY --from=build-env /app/out ${LAMBDA_TASK_ROOT}
CMD ["LambdaFunctions.Handlers::LambdaFunctions.OfficeRestrictionNotificationsHandler::Handle"]