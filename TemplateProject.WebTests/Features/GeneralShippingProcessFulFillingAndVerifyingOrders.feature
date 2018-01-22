Feature: General Shipping Process FulFilling And Verifying Orders
	In order to ship pump/supply and cooler orders
	As a Milk Bank User or Warehouse user
	I want to be able to fulfill and verify orders

Scenario:5.7.1.1 View outstanding orders not fulfilled by filtering status to “pending”
		Given Username MbtsMbTester1 logged in with password Prolacta99
		When cooler order is created
		And user navigates to Order Management
		And element with id coolerTab is clicked
		Then element coolerTab has class k-state-active
		When filter Pending in column Status in grid coolerOrdersGrid
		Then the 10 column in 1 row in coolerOrdersGrid grid should be Pending
		When pump order created
		And user navigates to Order Management
		When element with id pumpSupplyTab is clicked
		Then element pumpSupplyTab has class k-state-active
		When filter Pending in column Status in grid pumpOrdersGrid
		Then the 10 column in 1 row in pumpOrdersGrid grid should be Pending
		

Scenario:5.7.1.2 User can select multiple orders and print Cooler Packets, Bag Packets, Packing Slip. A total number of selected orders display
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When cooler order is created
	And cooler order is created
	And user navigates to Order Management
	And filter Pending in column Status in grid coolerOrdersGrid
	And row containing text Pending in grid coolerOrdersGrid is clicked
	Then element with id printPackets has value Print Cooler Packets (2)
	When element with id printPackets is clicked
	And switch to new tab
	Then the page Order/CoolerPacket should be displayed
	When switch to old tab
	And pump order created
	And pump order created
	And user navigates to Order Management
	And element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When filter No in column BagOnlyName in grid pumpOrdersGrid
	Then the 11 column in 1 row in pumpOrdersGrid grid should be No
	When row containing text No in grid pumpOrdersGrid is clicked
	Then element with id printPackingSlips has value Print Packing Slips (2)
	When element with id printPackingSlips is clicked
	And switch to new tab
	Then the page Order/PackingSlip should be displayed
	When switch to old tab
	And bag only order created
	And bag only order created
	And user navigates to Order Management
	When element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When filter Yes in column BagOnlyName in grid pumpOrdersGrid
	Then the 11 column in 1 row in pumpOrdersGrid grid should be Yes
	When row containing text Yes in grid pumpOrdersGrid is clicked
	Then element with id printPackingSlips has value Print Packing Slips (2)
	When element with id printPackingSlips is clicked
	And switch to new tab
	And scroll to bottom of screen
	Then the page Order/PackingSlip should be displayed

Scenario:5.7.1.3 The “fulfill” function is available for fulfillment process, which apply to Coolers and Pump Supply orders
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When cooler order is created
	And user navigates to Order Management
	And filter Pending in column Status in grid coolerOrdersGrid
	And element with class k-grid-Fulfill clicked in coolerOrdersGrid grid with row containing text Pending 
	Then the page Order/Step1 should be displayed
	When radiobutton with id ShippingMethod_Ground is clicked
	And text 1223-3344-4455 entered in TrackingNumber
	And element with id btnSubmitFulfillment is clicked
	Then the page Order/Step2 should be displayed
	When text 9988-7766-5544 entered in ReturnTrackingNumber
	And element with id btnSubmitFulfillment is clicked
	Then the page Order/Step3 should be displayed
	When element with id btnOrderManagement is clicked
	Then the page Orders should be displayed
	When pump order created
	And user navigates to Order Management
	And element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When filter Pending in column Status in grid pumpOrdersGrid
	When filter No in column BagOnlyName in grid pumpOrdersGrid
	And element with class k-grid-Fulfill clicked in pumpOrdersGrid grid with row containing text Pending
	Then the page Order/Step1 should be displayed
	When radiobutton with id ShippingMethod_Ground is clicked
	And text 1223-3344-4455 entered in TrackingNumber
	And element with id btnSubmitFulfillment is clicked
	Then the page Order/Step2 should be displayed
	When text 9988-7766-5544 entered in ReturnTrackingNumber
	And element with id btnSubmitFulfillment is clicked
	Then the page Order/Step3 should be displayed
	When element with id btnOrderManagement is clicked
	Then the page Orders should be displayed

Scenario:5.7.1.4 The “Confirm” function is available for fulfillment process, which apply to Bag Only orders
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
	Then the dropdown Order_ShippingMethod has selected value Ground
	When value 5 entered in numeric with Bags id
	And text Test Bag Only Order entered in Order_Notes
	And element with id btnPlaceOrder is clicked
	Then the page Order/OrderDetail should be displayed
	When user navigates to Order Management
	When element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When filter Yes in column BagOnlyName in grid pumpOrdersGrid
	And element with class k-grid-Confirm clicked in pumpOrdersGrid grid with row containing text 60002
	Then the kendo alert text should be Are you sure that you want to confirm this bag order?
	When click kendo alert box ok true
	Then the 10 column in 1 row in pumpOrdersGrid grid should be OK 

Scenario:5.7.1.5 Order page displays order details
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When cooler order is created
	And user navigates to Order Management
	And element with class k-i-expand clicked in coolerOrdersGrid grid with row containing text Fulfilled
	Then element with class orders is visible
	When element with id CoolerShippingTab is clicked
	Then element CoolerShippingTab has class k-state-active
	When pump order created
	And user navigates to Order Management
	When element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When element with class k-i-expand clicked in pumpOrdersGrid grid with row containing text No
	Then element with id PumpOrderItemsTab is visible
	When element with id PumpShippingTab is clicked
	Then element PumpShippingTab has class k-state-active

Scenario:5.7.1.6 Once fulfilled, Cooler Orders shall have a status of “Fulfilled”
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When cooler order is created
	When user navigates to Order Management
	When filter Pending in column Status in grid coolerOrdersGrid
	And element with class k-grid-Fulfill clicked in coolerOrdersGrid grid with row containing text Pending 
	Then the page Order/Step1 should be displayed
	When radiobutton with id ShippingMethod_Ground is clicked
	And text 1223-3344-4455 entered in TrackingNumber
	And element with id btnSubmitFulfillment is clicked
	Then the page Order/Step2 should be displayed
	When text 9988-7766-5544 entered in ReturnTrackingNumber
	And element with id btnSubmitFulfillment is clicked
	Then the page Order/Step3 should be displayed
	When element with id btnOrderManagement is clicked
	Then the page Orders should be displayed
	When filter Fulfilled in column Status in grid coolerOrdersGrid
	Then the 10 column in row containing 60002 text in coolerOrdersGrid grid should be Fulfilled

Scenario:5.7.1.7 Once fulfilled, Pump/Supply/Bag Only Orders shall have a status of “OK”
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
	Then the dropdown Order_ShippingMethod has selected value Ground
	When value 1 entered in numeric with DoublePumpingKit id
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
	And value 1 entered in numeric with 24mmBreastshields id 
	And text Test Note entered in Order_Notes
	And element with id btnPlaceOrder is clicked
	Then the page Order/OrderDetail should be displayed
	When user navigates to Order Management
	When element with id pumpSupplyTab is clicked
	Then element pumpSupplyTab has class k-state-active
	When filter Pending in column Status in grid pumpOrdersGrid
	And element with class k-grid-Fulfill clicked in pumpOrdersGrid grid with row containing text Pending
	Then the page Order/Step1 should be displayed
	When radiobutton with id ShippingMethod_Ground is clicked
	And text 1223-3344-4455 entered in TrackingNumber
	And element with id btnSubmitFulfillment is clicked
	Then the page Order/Step2 should be displayed
	When text 9988-7766-5544 entered in ReturnTrackingNumber
	And element with id btnSubmitFulfillment is clicked
	Then the page Order/Step3 should be displayed
	When element with id btnOrderManagement is clicked
	Then the page Orders should be displayed
	When filter OK in column Status in grid pumpOrdersGrid
	Then the 10 column in row containing 60002 text in pumpOrdersGrid grid should be OK

Scenario:5.7.2 Verifying Orders apply to only Cooler Orders
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When cooler order is created
	When user navigates to Order Management
	And cooler order fulfilled 60002
	And user logs out 
	Given Username MbtsWHTester1 logged in with password Prolacta99
	When user navigates to Order Management
	And filter Fulfilled in column Status in grid coolerOrdersGrid
	And element with class k-grid-Verify clicked in coolerOrdersGrid grid with row containing text Fulfilled
	Then the page Order/Verify should be displayed
	When element with id btnSubmitVerification is clicked
	Then the page Order/Verified should be displayed
	When element with id btnOrderManagement is clicked
	Then the page Orders should be displayed
	When filter OK in column Status in grid coolerOrdersGrid
	Then the 10 column in row containing 60002 text in coolerOrdersGrid grid should be OK

Scenario:5.7.2.1 User cannot verify his her own fulfilled order.
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When cooler order is created
	When user navigates to Order Management
	And cooler order fulfilled 60002
	And filter Fulfilled in column Status in grid coolerOrdersGrid
	And element with class k-grid-Verify clicked in coolerOrdersGrid grid with row containing text mbtsmbtester1
	Then the page Order/Verify should be displayed
	When element with id btnCancel is clicked
	Then the page Orders should be displayed
	When filter Fulfilled in column Status in grid coolerOrdersGrid
	Then the 10 column in row containing 60002 text in coolerOrdersGrid grid should be Fulfilled 

Scenario:5.7.2.2 Allow user to view outstanding orders that were not verified by filtering the status to “fulfilled”.
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Order Management
	And filter Fulfilled in column Status in grid coolerOrdersGrid
	Then the 10 column in 1 row in coolerOrdersGrid grid should be Fulfilled 

Scenario:5.7.2.3 User can click “verify” to each order to navigate to Order Verification
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When cooler order is created
	When user navigates to Order Management
	And cooler order fulfilled 60002
	And user logs out 
	Given Username MbtsWHTester1 logged in with password Prolacta99
	When user navigates to Order Management
	And filter Fulfilled in column Status in grid coolerOrdersGrid
	And element with class k-grid-Verify clicked in coolerOrdersGrid grid with row containing text Fulfilled
	Then the page Order/Verify should be displayed

Scenario:5.7.2.4 Once verified, an order shall have a status “OK”. Upon confirmation, this will update shipping date.
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When cooler order is created
	When user navigates to Order Management
	And cooler order fulfilled 60002
	And user logs out 
	Given Username MbtsWHTester1 logged in with password Prolacta99
	When user navigates to Order Management
	And filter Fulfilled in column Status in grid coolerOrdersGrid
	And element with class k-grid-Verify clicked in coolerOrdersGrid grid with row containing text Fulfilled
	Then the page Order/Verify should be displayed
	When element with id btnSubmitVerification is clicked
	Then the page Order/Verified should be displayed
	When element with id btnOrderManagement is clicked
	Then the page Orders should be displayed
	When filter OK in column Status in grid coolerOrdersGrid
	Then the 10 column in row containing 60002 text in coolerOrdersGrid grid should be OK
	  