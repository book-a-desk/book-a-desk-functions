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
    let getOfficeById (officeLocation : string) =
        let urlOffices = "https://api.test.book-a-desk.broadsign.net/offices" // To inject
        let request = HttpWebRequest.Create(urlOffices) :?> HttpWebRequest 
        request.ProtocolVersion <- HttpVersion.Version10
        request.Method <- "GET"
        let response = request.GetResponse() :?> HttpWebResponse
        let stream = response.GetResponseStream()
        let reader = new StreamReader(stream)
        let office = reader.ReadToEnd()
        let deserializedObject = JsonConvert.DeserializeObject<OfficeResponse>(officeLocation)
        let officeId = (deserializedObject.Items |> Array.filter (fun office -> office.Name = office.ToString())).[0].Id        
        stream.Close()
        officeId