module LambdaFunctions.Tests.OfficeTests

open System
open Xunit

let baseUrl = "https://api.dev.book-a-desk.broadsign.net" // TODO: To mock
        
[<Fact>]
let ``Given an office location in Canada When calling the offices endpoint Then returns office Id`` () = async {
    let officeLocation = "Montreal"
    let expectedOfficeId = "4b774d13-645b-4378-a925-1da565a35fd7"
    let! officeId = LambdaFunctions.Facade.Office.getOfficeById officeLocation baseUrl
    Assert.Equal(expectedOfficeId, officeId)
}

[<Fact>]
let ``Given an office location in Europe When calling the offices endpoint Then returns office Id`` () = async {
    let officeLocation = "Berlin"
    let expectedOfficeId = "16c3d468-c115-4452-8502-58b821d6640b"
    let! officeId = LambdaFunctions.Facade.Office.getOfficeById officeLocation baseUrl
    Assert.Equal(expectedOfficeId, officeId)
}


    
    