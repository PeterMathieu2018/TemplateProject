Feature: Order Management Donor Cooler
	In order to manage Cooler orders
	As a milk bank or warehouse user
	I want to be able to view, fulfill, verify, and print cooler packets

Scenario:5.3.1 A table to show all Cooler Orders in any status for donors
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Order Management
	And element with id coolerTab is clicked
	Then element coolerTab has class k-state-active

Scenario:5.3.2 An export feature to export the history view shall be added with current filters
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Order Management
	And element with id coolerTab is clicked
	Then element coolerTab has class k-state-active
	When element with class k-grid-excel is clicked
	Then file with name Cooler Order Export was exported

Scenario:5.3.3 User may also filter order table by every category except Donor Name and Outgoing Tracking Number
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When cooler order is created
	When user navigates to Order Management
	And filter 60002 in column DonorID in grid coolerOrdersGrid
	Then the 2 column in 1 row in coolerOrdersGrid grid should be 60002
	When filter on grid coolerOrdersGrid is cleared
	And filter Tiny Treasures Milk Bank in column MilkBankName in grid coolerOrdersGrid
	Then the 4 column in 1 row in coolerOrdersGrid grid should be Tiny Treasures Milk Bank
	When filter on grid coolerOrdersGrid is cleared
	And filter mbtsmbtester1 in column OrderBy in grid coolerOrdersGrid
	Then the 9 column in 1 row in coolerOrdersGrid grid should be mbtsmbtester1
	When filter on grid coolerOrdersGrid is cleared
	And filter Pending in column Status in grid coolerOrdersGrid
	Then the 10 column in 1 row in coolerOrdersGrid grid should be Pending

Scenario:5.3.3.1 Filter by Print Date
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When cooler order is created
	And cooler order is created
	And cooler packet is printed
	And filter currentDate in column DatePrinted in grid coolerOrdersGrid
	Then the 11 column in 1 row in coolerOrdersGrid grid should be currentDate

Scenario:5.3.4 User may also filter Cooler Order Management through Donor ID Search Bar
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Order Management
	And text 60002 entered in searchOrderId
	And element with id searchDonorID is clicked
	Then the 2 column in 1 row in coolerOrdersGrid grid should be 60002

Scenario:5.3.4.1 User will be able to see all cooler orders for a donor
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Order Management
	And filter 60002 in column DonorID in grid coolerOrdersGrid
	Then the 2 column in 1 row in coolerOrdersGrid grid should be 60002

Scenario:5.3.4.2 User may also select each cooler order individually for the donor
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When cooler order is created
	And user navigates to Order Management
	And filter Pending in column Status in grid coolerOrdersGrid
	And element with class k-i-expand clicked in coolerOrdersGrid grid with row containing text Pending

Scenario:5.3.5 Checkbox option present for each cooler order The user will be able to mark the option as checked
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When cooler order is created
	And cooler order is created
	And cooler packet is printed
	And filter 60002 in column DonorID in grid coolerOrdersGrid
	Then the 2 column in 1 row in coolerOrdersGrid grid should be 60002
	When element with class checkbox clicked in coolerOrdersGrid grid with row containing text Erin Povich
	Then element with class checkbox has attribute checked equal to true in coolerOrdersGrid grid with row with text Erin Povich

Scenario:5.3.6 An option to Print Cooler Packets
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When cooler order is created
	And cooler order is created
	And user navigates to Order Management
	And row containing text Not Printed in grid coolerOrdersGrid is clicked
	And element with id printPackets is clicked
	And switch to new tab
	Then the page Order/CoolerPacket should be displayed

Scenario:5.3.6.1.1 Total Count of cooler orders shall be displayed for the coolers being selected
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Order Management
	And filter null in column DatePrinted in grid coolerOrdersGrid
	And row containing text Not Printed in grid coolerOrdersGrid is clicked
	Then element with id printPackets has value Print Cooler Packets (2)

Scenario:5.3.7 User has the option to filter by Cooler Order Statuses
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When cooler order is created
	And user navigates to Order Management
	And filter Pending in column Status in grid coolerOrdersGrid
	Then the 10 column in 1 row in coolerOrdersGrid grid should be Pending
	#When filter on grid coolerOrdersGrid is cleared
	#And filter ReturnDelivered in column Status in grid coolerOrdersGrid
	#Then the 10 column in 1 row in coolerOrdersGrid grid should be ReturnDelivered
	When filter on grid coolerOrdersGrid is cleared
	And cooler order fulfilled 60002
	And filter Fulfilled in column Status in grid coolerOrdersGrid
	Then the 10 column in 1 row in coolerOrdersGrid grid should be Fulfilled
	When filter on grid coolerOrdersGrid is cleared
	And cooler order verified 60002
	And filter OK in column Status in grid coolerOrdersGrid
	Then the 10 column in 1 row in coolerOrdersGrid grid should be OK

Scenario:5.3.8 A list of all Cooler orders shall include
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Order Management
	Then the coolerOrdersGrid grid contains column Order #
	Then the coolerOrdersGrid grid contains column Donor ID
	Then the coolerOrdersGrid grid contains column Milk Bank Name
	Then the coolerOrdersGrid grid contains column Order Date
	Then the coolerOrdersGrid grid contains column Shipping Date
	Then the coolerOrdersGrid grid contains column Out Tracking
	Then the coolerOrdersGrid grid contains column Return Date
	Then the coolerOrdersGrid grid contains column Ordered By
	Then the coolerOrdersGrid grid contains column Status

Scenario:5.3.8.10.1 User can edit Donor ID name address email Milk Bank phone notes if not fulfilled
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Order Management
	And filter Pending in column Status in grid coolerOrdersGrid
	And element with class k-grid-Edit clicked in coolerOrdersGrid grid with row containing text Pending
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

Scenario:5.3.8.10.2	If order is fulfilled, Donor ID, Shipping Information, Outgoing Tracking and Return Tracking Label cannot be edited
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Order Management
	And filter Fulfilled in column Status in grid coolerOrdersGrid
	And element with class k-grid-Edit clicked in coolerOrdersGrid grid with row containing text Fulfilled
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

Scenario:5.3.8.10.3	The user may edit supplies quantity in Cooler Order if an order is not fulfilled.
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When cooler order is created
	And user navigates to Order Management
	And filter Pending in column Status in grid coolerOrdersGrid
	And element with class k-grid-Edit clicked in coolerOrdersGrid grid with row containing text Pending
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

#Scenario:5.3.8.10.3 need to confirm with business.

Scenario:5.3.8.10.5	Replacement Tracking number can edited at any time
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When cooler order is created
	When user navigates to Order Management
	And filter Pending in column Status in grid coolerOrdersGrid
	And element with class k-grid-Edit clicked in coolerOrdersGrid grid with row containing text Pending
	Then the page Order/EditOrder should be displayed
	When text 1223-3344-4455 entered in ReplacementTracking
	Then element with id ReplacementTracking has value 1223-3344-4455
	When user navigates to Order Management
	And cooler order fulfilled 60002
	And filter Fulfilled in column Status in grid coolerOrdersGrid
	And element with class k-grid-Edit clicked in coolerOrdersGrid grid with row containing text Fulfilled
	Then the page Order/EditOrder should be displayed
	When text 1223-3344-4455 entered in ReplacementTracking
	Then element with id ReplacementTracking has value 1223-3344-4455
	When user navigates to Order Management
	And cooler order verified 60002
	And filter OK in column Status in grid coolerOrdersGrid
	And element with class k-grid-Edit clicked in coolerOrdersGrid grid with row containing text OK
	Then the page Order/EditOrder should be displayed
	When text 1223-3344-4455 entered in ReplacementTracking
	Then element with id ReplacementTracking has value 1223-3344-4455
	#When user navigates to Order Management
	#And filter ReturnDelivered in column Status in grid coolerOrdersGrid
	#And element with class k-grid-Edit clicked in coolerOrdersGrid grid with row containing text ReturnDelivered
	#Then the page Order/EditOrder should be displayed
	#When text 1223-3344-4455 entered in ReplacementTracking
	#Then element with id ReplacementTracking has value 1223-3344-4455

Scenario:5.3.9 More order information shall be available when the order is clicked on and will contain the following information
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When cooler order is created
	And user navigates to Order Management
	And element with class k-i-expand clicked in coolerOrdersGrid grid with row containing text 60002
	Then element with class orders is visible

Scenario:5.3.9.1 Order Items
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When cooler order is created
	And user navigates to Order Management
	And element with class k-i-expand clicked in coolerOrdersGrid grid with row containing text 60002
	Then element with class orders is visible

Scenario:5.3.9.2 Shipping
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Order Management
	And element with class k-i-expand clicked in coolerOrdersGrid grid with row containing text Fulfilled
	Then element with class orders is visible
	When element with id CoolerShippingTab is clicked
	Then element CoolerShippingTab has class k-state-active

#Scenario:5.3.10	Print Cooler Packets will contain a donor face and a donor shipment page 
#	Given Username MbtsMbTester1 logged in with password Prolacta99
#	When user navigates to Order Management
#	And row containing text Not Printed in grid coolerOrdersGrid is clicked
#	And element with id printPackets is clicked
#	And switch to new tab
#	Then the page Order/CoolerPacket should be displayed