using CampingExpenseProcessor.DomainEntity;
using System.Collections.Generic;

namespace CampingExpenseProcessor.Service.Contractor
{
    public interface IDataFileManager
    {
        string GetDataContent(string fullDataFileName);

        string GetOutFileName(string fullDataFileName);

        bool SaveResultFile(IEnumerable<CampingTrip> trips, string fullTargetFileName);
    }
}