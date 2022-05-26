namespace OfficeRestrictionNotificationsCanada

open System
open System.IO
open System.Text
open System.Net

type Office =
    {
        Id: string
        Name: string
    }
 
type OfficeResponse =
    {
        Items: Office array
    }
   
type OfficeReference =
    {
        id: string
    }
    
type RestrictionNotifier =
    {
        Office: OfficeReference
        Date: string
    }
    

type OfficeRestrictionNotificationsCanadaHandler() =
    [<LambdaSerializer(typeof<Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer>)>]
    member _.Handle (context : ILambdaContext) = task {
        let officeToGet = "Montreal" // To be injected
        let urlOffices = "https://api.test.book-a-desk.broadsign.net/offices"
        let request = HttpWebRequest.Create(urlOffices) :?> HttpWebRequest 
        request.ProtocolVersion <- HttpVersion.Version10
        request.Method <- "GET"
        let response = request.GetResponse() :?> HttpWebResponse
        let stream = response.GetResponseStream()
        let reader = new StreamReader(stream)
        let office = reader.ReadToEnd()
        let deserializedObject = JsonConvert.DeserializeObject<OfficeResponse>(office)
        let officeId = (deserializedObject.Items |> Array.filter (fun office -> office.Name = officeToGet)).[0].Id
        
        stream.Close()
        
        // Will be injected on pipeline
        let url = "https://api.test.book-a-desk.broadsign.net/notify-office-restrictions"

        let request = HttpWebRequest.Create(url) :?> HttpWebRequest 
        request.ProtocolVersion <- HttpVersion.Version10
        request.Method <- "POST"

        // Will be injected on pipeline
        let officeId = "4b774d13-645b-4378-a925-1da565a35fd7"
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