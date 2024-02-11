using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingSystem
{
    public class ParkingLot
    {
        private int capacity;
        private Dictionary<int, Vehicle> parkingSlots;

        public ParkingLot(int capacity)
        {
            this.capacity = capacity;
            parkingSlots = new Dictionary<int, Vehicle>();
        }

        public void ParkVehicle(string registrationNumber, string color, string vehicleType)
        {
            if (IsFull())
            {
                Console.WriteLine("Sorry, parking lot is full");
                return;
            }

            int slotNumber = GetNextAvailableSlot();
            parkingSlots.Add(slotNumber, new Vehicle(registrationNumber, color, vehicleType));
            Console.WriteLine($"Allocated slot number: {slotNumber}");
        }

        public void Leave(int slotNumber)
        {
            if (parkingSlots.ContainsKey(slotNumber))
            {
                parkingSlots.Remove(slotNumber);
                Console.WriteLine($"Slot number {slotNumber} is free");
            }
            else
            {
                Console.WriteLine("Slot number not found");
            }
        }

        public void Status()
        {
            Console.WriteLine("Slot\tNo.\tType\tRegistration No\tColor");
            foreach (var entry in parkingSlots.OrderBy(x => x.Key))
            {
                Console.WriteLine($"{entry.Key}\t{entry.Value.RegistrationNumber}\t{entry.Value.Type}\t{entry.Value.Color}");
            }
        }

        public void TypeOfVehicles(string vehicleType)
        {
            int count = parkingSlots.Count(v => v.Value.Type.Equals(vehicleType, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(count);
        }

        public void RegistrationNumbersForVehiclesWithColor(string color)
        {
            var matchingVehicles = parkingSlots.Where(v => v.Value.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                                               .Select(v => v.Value.RegistrationNumber);
            Console.WriteLine(string.Join(", ", matchingVehicles));
        }

        public void SlotNumbersForVehiclesWithColor(string color)
        {
            var matchingSlots = parkingSlots.Where(v => v.Value.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                                            .Select(v => v.Key);
            Console.WriteLine(string.Join(", ", matchingSlots));
        }

        public void SlotNumberForRegistrationNumber(string registrationNumber)
        {
            var matchingSlot = parkingSlots.FirstOrDefault(v => v.Value.RegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase));
            if (matchingSlot.Key != 0)
            {
                Console.WriteLine(matchingSlot.Key);
            }
            else
            {
                Console.WriteLine("Not found");
            }
        }

        private bool IsFull()
        {
            return parkingSlots.Count >= capacity;
        }

        private int GetNextAvailableSlot()
        {
            for (int i = 1; i <= capacity; i++)
            {
                if (!parkingSlots.ContainsKey(i))
                {
                    return i;
                }
            }
            return -1;
        }
    }

    public class Vehicle
    {
        public string RegistrationNumber { get; }
        public string Color { get; }
        public string Type { get; }

        public Vehicle(string registrationNumber, string color, string type)
        {
            RegistrationNumber = registrationNumber;
            Color = color;
            Type = type;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ParkingLot parkingLot = null;

            while (true)
            {
                string input = Console.ReadLine();
                string[] tokens = input.Split(' ');

                if (tokens[0] == "create_parking_lot")
                {
                    int capacity = int.Parse(tokens[1]);
                    parkingLot = new ParkingLot(capacity);
                    Console.WriteLine($"Created a parking lot with {capacity} slots");
                }
                else if (tokens[0] == "park")
                {
                    parkingLot.ParkVehicle(tokens[1], tokens[2], tokens[3]);
                }
                else if (tokens[0] == "leave")
                {
                    int slotNumber = int.Parse(tokens[1]);
                    parkingLot.Leave(slotNumber);
                }
                else if (tokens[0] == "status")
                {
                    parkingLot.Status();
                }
                else if (tokens[0] == "type_of_vehicles")
                {
                    parkingLot.TypeOfVehicles(tokens[1]);
                }
                else if (tokens[0] == "registration_numbers_for_vehicles_with_color")
                {
                    parkingLot.RegistrationNumbersForVehiclesWithColor(tokens[1]);
                }
                else if (tokens[0] == "slot_numbers_for_vehicles_with_color")
                {
                    parkingLot.SlotNumbersForVehiclesWithColor(tokens[1]);
                }
                else if (tokens[0] == "slot_number_for_registration_number")
                {
                    parkingLot.SlotNumberForRegistrationNumber(tokens[1]);
                }
                else if (tokens[0] == "exit")
                {
                    break;
                }
            }
        }
    }
}