using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSystemExceptionsLibrary;
using CarRentalSystemEntityLibrary;

using CarRentalSystemUtilLibrary;
using Microsoft.Data.SqlClient;

namespace CarRentalSystemDaoLibrary
{
    public class CarLeaseRepositoryImpl : ICarLeaseRepository
    {
        public void AddCar(Car car)
        {
            try
            {
                using (SqlConnection cn = DBPropertyUtil.AppConnection())
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Vehicle (Make,Model,Year,DailyRate, Status,PassengerCapacity,EngineCapacity) VALUES (@Make,@Model,@Year,@DailyRate, @Status,@PassengerCapacity,@EngineCapacity)", cn))
                {
                    cmd.Parameters.AddWithValue("@Make", car.Make);
                    cmd.Parameters.AddWithValue("@Model", car.Model);
                    cmd.Parameters.AddWithValue("@Year", car.Year);
                    cmd.Parameters.AddWithValue("@DailyRate", car.DailyRate);
                    cmd.Parameters.AddWithValue("@Status", car.Status);
                    cmd.Parameters.AddWithValue("@PassengerCapacity", car.PassengerCapacity);
                    cmd.Parameters.AddWithValue("@EngineCapacity", car.EngineCapacity);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.Close();
                    cn.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding car: " + ex.Message);
            }
        }

        public void RemoveCar(int carID)
        {
            using (SqlConnection cn = DBPropertyUtil.AppConnection())
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Vehicle WHERE VehicleID = @CarID", cn))
            {
                cmd.Parameters.AddWithValue("@CarID", carID);

                cn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();

                if (rowsAffected == 0)
                {
                    throw new CarNotFoundException($"Car with ID {carID} not found.");
                }
            }
        }


        public List<Car> ListAvailableCars()
        {
            List<Car> cars = new List<Car>();
            try
            {
                SqlConnection cn = DBPropertyUtil.AppConnection();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Vehicle WHERE Status = 1", cn);
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cars.Add(new Car
                            {
                                VehicleID = Convert.ToInt32(dr["VehicleID"]),
                                Make = dr["Make"].ToString(),
                                Model = dr["Model"].ToString(),
                                Year = Convert.ToInt32(dr["Year"]),
                                DailyRate = Convert.ToDouble(dr["DailyRate"]),
                                Status = dr["Status"].ToString(),
                                PassengerCapacity = Convert.ToInt32(dr["PassengerCapacity"]),
                                EngineCapacity = Convert.ToInt32(dr["EngineCapacity"])
                            });
                        }
                    }
                    cmd.Dispose();
                    cn.Close();
                    cn.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error listing available cars: " + ex.Message);
            }
            return cars;
        }

        public List<Car> ListRentedCars()
        {
            List<Car> cars = new List<Car>();
            try
            {
                using (SqlConnection cn = DBPropertyUtil.AppConnection())
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Vehicle WHERE Status = 0", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cars.Add(new Car
                            {
                                VehicleID = Convert.ToInt32(dr["VehicleID"]),
                                Make = dr["Make"].ToString(),
                                Model = dr["Model"].ToString(),
                                Year = Convert.ToInt32(dr["Year"]),
                                DailyRate = Convert.ToDouble(dr["DailyRate"]),
                                Status = dr["Status"].ToString(),
                                PassengerCapacity = Convert.ToInt32(dr["PassengerCapacity"]),
                                EngineCapacity = Convert.ToInt32(dr["EngineCapacity"])
                            });
                        }
                    }
                    cmd.Dispose();
                    cn.Close();
                    cn.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error listing rented cars: " + ex.Message);
            }
            return cars;
        }

        public Car FindCarById(int VehicleID)
        {
            using (SqlConnection cn = DBPropertyUtil.AppConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Vehicle WHERE VehicleID = @VehicleID", cn))
            {
                cmd.Parameters.AddWithValue("@VehicleID", VehicleID);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return new Car
                        {
                            VehicleID = Convert.ToInt32(dr["VehicleID"]),
                            Make = dr["Make"].ToString(),
                            Model = dr["Model"].ToString(),
                            Year = Convert.ToInt32(dr["Year"]),
                            DailyRate = Convert.ToDouble(dr["DailyRate"]),
                            Status = dr["Status"].ToString(),
                            PassengerCapacity = Convert.ToInt32(dr["PassengerCapacity"]),
                            EngineCapacity = Convert.ToInt32(dr["EngineCapacity"])
                        };
                    }
                    else
                    {
                        throw new CarNotFoundException($"Car with ID {VehicleID} not found.");
                    }
                }
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
        }

        public void UpdateCar(Car car)
        {
            if (FindCarById(car.VehicleID) == null)  // Check if car exists first
            {
                throw new CarNotFoundException($"Car with ID {car.VehicleID} not found.");
            }

            SqlConnection cn = DBPropertyUtil.AppConnection();
            SqlCommand cmd = new SqlCommand(@"UPDATE Vehicle 
                                     SET Make = @Make, Model = @Model, Year = @Year, 
                                         DailyRate = @DailyRate, Status = @Status, 
                                         PassengerCapacity = @PassengerCapacity, EngineCapacity = @EngineCapacity 
                                     WHERE VehicleID = @VehicleID", cn);
            {
                cmd.Parameters.AddWithValue("@Make", car.Make);
                cmd.Parameters.AddWithValue("@Model", car.Model);
                cmd.Parameters.AddWithValue("@Year", car.Year);
                cmd.Parameters.AddWithValue("@DailyRate", car.DailyRate);
                cmd.Parameters.AddWithValue("@Status", car.Status);
                cmd.Parameters.AddWithValue("@PassengerCapacity", car.PassengerCapacity);
                cmd.Parameters.AddWithValue("@EngineCapacity", car.EngineCapacity);
                cmd.Parameters.AddWithValue("@VehicleID", car.VehicleID);

                cn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
        }

        public void AddCustomer(Customer customer)
        {
            try
            {
                SqlConnection cn = DBPropertyUtil.AppConnection();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Customer (firstName, lastName, email, phoneNumber) 
                                                         VALUES (@FirstName, @LastName, @Email, @PhoneNumber)", cn);
                {
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.Close();
                    cn.Dispose();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while adding customer: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding customer: " + ex.Message);
            }
        }

        public void RemoveCustomer(int customerID)
        {
            try
            {
                SqlConnection cn = DBPropertyUtil.AppConnection();
                SqlCommand cmd = new SqlCommand("DELETE FROM Customer WHERE customerID = @CustomerID", cn);
                {
                    cmd.Parameters.AddWithValue("@CustomerID", customerID);

                    cn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new CustomerNotFoundException($"Customer with ID {customerID} not found.");
                    }
                }
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while removing customer: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error removing customer: " + ex.Message);
            }
        }

        public List<Customer> ListCustomers()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                SqlConnection cn = DBPropertyUtil.AppConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customer", cn);
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            customers.Add(new Customer
                            {
                                CustomerID = Convert.ToInt32(dr["customerID"]),
                                FirstName = dr["firstName"].ToString(),
                                LastName = dr["lastName"].ToString(),
                                Email = dr["email"].ToString(),
                                PhoneNumber = dr["phoneNumber"].ToString()
                            });
                        }
                    }
                }
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while listing customers: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error listing customers: " + ex.Message);
            }
            return customers;
        }

        public Customer FindCustomerById(int customerID)
        {
            try
            {
                using (SqlConnection cn = DBPropertyUtil.AppConnection())
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Customer WHERE customerID = @CustomerID", cn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", customerID);

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Customer
                            {
                                CustomerID = Convert.ToInt32(dr["customerID"]),
                                FirstName = dr["firstName"].ToString(),
                                LastName = dr["lastName"].ToString(),
                                Email = dr["email"].ToString(),
                                PhoneNumber = dr["phoneNumber"].ToString()
                            };
                        }
                        else
                        {
                            throw new CustomerNotFoundException($"Customer with ID {customerID} not found.");
                        }
                    }
                    cmd.Dispose();
                    cn.Close();
                    cn.Dispose();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while finding customer: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error finding customer: " + ex.Message);
            }
        }


        public Lease CreateLease(int customerID, int vehicleID, DateTime startDate, DateTime endDate, string Type)
        {
            try
            {
                using (SqlConnection cn = DBPropertyUtil.AppConnection())
                {
                    cn.Open();

                    // 1. Check if the vehicle exists and is available
                    SqlCommand checkCmd = new SqlCommand("SELECT Status FROM Vehicle WHERE VehicleID = @VehicleID", cn);
                    checkCmd.Parameters.AddWithValue("@VehicleID", vehicleID);
                    object statusObj = checkCmd.ExecuteScalar();

                    if (statusObj == null)
                    {
                        Console.WriteLine("Vehicle not found.");
                        return null;
                    }

                    bool isAvailable = Convert.ToBoolean(statusObj);
                    if (!isAvailable)
                    {
                        Console.WriteLine("Vehicle is not available for leasing.");
                        return null;
                    }

                    // 2. Insert the lease
                    SqlCommand insertCmd = new SqlCommand(@"
                INSERT INTO Lease (VehicleID, CustomerID, StartDate, EndDate, Type) 
                VALUES (@VehicleID, @CustomerID, @StartDate, @EndDate, @Type); 
                SELECT SCOPE_IDENTITY();", cn);

                    insertCmd.Parameters.AddWithValue("@VehicleID", vehicleID);
                    insertCmd.Parameters.AddWithValue("@CustomerID", customerID);
                    insertCmd.Parameters.AddWithValue("@StartDate", startDate);
                    insertCmd.Parameters.AddWithValue("@EndDate", endDate);
                    insertCmd.Parameters.AddWithValue("@Type", Type);

                    int leaseID = Convert.ToInt32(insertCmd.ExecuteScalar());

                    // 3. Update car status to 'not available' (0)
                    SqlCommand updateCmd = new SqlCommand("UPDATE Vehicle SET Status = 0 WHERE VehicleID = @VehicleID", cn);
                    updateCmd.Parameters.AddWithValue("@VehicleID", vehicleID);
                    updateCmd.ExecuteNonQuery();

                    return new Lease
                    {
                        LeaseID = leaseID,
                        VehicleID = vehicleID,
                        CustomerID = customerID,
                        StartDate = startDate,
                        EndDate = endDate,
                        Type = Type
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while creating lease: " + ex.Message);
                return null;
            }
        }
        

        public Lease ReturnCar(int leaseID)
        {
            SqlConnection cn = DBPropertyUtil.AppConnection();

            // 1. Retrieve the lease record
            SqlCommand selectCmd = new SqlCommand(@"SELECT VehicleID, CustomerID, StartDate, EndDate, Type 
                                            FROM Lease WHERE LeaseID = @LeaseID;", cn);
            selectCmd.Parameters.AddWithValue("@LeaseID", leaseID);

            cn.Open();
            SqlDataReader reader = selectCmd.ExecuteReader();

            Lease lease = null;
            if (reader.Read())
            {
                lease = new Lease
                {
                    LeaseID = leaseID,
                    VehicleID = reader.GetInt32(0),
                    CustomerID = reader.GetInt32(1),
                    StartDate = reader.GetDateTime(2),
                    EndDate = reader.GetDateTime(3),
                    Type = reader.GetString(4)
                };
            }
            reader.Close();
            selectCmd.Dispose();

            if (lease == null)
            {
                cn.Close();
                cn.Dispose();
                throw new LeaseNotFoundException($"No lease found with LeaseID: {leaseID}");
            }

            // 2. Update lease end date if returning early
            DateTime today = DateTime.Today;
            if (today < lease.EndDate)
            {
                SqlCommand updateLeaseCmd = new SqlCommand(@"UPDATE Lease SET EndDate = @ActualReturnDate 
                                                     WHERE LeaseID = @LeaseID;", cn);
                updateLeaseCmd.Parameters.AddWithValue("@ActualReturnDate", today);
                updateLeaseCmd.Parameters.AddWithValue("@LeaseID", leaseID);
                updateLeaseCmd.ExecuteNonQuery();
                lease.EndDate = today;
                updateLeaseCmd.Dispose();
            }

            // 3. Mark vehicle as available again
            SqlCommand updateCarStatusCmd = new SqlCommand(@"UPDATE Vehicle SET Status = 1 WHERE VehicleID = @VehicleID;", cn);
            updateCarStatusCmd.Parameters.AddWithValue("@VehicleID", lease.VehicleID);
            updateCarStatusCmd.ExecuteNonQuery();
            updateCarStatusCmd.Dispose();

            cn.Close();
            cn.Dispose();

            return lease;
        }

        public List<Lease> ListActiveLeases()
        {
            List<Lease> leases = new List<Lease>();
            SqlConnection cn = DBPropertyUtil.AppConnection();
            SqlCommand cmd = new SqlCommand(@"SELECT LeaseID, VehicleID, CustomerID, StartDate, EndDate, Type FROM Lease WHERE EndDate >= GETDATE()", cn);

            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                leases.Add(new Lease
                {
                    LeaseID = reader.GetInt32(0),
                    VehicleID = reader.GetInt32(1),
                    CustomerID = reader.GetInt32(2),
                    StartDate = reader.GetDateTime(3),
                    EndDate = reader.GetDateTime(4),
                    Type = reader.GetString(5)
                });
            }

            reader.Close();
            cmd.Dispose();
            cn.Close();
            cn.Dispose();

            if (leases.Count == 0)
                throw new LeaseNotFoundException("No active leases found.");

            return leases;
        }

        public List<Lease> ListLeaseHistory()
        {
            List<Lease> leases = new List<Lease>();
            SqlConnection cn = DBPropertyUtil.AppConnection();
            SqlCommand cmd = new SqlCommand(@"SELECT LeaseID, VehicleID, CustomerID, StartDate, EndDate, Type FROM Lease", cn);

            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                leases.Add(new Lease
                {
                    LeaseID = reader.GetInt32(0),
                    VehicleID = reader.GetInt32(1),
                    CustomerID = reader.GetInt32(2),
                    StartDate = reader.GetDateTime(3),
                    EndDate = reader.GetDateTime(4),
                    Type = reader.GetString(5)
                });
            }

            reader.Close();
            cmd.Dispose();
            cn.Close();
            cn.Dispose();

            if (leases.Count == 0)
                throw new LeaseNotFoundException("No lease history found.");

            return leases;
        }

        public void RecordPayment(Lease lease, double amount)
        {
            try
            {
                using (SqlConnection cn = DBPropertyUtil.AppConnection())
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Payment (LeaseID, PaymentDate, Amount) VALUES (@LeaseID, @PaymentDate, @Amount)", cn))
                {
                    cmd.Parameters.AddWithValue("@LeaseID", lease.LeaseID);
                    cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Amount", amount);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.Close();
                    cn.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error recording payment: " + ex.Message);
            }
        }


        public Lease FindLeaseByID(int leaseID)
        {
            Lease lease = null;

            using (SqlConnection cn = DBPropertyUtil.AppConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Lease WHERE LeaseID = @LeaseID", cn);
                cmd.Parameters.AddWithValue("@LeaseID", leaseID);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    lease = new Lease
                    {
                        LeaseID = Convert.ToInt32(dr["LeaseID"]),
                        VehicleID = Convert.ToInt32(dr["VehicleID"]),
                        CustomerID = Convert.ToInt32(dr["CustomerID"]),
                        StartDate = Convert.ToDateTime(dr["StartDate"]),
                        EndDate = Convert.ToDateTime(dr["EndDate"]),
                        Type = dr["Type"].ToString()
                    };
                }

                dr.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }

            return lease;
        }
    }
}
