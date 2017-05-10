using CampingExpenseProcessor.Dal;
using NUnit.Framework;
using System;
using System.Linq;

namespace CampingExpenseProcessor.Service.Tests
{
    [TestFixture]
    public class DataFileRepositoryTests
    {
        private IRepository _repository;

        [Test]
        public void Can_Get_Correct_Data_With_One_Trip_From_DataFile()
        {
            // Arrange
            var data = string.Format("3{0}2{0}10.00{0}20.00{0}4{0}15.00{0}15.01{0}3.00{0}3.01{0}3{0}5.00{0}9.00{0}4.00{0}0{0}", Environment.NewLine);
            _repository = new DataFileRepository(data);

            // Act
            var trips = _repository.GetAllTrips();

            // Assert
            Assert.NotNull(trips);
            var trip = trips.First();
            Assert.NotNull(trip);
            Assert.That(trip.Expenses.Count(), Is.EqualTo(3));
            var expenses = trip.Expenses.ToList();
            Assert.That(expenses[0].PersonId, Is.EqualTo(1));
            Assert.That(expenses[0].Expenses.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Can_Get_Correct_Data_With_Multiple_Trip_From_DataFile()
        {
            // Arrange
            var data = string.Format("3{0}2{0}10.00{0}20.00{0}4{0}15.00{0}15.01{0}3.00{0}3.01{0}3{0}5.00{0}9.00{0}4.00{0}2{0}2{0}8.00{0}6.00{0}2{0}9.20{0}6.75{0}0{0}", Environment.NewLine);
            _repository = new DataFileRepository(data);

            // Act
            var trips = _repository.GetAllTrips();

            // Assert
            Assert.NotNull(trips);
            Assert.That(trips.ToList().Count, Is.EqualTo(2));
            var trip = trips.First();
            Assert.NotNull(trip);
            Assert.That(trip.Expenses.Count(), Is.EqualTo(3));
            var expenses = trip.Expenses.ToList();
            Assert.That(expenses[0].PersonId, Is.EqualTo(1));
            Assert.That(expenses[0].Expenses.Count(), Is.EqualTo(2));
        }


        // ToDo: add more test case for invalid data scheme and other defensive coding
    }
}