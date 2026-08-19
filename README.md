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
- View customer information

### Account Management
- Open Savings and Current accounts.
- Prevent customers from opening multiple accounts of the same type.
- Generate unique customer IDs.
- Generate unique account numbers.
- View account information.
- Persist account information to JSON.

### Transactions
- Deposit funds into an account.
- Withdraw funds from an account.
- Generate unique transaction IDs.
- Record every transaction.
- Persist transaction records to JSON.
- View transaction history.
- Display current account balance after each transaction.

### Validation and Error Handling
- Verify account PINs before withdrawal operations.
- Validate account types.
- Handle invalid operations using exceptions.
- Display meaningful error messages to the user.


## Project Structure

```text
BankingManagementSystem/
├── Data/
│   └── bank.json       # Runtime Data, ignored by Git
├── Models/
│   ├── Account.cs
│   ├── Address.cs
│   ├── Customer.cs
│   ├── Transaction.cs
├── Services/
│   ├── BankingService.cs
│   ├── JsonStorage.cs
├── Program.cs
├── BankingManagementSystem.csproj
├── .gitignore
└── README.md
```

### Folder Description

- **Models/** - Contains the core classes that represent the banking system, including customers, accounts, addresses, and transactions.
- **Services/** - Contains the business logic and data persistence services responsible for managing banking operations and saving/loading data
- **Data/** - Stores runtime application data in JSON format. The bank.json file is excluded from version control through .gitignore.
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

### Version 2.0 - In Progress

Implemented so far:

- Refactored `Program.cs`
- JSON data loading and persistence
- Unique transaction ID generation
- Persistent transaction history
- Account balance persistence
- PIN verification for withdrawal operations
- Account-to-account transfers

Planned:

- Secure PIN hashing
- Further code organization and improvements

### Version 1.0

Version 1 established the core banking functionality, including:

- Customer registration
- Account creation
- Deposits
- Withdrawals
- Customer information lookup
- Account information lookup
- Transaction history
- Input validation
- Exception handling


### Future Versions

- Interest calculation
- User authentication
- Admin dashboard
- Database integration
- Unit testing


## Author

Developed by **Elemide Joshua Damilare** as a personal software engineering project for learning object-oriented programming and C#.