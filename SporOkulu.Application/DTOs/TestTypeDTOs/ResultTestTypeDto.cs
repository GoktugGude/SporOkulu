namespace SporOkulu.Application;

public sealed record DetailTestTypeDto(
    int Id,
    string Name,
    string Unit
)
{
    public DetailTestTypeDto() : this(0, string.Empty,string.Empty){}
}
