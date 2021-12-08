# Candid Contributions Website - (OLD v8 WEBSITE)

![GitHub](https://img.shields.io/github/license/candidcontributions/Candid-Contribs-Web)

:warning: **Please note that this is the old Umbraco version 8 website**. The open source home of our current (Umbraco v9) website is https://github.com/CandidContributions/CanConCloud.

We have kept this repo public as we [documented the issues](https://github.com/CandidContributions/CanConCloud/issues/1) when upgrading to version 9 over on the new repo and some of them make reference back to this codebase.

## Database Restore

*Sorry, we do not have a publicly avaiable version of our v8 database.*

## Build in Visual Studio

* Open `CandidContribs\CandidContribs.sln` in Visual Studio
* Make sure to allow NuGet Package Restore in VS (Tools > Options > Package Manager)
* Build the solution (this will create a `Web.ConnectionStrings.config` file in the root of your website)
* Open `Web.ConnectionStrings.config` and update the connection string

## Front-End Notes

To build Front-End (webpack / scss)

* Navigate into `app`
* CMD > `npm install`
* To watch files run `npm run watch`
* To build files run `npm run build`

## View Website

View the website in your browser. You can log into the back office using <i>coming soon</i>
