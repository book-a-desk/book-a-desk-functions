namespace LambdaFunctions.Facade

open Book_A_Desk.Api.Tests
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
        let officeId = (deserializedObject.Items |> Array.filter (fun office -> office.Name = officeLocation.ToString())).[0].Id        
        return officeId
    }
        