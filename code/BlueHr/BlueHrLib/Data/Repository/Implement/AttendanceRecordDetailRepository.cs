using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Repository.Implement
{
    public class AttendanceRecordDetailRepository : RepositoryBase<AttendanceRecordDetail>, IAttendanceRecordDetailRepository
    {
        private BlueHrDataContext context;

        public AttendanceRecordDetailRepository(IDataContextFactory dc) : base(dc)
        {
            this.context = dc.Context as BlueHrDataContext;
        }

        public bool HasExistDetail(DateTime dateTime, string staffNr)
        {
            //判断 日期是否已经存在，已经存在则不添加
            
            try
            {
                var hasExistStaff = this.context.GetTable<AttendanceRecordDetail>().Where(c => c.recordAt.Equals(dateTime)).FirstOrDefault(c => c.staffNr.Equals(staffNr));
               
                if (hasExistStaff == null)
                {
                    // 数据库中不存在， 可以添加
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Create(AttendanceRecordDetail attendanceRecordDetail)
        {
            try
            {
                //通过员工判断 这个时间点是否已经打完卡

                var hasExistResult = HasExistDetail(attendanceRecordDetail.recordAt, attendanceRecordDetail.staffNr);

                if (hasExistResult)
                {
                    //添加
                    attendanceRecordDetail.soureType = "300";
                    attendanceRecordDetail.createdAt = DateTime.Now;
                    attendanceRecordDetail.isCalculated = false;

                    this.context.GetTable<AttendanceRecordDetail>().InsertOnSubmit(attendanceRecordDetail);
                    this.context.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception )
            {
                return false;
            }
        }

        public IQueryable<AttendanceRecordDetail> Search(AttendanceRecordDetailSearchModel searchModel)
        {
            IQueryable<AttendanceRecordDetail> q = this.context.AttendanceRecordDetail;
            if (!string.IsNullOrEmpty(searchModel.StaffNr))
            {
                q = q.Where(s => s.staffNr.Contains(searchModel.StaffNr));
            }

            if (!string.IsNullOrEmpty(searchModel.StaffNrAct))
            {
                q = q.Where(s => s.staffNr.Equals(searchModel.StaffNrAct));
            }

            if (searchModel.RecordAtFrom.HasValue)
            {
                q = q.Where(s => s.recordAt >= searchModel.RecordAtFrom);
            }

            if (searchModel.RecordAtEnd.HasValue)
            {
                q = q.Where(s => s.recordAt <= searchModel.RecordAtEnd);
            }
            // AS [t0] ORDER BY [t0].[recordAt] DESC, [t0].[staffNr]
            //return q.OrderBy(s => s.staffNr).OrderByDescending(s => s.recordAt);

            //   AS[t0] ORDER BY[t0].[staffNr], [t0].[recordAt] DESC
            // 因为建立了staffnr asc + recordat desc 的索引
            return q.OrderByDescending(s => s.recordAt).OrderBy(s => s.staffNr);
        }

        public bool Update(AttendanceRecordDetail attendanceRecordDetail)
        {
            //只能修改设备号  和  时间， 员工号不能修改
            AttendanceRecordDetail ard = this.context.GetTable<AttendanceRecordDetail>().FirstOrDefault(c => c.id.Equals(attendanceRecordDetail.id));

            // 如果时间 没有修改， 不检查时间， 直接修改设备号
            if(ard.recordAt == attendanceRecordDetail.recordAt)
            {
                ard.device = attendanceRecordDetail.device;

                try
                {
                    this.context.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }else
            {
                //判断修改过后的时间是否已经存在

                if (ard != null)
                {
                    var hasExistResult = HasExistDetail(attendanceRecordDetail.recordAt, attendanceRecordDetail.staffNr);
                    //如果不存在， 则进行修改添加

                    if (hasExistResult)
                    {
                        ard.recordAt = attendanceRecordDetail.recordAt;
                        ard.device = attendanceRecordDetail.device;

                        try
                        {
                            this.context.SubmitChanges();
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        //如果存在， 返回false
                        return false;
                    }
                }else
                {
                    //没有查询到， 一般不存在这种情况，除非手动输入id 打开界面
                    return false;
                }
            }
        }

        public AttendanceRecordDetail FindById(int id)
        {
            return this.context.GetTable<AttendanceRecordDetail>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool DeleteById(int id)
        {
            AttendanceRecordDetail ards = this.context.GetTable<AttendanceRecordDetail>().FirstOrDefault(c => c.id.Equals(id));

            if (ards != null)
            {
                this.context.GetTable<AttendanceRecordDetail>().DeleteOnSubmit(ards);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
