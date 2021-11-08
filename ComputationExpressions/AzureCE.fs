module AzureCE

open Farmer
open Farmer.Builders
let myStorageAccount = storageAccount {
    name "myTestStorage"
    add_public_container "myContainer"
}

let myWebApp = webApp {
    name "myTestWebApp"
    setting "storageKey" myStorageAccount.Key
}

let deployment = arm {
    location Location.NorthEurope
    add_resources [
        myStorageAccount
        myWebApp
    ]
}
