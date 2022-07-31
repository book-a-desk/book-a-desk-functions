namespace LambdaFunctions

module Constants =
    let config = Config.configuration
    let baseUrl = config.Item("Book-A-Desk-Api:URL")