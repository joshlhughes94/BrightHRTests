Feature: Feature1

Scenario Outline: Login Successfully, Navigate to Employee Page on <browser>
	Given I run the test on "<browser>"
	And I have navigated to the BrightHR login page
	When I enter valid login credentials
	And I have selected Employees from the menu
	And I have selected the Add Employee Button
	When I have created a valid Employee
	Then I am displayed the employee added success message
	And I select the add another employee button
	And I create a second employee
	Then I am displayed both employees in the employees list
	Examples: 
	| browser          |
	| browser-firefox  |
	| browser-webkit   |
	| browser-chromium |
