namespace OfficeRestrictionNotificationsEurope

open System
open System.Text
open System.Net
open Amazon.Lambda.Core
open Newtonsoft.Json

type OfficeReference =
    {
        id: string
    }
    
type RestrictionNotifier =
    {
        Office: OfficeReference
        Date: string
    }
    
type OfficeRestrictionNotificationsEuropeHandler() =
    [<LambdaSerializer(typeof<Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer>)>]
    member _.Handle (context : ILambdaContext) = task {
        // Will be injected on pipeline
        let url = "https://api.test.book-a-desk.broadsign.net/notify-office-restrictions"

        let request = HttpWebRequest.Create(url) :?> HttpWebRequest 
        request.ProtocolVersion <- HttpVersion.Version10
        request.Method <- "POST"

        // Will be injected on pipeline
        let officeId = "16c3d468-c115-4452-8502-58b821d6640b"
        let today = DateTime.Today.ToString "MM/dd/yyyy HH:mm:ss"
        
        let restrictionNotifier =
            {
                Office = { id = officeId.ToString() }
                Date = today
            }
        let serializedRestrictionNotifier = JsonConvert.SerializeObject(restrictionNotifier)
        let postBytes = Encoding.ASCII.GetBytes(serializedRestrictionNotifier)
        request.ContentType <- "application/x-www-form-urlencoded";
        request.ContentLength <- int64 postBytes.Length
        
        printfn $"Sending request to {request.Address} on {today} for office {officeId}"
        let reqStream = request.GetRequestStream() 
        reqStream.Write(postBytes, 0, postBytes.Length);
        reqStream.Close()

        let response = request.GetResponse() :?> HttpWebResponse
        let statusCode = response.StatusCode.ToString()
        let statusDescription = response.StatusDescription.ToString()
        let result = $"StatusCode: {statusCode}, StatusDescription: {statusDescription}"
        return result
    }