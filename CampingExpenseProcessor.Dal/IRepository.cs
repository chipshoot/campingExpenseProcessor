using System.Collections.Generic;
using CampingExpenseProcessor.DomainEntity;

namespace CampingExpenseProcessor.Dal
{
    /// <summary>
    /// The interface is using for getting data from data source, the source could be database
    /// or data file
    /// </summary>
    public interface IRepository
    {
        IEnumerable<CampingTrip> GetAllTrips();
    }
}