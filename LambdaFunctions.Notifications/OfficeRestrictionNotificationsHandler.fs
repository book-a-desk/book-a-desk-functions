namespace LambdaFunctions

open Amazon.Lambda.Core
open Newtonsoft.Json.Linq
type MyEvent =
    {
        location: string
    }

[<assembly: LambdaSerializer(typeof<Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer>)>]
()

type OfficeRestrictionNotificationsHandler() =
    member _.Handle (event: JObject) = task {
        let eventDeserialized = event.ToObject<MyEvent>()
        printfn $"OfficeRestrictionNotificationsHandler was successfully called with location {eventDeserialized.location}"
    }