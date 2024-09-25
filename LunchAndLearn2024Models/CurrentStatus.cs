using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace LunchAndLearn2024Models;
[JsonConverter(typeof(JsonStringEnumConverter<CurrentStatus>))]
public enum CurrentStatus
{
    [EnumMember(Value = "next")]
    next,
    [EnumMember(Value = "started")]
    started,
    [EnumMember(Value = "completed")]
    completed,
    [EnumMember(Value = "abandoned")]
    abandoned
}
