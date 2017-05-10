using System.Collections.Generic;

namespace CampingExpenseProcessor.DomainEntity
{
    public class CampingTrip
    {
        public IEnumerable<CampingExpense> Expenses { get; set; }
    }
}