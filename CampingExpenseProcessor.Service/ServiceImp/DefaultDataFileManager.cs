using CampingExpenseProcessor.DomainEntity;
using CampingExpenseProcessor.Service.Contractor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CampingExpenseProcessor.Service.ServiceImp
{
    public class DefaultDataFileManager : IDataFileManager
    {
        public string GetDataContent(string fullDataFileName)
        {
            if (string.IsNullOrEmpty(fullDataFileName))
            {
                return null;
            }

            if (!File.Exists(fullDataFileName))
            {
                return null;
            }

            var dataStr = File.ReadAllText(fullDataFileName);
            return dataStr;
        }

        public string GetOutFileName(string fullDataFileName)
        {
            if (string.IsNullOrEmpty(fullDataFileName))
            {
                throw new ArgumentNullException(nameof(fullDataFileName));
            }

            var fileName = $"{Path.GetFileNameWithoutExtension(fullDataFileName)}.out";
            return fileName;
        }

        public bool SaveResultFile(IEnumerable<CampingTrip> trips, string fullTargetFileName)
        {
            var triplst = trips as IList<CampingTrip> ?? trips.ToList();
            if (trips == null || !triplst.Any())
            {
                throw new ArgumentNullException(nameof(trips));
            }

            if (string.IsNullOrEmpty(fullTargetFileName))
            {
                return false;
            }

            if (File.Exists(fullTargetFileName))
            {
                try
                {
                    File.Delete(fullTargetFileName);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            var sb = new StringBuilder();
            foreach (var trip in triplst)
            {
                foreach (var exp in trip.Expenses)
                {
                    sb.Append(exp.Debit < 0 ? $"({Math.Abs(exp.Debit):C}){Environment.NewLine}" : $"{Math.Abs(exp.Debit):C}{Environment.NewLine}");
                }

                sb.Append($"{Environment.NewLine}");
            }

            try
            {
                var file = new StreamWriter(fullTargetFileName);
                file.Write(sb);
                file.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}