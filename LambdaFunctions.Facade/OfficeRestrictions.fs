namespace LambdaFunctions.Facade

open System
open Book_A_Desk.Api.Tests
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
    let sendOfficeRestrictionsNotifications (officeId : string) (baseUrl : string) = async {
        let restrictionsUrl = $"{baseUrl}/notify-office-restrictions"
        let today = DateTime.Today.ToString "MM/dd/yyyy HH:mm:ss"      
        let restrictionNotifier =
            {
                Office = { id = officeId.ToString() }
                Date = today
            }
        let serializedRestrictionNotifier = JsonConvert.SerializeObject(restrictionNotifier)
        let! result = HttpRequest.postAsyncGetContent restrictionsUrl serializedRestrictionNotifier        
        return result
    }