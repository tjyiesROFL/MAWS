# MAWS ![Build and Deploy](https://github.com/kavichapagain/MAWS/workflows/Build%20and%20Deploy/badge.svg)

Murdoch Academic Workload Management System

## Getting Started

* clone the repo 
```
git clone https://github.com/kavichapagain/MAWS.git
```
* Open `MAWS.sln` in visual studio
* Add `appsettings.json`
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=;Port=5432;Database=;Username=;Password="

  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}

```

* Run 

## Unit Testing

We use XUnit testing framework for unit testing. 

* **CLI**
  - `cd MAWS.Tests`
  - `dotnet test`

* **Visual Studio**
  - Open Test Explorer by selecting `View > Test Explorer` from the menu bar in Visual Studio. (Or `Ctrl + E, T`)

  - Run


## Deploy

---

---
github actions

(mawsweb.azure) azure - add configuration - conn string 

(digitalocean) database - postgres 

sits behind the MAIS identity server 

---

## Authors

* [**Brodie Cole**](mailto:)
* [**Craig Thomson**](mailto:)
* [**Jack Kelly**](mailto:)
* [**Kavi Chapagain**](mailto:kav.chapagain@gmail.com)
* [**Leanne Thomson**](mailto: )
* [**Prince Thomson**](mailto: )
