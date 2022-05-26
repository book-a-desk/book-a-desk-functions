namespace LambdaFunctions

open Amazon.Lambda.Core

[<assembly: LambdaSerializer(typeof<Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer>)>]
()

type OfficeRestrictionNotificationsHandler() =
    member _.Handle (context: ILambdaContext) = task {
        printfn "OfficeRestrictionNotificationsHandler was successfully called"
    }