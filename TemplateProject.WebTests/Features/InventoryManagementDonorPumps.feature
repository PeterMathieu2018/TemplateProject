Feature: Inventory Management Donor Pumps
	In order manage Pumps
	As a Milk Bank or Warehouse user
	I want to be able to add, edit, and delete pump information

Scenario:5.5.1.1 Ability to add a new pump
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to pumps
	And element with id btnAddPump is clicked
	And text 987654321 entered in PumpSerialNumber
	And text 01/11/2018 entered in DateAcquired
	And dropdown with id pumpStatus value Available
	And text 0 entered in HoursUsed
	And text Test Note entered in Notes
	And element with class k-grid-update is clicked
	Then the 1 column in 1 row in Inventory grid should be 987654321 
	Then the 2 column in 1 row in Inventory grid should be 01/11/2018
	Then the 3 column in 1 row in Inventory grid should be Available
	Then the 4 column in 1 row in Inventory grid should be 0
	Then the 5 column in 1 row in Inventory grid should be Test Note

Scenario:5.5.1.1.2 User can edit the Serial Number, Date Acquired, and Notes.
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to pumps
	And element with class k-grid-edit is clicked
	And dropdown with id pumpStatus value Pending
	And text 1 entered in HoursUsed
	And text This is an edit to a note. entered in Notes
	And element with class k-grid-update is clicked
	Then the 3 column in 1 row in Inventory grid should be Pending
	Then the 4 column in 1 row in Inventory grid should be 1
	Then the 5 column in 1 row in Inventory grid should be This is an edit to a note.

Scenario:5.5.1.1.3 User can change the option of ‘Status’ to ‘Returned but Not Available’ for the scenario that a pump is checked out for other purpose.
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to pumps
	And element with class k-grid-edit is clicked
	And dropdown with id pumpStatus value Maintenance
	And element with class k-grid-update is clicked
	Then the 3 column in 1 row in Inventory grid should be Maintenance

Scenario:5.5.1.2 Ability to search Pump Inventory
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When new pump added
	And text 987654321 entered in pumpSearchBox
	Then the 1 column in 1 row in Inventory grid should be 987654321

Scenario:5.5.1.3 A table containing Pump Inventory information
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to pumps
	Then the Inventory grid contains column Pump Serial Number
	Then the Inventory grid contains column Date Acquired
	Then the Inventory grid contains column Status
	Then the Inventory grid contains column Hours Used
	Then the Inventory grid contains column Notes

Scenario:5.5.1.3.3 Status. The status contains Available, Pending, Retired, Not Returned, Maintenance
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to pumps
	And element with id btnAddPump is clicked
	Then dropdown with id pumpStatus has Available,Pending,Not Returned,Returned,Retired,Maintenance

Scenario:5.5.1.3.6 The usage history shall be available when clicking for the number of times used with the following information
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When pump order created
	And user navigates to pumps
	And text 987654321 entered in pumpSearchBox
	When element with class k-i-expand clicked in Inventory grid with row containing text 987654321
	Then element with id PumpHistory is visible 
	Then the PumpHistory grid contains column Order ID
	Then the PumpHistory grid contains column Donor ID
	Then the PumpHistory grid contains column First Name
	Then the PumpHistory grid contains column Last Name
	Then the PumpHistory grid contains column Status
	Then the PumpHistory grid contains column Order Actual Date
	Then the PumpHistory grid contains column Tracking Number
	Then the PumpHistory grid contains column Date Shipped
	Then the PumpHistory grid contains column Return Tracking Number
	Then the PumpHistory grid contains column Date Returned

Scenario:5.5.1.4.1 User can edit only the Status, Hours Used, and Notes
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to pumps
	And element with class k-grid-edit is clicked
	Then element with id PumpSerialNumber has attribute readonly with value true
	Then element with id DateAcquired has attribute readonly with value true

Scenario:5.5.1.4.2.1 Will be used for warehouse to state pumps have been returned
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to pumps
	And element with id statusHeader is clicked
	And element with class k-grid-edit is clicked
	And dropdown with id pumpStatus value Returned
	And element with class k-grid-update is clicked
	Then the 3 column in 1 row in Inventory grid should be Returned

Scenario:5.5.1.4.2.2 Upon confirmation, this will update the Return Date
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When pump order created
	And user navigates to pumps
	And text Pending entered in pumpSearchBox
	And element with class k-grid-edit is clicked
	And dropdown with id pumpStatus value Returned
	And element with class k-grid-update is clicked
	When element with class k-i-expand clicked in Inventory grid with row containing text Returned
	Then element with id PumpHistory is visible 

Scenario:5.5.1.5 User shall be able to delete entries of pump inventory
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to pumps
	And element with id statusHeader is clicked
	And element with class k-grid-delete is clicked
	Then the alert text should be Are you sure you want to delete this record?
	When click alert box ok true
	Then the number of rows in grid Inventory should be 14