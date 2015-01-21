# GiledRosedExpands

Data format
GET localhost:3512/api/items

output data
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
POST localhost:3512/api/purchases

input data

{
    "ItemName": "Item 1",
    "Username": "Jorge"
}

output data
{
	"PurchaseId": 2,
	"ItemName": "Item 1",
	"Username": "Jorge"
}

Security

In order to verify the request comes from the actual buyer we can apply different techniques:

HTTPS
 With HTTPS, your traffic is far less likely to be subject to packet sniffing attacks. If we want to make sure that every request comes
 from https, the best option is use an Action filter 
 
Authentication filters
 Authentication filters let you set an authentication scheme for individual controllers or actions. In this case would use token-based authentication.

 
Authorization filters
 In case that purchase option wouldn't be available for all authenticated used we could use authorization filter to restrict the access.
 

