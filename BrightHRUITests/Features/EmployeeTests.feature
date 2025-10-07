Feature: EmployeeTests

Scenario Outline: (TEST) on <browser>
	Given I run the test on "<browser>"
	And I have navigated to the TestSite login page
	Examples: 
	| browser          |
	| browser-firefox  |
	| browser-chromium |
