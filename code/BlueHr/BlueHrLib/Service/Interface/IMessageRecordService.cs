﻿using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Message;

namespace BlueHrLib.Service.Interface
{
    public interface IMessageRecordService
    {
        /// <summary>
        /// 创建信息
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="operatorId"></param>
        /// <param name="type"></param>
        /// <param name="text"></param>
        /// <param name="uniqString">唯一性键</param>
        void Create(string staffNr, int? operatorId, MessageRecordType type, string text,string uniqString=null);

        /// <summary>
        /// 创建员工基础信息被编辑消息
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="operatorId"></param>
        /// <param name="fieldName"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        void CreateStaffBasicEdited(string staffNr, int operatorId,string fieldName, string oldValue, string newValue);

        /// <summary>
        /// 创建员工转正提醒
        /// </summary>
        /// <param name="datetime"></param>
        void CreateToFullMemberMessage(DateTime datetime);

        /// <summary>
        /// 创建员工考勤异常消息
        /// </summary>
        /// <param name="attendanceDate"></param>
        void CreateAttExceptionMessage(DateTime attendanceDate);

        /// <summary>
        /// 创建员工转正消息
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="operatorId"></param>
        void CreateStaffFullMemeberMessage(string staffNr, int operatorId, string content);


        /// <summary>
        /// 创建员工离职消息
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="operatorId"></param>
        void CreateStaffResignMessage(string staffNr, int operatorId);



        /// <summary>
        /// 创建员工调岗消息
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="operatorId"></param>
        void CreateStaffShiftJobMessage(string staffNr, int operatorId, string oldJobStr, string newJobStr);
        int CountToEmployees();


        /// <summary>
        /// 创建员工调整考勤消息
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="operatorId"></param>
        void CreateStaffUpdateAttHourMessage(string staffNr, int operatorId,string oldHour, string newHour,string oldActHour,string newActHour);
        List<MessageRecord> FindBystaffNrShiftJob(string staffNr);



        /// <summary>
        /// 创建员工身份证验证消息
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="operatorId"></param>
        void CreateStaffIdCheckMessage(string staffNr);

        /// <summary>
        /// 获取未读的消息数量
        /// </summary>
        /// <param name="read"></param>
        /// <returns></returns>
        int CountUnRead(bool read = false);

        /// 根据类型和是否阅读获取列表
        /// </summary>
        /// <param name="catetory"></param>
        /// <param name="all">true is all, false is unread</param>
        /// <returns></returns>
        IQueryable<MessageRecordView> GetByCateAndAllOrUnread(MessageRecordCatetory catetory, bool all,MessageRecordSearchModel searchModel = null);
        IQueryable<MessageRecordView> GetEmployee(MessageRecordCatetory catetory, MessageRecordSearchModel searchModel = null);

        /// <summary>
        /// 阅读消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Read(int id);
        List<MessageRecord> GetAllTableName();
        List<MessageRecord> LoginDetail();
        List<MessageRecord> FindByType(int type);
    }
}
