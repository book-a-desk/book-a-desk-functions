namespace LambdaFunctions

open Amazon.Lambda.Core
type MyEvent =
    {
        location: string
    }

[<assembly: LambdaSerializer(typeof<Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer>)>]
()

type OfficeRestrictionNotificationsHandler() =
    member _.Handle event = task {
        // let eventDeserialized = event.ToObject<MyEvent>()
        printfn $"OfficeRestrictionNotificationsHandler was successfully called with location {event}"
    }