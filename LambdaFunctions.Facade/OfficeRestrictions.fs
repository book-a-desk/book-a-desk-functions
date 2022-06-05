namespace LambdaFunctions.Facade

open System
open System.Text
open System.Net
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
    
module OfficeRestrictions =
    let sendOfficeRestrictionsNotifications (officeId : string) (baseUrl : string) = task {
        let restrictionsUrl = $"{baseUrl}/notify-office-restrictions"
        let request = HttpWebRequest.Create(restrictionsUrl) :?> HttpWebRequest 
        request.ProtocolVersion <- HttpVersion.Version10
        request.Method <- "POST"
                   
        let today = DateTime.Today.AddDays(1).ToString "MM/dd/yyyy HH:mm:ss"      
        let restrictionNotifier =
            {
                Office = { id = officeId.ToString() }
                Date = today
            }
        let serializedRestrictionNotifier = JsonConvert.SerializeObject(restrictionNotifier)
        let postBytes = Encoding.ASCII.GetBytes(serializedRestrictionNotifier)
        request.ContentType <- "application/x-www-form-urlencoded";
        request.ContentLength <- int64 postBytes.Length
        
        let reqStream = request.GetRequestStream() 
        reqStream.Write(postBytes, 0, postBytes.Length);
        reqStream.Close()

        let response = request.GetResponse() :?> HttpWebResponse
        let statusCode = response.StatusCode.ToString()
        let statusDescription = response.StatusDescription.ToString()
        let result = $"StatusCode: {statusCode}, StatusDescription: {statusDescription}"
        
        return result
    }