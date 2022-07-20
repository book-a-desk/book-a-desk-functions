namespace LambdaFunctions.Facade

open System
open System.Net.Http
open System.Text

module HttpRequest =
    
    let baseUrl = Environment.GetEnvironmentVariable("BOOK_A_DESK_BASE_API_URL")
    
    let getAsyncGetContent (url : string) = async {
        let httpClient = new HttpClient()
        let! response = httpClient.GetAsync(url) |> Async.AwaitTask
        response.EnsureSuccessStatusCode () |> ignore
        let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
        return content
    }
        
    let postAsyncGetContent (url : string) content = async {
        let httpClient = new HttpClient()
        let content = new StringContent(content, Encoding.UTF8, "application/json")
        let! response = httpClient.PostAsync(url, content) |> Async.AwaitTask
        response.EnsureSuccessStatusCode () |> ignore
        let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
        return content
    }