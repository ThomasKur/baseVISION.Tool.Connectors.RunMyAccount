# RunMyAccount API Client

This library allows connecting to the RunMyAccount API to list the most important entities and creating invoices.

## Getting Started

At the begining you need to generate your API access tokens. More information on the RunMyAccounts help page
https://www.runmyaccounts.ch/buchhaltungs-hilfe/doku.php/api/start#wo_finde_ich_den_api_key

The API client is available as a nuget package and can easily be installed from there.

Creating the client by specifying the secrets you received in the step before:
```
RunMyAccountsClient client = new RunMyAccountsClient(string tenant, string apikey);
            
```

### Retrieving Objects

Retrieve all invoices:
```
List<RunMyAccountsInvoiceExist> invoices = client.ListAllInvoices();
```

Creating a new client:
```
RunMyAccountsInvoice i = new RunMyAccountsInvoice();
client.CreateInvoice(i);
```

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Thomas Kurth** - *Initial work* 

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
