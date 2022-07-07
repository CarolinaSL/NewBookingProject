﻿using NewBookingApp.Core.Model;

namespace NewBookingProject.Passenger.API.Passengers.Models
{
    public class Passenger : Aggregate<long>
    {

        public string PassportNumber { get; private set; }
        public string Name { get; private set; }
        public PassengerType PassengerType { get; private set; }
        public int Age { get; private set; }

        public Passenger CompleteRegistrationPassenger(long id, string name, string passportNumber, PassengerType passengerType, int age)
        {
            var passenger = new Passenger
            {
                Name = name,
                PassportNumber = passportNumber,
                PassengerType = passengerType,
                Age = age,
                Id = id
            };
            return passenger;
        }

        public static Passenger Create(long id, string name, string passportNumber, bool isDeleted = false)
        {
            var passenger = new Passenger { Id = id, Name = name, PassportNumber = passportNumber, IsDeleted = isDeleted };
            return passenger;
        }

    }

}
