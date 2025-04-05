using CarRentalSystemEntityLibrary;
using CarRentalSystemHelperLibrary;
using System;
using System.Collections.Generic;

internal class Program
{
    private static void Main(string[] args)
    {

        CarServiceHelper helper = new CarServiceHelper();

        while (true)
        {
            Console.WriteLine("1. Car Management");
            Console.WriteLine("2. Customer Management");
            Console.WriteLine("3. Lease Management");
            Console.WriteLine("4. Payment Management");
            Console.WriteLine("5. Exit");
            Console.Write("Choose a category: ");

            int mainChoice = Convert.ToInt32(Console.ReadLine());

            switch (mainChoice)
            {
                case 1: CarMenu(helper); break;
                case 2: CustomerMenu(helper); break;
                case 3: LeaseMenu(helper); break;
                case 4: PaymentMenu(helper); break;
                case 5: Environment.Exit(0); break;
                default: Console.WriteLine("Invalid option. Try again."); break;
            }
        }
    }

        private static void CustomerMenu(CarServiceHelper helper)
    {
        Console.WriteLine("\n--- Customer Management ---");
        Console.WriteLine("1. Add Customer");
        Console.WriteLine("2. Remove Customer");
        Console.WriteLine("3. List Customers");
        Console.WriteLine("4. Find Customer by ID");
        Console.Write("Select an operation: ");

        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1: InsertCustomer(helper); break;
            case 2: DeleteCustomer(helper); break;
            case 3: ShowAllCustomers(helper); break;
            case 4: FindCustomerByID(helper); break;
            default: Console.WriteLine("Invalid customer operation."); break;
        }
    }




    private static void CarMenu(CarServiceHelper helper)
    {
        Console.WriteLine("\n--- Car Management ---");
        Console.WriteLine("1. Add Car");
        Console.WriteLine("2. Update Car");
        Console.WriteLine("3. Delete Car");
        Console.WriteLine("4. Find Car by ID");
        Console.WriteLine("5. Show Available Cars");
        Console.WriteLine("6. Show Rented Cars");
        Console.Write("Select an operation: ");

        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1: InsertCar(helper); break;
            case 2: UpdateCar(helper); break;
            case 3: DeleteCar(helper); break;
            case 4: FindCarByID(helper); break;
            case 5: ShowAllCars(helper); break;
            case 6: ShowRentedCars(helper); break;
            default: Console.WriteLine("Invalid car operation."); break;
        }
    }

    private static void LeaseMenu(CarServiceHelper helper)
    {
        Console.WriteLine("\n--- Lease Management ---");
        Console.WriteLine("1. Rent a Car");
        Console.WriteLine("2. Return a Car");
        Console.WriteLine("3. Show Active Leases");
        Console.WriteLine("4. Show Lease History");
        Console.Write("Select an operation: ");

        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1: RentCar(helper); break;
            case 2: ReturnCar(helper); break;
            case 3: ShowActiveLeases(helper); break;
            case 4: ShowLeaseHistory(helper); break;
            default: Console.WriteLine("Invalid lease operation."); break;
        }
    }

    private static void PaymentMenu(CarServiceHelper helper)
    {
        Console.WriteLine("\n--- Payment Management ---");
        Console.WriteLine("1. Record Payment");
        Console.Write("Select an operation: ");

        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1: RecordPayment(helper); break;
            default: Console.WriteLine("Invalid payment operation."); break;
        }
    }
    private static void InsertCar(CarServiceHelper helper)
    {
        Car car = new Car();
        Console.Write("Enter Make: ");
        car.Make = Console.ReadLine();
        Console.Write("Enter Model: ");
        car.Model = Console.ReadLine();
        Console.Write("Enter Year: ");
        car.Year = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter Daily Rate: ");
        car.DailyRate = Convert.ToDouble(Console.ReadLine());
        Console.Write("Enter Status (Available/NotAvailable): ");
        car.Status = Console.ReadLine();
        Console.Write("Enter Passenger Capacity: ");
        car.PassengerCapacity = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter Engine Capacity: ");
        car.EngineCapacity = Convert.ToInt32(Console.ReadLine());

        helper.InsertCar(car);
        Console.WriteLine("Car added successfully!");
    }

    private static void UpdateCar(CarServiceHelper helper)
    {
        Car car = new Car();
        Console.Write("Enter Vehicle ID: ");
        car.VehicleID = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter Make: ");
        car.Make = Console.ReadLine();
        Console.Write("Enter Model: ");
        car.Model = Console.ReadLine();
        Console.Write("Enter Year: ");
        car.Year = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter Daily Rate: ");
        car.DailyRate = Convert.ToDouble(Console.ReadLine());
        Console.Write("Enter Status (Available(1)/NotAvailable(0)): ");
        car.Status = Console.ReadLine();
        Console.Write("Enter Passenger Capacity: ");
        car.PassengerCapacity = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter Engine Capacity: ");
        car.EngineCapacity = Convert.ToInt32(Console.ReadLine());

        helper.UpdateCar(car);
        Console.WriteLine("Car updated successfully!");
    }

    private static void DeleteCar(CarServiceHelper helper)
    {
        Console.Write("Enter Vehicle ID to delete: ");
        int carID = Convert.ToInt32(Console.ReadLine());
        helper.DeleteCar(carID);
        Console.WriteLine("Car deleted successfully!");
    }

    private static void FindCarByID(CarServiceHelper helper)
    {
        Console.Write("Enter Vehicle ID: ");
        int carID = Convert.ToInt32(Console.ReadLine());
        Car car = helper.FindCarByID(carID);
        if (car != null)
        {
            Console.WriteLine($"ID: {car.VehicleID}, Make: {car.Make}, Model: {car.Model}, Year: {car.Year}, Rate: {car.DailyRate}, Status: {car.Status}");
        }
        else
        {
            Console.WriteLine("Car not found!");
        }
    }

    private static void ShowAllCars(CarServiceHelper helper)
    {
        List<Car> cars = helper.ListAllCars();
        foreach (var car in cars)
        {
            Console.WriteLine($"ID: {car.VehicleID}, Make: {car.Make}, Model: {car.Model}, Year: {car.Year}, Rate: {car.DailyRate}, Status: {car.Status}");
        }
    }

    private static void ShowRentedCars(CarServiceHelper helper)
    {
        List<Car> cars = helper.ListRentedCars();
        foreach (var car in cars)
        {
            Console.WriteLine($"ID: {car.VehicleID}, Make: {car.Make}, Model: {car.Model}, Year: {car.Year}, Rate: {car.DailyRate}, Status: {car.Status}");
        }
    }

    private static void InsertCustomer(CarServiceHelper helper)
    {
        Customer customer = new Customer();
        Console.Write("Enter First Name: ");
        customer.FirstName = Console.ReadLine();
        Console.Write("Enter Last Name: ");
        customer.LastName = Console.ReadLine();
        Console.Write("Enter Email: ");
        customer.Email = Console.ReadLine();
        Console.Write("Enter Phone Number: ");
        customer.PhoneNumber = Console.ReadLine();

        helper.InsertCustomer(customer);
        Console.WriteLine("Customer added successfully!");
    }

    private static void DeleteCustomer(CarServiceHelper helper)
    {
        Console.Write("Enter Customer ID to remove: ");
        int customerID = Convert.ToInt32(Console.ReadLine());
        helper.DeleteCustomer(customerID);
        Console.WriteLine("Customer removed successfully!");
    }

    private static void ShowAllCustomers(CarServiceHelper helper)
    {
        List<Customer> customers = helper.ListCustomers();
        foreach (var customer in customers)
        {
            Console.WriteLine($"ID: {customer.CustomerID}, Name: {customer.FirstName} {customer.LastName}, Email: {customer.Email}, Phone: {customer.PhoneNumber}");
        }
    }

    private static void FindCustomerByID(CarServiceHelper helper)
    {
        Console.Write("Enter Customer ID: ");
        int customerID = Convert.ToInt32(Console.ReadLine());
        Customer customer = helper.FindCustomerByID(customerID);
        if (customer != null)
        {
            Console.WriteLine($"ID: {customer.CustomerID}, Name: {customer.FirstName} {customer.LastName}, Email: {customer.Email}, Phone: {customer.PhoneNumber}");
        }
        else
        {
            Console.WriteLine("Customer not found!");
        }
    }

    private static void RentCar(CarServiceHelper helper)
    {
        Console.Write("Enter Customer ID: ");
        int customerID = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter Vehicle ID: ");
        int vehicleID = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter Start Date (yyyy-mm-dd): ");
        DateTime startDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter End Date (yyyy-mm-dd): ");
        DateTime endDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter Lease Type (DailyLease/MonthlyLease): ");
        string leaseType = Console.ReadLine();

        Lease lease = helper.CreateLease(customerID, vehicleID, startDate, endDate, leaseType);
        if (lease != null)
        {
            Console.WriteLine($"Lease created successfully! Lease ID: {lease.LeaseID}");
        }
        else
        {
            Console.WriteLine("Lease creation failed. The car might not be available or an error occurred.");
        }
    }

    private static void ReturnCar(CarServiceHelper helper)
    {
        Console.Write("Enter Lease ID: ");
        int leaseID = Convert.ToInt32(Console.ReadLine());

        Lease lease = helper.ReturnCar(leaseID);
        Console.WriteLine($"Car returned successfully! Lease ID: {lease.LeaseID}");
    }

    private static void ShowActiveLeases(CarServiceHelper helper)
    {
        List<Lease> leases = helper.ListActiveLeases();
        foreach (var lease in leases)
        {
            Console.WriteLine($"Lease ID: {lease.LeaseID}, Customer ID: {lease.CustomerID}, Car ID: {lease.VehicleID}, Start Date: {lease.StartDate}, End Date: {lease.EndDate}, Type: {lease.Type}");
        }
    }

    private static void ShowLeaseHistory(CarServiceHelper helper)
    {
        List<Lease> leases = helper.ListLeaseHistory();
        foreach (var lease in leases)
        {
            Console.WriteLine($"Lease ID: {lease.LeaseID}, Customer ID: {lease.CustomerID}, Car ID: {lease.VehicleID}, Start Date: {lease.StartDate}, End Date: {lease.EndDate}, Type: {lease.Type}");
        }
    }

    private static void RecordPayment(CarServiceHelper helper)
    {
        Console.Write("Enter Lease ID: ");
        int leaseID = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter Payment Amount: ");
        double amount = Convert.ToDouble(Console.ReadLine());

        Lease lease = helper.FindLeaseByID(leaseID); 
        if (lease != null)
        {
            helper.RecordPayment(lease, amount);
            Console.WriteLine("Payment recorded successfully!");
        }
        else
        {
            Console.WriteLine("Invalid Lease ID! Payment failed.");
        }
    }
}

