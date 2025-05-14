# Agri-Energy Connect Platform Prototype

## Overview

The Agri-Energy Connect Platform is a visionary initiative aimed at bridging the gap between the agricultural sector and green energy technology providers. This prototype demonstrates the core functionalities of the intended final product, focusing on sustainable agriculture and renewable energy solutions.

## Features

### Database Development and Integration
- A relational database is designed to manage information about farmers and their products.
- The database is populated with sample data to simulate real-world scenarios.

### User Roles and Authentication
- **Farmer**: Can add products to their profile and view their own product listings.
- **Employee**: Can add new farmer profiles, view all products from specific farmers, and use filters for product searching.
- Secure login functionality with authentication mechanisms to protect user data.

### Functional Features
- **For Farmers**: Add new products with details like name, category, and production date.
- **For Employees**: Add new farmer profiles, view, and filter products based on criteria such as date range and product type.

### User Interface Design
- User-friendly interface with intuitive navigation.
- Responsive design for accessibility on desktops, tablets, and smartphones.

### Data Accuracy and Validation
- Data validation checks to maintain accuracy and consistency.
- Error-handling mechanisms to prevent system crashes and data corruption.

## Development Process and Testing
- Developed iteratively with testing for each functionality.
- User experience (UX) testing conducted to gather feedback on usability.

## Installation and Setup

### Prerequisites
- Visual Studio
- .NET Framework
- SQL Server

### Setup Instructions
1. **Clone the Repository**
   ```bash
   git clone (https://github.com/ST10294145/GreenGrid.git)
   ```
2. **Database Setup**
   - Import the provided SQL scripts to set up the database with sample data.

3. **Build and Run**
   - Open the solution file in Visual Studio.
   - Build the solution and run the application.

## Usage

### Logging In
Use the following sample credentials to log in to the system:

#### Farmer Account
- **Email**: farmer.admin@greengrid.com  
- **Password**: Farmer@123  

#### Employee Account
- **Email**: employee.user@greengrid.com  
- **Password**: Employee@1234  

### Adding Data
- Farmers can add products through their dashboard.
- Employees can add farmer profiles and view/filter product lists.

## Contribution

Feel free to fork the repository and submit pull requests. For major changes, please open an issue to discuss what you would like to change.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

