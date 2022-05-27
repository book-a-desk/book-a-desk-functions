namespace LambdaFunctions

open Amazon.Lambda.Core
open FSharp.Json
type MyEvent =
    {
        location: string
    }

[<assembly: LambdaSerializer(typeof<Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer>)>]
()

type OfficeRestrictionNotificationsHandler() =
    member _.Handle (event: string) = task {
        let eventDeserialized = Json.deserialize<MyEvent> event
        printfn $"OfficeRestrictionNotificationsHandler was successfully called with location {eventDeserialized.location}"
    }