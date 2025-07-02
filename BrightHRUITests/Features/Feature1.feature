Feature: Feature1

Scenario Outline: Login Successfully, Navigate to Employee Page on <browser>
	Given I run the test on "<browser>"
	And I have navigated to the BrightHR login page
	#When I enter valid login credentials
	#And I have selected Employees from the menu
	#Then I am displayed the employees page
	Examples: 
	| browser          |
	| browser-firefox  |
	| browser-webkit   |
	| browser-chromium |
