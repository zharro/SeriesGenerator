namespace SeriesGenerator.DataAccess
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using Dapper;
    using Data;
    using DataAccessContracts.Dto;

    public sealed class SequenceDataAccess
    {
        private readonly string _connString;

        public SequenceDataAccess(string connString)
        {
            _connString = connString; 
        }

        public void AddSequence(SequenceDto seqDto)
        {
            var seq = seqDto.CreateSequence();
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                conn.Execute(@"INSERT Sequences (SeqType, Numbers) VALUES(@SeqType, @Numbers)", 
                            new List<Sequence> { seq });
            }
        }

        public SequenceDto GetLastSequence()
        {
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                var seq = conn.Query<Sequence>("SELECT TOP 1 * FROM Sequences ORDER BY Id DESC")
                    .FirstOrDefault();
                return seq == null ? 
                    null :
                    seq.CreateSequenceDto();
            }
        }

        public IEnumerable<SequenceDto> GetSequences()
        {
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                var sequences = conn.Query<Sequence>("SELECT * FROM Sequences");
                return sequences.Select(s => s.CreateSequenceDto());
            }
        }
    }
}