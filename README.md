
# Employee Leave Management System - ASP.NET Core & Entity Framework

_A practical Employee Leave Management System built with ASP.NET Core and Entity Framework Core, designed to help you master modern .NET web development._

# Features

 - Modern Web Stack: Built with ASP.NET Core MVC and Entity Framework
   Core.
 - Authentication: Secure user management with ASP.NET Core Identity.
 - Repository Pattern: Clean architecture with dependency injection.
 - Data Management: Entity Framework Core data models with AutoMapper.
 - Responsive UI: Bootstrap-powered interface with admin theme.
 - Deployment Ready: Configurations for IIS and Azure App Services.
 - Email Integration: Built-in email notification services.

# 👤 Employee Functionality

 - Submit leave requests with type and duration
 - View personal leave balances and request history
 - Receive status notifications on leave requests

# 🛠️ Administrator Functionality

 - Review and approve/reject leave requests
 - Manage leave types and employee allocations
 - View and filter leave records across the organization
# Skills Gained
 - Core MVC architecture (Models, Views, Controllers)
 - Database operations with Entity Framework Core
 - Authentication and authorization
 - Repository pattern and dependency injection
 - ViewModels and AutoMapper implementation
 - Security best practices
 - Bootstrap theming and layout
 - NuGet package management
 - Deployment to IIS and Azure
 - Email service integration
 - Source control with GitHub

# Technologies Used

 - ASP.NET Core 9.0
 - Entity Framework Core 9.0
 - ASP.NET Core Identity
 - AutoMapper
 - Bootstrap 5
 - SQL Server
 - SendGrid/MailKit (for email)
# License

This project is licensed under the MIT License - see the LICENSE file for details.


## Installation Instructions
###  1. Clone the Repository

    git clone https://github.com/IsmailJAziza/LeaveManagement.git
    cd LeaveManagementSystem
### 2. Open in Visual Studio
-   Go to  `appsettings.json`  in the  `LeaveManagementSystem.Web`  project.
-   Update the  `DefaultConnection`  string with your SQL Server instance.

Example:

    "ConnectionStrings": {
	    "DefaultConnection": "Server=HP-OMEN15\\SQLEXPRESS  ;Database= LeaveManagement;User Id=;Password=;Integrated Security=True;Encrypt=False ;Trusted_Connection=True;MultipleActiveResultSets=true;"
     },

### 3. Apply Migrations
Using the Package Manager Console:

    Update-Database
### 4. Run the Application
-   Set  `LeaveManagementSystem.`  as the startup project.
-   Press  `F5`  or click the  **Run**  button in Visual Studio.
### 5. Log In

**Default Admin Credentials:**
-   **Username:**  [admin@localhost.com](mailto:admin@localhost.com)
-   **Password:**  P@ssword1

