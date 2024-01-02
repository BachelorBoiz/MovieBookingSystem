Feature: CreateBooking
	
Scenario: Create booking successfully
	Given a list of available movies
	And a customer with no overdue bookings
	And a booking with a valid date span
	When i create a booking
	Then the booking should be added successfully
	
Scenario: Failing to create booking when customer has overdue booking
	Given a list of available movies
	And a customer with overdue bookings
	And a booking with a valid date span
	When i create a booking
	Then the booking should throw a CustomerHasOverDueBookingException
	
Scenario: Failing to create booking when movie is unavailable
	Given a list with an unavailable movie
	And a customer with no overdue bookings
	And a booking with a valid date span
	When i create a booking
	Then the booking should throw a MovieUnavailableException
	
Scenario: Failing to create booking when customer tries to book more movies than available
	Given a list of available movies with limited quantities
	And a customer with no overdue bookings
	And a booking with an attempt at booking more movies than available and a valid date span
	When i create a booking
	Then the booking should throw a MovieUnavailableException
	
Scenario: Failing to create a booking with invalid date span
	Given a list of available movies
	And a customer with no overdue bookings
	And a booking with an invalid date span
	When i create a booking
	Then the booking should throw a InvalidDateSpanException
	
	
