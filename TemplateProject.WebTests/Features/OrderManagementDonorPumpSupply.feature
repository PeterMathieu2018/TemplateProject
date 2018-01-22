Feature:Order Management Donor Pump Supply
	In order to manage Pump Supply orders
	As a milk bank or warehouse user
	I want to be able to view, fulfill, verify, and print packing slips

Scenario:5.4.1 A table to show all Pump Supply Orders in any status for a donor
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Order Management
	And element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active

Scenario:5.4.2 An export feature to export the history view shall be added with current filters
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Order Management
	And element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When element with class k-grid-excel in element with id pumpOrdersGrid clicked
	Then file with name Pump Supply Order Export was exported

Scenario:5.4.3 User may also filter order table by every category except Donor Name and Outgoing Tracking Number
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When pump order created
	And user navigates to Order Management
	And element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When filter 60002 in column DonorID in grid pumpOrdersGrid
	Then the 2 column in 1 row in pumpOrdersGrid grid should be 60002
	When filter on grid pumpOrdersGrid is cleared
	And filter Tiny Treasures Milk Bank in column MilkBankName in grid pumpOrdersGrid
	Then the 4 column in 1 row in pumpOrdersGrid grid should be Tiny Treasures Milk Bank
	When filter on grid pumpOrdersGrid is cleared
	And filter mbtsmbtester1 in column OrderBy in grid pumpOrdersGrid
	Then the 9 column in 1 row in pumpOrdersGrid grid should be mbtsmbtester1
	When filter on grid pumpOrdersGrid is cleared
	And filter Pending in column Status in grid pumpOrdersGrid
	Then the 10 column in 1 row in pumpOrdersGrid grid should be Pending

Scenario:5.4.4 User may filter “Bag Only” category by selecting “Yes”
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When bag only order created
	When user navigates to Order Management
	And element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When filter Yes in column BagOnlyName in grid pumpOrdersGrid
	Then the 11 column in 1 row in pumpOrdersGrid grid should be Yes

Scenario:5.4.4.1 The printed Bag Packet will contain a Welcome Letter and Collecting Milk for Donation
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When bag only order created
	When bag only order created
	When user navigates to Order Management
	And element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When filter Yes in column BagOnlyName in grid pumpOrdersGrid
	Then the 11 column in 1 row in pumpOrdersGrid grid should be Yes
	When row containing text Yes in grid pumpOrdersGrid is clicked
	And element with id printPackingSlips is clicked
	And switch to new tab
	And scroll to bottom of screen
	Then the page Order/PackingSlip should be displayed

Scenario:5.4.5 User has the option to filter by Pump Supply Order Statuses
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When pump order created
	When user navigates to Order Management
	And element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When filter Pending in column Status in grid pumpOrdersGrid
	Then the 10 column in 1 row in pumpOrdersGrid grid should be Pending
	#When filter on grid pumpOrdersGrid is cleared
	#And filter ReturnDelivered in column Status in grid pumpOrdersGrid
	#Then the 10 column in 1 row in pumpOrdersGrid grid should be ReturnDelivered
	When pump order fulfilled 60002
	And filter OK in column Status in grid pumpOrdersGrid
	Then the 10 column in 1 row in pumpOrdersGrid grid should be OK

Scenario:5.4.6 A list of all Pump Supply orders shall include
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Order Management
	And element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	Then the pumpOrdersGrid grid contains column Order #
	Then the pumpOrdersGrid grid contains column Donor ID
	Then the pumpOrdersGrid grid contains column Milk Bank Name
	Then the pumpOrdersGrid grid contains column Order Date
	Then the pumpOrdersGrid grid contains column Shipping Date
	Then the pumpOrdersGrid grid contains column Out Tracking
	Then the pumpOrdersGrid grid contains column Return Date
	Then the pumpOrdersGrid grid contains column Order By
	Then the pumpOrdersGrid grid contains column Status
	Then the pumpOrdersGrid grid contains column Bag Only

Scenario:5.4.6.10.1	If order is fulfilled, Donor ID, Return Tracking, and Pump Serial Number cannot be edited 
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When pump order created
	When user navigates to Order Management
	And pump order fulfilled 60002
	And element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When filter OK in column Status in grid pumpOrdersGrid
	And element with class k-grid-Edit clicked in pumpOrdersGrid grid with row containing text OK
	Then the page Order/EditOrder should be displayed
	Then element with id Order_DonorID has attribute readonly with value true
	Then element with id updateDonorInfo has attribute readonly with value true
	Then element with id MilkBank has attribute disabled with value true
	Then element with id FirstName has attribute readonly with value true
	Then element with id Street has attribute readonly with value true
	Then element with id City has attribute readonly with value true
	Then element with id State has attribute readonly with value true
	Then element with id ZipCode has attribute readonly with value true
	Then element with id Email has attribute readonly with value true
	Then element with id PhoneNumber has attribute readonly with value true
	Then element with id OutgoingTracking has attribute readonly with value true
	Then element with id ReturnTracking has attribute readonly with value true
	Then element with id PumpSerial has attribute readonly with value true

Scenario:5.4.6.10.2	User can edit Donor ID name address email Milk Bank phone notes
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When pump order created
	And user navigates to Order Management
	And element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When filter Pending in column Status in grid pumpOrdersGrid
	And element with class k-grid-Edit clicked in pumpOrdersGrid grid with row containing text Pending
	Then the page Order/EditOrder should be displayed
	When text 60002 entered in Order_DonorID
	And element with id updateDonorInfo is clicked
	Then the dropdown MilkBank has selected value Tiny Treasures Milk Bank
	Then element with id FirstName has value Erin
	Then element with id LastName has value Povich
	Then element with id Street has value 309 Cimarron Valley Trail
	Then element with id State has value MO
	Then element with id ZipCode has value 63385
	Then element with id Email has value erinpovich@yahoo.com
	Then element with id PhoneNumber has value (314)-803-6875
	When text Test Note Edit entered in Order_Notes
	Then element with id Order_Notes has value Test Note Edit
	When element with id btnUpdateOrder is clicked
	Then the page Order/OrderDetail should be displayed
	Then element with id donorId has text 60002
	Then element with id milkBank has text Tiny Treasures Milk Bank
	Then element with id firstName has text Erin
	Then element with id lastName has text Povich
	Then element with id street has text 309 Cimarron Valley Trail
	Then element with id state has text MO
	Then element with id zipCode has text 63385
	Then element with id email has text erinpovich@yahoo.com
	Then element with id phoneNumber has text (314)-803-6875
	Then element with id orderNotes has text Test Note Edit

Scenario:5.4.6.10.3	The user may edit supplies quantity in Pump Supply Order
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When pump order created
	And user navigates to Order Management
	And element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When filter Pending in column Status in grid pumpOrdersGrid
	And element with class k-grid-Edit clicked in pumpOrdersGrid grid with row containing text Pending
	Then the page Order/EditOrder should be displayed
	When value 2 entered in numeric with DoublePumpingKit id
	And value 2 entered in numeric with 24mmBreastshields id
	And value 2 entered in numeric with 27mmBreastshields id
	And value 2 entered in numeric with 30mmBreastshields id
	And value 2 entered in numeric with 8ozBottlesSet id
	And value 2 entered in numeric with PersonalFitConnectors id
	And value 2 entered in numeric with ValvesandMembranes id
	And value 2 entered in numeric with Membranes id
	And value 2 entered in numeric with Tubing id
	And value 2 entered in numeric with Bags id
	And value 2 entered in numeric with YellowBag id
	Then numeric with id DoublePumpingKit has value 2
	Then numeric with id 24mmBreastshields has value 2
	Then numeric with id 27mmBreastshields has value 2
	Then numeric with id 30mmBreastshields has value 2
	Then numeric with id 8ozBottlesSet has value 2
	Then numeric with id PersonalFitConnectors has value 2
	Then numeric with id ValvesandMembranes has value 2
	Then numeric with id Membranes has value 2
	Then numeric with id Tubing has value 2
	Then numeric with id Bags has value 2
	Then numeric with id Tubing has value 2
	Then numeric with id YellowBag has value 2

#Scenario:5.4.6.10.4	Mark the status of an order as ‘Shipped’ without tracking number (normally for bag-only orders). Upon confirmation, update ‘Shipping Date’

Scenario:5.4.6.10.5	Replacement Tracking number can edited at any time
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When pump order created
	And user navigates to Order Management
	And element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When filter Pending in column Status in grid pumpOrdersGrid
	And element with class k-grid-Edit clicked in pumpOrdersGrid grid with row containing text Pending
	Then the page Order/EditOrder should be displayed
	When text 1223-3344-4455 entered in ReplacementTracking
	Then element with id ReplacementTracking has value 1223-3344-4455
	When user navigates to Order Management
	And pump order fulfilled 60002
	And element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When filter OK in column Status in grid pumpOrdersGrid
	And element with class k-grid-Edit clicked in pumpOrdersGrid grid with row containing text OK
	Then the page Order/EditOrder should be displayed
	When text 1223-3344-4455 entered in ReplacementTracking
	Then element with id ReplacementTracking has value 1223-3344-4455

Scenario:5.4.7 More order information shall be available when the order is clicked on and will contain the following information
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When pump order created
	When user navigates to Order Management
	And element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When element with class k-i-expand clicked in pumpOrdersGrid grid with row containing text No
	Then element with class orders is visible

Scenario:5.4.7.1 Order Items
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Order Management
	And element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When element with class k-i-expand clicked in pumpOrdersGrid grid with row containing text No
	Then element with class orders is visible

Scenario:5.4.7.2 Shipping Information
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Order Management
	And element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When element with class k-i-expand clicked in pumpOrdersGrid grid with row containing text No
	Then element with class orders is visible
	When element with id PumpShippingTab is clicked
	Then element PumpShippingTab has class k-state-active

Scenario:5.4.7.3 Print Packing Slip will contain the following
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When pump order created
	When user navigates to Order Management
	And element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When filter No in column BagOnlyName in grid pumpOrdersGrid
	Then the 11 column in 1 row in pumpOrdersGrid grid should be No
	When row containing text No in grid pumpOrdersGrid is clicked
	And element with id printPackingSlips is clicked
	And switch to new tab
	Then the page Order/PackingSlip should be displayed