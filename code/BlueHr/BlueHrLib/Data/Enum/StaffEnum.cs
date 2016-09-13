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
    /// 本地城镇
    /// </summary>
    [Description("本地城镇")]
    NotFarmer = 0,


    /// <summary>
    /// 本地农村
    /// </summary>
    [Description("本地农村")]
    Farmer = 1,

    /// <summary>
    /// 返聘人员
    /// </summary>
    [Description("返聘人员")]
    Back =2,

    /// <summary>
    /// 外地城镇
    /// </summary>
    [Description("外地城镇")]
    OutNotFarmer =3,


    /// <summary>
    /// 外地农村
    /// </summary>
    [Description("外地农村")]
    OutFarmer =4
}