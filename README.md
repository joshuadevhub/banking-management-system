## Banking Management System

## Overview
The Banking Management System is a console-based application developed in C# to simulate core banking operations. It demonstrates object-oriented programming principles such as encapsulation, abstraction, and separation of responsibilities while providing a simple banking experience through a command-line interface.

The application allows users to register customers, open bank accounts, perform deposits and withdrawals, and view customer, account, and transaction information through a simple command-line interface.

The project was built as a personal software engineering project to strengthen C# programming skills and object-oriented design.


## Features

### Customer Management
- Register new customers.
- Validate customer information (name, phone number, and email).
- Prevent duplicate phone numbers and email addresses.
- View customer information.

### Account Management
- Open Savings and Current accounts.
- Prevent customers from opening multiple accounts of the same type.
- Generate unique customer IDs.
- Generate unique account numbers.
- View account information.

### Transactions
- Deposit funds into an account.
- Withdraw funds from an account.
- Record every transaction.
- View transaction history.
- Display current account balance after each transaction.

### Validation and Error Handling
- Validate account PINs.
- Validate account types.
- Handle invalid operations using exceptions.
- Display meaningful error messages to the user.


## Project Structure

```text
BankingManagementSystem/
├── Data/
│   └── bank.json
├── Models/
│   ├── Account.cs
│   ├── Address.cs
│   ├── Customer.cs
│   ├── Transaction.cs
├── Services/
│   └── BankingService.cs
├── Program.cs
├── BankingManagementSystem.csproj
├── .gitignore
└── README.md
```

### Folder Description

- **Models/** - Contains the core classes that represent the banking system, including customers, accounts, addresses, and transactions.
- **Services/** - Contains the business logic responsible for managing customers, accounts, and banking operations.
- **Data/** - Stores application data. This folder is prepared for JSON data persistence.
- **Program.cs** - The entry point of the application that provides the console-based user interface.


## Technologies Used

- **Language:** C#
- **Framework:** .NET Console Application
- **IDE:** Visual Studio Code
- **Version Control:** Git
- **Repository Hosting:** GitHub


## How to Run

1. Clone the repository.

   ```bash
   git clone https://github.com/joshuadevhub/banking-management-system.git
   ```

2. Navigate to the project directory.

   ```bash
   cd banking-management-system
   ```

3. Build the project.

   ```bash
   dotnet build
   ```

4. Run the application.

   ```bash
   dotnet run
   ```


## Current Version

### Version 1.0

Implemented features include:

- Customer registration
- Account creation
- Deposits
- Withdrawals
- Customer information lookup
- Account information lookup
- Transaction history
- Input validation
- Exception handling


## Roadmap

### Version 2 (Planned)

- Refactor `Program.cs`
- Save and load data using JSON
- Generate unique transaction IDs
- Implement account-to-account transfers
- Add PIN verification
- Implement PIN change functionality
- Improve code organization

### Future Versions

- Interest calculation
- User authentication
- Admin dashboard
- Database integration
- Unit testing


## Author

Developed by **Elemide Joshua Damilare** as a personal software engineering project for learning object-oriented programming and C#.