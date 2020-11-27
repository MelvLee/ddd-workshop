Feature: SeatReservation

Scenario: Customer reserves an available seat
	Given the Screening for Movie 'Black Widow' scheduled on '2020-11-27 12:30' in Room 1 in Cinema 'Pathe Scheveningen'
	And there are no Reservations
	When Customer 'Alfred' reserves Seats 'A-1'
	Then Seats 'A-1' should be reserved for Customer 'Alfred' for the given Screening

Scenario: Customer reserves a reserved seat
	Given the Screening for Movie 'Black Widow' scheduled on '2020-11-27 12:30' in Room 1 in Cinema 'Pathe Scheveningen'
	And the Reservations
	| Customer | Seats    | Screening                                                    |
	| Alfred   | A-1, A-2 | Black Widow in Pathe Scheveningen, Room 1 @ 27-11-2020 12:30 |
	When Customer 'Clark' reserves Seats 'A-2, A-3'
	Then Customer 'Clark' should be informed that the Seats are not available

Scenario: Customer reserves multiple available seats
	Given the Screening for Movie 'Black Widow' scheduled on '2020-11-27 12:30' in Room 1 in Cinema 'Pathe Scheveningen'
	And the Reservations
	| Customer | Seats    | Screening                                                    |
	| Alfred   | A-1, A-2 | Black Widow in Pathe Scheveningen, Room 1 @ 27-11-2020 12:30 |
	| Clark    | B-4      | Black Widow in Pathe Scheveningen, Room 1 @ 27-11-2020 12:30 |
	When Customer 'Bruce' reserves Seats 'A-5, A-6 '
	Then Seats 'A-5, A-6' should be reserved for Customer 'Bruce' for the given Screening

Scenario: Customer requests his reservations
	Given the Screening for Movie 'Black Widow' scheduled on '2020-11-27 12:30' in Room 1 in Cinema 'Pathe Scheveningen'
	And the Reservations
	| Customer | Seats    | Screening                                                    |
	| Alfred   | A-1, A-2 | Black Widow in Pathe Scheveningen, Room 1 @ 27-11-2020 12:30 |
	When Customer 'Alfred' queries his Reservations
	Then Customer 'Alfred' should get the following Reservations
	| Customer | Seats    | Screening                                                    |
	| Alfred   | A-1, A-2 | Black Widow in Pathe Scheveningen, Room 1 @ 27-11-2020 12:30 |

Scenario: Customer reserves additional seats and see their full reservations
	Given the Screening for Movie 'Black Widow' scheduled on '2020-11-27 12:30' in Room 1 in Cinema 'Pathe Scheveningen'
	And the Reservations
	| Customer | Seats    | Screening                                                    |
	| Alfred   | A-1, A-2 | Black Widow in Pathe Scheveningen, Room 1 @ 27-11-2020 12:30 |
	When Customer 'Alfred' reserves Seats 'A-5, A-6 '
	And Customer 'Alfred' queries his Reservations
	Then Customer 'Alfred' should get the following Reservations
	| Customer | Seats              | Screening                                                    |
	| Alfred   | A-1, A-2, A-5, A-6 | Black Widow in Pathe Scheveningen, Room 1 @ 27-11-2020 12:30 |
