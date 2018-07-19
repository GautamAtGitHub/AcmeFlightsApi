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
1. Download, build and run the AcmeFlightsAPI solution. Then navigate to <b>`http://<server>/swagger`</b>. This will open swagger test page as below. 
 
 ![image](https://user-images.githubusercontent.com/34414643/42915797-5b08df20-8b45-11e8-88c1-be72f95ba526.png)

2. To check flights availability. 
  - click on `/api/Flights/Availability` from swagger UI and press `Try it out`. 
  - Provide <b>StartDate, EndDate and No of PAX</b>. 
  - You can use start date as today and end date as tomorrow and No of Pax upto 6. 
  - Press Execute.
  
<b>Curl - </b> 
`curl -X GET "http://localhost:54808/api/Flights/Availability?StartDate=07%2F19%2F2018&EndDate=07%2F20%2F2018&NumberOfPax=3" -H "accept: application/json"`
 
 ![image](https://user-images.githubusercontent.com/34414643/42915994-3defdba4-8b46-11e8-9d70-a8d77db5722f.png)
 
3. You will receive following response, which includes Schedule object with id `1001` as well as flight booking API link to book the flight.
 
 ![image](https://user-images.githubusercontent.com/34414643/42916058-a8b5eb0e-8b46-11e8-9f04-21186c485f8a.png)

4. If flight schedule is available, then send next request to book the flight with available Schedule ID, Name and No of Pax as below.

<b>Curl - </b> 
`curl -X POST "http://localhost:54808/api/Flights/Booking/1001" -H "accept: application/json" -H "Content-Type: application/json-patch+json" -d "{ \"bookingId\": 0, \"name\": \"Gautam\", \"noOfPax\": 3, \"bookingDate\": \"2018-07-19T01:40:39.085Z\", \"scheduleId\": 1001}"`

 ![image](https://user-images.githubusercontent.com/34414643/42916270-b6db9336-8b47-11e8-85ba-68edcbb4bba0.png)
 
 5. As per following response, booking is confirmed with booking id 1
 ![image](https://user-images.githubusercontent.com/34414643/42916541-203bb5c6-8b49-11e8-9508-c4da29829c8f.png)

 6. Check if booking is confirmed by giving the booking ID 1
 
 <b>Curl</b> `curl -X GET "http://localhost:54808/api/Flights/Booking/1" -H "accept: application/json"`
 
 ![image](https://user-images.githubusercontent.com/34414643/42916587-661b2130-8b49-11e8-8427-121ab793c0b3.png)
 ![image](https://user-images.githubusercontent.com/34414643/42916610-79ed2bd6-8b49-11e8-867d-23de7387074d.png)

 7. After the booking you can check if the availability is decreased or not. The availability should be 3 seats after first booking.
 
 <b>Curl</b> `curl -X GET "http://localhost:54808/api/Flights/Availability?StartDate=07%2F19%2F2018&EndDate=07%2F20%2F2018&NumberOfPax=3" -H "accept: application/json"`
 
 ![image](https://user-images.githubusercontent.com/34414643/42916649-af88fdb0-8b49-11e8-9929-ed51d455f190.png)

## Unit Test Results.

Following is the unit test results. All unit test have passed successfully. We can add more advanced unit tests to this solution given more time.

![image](https://user-images.githubusercontent.com/34414643/42916851-8d087116-8b4a-11e8-9fa2-f70daf9717e5.png)


