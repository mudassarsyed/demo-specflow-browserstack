Feature: GoogleSearch

	@Browser_Chrome
	@Browser_Safari
	@Browser_Firefox
	Scenario: Goto Google
		Given goto Google
		Then title should be 'Google'
