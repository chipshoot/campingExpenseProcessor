using CampingExpenseProcessor.DomainEntity;
using CampingExpenseProcessor.Service.Contractor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampingExpenseProcessor.Service.ServiceImp
{
    public class DefaultExpenseManager : IExpenseManager
    {
        public void ExpenseExchange(IEnumerable<CampingTrip> trips)
        {
            if (trips == null)
            {
                throw new ArgumentNullException(nameof(trips));
            }

            var triplst = trips as IList<CampingTrip> ?? trips.ToList();
            foreach (var trip in triplst)
            {
                var total = trip.Expenses.SelectMany(exp => exp.Expenses)
                    .Aggregate(0m, (current, itm) => current + itm);
                var expensePerPerson = total / trip.Expenses.Count();

                foreach (var exp in trip.Expenses)
                {
                    var actualExp = exp.Expenses.Sum();
                    exp.Debit = Math.Round(expensePerPerson - actualExp, 2);
                }
            }
        }
    }
}