namespace SeriesGenerator.Data
{
    using DataAccessContracts;

    public sealed class Sequence
    {
        public long Id { get; set; }
        public SequenceType SeqType { get; set; }
        public string Numbers { get; set; }
    }
}