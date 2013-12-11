namespace SeriesGenerator.DataAccessContracts.Dto
{
    public class ResultDto
    {
        public long SequenceId { get; set; }
        public string SequenceType { get; set; }
        public int CountOfElements { get; set; }
        public long MaxElement { get; set; }
        public double AverageElement { get; set; }
    }
}