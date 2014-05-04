cd 'C:\Users\Moshe\Documents\visual studio 2013\Projects\Salesforce Api Test\BulkApiUnitTest\bin\Debug\'

ADD-TYPE -Path 'BulkApi.dll'

$UserName = "jewpaltz@gmail.com.uat";
$Password = "parents3";
$SecurityToken = "tADbTTKuQnIH2AH9YF0CLPvy";


$csv = Get-Content '.\contactsupsert.csv' | out-string
$runner1 = New-Object -TypeName BulkApi.BatchRunner -ArgumentList $UserName, $Password, $SecurityToken, upsert, "Contact", CSV, $csv, "CMS_Family_ID__c"

$SOQL = "select FirstName, LastName, Phone from Contact where LastName = 'Plotkin'";
$runner2 = New-Object -TypeName BulkApi.BatchRunner -ArgumentList $UserName, $Password, $SecurityToken, query, "Contact", CSV, $SOQL, null

[System.Threading.Tasks.Task].WhenAll($runner1.Task, $runner2.Task).ContinueWith([Action[System.Threading.Tasks.Task]] {
    param($t)
    foreach($s in $t.Result){
        Write-Host $s
    }
})
