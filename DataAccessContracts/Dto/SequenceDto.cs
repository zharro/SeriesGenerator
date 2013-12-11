namespace SeriesGenerator.DataAccessContracts.Dto
{
    using System.Collections.Generic;

    public class SequenceDto
    {
        public long Id { get; set; }
        public SequenceType Type { get; set; }
        public IEnumerable<long> Numbers { get; set; } 
    }
}