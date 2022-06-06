namespace LambdaFunctions

open Amazon.Lambda.Core
open System.Text.Json
open LambdaFunctions.Facade
type OfficeRestrictionNotificationEvent =
    {
        location: string
    }

[<assembly: LambdaSerializer(typeof<Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer>)>]()
type OfficeRestrictionNotificationsHandler() =
    member _.Handle (event: JsonDocument) = task {
        let baseUrl = "https://api.dev.book-a-desk.broadsign.net" // To inject
        let eventDeserialized = JsonSerializer.Deserialize<OfficeRestrictionNotificationEvent>(event)
        let! officeId = Office.getOfficeById eventDeserialized.location HttpRequest.baseUrl
        let! sendNotification = OfficeRestrictions.sendOfficeRestrictionsNotifications officeId HttpRequest.baseUrl
        printfn $"Notification successfully sent for location {eventDeserialized.location} with OfficeId {officeId}"
    }