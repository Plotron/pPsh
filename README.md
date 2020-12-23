
# Spring Framework MVC Example: pPsh 

A simple project demonstrating how **ASP .NET MVC** can be used in conjunction with **Razor** to implement a simple **REST API IoT push service** web application.

## Features

* read, create, edit and delete virtual devices and execution scenarios
* configure email notifications for events
* push events with parameters using a simple GET request

**Notice**: this project was restored from an old backup and some features are missing. I'm currently working on restoring those features.

## Setup
The steps below will get you up and running with a local development environment. We assume you have the following installed:

* Microsoft Visual Studio
* .NET Framework
* IIS Express Server

### Development
Launch the project in Microsoft Visual Studio.

Configuration
-----
The `Web.config` file contains several important configuration properties.
| Key               | Description                     |
|-------------------|---------------------------------|
| DefaultConnection| database connection information (default is local) |
| serviceMailAddress | email address of the no-reply service (default is no-reply@pPsh.net) |
| eventServiceMailAddress| email address of API push notifications (default is api.mail@pPsh.net) |
| smtpHost| SMTP host for the server email functionality (default is smtp.mailtrap.io)|
| smtpPort| SMTP port for the server email functionality (default is 2525) |
| smtpUser| SMTP host for the server email functionality |
| smptPassword| SMTP password for the server email functionality |


