# RunMyAccount API Client

This library allows connecting to the RunMyAccount API. This allows you to create integrated applications with youre invoicing system.

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

Creating a new invoice:
```
RunMyAccountsInvoice i = new RunMyAccountsInvoice();
client.CreateInvoice(i);
```

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Release Notes

### 3.0.8

	- New Features
		- No
	- Bug fixes
		- Project and department not supplied in request

### 3.0.7

	- New Features
		- InvoiceExist object now contains the customer object
	- Bug fixes
		- No

### 3.0.6 

	- New Features
		- Amount and Netamount and Paid of Accounts Payable Object are now a Double data type
	- Bug fixes
		- No

### 3.0.0

	- New .NET

### History

2.0.5: Get Invoices Timeout Fix with own HttpClient instead of RestSharp
2.0.4: Retrun status on create methods
2.0.3: Update Restsharp, Improve Multithreading and add Timeout of 10 seconds
2.0.2: Bugfix Json Convert of Articles
2.0.1: Stable Release
2.0.0-alpha: Remove JSON.Net dependency and update RestSharp to 107
1.0.2: Updating Json.NET and RestSharp
1.0.1: Replace special characters in ordernumber 
1.0.0: First Release

## Authors

* **Thomas Kurth** 

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
