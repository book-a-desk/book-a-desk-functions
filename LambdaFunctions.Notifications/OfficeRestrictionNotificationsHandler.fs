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
        let eventDeserialized = JsonSerializer.Deserialize<MyEvent>(event)
        let officeId = Office.getOfficeById(eventDeserialized.location)
        printfn $"Handler was called with location {eventDeserialized.location} with officeId {officeId}"
    }