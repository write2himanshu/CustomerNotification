# Customer Notification
Application constructs and sends out the notification based on the communication type channel 
# **Folders**

## CustomerNotification.API

This contains API solution built in DotNet Core 3.1
> Build and Run the Solution => Swagger documentation page is displayed

> Use the swagger page to execute the API, or
 
> Postman collection file: UserMessaging.postman_collection.json from the location: CustomerNotification/Postman Collection/ can be used to run the API calls from Postman with the sample request body included

## CustomerNotification.Common
This contains common or shared implementation logic

## CustomerNotification.Services
This contains Messaging services and other additional services to handle the message formatting and processing


## Scope of enhancement/ TODO's:
- Logging is to be done through an independent logging platform for which the setup and process is configured in the BaseService.cs in API
- file based logging is currently implemented
- Unit tests needs to be implemented

## Key technologies used
- DotNet Core 3.1
- Swashbuckle Swagger definition in API
