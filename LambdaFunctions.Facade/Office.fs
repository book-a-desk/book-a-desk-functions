namespace LambdaFunctions.Facade

open System.IO
open System.Net
open Newtonsoft.Json

type Office =
    {
        Id: string
        Name: string
    }
 
type OfficeResponse =
    {
        Items: Office array
    }

module Office =
    let getOfficeById (officeLocation : string) (baseUrl : string) =
        let officesUrl = $"{baseUrl}/offices" 
        let request = HttpWebRequest.Create(officesUrl) :?> HttpWebRequest 
        request.ProtocolVersion <- HttpVersion.Version10
        request.Method <- "GET"
        let response = request.GetResponse() :?> HttpWebResponse
        let stream = response.GetResponseStream()
        let reader = new StreamReader(stream)
        let office = reader.ReadToEnd()
        let deserializedObject = JsonConvert.DeserializeObject<OfficeResponse>(office)
        let officeId = (deserializedObject.Items |> Array.filter (fun office -> office.Name = officeLocation.ToString())).[0].Id        
        stream.Close()
        officeId