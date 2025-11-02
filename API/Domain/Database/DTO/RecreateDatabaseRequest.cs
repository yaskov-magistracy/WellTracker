namespace Domain.Database.DTO;

public class RecreateDatabaseRequest
{
    public AutoFillingParams AutoFillingParams { get; set; } = AutoFillingParams.OnlySimpleData;
}

public enum AutoFillingParams
{
    OnlySimpleData,
    SomeRealData,
    FullRealData
}