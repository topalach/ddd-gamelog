using GameLog.Domain.Common;

namespace GameLog.Tests.Common;

public static class TestData
{
    public static NonEmptyDateTime SomeDomainTime
        => new(new DateTimeOffset(2022, 11, 15, 0, 0, 0, TimeSpan.FromHours(2)));

    public static DateTimeOffset SomeDateTimeOffset => new(2021, 10, 14, 0, 0, 0, TimeSpan.FromHours(1));
}