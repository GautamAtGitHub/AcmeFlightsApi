# AcmeFlightsApi
Acme Flights API POC

## Problem definition

Acme Remote Flights is a helicopter flight operator which specialises in transporting passengers to remote cities. 
 
The flights all operate within the same time zone.  The same flights repeat every day of the year.

Need to create a REST API to Check the flight availability : by giving start and end Date. No of pax.

## Highlights
1. Implemented RESTful Web API for ACME flight. 
2. Flights information is already loaded into the system.
2. Used in memory database to store the values.
3. Implemented Swagger for testing. Sample JSON requests for each operation have been included on Swagger page. Please visit `http://<server>/swagger` to access swagger.
4. Implemented HATEOAS guidelines to let the users know 'what to do next'.


## How to Use the API
1. Check flights availability by providing StartDate, EndDate and No of PAX.
2. If flight is available, then send next request to book the flight with available Schedule ID.
3. Check if booking is confirmed by giving the booking ID
4. After the booking now you can check if the availability is decreased of not.

## Assumptions
1. Each flight have only 6 seats to fill (Since it is a small helicopter)
2. After each booking availability will decrease.
3. It is assumed that after each booking money is collected from payment getway.
