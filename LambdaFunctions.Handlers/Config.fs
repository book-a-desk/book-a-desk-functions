namespace LambdaFunctions

open Amazon.Extensions.NETCore.Setup
open Microsoft.Extensions.Configuration

module Config =
    let configureAzureAppConfiguration (builder: IConfigurationBuilder) useDevelopmentStorage =
        match useDevelopmentStorage with
        | false ->
            builder.AddSystemsManager($"/BookADesk/", AWSOptions()) |> ignore
        | true -> ()
    let configuration =
        let builder = new ConfigurationBuilder()
        builder.AddEnvironmentVariables() |> ignore
        let assembly = System.Reflection.Assembly.GetExecutingAssembly()
        builder.AddUserSecrets(assembly, optional = true) |> ignore
        let useDevelopmentStorage = builder.Build().Item("AWS_DEVELOPMENTSTORAGE") |> bool.Parse
        configureAzureAppConfiguration builder useDevelopmentStorage
        builder.Build()