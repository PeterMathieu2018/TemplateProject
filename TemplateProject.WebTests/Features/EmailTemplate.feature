Feature: Email Template
	In order to send out thank you and follow up emails to donors
	As a Milk Bank User
	I want to be able to add, edit, and delete Email Templates

Scenario:5.10.1	User can add new Thank You email templates. Include templates for the following Milk Banks
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Email Templates
	And element with id btnAddEmailTemplate is clicked
	And dropdown with id TemplateType value Thank You Letter
	And dropdown with id TemplateMilkBank value Tiny Treasures Milk Bank
	And text Test TTB Thank You entered in TemplateTitle
	And text TTTBTY entered in ShortTitle
	And text Test Subject entered in TemplateSubject
	And text Test Body entered in TemplateBody
	And text Test Signature entered in TemplateSignature
	And element with class k-grid-update is clicked
	Then the 1 column in row containing Test TTB Thank You text in EmailTemplate grid should be Tiny Treasures Milk Bank
	Then the 3 column in row containing Test TTB Thank You text in EmailTemplate grid should be Thank You Letter
	When email template deleted Test TTB Thank You
	Then the number of rows in grid EmailTemplate should be 0

Scenario:5.10.2	Edit function is available for each of the Thank You email templates
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When thank you email template created
	And element with class k-grid-edit clicked in EmailTemplate grid with row containing text Test TTB Thank You
	And dropdown with id TemplateType value HH Follow Up
	And dropdown with id TemplateMilkBank value Helping Hands (Komen) Milk Bank
	And text Test HH Follow Up entered in TemplateTitle
	And text THHFU entered in ShortTitle
	And element with class k-grid-update is clicked
	Then the 1 column in row containing Test HH Follow Up text in EmailTemplate grid should be Helping Hands (Komen) Milk Bank
	Then the 3 column in row containing Test HH Follow Up text in EmailTemplate grid should be HH Follow Up
	When email template deleted Test HH Follow Up
	Then the number of rows in grid EmailTemplate should be 0

Scenario:5.10.2.1 A function to activate the email template
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Email Templates
	And element with id btnAddEmailTemplate is clicked
	And dropdown with id TemplateType value Thank You Letter
	And dropdown with id TemplateMilkBank value Tiny Treasures Milk Bank
	And text Test TTB Thank You entered in TemplateTitle
	And text TTTBTY entered in ShortTitle
	And element with id TemplateActive is clicked
	And text Test Subject entered in TemplateSubject
	And text Test Body entered in TemplateBody
	And text Test Signature entered in TemplateSignature
	And element with class k-grid-update is clicked
	Then the 1 column in row containing Test TTB Thank You text in EmailTemplate grid should be Tiny Treasures Milk Bank
	Then the 3 column in row containing Test TTB Thank You text in EmailTemplate grid should be Thank You Letter
	Then the 4 column in row containing Test TTB Thank You text in EmailTemplate grid should be Yes
	When email template deleted Test TTB Thank You
	Then the number of rows in grid EmailTemplate should be 0

Scenario:5.10.2.2 A function to set email to send automatically
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Email Templates
	And element with id btnAddEmailTemplate is clicked
	And dropdown with id TemplateType value Thank You Letter
	And dropdown with id TemplateMilkBank value Tiny Treasures Milk Bank
	And text Test TTB Thank You entered in TemplateTitle
	And text TTTBTY entered in ShortTitle
	And element with id AutomaticSend is clicked
	And text Test Subject entered in TemplateSubject
	And text Test Body entered in TemplateBody
	And text Test Signature entered in TemplateSignature
	And element with class k-grid-update is clicked
	Then the 5 column in row containing Test TTB Thank You text in EmailTemplate grid should be Yes
	When email template deleted Test TTB Thank You
	Then the number of rows in grid EmailTemplate should be 0

Scenario:5.10.2.3 A function to set number of days for email to be sent automatically
	Given Username MbtsMbTester1 logged in with password Prolacta99
	When user navigates to Email Templates
	And element with id btnAddEmailTemplate is clicked
	And dropdown with id TemplateType value HH Follow Up
	And dropdown with id TemplateMilkBank value Helping Hands (Komen) Milk Bank
	And text Test HH Follow Up entered in TemplateTitle
	And text THHFU entered in ShortTitle
	And element with id AutomaticSend is clicked
	And value 5 entered in numeric with DaysFromLeadToSend id 
	And text Test Subject entered in TemplateSubject
	And text Test Body entered in TemplateBody
	And text Test Signature entered in TemplateSignature
	And element with class k-grid-update is clicked
	Then the 6 column in row containing Test HH Follow Up text in EmailTemplate grid should be 5
	When email template deleted Test HH Follow Up
	Then the number of rows in grid EmailTemplate should be 0

