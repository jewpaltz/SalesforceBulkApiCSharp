Salesforce Bulk Api for .Net
=======================
An SDK (i.e. light wrapper) for the Salesforce Bulk Api for use in .net, c#, vb, powershell, etc. implements Async Task pattern

### Automate your Salesforce import quickly and easily
You can create short (~2 lines per Object) comandline programs or Powershell scripts to automaticaly upload your data using the BulkApi
It is so simple to get started. You'll be uploading your data in less than five minutes.

Take a look at the unit tests to see how simple it is to upload or download csv files directly with the Bulk Api.

You can also take a look at the Powershell Script which uploads and sownloads data in a few lines of code.

#### The api uses the asynch/await patern so that you can run and monitor many job/batches at the same time.

Uploading data is someting that needs to be scriptable and repeatable.
When you are testing a bulk import of data into salesforce it can take quite a bit of time to setup the import just right. Once you have setup the import, you want to be able to reapeat those steps with one click.
Salesforce provides the "BulkApi" for importing data, and there are many tools that make use of this api. However none of them offer a good scripting solution.

#### Powershell Script
```
cd 'C:\Users\Moshe\Documents\visual studio 2013\Projects\Salesforce Api Test\BulkApiUnitTest\bin\Debug\'

ADD-TYPE -Path 'BulkApi.dll'

$UserName = "jewpaltz@gmail.com.uat";
$Password = "";
$SecurityToken = "";


$csv = Get-Content '.\contactsupsert.csv' | out-string
$runner = New-Object -TypeName BulkApi.BatchRunner -ArgumentList $UserName, $Password, $SecurityToken, upsert, "Contact", CSV, $csv, "CMS_Family_ID__c"
$runner.Task.Result

$SOQL = "select FirstName, LastName, Phone from Contact where LastName = 'Plotkin'";
$runner = New-Object -TypeName BulkApi.BatchRunner -ArgumentList $UserName, $Password, $SecurityToken, query, "Contact", CSV, $SOQL, null
$runner.Task.Result
```