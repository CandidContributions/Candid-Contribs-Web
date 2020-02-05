# Candid Contributions Website

![GitHub](https://img.shields.io/github/license/candidcontributions/Candid-Contribs-Web)

The source of candidcontributions.com.

## Database Restore

Download the SQL Server Database from <i>coming soon</i> and restore into SQL Server 2016 (or later).

## Build in Visual Studio

* Open `CandidContribs\CandidContribs.sln` in Visual Studio
* Make sure to allow NuGet Package Restore in VS (Tools > Options > Package Manager)
* Build the solution (this will create a `Web.ConnectionStrings.config` file in the root of your website)
* Open `Web.ConnectionStrings.config` and update the connection string

## Front-End Notes

To build Front-End (webpack / scss)

* Navigate into `app`
* CMD > `npm install`
* To watch files run `npm run-script watch`
* To build files run `npm run-script build`

## View Website

View the website in your browser. You can log into the back office using <i>coming soon</i>