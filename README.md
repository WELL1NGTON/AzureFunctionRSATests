# Test Project to Verify the use of RSA inside AzureFunctions

## Requirements

- [.NET 6.0+](https://dotnet.microsoft.com/en-us/download)
- [Azure Functions Core Tools](https://github.com/Azure/azure-functions-core-tools)
- [dotnet script](https://github.com/dotnet-script/dotnet-script)
- [Azurite if not using Visual Studio](https://github.com/Azure/Azurite)
- [OpenSSL](https://www.openssl.org/)

## Setup

### Make sure dotnet script is installed and configured

on Windows:

```pwsh
dotnet tool install -g dotnet-script

dotnet script register
```

on Linux:

```bash
dotnet tool install -g dotnet-script

# make generate-keys.csx executable
chmod +x generate-keys.csx
```

### Generate the RSA keys

Simply run the script `generate-keys.csx` to generate the keys.

### If using Visual Studio Code or Linux, start azurite

```pwsh

azurite --silent

```

### Run the Azure Functions

Just run the project in Visual Studio or Visual Studio Code or run the following commands:

```pwsh
cd src
cd AzureFunctionsRSATests.API

func start
```

### Test the Endpoint GET ~/api/TestRsaGet

```pwsh
curl -X GET "http://localhost:7071/api/TestRsaGet" -H "accept: */*"
```
