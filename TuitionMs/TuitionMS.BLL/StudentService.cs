using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuitionMS.DAL;

namespace TuitionMS.BLL
{
    public class StudentService
    {
        // 创建Tuition.DAL数据访问层中的类的实例
        TuitionMSDataContext db = new TuitionMSDataContext();

        /*  summary:    检查学生输入的学号和密码是否正确
         *  param:      username(学生学号) password(密码)
         *  return:     若用户名与密码正确，则返回用户studNo
         *  author:     myThis
         * */
        public string CheckLogin(string username, string password)
        {
            //  查询数据库中查询学号 注:由于Linq的查询结果是集合，故需使用FirstOrDefault获得对应的值
            Students students = (from s in db.Students
                                 where s.studNo == username && s.studPwd == password
                                 select s).FirstOrDefault();
            if (students != null)
            {
                return students.studNo + ' ' + students.studName;
            }
            else
            {
                return "";
            }
        }

        /*  summary:    相应学号学生的姓名
         *  param:      studNo(学生学号)
         *  return:     学生姓名
         *  author:     myThis
         * */
        public string GetStudentName(string studNo)
        {
            //  查询数据库中查询学号 注:由于Linq的查询结果是集合，故需使用FirstOrDefault获得对应的值
            Students students = (from s in db.Students
                                 where s.studNo == studNo
                                 select s).FirstOrDefault();
            if (students != null)
            {
                return students.studName;
            }
            else
            {
                return "";
            }
        }

        /*  summary:    判断学生表中是否存在输入的用户名和邮箱
         *  param:      studNo(学生学号) email(学生邮箱)
         *  return:     存在时返回true，不存在返回false
         *  author:     myThis
         * */
        public bool IsEmailExits(string studNo, string email)
        {
            //  查询数据库中查询学号 注:由于Linq的查询结果是集合，故需使用FirstOrDefault获得对应的值
            Students students = (from s in db.Students
                                 where s.studNo == studNo.Trim() && s.studEmail == email.Trim()
                                 select s).FirstOrDefault();
            if (students != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*  summary:    判断密码是否正确
         *  param:      studNo(学生学号) password(学生邮箱)
         *  return:     正确返回true，不正确返回false
         *  author:     myThis
         * */
        public bool IsPassword(string studNo, string password)
        {
            //  查询数据库中查询学号 注:由于Linq的查询结果是集合，故需使用FirstOrDefault获得对应的值
            Students students = (from s in db.Students
                                 where s.studNo == studNo.Trim() && s.studPwd == password.Trim()
                                 select s).FirstOrDefault();
            if (students != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*  summary:    判断学生表中是否存在输入的用户名和邮箱
         *  param:      studNo(学生学号) email(学生邮箱)
         *  return:     存在时返回true，不存在返回false
         *  author:     myThis
         * */
        public void SetStudentPassword(string studNo, string oldPwd, string newPwd)
        {
            //  查询数据库中查询学号 注:由于Linq的查询结果是集合，故需使用FirstOrDefault获得对应的值
            Students student = (from s in db.Students
                                 where s.studNo == studNo.Trim() && s.studPwd == oldPwd.Trim()
                                 select s).First();
            student.studPwd = newPwd.Trim();
            db.SubmitChanges();
        }


        /*  summary:    将学生密码重置为123456
         *  param:      studNo(学生学号) email(学生邮箱)
         *  return:     void
         *  author:     myThis
         * */
        public void ResetPassword(string studNo, string email)
        {
            //  查询数据库中查询学号 注:由于Linq的查询结果是集合，故需使用FirstOrDefault获得对应的值
            Students students = (from s in db.Students
                                 where s.studNo == studNo.Trim() && s.studEmail == email.Trim()
                                 select s).First();
            // 将密码重置为123456
            students.studPwd = "123456";
            // 提交密码,修改数据库
            db.SubmitChanges();
        }


        /*  summary:    获得该生未缴费的学期
         *  param:      studNo(学生学号) 
         *  return:     List<Term> 未缴费的
         *  author:     myThis
         * */
        public List<Term> GetNoPayTermByStudNo(string studNo)
        {
            return (from term in db.Term
                    from studterm in db.StudTuition
                    where studterm.studNo == studNo && term.termNo == studterm.termNo && studterm.sData == null
                    select term).ToList();
        }

        /*  summary:    获得该生已缴费的学期
         *  param:      studNo(学生学号) 
         *  return:     List<Term> 未缴费的
         *  author:     myThis
         * */
        public List<Term> GetPayedTermByStudNo(string studNo)
        {
            return (from term in db.Term
                    from studterm in db.StudTuition
                    where studterm.studNo == studNo && term.termNo == studterm.termNo && studterm.sData != null
                    select term).ToList();
        }


        /*  summary:    获得该生的专业号
         *  param:      studNo(学生学号) 
         *  return:     string specNo(专业号)
         *  author:     myThis
         * */
        public string GetSpecNoByStudNo(string stuNo)
        {
            var result = (from s in db.Students
                          from c in db.Class
                          where s.studNo == stuNo && c.clasNo == s.clasNo
                          select c).FirstOrDefault();
            // 注意此处可能返回的值是空的值,但是并不清楚到底是什么类型
            return result.specNo;
        }


        /*  summary:    获得专业某学期的学费
         *  param:      termNo(学期号) ,specNo(专业号)
         *  return:     List<Tuition> (学费表)
         *  author:     myThis
         * */
        public List<Tuition> GetTuitionByTermAndSpecNo(string termNo, string specNo)
        {
            return (from tui in db.Tuition
                    where tui.termNo == termNo && tui.specNo == specNo
                    select tui).ToList();
        }


        /*  summary:    获得学生某学期的应缴纳的学费
         *  param:      termNo(学期号) ,studNo(专业号),specNo(专业号)
         *  return:     object (应缴纳)
         *  author:     myThis
         * */
        public object GetPayStudTuitionByTermAndSpecNo(string termNo, string specNo, string studNo)
        {
            // 将学生缴费表与当前学生的缴费记录表进行结合并显示
            var result = from tui in db.Tuition
                         join studTui in db.StudTuition on tui.termNo equals studTui.termNo
                         where tui.termNo == termNo && tui.specNo == specNo && studTui.termNo == termNo && studTui.studNo == studNo
                         select new
                         {
                             Tuition = tui.tuition1,
                             Premiun = tui.premiun,
                             Accom = tui.accom,
                             Book = tui.book,
                             Other = tui.other,
                             STuiWaiver = (studTui.sTuiWaiver == null) ? 0 : studTui.sTuiWaiver,
                             Sloans = (studTui.sLoans == null) ? 0 : studTui.sLoans
                         };
            return result;
        }

        /*  summary:    获得学生某学期的已经缴纳的学费
         *  param:      termNo(学期号) ,studNo(学号),specNo(专业号)
         *  return:     object (应缴纳)
         *  author:     myThis
         * */
        public object GetPayedStudTuitionByTermAndSpecNo(string termNo, string studNo)
        {
            // 确保当前学期为已完成缴费的学期
            // 将学生缴费表与当前学生的缴费记录表进行结合并显示
            var result = from studTui in db.StudTuition
                         where studTui.termNo == termNo && studTui.studNo == studNo
                         select new
                         {
                             学费 = studTui.sTuition,
                             保险费 = studTui.sPremiun,
                             住宿费 = studTui.sAccom,
                             书本费 = studTui.sBook,
                             其他费用 = studTui.sOther,
                             贷款 = (studTui.sTuiWaiver == null) ? 0 : studTui.sTuiWaiver,
                             学费减免 = (studTui.sLoans == null) ? 0 : studTui.sLoans,
                             已缴费 = studTui.sRealPay
                         };
            return result;
        }

        /*  summary:    获得学生应缴纳的总费用
         *  param:      termNo(学期号) ,studNo(专业号)
         *  return:     List<StudTuition> (应缴纳)
         *  author:     myThis
         * */
        public int GetPayAllByTermNoAndSpecNo(string termNo, string specNo, string studNo)
        {
            // 将学生缴费表与当前学生的缴费记录表进行结合并显示
            var result = from tui in db.Tuition
                         join studTui in db.StudTuition on tui.termNo equals studTui.termNo
                         where tui.termNo == termNo && tui.specNo == specNo && studTui.termNo == termNo && studTui.studNo == studNo
                         select new
                         {
                             Tuition = tui.tuition1,
                             Premiun = tui.premiun,
                             Accom = tui.accom,
                             Book = tui.book,
                             Other = tui.other,
                             STuiWaiver = (studTui.sTuiWaiver == null) ? 0 : studTui.sTuiWaiver,
                             Sloans = (studTui.sLoans == null) ? 0 : studTui.sLoans
                         };
            // 将减免与贷款相减得到应缴纳学费
            int all = (int)result.First().Tuition + (int)result.First().Premiun +
                (int)result.First().Accom + (int)result.First().Book +
                (int)result.First().Other - (int)result.First().STuiWaiver - (int)result.First().Sloans;

            return all;
        }

        /*  summary:    支付当前学期学费
         *  param:      termNo(学期号) ,studNo(专业号),学号(studNo)
         *  return:     void
         *  author:     myThis
         * */
        public void PayStudTuition(string termNo, string specNo, string studNo)
        {
            // 获得总金额
            int all = GetPayAllByTermNoAndSpecNo(termNo, specNo, studNo);
            // 改写学生的学费表
            StudTuition reStuT = (from studtui in db.StudTuition
                                  where studtui.studNo == studNo && studtui.termNo == termNo
                                  select studtui).First();
            // 获得学费缴纳表
            Tuition reTuition = (from tuition in db.Tuition
                                 where tuition.termNo == termNo && tuition.specNo == specNo
                                 select tuition).First();
            // 获得当前的日期 如2018-11-11
            string NowData = DateTime.Now.ToString("yyyy-MM-dd"); ;
            // 填写学费
            if (reStuT != null && reTuition != null)
            {
                reStuT.sAccom = reTuition.accom;        // 住宿费
                reStuT.sBook = reTuition.book;          // 书本费
                reStuT.sData = NowData;                 // 当前日期
                reStuT.sOther = reTuition.other;        // 其他费(学杂费)
                reStuT.sPremiun = reTuition.premiun;    // 保险费
                reStuT.sRealCharge = all;               // 实际缴费费
                reStuT.sRealPay = all;                  // 实际支付费
                reStuT.sTuition = reTuition.tuition1;   // 学费
            }
            // 提交数据
            db.SubmitChanges();
        }



        /*  summary:    获得学生的个人信息
         *  param:      学号(studNo)
         *  return:     void
         *  author:     myThis
         * */
        public string[] GetStudentMessage(string studNo)
        {
            // 将学生表与班级表结合
            var result = from s in db.Students
                         join c in db.Class on s.clasNo equals c.clasNo
                         where s.clasNo == c.clasNo && s.studNo == studNo
                         select new
                         {
                             no = s.studNo,
                             name = s.studName,
                             className = c.clasName,
                             email = s.studEmail,
                             birth = s.studBirth,
                             tel = s.studTel,
                             sex = s.studSex
                         };
            if(result != null)
            {
                string[] s = { result.First().name, result.First().className,
                    result.First().email, result.First().birth,
                    result.First().tel, result.First().sex };
                return s;
            }
            return null;
        }

        /*  summary:    修改学生的邮箱,性别，生日，电话
         *  param:      学号(studNo)
         *  return:     void
         *  author:     myThis
         * */
        public void SetStudentMessage(string studNo,string email,string tel,string sex,string birth)
        {
            // 获取该生的学生信息,对相关信息进行更改
            Students student = (from s in db.Students
                                where s.studNo == studNo
                                select s).First();
            student.studEmail = email;
            student.studTel = tel;
            student.studSex = sex;
            student.studBirth = birth;
            // 修改学生相关信息
            db.SubmitChanges();
        }

        /*  summary:    学生提交反馈建议
         *  param:      学号(studNo)
         *  return:     void
         *  author:     myThis
         * */
         public void SubmitFbMassage(string propObject,string propContent, string propTime)
        {
            // 建立反馈建议实例
            Propose propose = new Propose();
            propose.propObject = propObject;
            propose.propContent = propContent;
            propose.propTime = DateTime.Parse(propTime);
            db.Propose.InsertOnSubmit(propose);
            db.SubmitChanges();
        }
    }
}
