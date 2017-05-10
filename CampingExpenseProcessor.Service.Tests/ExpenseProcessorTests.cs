using CampingExpenseProcessor.DomainEntity;
using CampingExpenseProcessor.Service.Contractor;
using CampingExpenseProcessor.Service.ServiceImp;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CampingExpenseProcessor.Service.Tests
{
    [TestFixture]
    public class ExpenseProcessorTests
    {
        private IExpenseManager _manager;

        [Test]
        public void Can_Get_Process_Expense()
        {
            // Arrange
            var trip = new List<CampingTrip>
            {
                new CampingTrip
                {
                    Expenses = new List<CampingExpense>
                    {
                        new CampingExpense {PersonId = 1, Expenses = new List<decimal> {10.00m, 20.00m}},
                        new CampingExpense {PersonId = 2, Expenses = new List<decimal> {15.00m, 15.01m, 3.00m, 3.01m}},
                        new CampingExpense {PersonId = 3, Expenses = new List<decimal> {5.00m, 9.00m, 4.00m}},
                    }
                }
            };
            _manager = new DefaultExpenseManager();

            // Act
            _manager.ExpenseExchange(trip);

            // Arrange
            Assert.That(trip.First().Expenses.ToList()[0].Debit, Is.EqualTo(-1.99));
        }
    }
}