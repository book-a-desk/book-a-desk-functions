module LambdaFunctions.Facade.Tests

open System
open Xunit

type OfficeId = OfficeId of Guid
type CityName = CityName of string
 
 type OpeningHours =
     {
         Text: string
     }
 
 type Office =
     {
         Id: OfficeId
         Name: CityName
         OpeningHours : OpeningHours
     }

 module Offices =
    
    let officeToTest = CityName "Montreal"
  
    let All =
        [
            {
                Id = Guid.Parse("4B774D13-645B-4378-A925-1DA565A35FD7") |> OfficeId
                Name = CityName "Montreal"
                OpeningHours =
                    {
                        Text = "7:30am to 6:30pm from Tuesday to Thursday"
                    }
            }
            {
                Id = Guid.Parse("16C3D468-C115-4452-8502-58B821D6640B") |> OfficeId
                Name = CityName "Berlin"
                OpeningHours =
                    {
                        Text = "7:00am to 7:00pm from Tuesday to Thursday"
                    }
            }
        ]
        
[<Fact>]
let ``Given an office When retrieving an office Then returns office Id`` () =
    let offices = Offices.All
    