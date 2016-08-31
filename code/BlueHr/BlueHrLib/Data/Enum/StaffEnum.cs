using System.ComponentModel;

public enum IsOnTrail
{
    /// <summary>
    /// 试用期
    /// </summary>
    [Description("是")]
    OnTrial = 1,

    /// <summary>
    /// 正式
    /// </summary>
    [Description("否")]
    OffTrial = 0
}

public enum Sex
{
    /// <summary>
    /// 男
    /// </summary>
    [Description("男")]
    Male = 0,

    /// <summary>
    /// 女
    /// </summary>
    [Description("女")]
    Female = 1
}

public enum IsPayCPF
{
    /// <summary>
    /// 交了
    /// </summary>
    [Description("是")]
    OnPayCPF = 1,

    /// <summary>
    /// 没交
    /// </summary>
    [Description("否")]
    OffPayCPF = 0
}

public enum ResidenceType
{
    /// <summary>
    /// 农业
    /// </summary>
    [Description("农业")]
    Farmer = 1,

    /// <summary>
    /// 非农业
    /// </summary>
    [Description("非农")]
    NotFarmer = 0
}