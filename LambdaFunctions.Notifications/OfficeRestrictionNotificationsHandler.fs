namespace LambdaFunctions

open Amazon.Lambda.Core

[<assembly: LambdaSerializer(typeof<Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer>)>]
()

type OfficeRestrictionNotificationsHandler() =
    member _.Handle (event: string) = task {
        printfn $"OfficeRestrictionNotificationsHandler was successfully called with event {event}"
    }