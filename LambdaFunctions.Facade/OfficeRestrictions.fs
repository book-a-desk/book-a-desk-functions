﻿namespace LambdaFunctions.Facade

open System
open System.Text
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
    let sendOfficeRestrictionsNotifications = task {
        
        let url = "https://api.test.book-a-desk.broadsign.net/notify-office-restrictions"         // Will be injected on pipeline

        let request = HttpWebRequest.Create(url) :?> HttpWebRequest 
        request.ProtocolVersion <- HttpVersion.Version10
        request.Method <- "POST"

        let officeId = Office.getOfficeById("Montreal")                                           // Will be injected on pipeline                        
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