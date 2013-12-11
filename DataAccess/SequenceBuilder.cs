namespace SeriesGenerator.DataAccess
{
    using System;
    using System.Linq;
    using Data;
    using DataAccessContracts;
    using DataAccessContracts.Dto;

    public static class SequenceBuilder
    {
        public static SequenceDto CreateSequenceDto(this Sequence sequence)
        {
            var numbers = sequence.Numbers.Split(',').Select(long.Parse);
            return new SequenceDto
            {
                Id = sequence.Id,
                Type = sequence.SeqType,
                Numbers = numbers
            };
        }

        public static Sequence CreateSequence(this SequenceDto sequenceDto)
        {
            return new Sequence
            {
                Id = sequenceDto.Id,
                SeqType = sequenceDto.Type,
                Numbers = String.Join(",", sequenceDto.Numbers)
            };
        }
    }
}