using System.Collections.Generic;
using CampingExpenseProcessor.DomainEntity;

namespace CampingExpenseProcessor.Service.Contractor
{
    public interface IExpenseManager
    {
        void ExpenseExchange(IEnumerable<CampingTrip> trips);
    }
}