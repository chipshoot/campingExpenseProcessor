using System.Collections.Generic;

namespace CampingExpenseProcessor.DomainEntity
{
    public class CampingExpense
    {
        public int PersonId { get; set; }

        public IEnumerable<decimal> Expenses { get; set; }

        public decimal Debit { get; set; }
    }
}