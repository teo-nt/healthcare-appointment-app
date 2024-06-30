# Healthcare Appointment App
Be sure to include in the project an appsettings.json file that has the following format:

    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=localhost\\sqlexpress; Database=your_db; User=your_user; Password=your_password; MultipleActiveResultSets=True; TrustServerCertificate=True;"
      },
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "AllowedHosts": "*",
      "JWTSettings": {
        "securityKey": "YOUR SECURITY KEY",
        "ValidAudience": "Insert audience",
        "ValidIssuer": "Insert issuer"
      },
      "Serilog": {
        "MinimumLevel": {
          "Default": "Debug",
          "Override": {
            "Microsoft": "Information"
          }
        },
        "WriteTo": [
          {
            "Name": "Console"
          },
          {
            "Name": "File",
            "Args": {
              "path": "Logs/logs.txt",
              "rollingInterval": "Day",
              "outputTemplate": "[{Timestamp:dd-MM-yyyy HH:mm:ssfff zzz} {SourceContext} {level}] {Message}{NewLine}{Exception}",
              "retainedFileCountLimit": null,
              "fileSizeLimitBytes": null
            }
          }
        ],
        "Enrich": [ "FromLogContext" ]
      }
    }
