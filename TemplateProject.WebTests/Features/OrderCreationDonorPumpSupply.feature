Feature: FRS Req 5.1 Donor Pump Order Management
	In order to order pumps and supplies for donors
	As amilk bank usder
	I want to be able to create orders for donors containing a pump and supplies.

Scenario:5.1.1.1 The system shall display a count of available pumps.
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When element with id menuInventory is clicked
	And element with id menuPumps is clicked
	Then the page Inventory/Pumps should be displayed
	When text Available entered in pumpSearchBox
	Then element with class k-pager-info contains text 1 - 15 of

Scenario:5.1.1.2 Create Add an Order
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When new pump added
	And user navigates to add pump supply order page
	And text 60002 entered in Order_DonorID
	And element with id updateDonorInfo is clicked
	Then the dropdown MilkBank has selected value Tiny Treasures Milk Bank
	Then element with id FirstName has value Erin
	Then element with id LastName has value Povich
	Then element with id Street has value 309 Cimarron Valley Trail 
	Then element with id State has value MO
	Then element with id ZipCode has value 63385
	Then element with id Email has value erinpovich@yahoo.com
	Then element with id PhoneNumber has value (314)-803-6875
	Then the dropdown Order_ShippingMethod has selected value Ground
	When element with id showPump is clicked
	And text 987654321 entered in PumpSerial
	And value 1 entered in numeric with DoublePumpingKit id
	And value 1 entered in numeric with 24mmBreastshields id
	And value 1 entered in numeric with 27mmBreastshields id
	And value 1 entered in numeric with 30mmBreastshields id
	And value 1 entered in numeric with 8ozBottlesSet id
	And value 1 entered in numeric with PersonalFitConnectors id
	And value 1 entered in numeric with ValvesandMembranes id
	And value 1 entered in numeric with Membranes id
	And value 1 entered in numeric with Tubing id
	And value 1 entered in numeric with Bags id
	And value 1 entered in numeric with YellowBag id
	And text Test Note entered in Order_Notes
	And element with id btnPlaceOrder is clicked
	Then the page Order/OrderDetail should be displayed

Scenario:5.1.1.2.1 The user can enter Donor ID to pre-populate name address email phone Milk Bank
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to add pump supply order page
	And text 60002 entered in Order_DonorID
	And element with id updateDonorInfo is clicked
	Then the dropdown MilkBank has selected value Tiny Treasures Milk Bank
	Then element with id FirstName has value Erin
	Then element with id LastName has value Povich
	Then element with id Street has value 309 Cimarron Valley Trail 
	Then element with id State has value MO
	Then element with id ZipCode has value 63385
	Then element with id Email has value erinpovich@yahoo.com
	Then element with id PhoneNumber has value (314)-803-6875

Scenario:5.1.1.2.2 If Donor ID is blank, the user can manually enter the name address email phone Milk Bank.
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to add pump supply order page
	And dropdown with id MilkBank value Texas Childrens Hospital Milk Bank
	And text Joselyn entered in FirstName
	And text Fiennes entered in LastName
	And text 1234 Jackson Ave entered in Street
	And text Los Angeles entered in City
	And text CT entered in State
	And text 90000 entered in ZipCode
	And text jFiennes@pmail.com entered in Email
	And text (789)-456-1235 entered in PhoneNumber
	Then the dropdown MilkBank has selected value Texas Childrens Hospital Milk Bank
	Then element with id FirstName has value Joselyn
	Then element with id LastName has value Fiennes
	Then element with id Street has value 1234 Jackson Ave
	Then element with id City has value Los Angeles
	Then element with id State has value CT
	Then element with id ZipCode has value 90000
	Then element with id Email has value jFiennes@pmail.com
	Then element with id PhoneNumber has value (789)-456-1235

Scenario:5.1.1.2.3 Order Date automatically loads the date at the time of order creation
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to add pump supply order page
	Then the date field Order_OrderActualDate has current date

Scenario:5.1.1.2.4 An option to include pump in the order
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to add pump supply order page
	When element with id showPump is clicked
	And text 1555021 entered in PumpSerial
	Then element with id PumpSerial has value 1555021

Scenario:5.1.1.2.5 The user shall be able to add supplies & their quantity in the pump order
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to add pump supply order page
	And value 1 entered in numeric with DoublePumpingKit id
	And value 1 entered in numeric with 24mmBreastshields id
	And value 1 entered in numeric with 27mmBreastshields id
	And value 1 entered in numeric with 30mmBreastshields id
	And value 1 entered in numeric with 8ozBottlesSet id
	And value 1 entered in numeric with PersonalFitConnectors id
	And value 1 entered in numeric with ValvesandMembranes id
	And value 1 entered in numeric with Membranes id
	And value 1 entered in numeric with Tubing id
	And value 1 entered in numeric with Bags id
	And value 1 entered in numeric with YellowBag id
	Then numeric with id DoublePumpingKit has value 1
	Then numeric with id 24mmBreastshields has value 1
	Then numeric with id 27mmBreastshields has value 1
	Then numeric with id 30mmBreastshields has value 1
	Then numeric with id 8ozBottlesSet has value 1
	Then numeric with id PersonalFitConnectors has value 1
	Then numeric with id ValvesandMembranes has value 1
	Then numeric with id Membranes has value 1
	Then numeric with id Tubing has value 1
	Then numeric with id Bags has value 1
	Then numeric with id Tubing has value 1
	Then numeric with id YellowBag has value 1
	
Scenario:5.1.1.2.6 The list of supplies will be displayed as a table with all items and quantity field next to each item
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When element with id menuOrders is clicked
	And element with id menuCreatePump/SupplyOrder is clicked
	Then the page Order/AddPumpSupplyOrder should be displayed

Scenario:5.1.1.2.7 Notes field shall be present for the user to enter comments for this order
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to add pump supply order page
	And text Test Note entered in Order_Notes
	Then element with id Order_Notes has value Test Note

Scenario:5.1.1.2.8 The user can select shipping methods
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to add pump supply order page
	Then dropdown with id Order_ShippingMethod has Ground,Priority Overnight,2 Day,Express,Saturday Delivery,USPS
	