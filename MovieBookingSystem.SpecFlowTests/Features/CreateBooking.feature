Feature: CreateBooking
	
Scenario: Create booking successfully
	Given a list of available movies
	When i create a booking
	Then the booking should be added successfully
	
