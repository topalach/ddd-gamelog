using System;
using GameLog.Application.Utils;
using GameLog.Domain.Common;

namespace GameLog.Tests.Mocks;

public class MockTimeService : ITimeService
{
    public static readonly DateTimeOffset DefaultUtcNow = new(2022, 8, 14, 0, 0, 0, TimeSpan.Zero);
    public static readonly NonEmptyDateTime DefaultNonEmptyUtcNow = new(DefaultUtcNow);

    private readonly DateTimeOffset _utcNowValue;

    public MockTimeService()
    {
        _utcNowValue = DefaultUtcNow;
    }

    public MockTimeService(DateTimeOffset utcNowValue)
    {
        _utcNowValue = utcNowValue;
    }

    public DateTimeOffset UtcNow() => _utcNowValue;
}