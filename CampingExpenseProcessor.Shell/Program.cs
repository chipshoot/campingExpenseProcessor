using CampingExpenseProcessor.Dal;
using CampingExpenseProcessor.Service.Contractor;
using CampingExpenseProcessor.Service.ServiceImp;

namespace CampingExpenseProcessor.Shell
{
    internal class Program
    {
        private static IDataFileManager _fileManager;
        private static IExpenseManager _expenseManager;
        private static IRepository _repo;

        private static void Main(string[] args)
        {
            _fileManager = new DefaultDataFileManager();
            _expenseManager = new DefaultExpenseManager();

            if (args.Length != 1)
            {
                return;
            }

            var fileName = args[0];
            var dataString = _fileManager.GetDataContent(fileName);
            if (string.IsNullOrEmpty(dataString))
            {
                return;
            }

            _repo = new DataFileRepository(dataString);
            var trips = _repo.GetAllTrips();
            _expenseManager.ExpenseExchange(trips);

            var outFile = _fileManager.GetOutFileName(fileName);
            _fileManager.SaveResultFile(trips, outFile);
        }
    }
}