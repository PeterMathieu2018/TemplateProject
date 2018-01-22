Feature: InventoryManagementDononrSupplies
	In order to manage Donor Suppliese
	As a milk Bank User
	I want to be able to add, edit, and delete supplies

Scenario:5.6.1 User has the ability to add new supply record
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to supplies
	And element with id btnAddSupplies is clicked
	And text TestItem entered in ItemName
	And text 0122334444 entered in SupplyPartNumber
	And text 0122334444 entered in ProlactaPartNumber	
	And element with class k-grid-update is clicked
	Then the 1 column in 1 row in Supplies grid should be TestItem 
	Then the 2 column in 1 row in Supplies grid should be 0122334444
	Then the 3 column in 1 row in Supplies grid should be 0122334444
	When element with class k-grid-delete clicked in Supplies grid with row containing text TestItem
	Then the alert text should be Are you sure you want to delete this record?
	When click alert box ok true
	Then the number of rows in grid Supplies should be 11

Scenario:5.6.2 The supply inventory page shall include
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to supplies
	Then the Supplies grid contains column Image Path
	Then the Supplies grid contains column Item Name
	Then the Supplies grid contains column Prolacta Part Number
	Then the Supplies grid contains column Supply Part Number

Scenario:5.6.2.5 Ability to edit entries of supply inventory
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to supplies
	And element with id btnAddSupplies is clicked
	And text TestItem entered in ItemName
	And text 0122334444 entered in SupplyPartNumber
	And text 0122334444 entered in ProlactaPartNumber	
	And element with class k-grid-update is clicked
	Then the 1 column in 1 row in Supplies grid should be TestItem 
	Then the 2 column in 1 row in Supplies grid should be 0122334444
	Then the 3 column in 1 row in Supplies grid should be 0122334444
	When element with class k-grid-edit clicked in Supplies grid with row containing text TestItem
	And text TestItemUpdate entered in ItemName
	And text 1 entered in ProlactaPartNumber
	And text 0 entered in SupplyPartNumber
	And element with class k-grid-update is clicked
	Then the 1 column in row containing TestItemUpdate text in Supplies grid should be TestItemUpdate
	Then the 2 column in row containing TestItemUpdate text in Supplies grid should be 1
	Then the 3 column in row containing TestItemUpdate text in Supplies grid should be 0
	When element with class k-grid-delete clicked in Supplies grid with row containing text TestItemUpdate
	Then the alert text should be Are you sure you want to delete this record?
	When click alert box ok true
	Then the number of rows in grid Supplies should be 11

Scenario:5.6.2.6 Ability to delete entries of supply inventory
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to supplies
	And element with id btnAddSupplies is clicked
	And text TestItem entered in ItemName
	And text 0122334444 entered in SupplyPartNumber
	And text 0122334444 entered in ProlactaPartNumber	
	And element with class k-grid-update is clicked
	Then the 1 column in 1 row in Supplies grid should be TestItem 
	Then the 2 column in 1 row in Supplies grid should be 0122334444
	Then the 3 column in 1 row in Supplies grid should be 0122334444
	When element with class k-grid-delete clicked in Supplies grid with row containing text TestItem
	Then the alert text should be Are you sure you want to delete this record?
	When click alert box ok true
	Then the number of rows in grid Supplies should be 11