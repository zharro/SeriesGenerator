namespace SeriesGenerator.DataAccessContracts
{
    using System.ComponentModel;

    public enum SequenceType
    {
        [Description("Fibonacci")]
        Fibonacci,
        [Description("Bell")]
        Bell,
        [Description("Catalan")]
        Catalan
    }
}