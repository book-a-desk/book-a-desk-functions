namespace LambdaFunctions.Facade

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
    let getOfficeById (officeLocation : string) (baseUrl : string) = async {
        let officesUrl = $"{baseUrl}/offices"
        let! result = HttpRequest.getAsyncGetContent officesUrl
        let deserializedObject = JsonConvert.DeserializeObject<OfficeResponse>(result)
        let officeId = (deserializedObject.Items |> Array.filter (fun office -> office.Name = officeLocation)).[0].Id        
        return officeId
    }
        