

///##################################################################################################################################################
/// Basic Store System Version.02
///
/// created by James Jordan
/// created on 29/04/2021
///##################################################################################################################################################
/*

How it works:
The store system is designed to be as unobtrusive and use as few scripts as possible.

***Components***

* Scriptable Objects:

    StoreData - Contains the stores itemdata and methods used in obtaining item data for use elsewhere.

    StoreTabData - The data used by StoreTabControllers. They contain an identifier, used to read from StoreData and allow for the overriding of an objects
    price without having to modify store data.

* Controllers:
       
    StoreTabController - Handles UI set up for its given StoreTabData, populating a scroll rect with buttons using StoreData to affect
    the icon and price displayed.

    Currency_Controller - Stores Currency information such as how much of a certain currency the player possesses. Contains methods for comparing the price of 
    the object with 




*/