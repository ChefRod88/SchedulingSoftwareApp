Client Scheduling Application

Welcome to the Client Scheduling Application! This project is designed to manage client appointments and customer information efficiently, with features including user authentication, data validation, timezone support, and reporting.

📁 Included Files

Visual Studio Project Folder - Complete project source code.

client_schedule.sql - MySQL database schema with pre-populated data for testing.

🛠️ Prerequisites

Before running the project, ensure the following software is installed:

Visual Studio 2022 (or later) - .NET Framework 4.7.2 or higher.

MySQL Workbench - For managing the MySQL database.

📦 Required NuGet Packages

Ensure the following NuGet packages are installed in your Visual Studio project:

MySql.Data (by Oracle Corporation) - Required for database connectivity.

Newtonsoft.Json (by James Newton-King) - Required for JSON handling.

📂 Database Setup

Import the Database

Use MySQL Workbench to import the provided client_schedule.sql file.

Navigate to Server → Data Import → Import from Self-Contained File.

Select the client_schedule.sql file.

Choose client_schedule as the target schema.

Click Start Import.

Verify Database Connection

Ensure the database connection string in the project’s App.config file is correct:

<connectionStrings>
    <add name="client_schedule"
         connectionString="Server=localhost;Port=3306;Database=client_schedule;User ID=your_username;Password=your_password;SslMode=None;"
         providerName="MySql.Data.MySqlClient" />
</connectionStrings>

🚀 Running the Application

Open the Solution

Open the Client Scheduling Application solution file (.sln) in Visual Studio.

Build the Project

Click Build → Build Solution (or press Ctrl+Shift+B).

Run the Application

Press F5 or click Start to run the project.

Client Scheduling Application

Welcome to the Client Scheduling Application! This project is designed to manage client appointments and customer information efficiently, with features including user authentication, data validation, timezone support, and reporting.

📁 Included Files

Visual Studio Project Folder - Complete project source code.

client_schedule.sql - MySQL database schema with pre-populated data for testing.

🛠️ Prerequisites

Before running the project, ensure the following software is installed:

Visual Studio 2022 (or later) - .NET Framework 4.7.2 or higher.

MySQL Workbench - For managing the MySQL database.

📦 Required NuGet Packages

Ensure the following NuGet packages are installed in your Visual Studio project:

MySql.Data (by Oracle Corporation) - Required for database connectivity.

Newtonsoft.Json (by James Newton-King) - Required for JSON handling.

📂 Database Setup

Import the Database

Use MySQL Workbench to import the provided client_schedule.sql file.

Navigate to Server → Data Import → Import from Self-Contained File.

Select the client_schedule.sql file.

Choose client_schedule as the target schema.

Click Start Import.

Verify Database Connection

Ensure the database connection string in the project’s App.config file is correct:

<connectionStrings>
    <add name="client_schedule"
         connectionString="Server=localhost;Port=3306;Database=client_schedule;User ID=your_username;Password=your_password;SslMode=None;"
         providerName="MySql.Data.MySqlClient" />
</connectionStrings>

🚀 Running the Application

Open the Solution

Open the Client Scheduling Application solution file (.sln) in Visual Studio.

Build the Project

Click Build → Build Solution (or press Ctrl+Shift+B).

Run the Application

Press F5 or click Start to run the project.

💡 Features and Functionality

1. Login Form

User authentication.

Localization support (English and Spanish).

User location detection (City, State, Country, UTC Timezone).

Upcoming appointment alerts within 15 minutes.

2. Customer Management

Add, update, and delete customer records.

Data validation (name, address, phone number).

Exception handling for database operations.

3. Appointment Management

Add, update, and delete appointments.

Time zone adjustments.

Overlapping appointment prevention.

Business hours enforcement (9:00 AM to 5:00 PM, Monday to Friday).

4. Calendar View

Month and day view for appointments.

5. Reports

Appointment types by month.

Schedules for each user.

Custom reports.

6. Audit Logging

Login attempts are recorded in Login_History.txt with timestamps and user information.

✅ Testing and Validation

Ensure the client_schedule database is correctly imported before testing. The application has been tested to meet all rubric requirements.

🤝 Support

If you encounter any issues or have questions, please feel free to reach out. Thank you for your time and consideration. I hope you enjoy reviewing my project.



