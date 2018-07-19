# Acme Flights Api
Acme Flights API POC

## Problem definition

Acme Remote Flights is a helicopter flight operator which specialises in transporting passengers to remote cities. 
 
The flights all operate within the same time zone.  The same flights repeat every day of the year.

Need to create a REST API to Check the flight availability : by giving start and end Date. No of pax.

## Highlights
1. Implemented <b>RESTful Web API</b> for ACME flight. The solution is designed using <b>.Net Core</b> 
2. Used <b>`in-memory`</b> database to store the values and Flights information is already loaded into the system.
3. Implemented <b>`HATEOAS`</b> guidelines to let the client know <b>'what to do next'.</b>
4. <b>Repository pattern</b> is implemented to assist unit testing.
5. <b>Unit tests</b> have been written for the important methods of API.
6. In-memory database is used to create <b>mock object</b> for Unit testing purpose.
7. Proper <b>Exception handling</b> is implemented.
8. Implemented <b>Swagger</b> for testing. Sample JSON requests for each operation have been included on Swagger page. Please visit <b>`http://<server>/swagger`</b> to access swagger.

## Assumptions
1. Each flight have only 6 seats to fill (Since it is a small helicopter)
2. After each booking, the flght seat availability will decrease.
3. It is assumed that after each booking money is collected from payment getway.

## How to Use the API
1. Check flights availability by providing StartDate, EndDate and No of PAX.
2. If flight is available, then send next request to book the flight with available Schedule ID.
3. Check if booking is confirmed by giving the booking ID
4. After the booking now you can check if the availability is decreased of not.


