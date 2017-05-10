using CampingExpenseProcessor.DomainEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace CampingExpenseProcessor.Dal
{
    /// <summary>
    /// The repository is using for getting data from data file
    /// </summary>
    /// <seealso cref="CampingExpenseProcessor.Dal.IRepository" />
    public class DataFileRepository : IRepository
    {
        private string _data;
        private IEnumerable<CampingTrip> _campingTrip;

        public DataFileRepository(string dataString)
        {
            if (string.IsNullOrEmpty(dataString))
            {
                throw new ArgumentNullException(nameof(dataString));
            }

            _data = dataString;
        }

        public IEnumerable<CampingTrip> GetAllTrips()
        {
            return _campingTrip ?? (_campingTrip = ParseData());
        }

        private IEnumerable<CampingTrip> ParseData()
        {
            if (string.IsNullOrEmpty(_data))
            {
                return null;
            }

            var strReader = new StringReader(_data);

            // get person number
            var personCount = 0;
            var line = strReader.ReadLine();
            var trips = new List<CampingTrip>();

            // go through the data file to find out all trip expense
            while (true)
            {
                if (line.IsCloseNumber() || line == null)
                {
                    break;
                }

                // get trip participant number
                if (line.IsPositiveNumber())
                {
                    personCount = GetPositiveNumber(line);
                    if (personCount <= 0)
                    {
                        throw new DataException("Cannot get valid person number");
                    }
                }

                // get expense for each participant
                var expenses = new List<CampingExpense>();
                for (var i = 0; i < personCount; i++)
                {
                    line = strReader.ReadLine();
                    if (line.IsPositiveNumber())
                    {
                        var recs = GetPositiveNumber(line);
                        var charges = new List<decimal>();
                        for (var j = 0; j < recs; j++)
                        {
                            line = strReader.ReadLine();
                            if (line.IsCharge())
                            {
                                var charge = GetCharge(line);
                                charges.Add(charge);
                            }
                        }
                        var expense = new CampingExpense
                        {
                            PersonId = i + 1,
                            Expenses = charges
                        };
                        expenses.Add(expense);
                    }
                }

                var trip = new CampingTrip
                {
                    Expenses = expenses
                };
                trips.Add(trip);

                line = strReader.ReadLine();
            }

            return trips;
        }

        private static int GetPositiveNumber(string numStr)
        {
            int num;
            if (!int.TryParse(numStr, out num))
            {
                return 0;
            };

            return num;
        }

        private static decimal GetCharge(string numStr)
        {
            decimal num;
            if (!decimal.TryParse(numStr, out num))
            {
                return 0;
            };

            return num;
        }
    }
}