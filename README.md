# GiledRosedExpands

## How to run the test

To run machine.specifications unit test from Resharper it's needed to install **Machine.Specifications.Runner.Resharper** from Resharper extension manager

Considerations
-----------
Due the low complexity of this code I haven't added a service layer that wouldn't add much benefits at this point of development.

The same reason applies to not make DB calls async, there are no expensive DB calls. Performing an async operation is vastly more complicated than performing a sync operation (in terms of what happens behind the scenes).

Data format
-----------
GET localhost:3512/api/items

Output data

```json
[
	{
		"Name": "Item 1",
		"Description": "Description item 1",
		"Price": 10
	},
	{
		"Name": "Item 2",
		"Description": "Description item 2",
		"Price": 20
	},
	{
		"Name": "Item 3",
		"Description": "Description item 3",
		"Price": 30
	}
]
```
POST localhost:3512/api/purchases

Input data

```json
{
    "ItemName": "Item 1",
    "Username": "Jorge"
}
```
Output data

```json
{
	"PurchaseId": 2,
	"ItemName": "Item 1",
	"Username": "Jorge"
}
```
Security
-----------

In order to verify the request comes from the real buyer we can apply different techniques:

HTTPS
 With HTTPS, your traffic is far less likely to be subject to packet sniffing attacks. If we want to make sure that every request comes
 from https, the best option is use an Action filter 
 
Authentication filters
 Authentication filters let us set an authentication scheme for individual controllers or actions. In this case would use token-based authentication.

 
Authorization filters
 In case that purchase option wouldn't be available for all authenticated used we could use authorization filter to restrict the access.
 

