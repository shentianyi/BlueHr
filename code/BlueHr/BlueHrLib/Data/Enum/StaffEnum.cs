using System.ComponentModel;


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