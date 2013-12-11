namespace SeriesGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Services;
    using System.Web.UI;
    using DataAccess;
    using DataAccessContracts;
    using DataAccessContracts.Dto;

    public partial class _Default : Page
    {
        // TODO: Specify your connection string
        private const string ConnString = ;
        private static readonly SequenceDataAccess Da = new SequenceDataAccess(ConnString);

        [WebMethod]
        public static ResultDto GenerateSequence(string sequenceTypeValue, string countOfItemsValue)
        {
            var sequenceType = (SequenceType)Enum.Parse(typeof(SequenceType), sequenceTypeValue);
            var countOfItems = int.Parse(countOfItemsValue);
            Func<IEnumerable<long>> generator = null;
            ResultDto result = null;
            switch (sequenceType)
            {
                case SequenceType.Fibonacci:
                    {
                        generator = SequenceGenerator.Fibonacci;
                        break;
                    }
                case SequenceType.Bell:
                    {
                        generator = SequenceGenerator.Bell;
                        break;
                    }
                case SequenceType.Catalan:
                    {
                        generator = SequenceGenerator.Catalan;
                        break;
                    }
            }
            var generationTask = new Task<IEnumerable<long>>(
                        count => generator().TakeWhile((number, index) => index < (int)count).ToList(),
                        countOfItems);
            generationTask.Start();
            generationTask.ContinueWith(genTask =>
                Da.AddSequence(
                    new SequenceDto
                    {
                        Numbers = genTask.Result,
                        Type = sequenceType
                    }), TaskContinuationOptions.OnlyOnRanToCompletion)
                .ContinueWith(saveTask =>
                {
                    var lastSequence = Da.GetLastSequence();
                    result = new ResultDto
                    {
                        SequenceId = lastSequence.Id,
                        SequenceType = lastSequence.Type.GetDescription(),
                        CountOfElements = lastSequence.Numbers.Count(),
                        AverageElement = lastSequence.Numbers.Average(),
                        MaxElement = lastSequence.Numbers.Max()
                    };
                }, TaskContinuationOptions.OnlyOnRanToCompletion)
                .Wait();
           
            if (generationTask.Exception != null)
            {
                throw generationTask.Exception.InnerException;
            }
            return result;
        }
    }
}
