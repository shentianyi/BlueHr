using System.ComponentModel;

public enum IsOnTrail
{
    /// <summary>
    /// 试用期
    /// </summary>
    [Description("试用期")]
    OnTrial = 1,

    /// <summary>
    /// 正式
    /// </summary>
    [Description("正式")]
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