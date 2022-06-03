namespace LambdaFunctions

open Amazon.Lambda.Core
open System.Text.Json
open LambdaFunctions.Facade
type MyEvent =
    {
        location: string
    }

[<assembly: LambdaSerializer(typeof<Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer>)>]
()

type OfficeRestrictionNotificationsHandler() =
    member _.Handle (event: JsonDocument) = task {
        let officeId = Office.getOfficeById(event.RootElement[0].GetProperty("location"))
        let eventDeserialized = JsonSerializer.Deserialize<MyEvent>(event);
        printfn $"OfficeRestrictionNotificationsHandler was successfully called with location {eventDeserialized.location} with officeId {officeId}"
    }