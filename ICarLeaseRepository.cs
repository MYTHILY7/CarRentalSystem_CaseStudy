using CarRentalSystemEntityLibrary;

namespace CarRentalSystemDaoLibrary
{
    public interface ICarLeaseRepository
    {
        void AddCar(Car car);
        void RemoveCar(int carID);
        List<Car> ListAvailableCars();
        List<Car> ListRentedCars();
        Car FindCarById(int carID);
        void UpdateCar(Car car);

        void AddCustomer(Customer customer);
        void RemoveCustomer(int customerID);
        List<Customer> ListCustomers();
        Customer FindCustomerById(int customerID);

        Lease CreateLease(int customerID, int vehicleID, DateTime startDate, DateTime endDate, string leaseType);
        Lease ReturnCar(int leaseID);
        List<Lease> ListActiveLeases();
        List<Lease> ListLeaseHistory();

        void RecordPayment(Lease lease, double amount);

        Lease FindLeaseByID(int leaseID);


    }
}
