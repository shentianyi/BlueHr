using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using TestCon.Properties;
using Brilliantech.Framwork.Utils.LogUtil;

namespace TestCon
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(double.Parse(".1"));
            
            //DataContext dc = new DataContext(Settings.Default.db);
            //List<string> staffNrs = new List<string>();
            //List<DateTime> datetime = new List<DateTime>();
            //for(int i = 0; i < 3000; i++)
            //{
            //    staffNrs.Add(i.ToString()+"n");
            //    datetime.Add(DateTime.Now.AddDays(0 - i).Date);
            //}


            //string nrsq = string.Format(",{0},", string.Join(",", staffNrs));
            //string dateq = string.Format(",{0},", string.Join(",", datetime.Select(ss => ss.ToString("yyyy-MM-dd")).ToList()));

            //IQueryable<AttendanceRecordCal> cq = dc.Context.GetTable<AttendanceRecordCal>()
            //  .Where(s => nrsq.IndexOf("," + s.staffNr + ",") != -1)
            //  .Where(ss=> dateq.IndexOf("," + ss.attendanceDate.ToString("yyyy-MM-dd") + ",")!=-1).Take(20);


            ////List<AttendanceRecordCal> _updateCals = dc.Context.GetTable<AttendanceRecordCal>()
            ////   .AsEnumerable()
            ////  .Join(staffNrs, s => s.staffNr, ci => ci, (s, ci) => s)
            ////  .Join(datetime, sss => sss.attendanceDate, cci => cci, (sss, cci) => sss).ToList();
            //// List<AttendanceRecordDetailView> _updateCals = dc.Context.GetTable<AttendanceRecordDetailView>()
            ////   .AsEnumerable()
            ////  .Join(staffNrs, s => s.staffNr, ci => ci, (s, ci) => s)
            ////.Join(datetime, sss => sss.recordAtDate, cci => cci, (sss, cci) => sss)
            ////   .ToList();

            //IQueryable<AttendanceRecordDetailView> q = dc.Context.GetTable<AttendanceRecordDetailView>()
            //  .Where(s => ("," + string.Join(",", staffNrs.ToArray()) + ",").IndexOf("," + s.staffNr + ",") != -1).Take(20);

            //List<AttendanceRecordDetailView> li = q.ToList();

            Console.Read();
            //try
            //{
            //    int.Parse("&*^Df");
            //}
            //catch (Exception ex)
            //{

            //    LogUtil.Logger.Error("服务运行时出错", ex);
            //}
            //Console.WriteLine(" 1. create staff \n 2.create shiftshedule\n 3.create record\n 4.calcualte staff attendance \n 5. PRESS q to quit.");
            //string ke = Console.ReadLine();
            //while (!ke.Equals("q"))
            //{
            //    Cmd(ke);
            //    ke = Console.ReadLine();
            //}
            //Console.WriteLine("quiting....press ENTER....");
            //Console.Read();
        }
        static void Cmd(string ke)
        {
           
            DateTime st = DateTime.Now;
            switch (ke)
            {
                case "1":
                    CreateStaffs();
                    break;
                case "2":
                    CreateShiftSheduce();
                    break;
                case "3":
                    CreateAttRecord();
                    break;
                case "4":
                    Console.WriteLine("input date(default is DateTime.now):");
                    string dates = Console.ReadLine();
                    DateTime date = string.IsNullOrEmpty(dates)? DateTime.Now : DateTime.Parse(dates);
                    AttendanceRecordService ars = new AttendanceRecordService(Settings.Default.db);
                    ars.CalculateAttendRecord(date ,new List<string>() { "晚" });
                    break;
                default:
                    break;
            }

            Console.WriteLine((DateTime.Now - st).TotalMilliseconds);
        }
        static void CreateStaffs()
        {
            int n = 0;
            for(int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);

                IStaffService ss = new StaffService(Settings.Default.db);
                List<Staff> staffs = new List<Staff>();

                for (int j = 0; j < 40; j++)
                {
                    staffs.Add(new Staff() { nr = n.ToString(), id = n.ToString(), workStatus = (int)WorkStatus.OnWork, companyId = 1, departmentId = 1 });
                  
                    n += 1;
                }
                ss.Creates(staffs);
            }
        }

        static void CreateShiftSheduce() {
          for (DateTime date = DateTime.Now.AddYears(-2).Date; date < DateTime.Now.Date; date=date.AddDays(1))
            { 
                Console.WriteLine(date);

            
                int n = 0;
                for (int i = 0; i < 100; i++)
                {

                    DataContext dc = new DataContext(Settings.Default.db);
                    List<ShiftSchedule> ss = new List<ShiftSchedule>();
                    for (int j = 0; j < 40; j++)
                    {
                        ss.Add(new ShiftSchedule() { shiftId = 1, scheduleAt = date, staffNr = n.ToString() });
                        n += 1;
                    }
                    dc.Context.GetTable<ShiftSchedule>().InsertAllOnSubmit(ss);
                    dc.SaveAll();
                }
            }
        }

        static void CreateAttRecord()
        {
            for (DateTime date = DateTime.Now.AddYears(-2).Date; date < DateTime.Now.Date; date = date.AddDays(1))
            {
                Console.WriteLine(date);


                int n = 0;
                for (int i = 0; i < 100; i++)
                {

                    DataContext dc = new DataContext(Settings.Default.db);
                    List<AttendanceRecordDetail> ss = new List<AttendanceRecordDetail>();
                    for (int j = 0; j < 10; j++)
                    {
                        // total 10h
                        // in 8:30
                        ss.Add(new AttendanceRecordDetail()
                        {
                            staffNr = n.ToString(),
                            createdAt = DateTime.Now,
                            soureType = "test",
                            recordAt = date.Add(new TimeSpan(8, 30, 0))
                        });
                        // out 9:30
                        ss.Add(new AttendanceRecordDetail()
                        {
                            staffNr = n.ToString(),
                            createdAt = DateTime.Now,
                            soureType = "test",
                            recordAt = date.Add(new TimeSpan(9, 30, 0))
                        });
                        // in 9:35
                        ss.Add(new AttendanceRecordDetail()
                        {
                            staffNr = n.ToString(),
                            createdAt = DateTime.Now,
                            soureType = "test",
                            recordAt = date.Add(new TimeSpan(9, 35, 0))
                        });
                        //out 11:40
                        ss.Add(new AttendanceRecordDetail()
                        {
                            staffNr = n.ToString(),
                            createdAt = DateTime.Now,
                            soureType = "test",
                            recordAt = date.Add(new TimeSpan(11, 40, 0))
                        });
                        // in 11:42
                        ss.Add(new AttendanceRecordDetail()
                        {
                            staffNr = n.ToString(),
                            createdAt = DateTime.Now,
                            soureType = "test",
                            recordAt = date.Add(new TimeSpan(11, 42, 0))
                        });
                        //out 12:30
                        ss.Add(new AttendanceRecordDetail()
                        {
                            staffNr = n.ToString(),
                            createdAt = DateTime.Now,
                            soureType = "test",
                            recordAt = date.Add(new TimeSpan(12, 30, 0))
                        });
                        // in 13:30
                        ss.Add(new AttendanceRecordDetail()
                        {
                            staffNr = n.ToString(),
                            createdAt = DateTime.Now,
                            soureType = "test",
                            recordAt = date.Add(new TimeSpan(13, 30, 0))
                        });

                        //out 15:30
                        ss.Add(new AttendanceRecordDetail()
                        {
                            staffNr = n.ToString(),
                            createdAt = DateTime.Now,
                            soureType = "test",
                            recordAt = date.Add(new TimeSpan(15, 30, 0))
                        });
                        // out 15:30:01
                        ss.Add(new AttendanceRecordDetail()
                        {
                            staffNr = n.ToString(),
                            createdAt = DateTime.Now,
                            soureType = "test",
                            recordAt = date.Add(new TimeSpan(15, 30, 01))
                        });

                        // out 15:30:02
                        ss.Add(new AttendanceRecordDetail()
                        {
                            staffNr = n.ToString(),
                            createdAt = DateTime.Now,
                            soureType = "test",
                            recordAt = date.Add(new TimeSpan(15, 30, 02))
                        });

                        // out 15:30:02
                        ss.Add(new AttendanceRecordDetail()
                        {
                            staffNr = n.ToString(),
                            createdAt = DateTime.Now,
                            soureType = "test",
                            recordAt = date.Add(new TimeSpan(15, 30, 02))
                        });


                        // in 15:40:00
                        ss.Add(new AttendanceRecordDetail()
                        {
                            staffNr = n.ToString(),
                            createdAt = DateTime.Now,
                            soureType = "test",
                            recordAt = date.Add(new TimeSpan(15, 40, 00))
                        });

                        //out 19:40
                        ss.Add(new AttendanceRecordDetail()
                        {
                            staffNr = n.ToString(),
                            createdAt = DateTime.Now,
                            soureType = "test",
                            recordAt = date.Add(new TimeSpan(19, 40, 00))
                        });

                        n += 1;
                    }
                    dc.Context.GetTable<AttendanceRecordDetail>().InsertAllOnSubmit(ss);
                    dc.SaveAll();
                }
            }
        }


    }
}
