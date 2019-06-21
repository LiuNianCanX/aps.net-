using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuitionMS.DAL;

namespace TuitionMS.BLL
{
    public class FinanceService
    {
        // 创建Tuition.DAL数据访问层中的类的实例
        TuitionMSDataContext db = new TuitionMSDataContext();

        /*  summary:    当前学生缴费表是否存在
         *  param:      string termNo(学期), string studNo
         *  return:     bool true表示存在 false表示不存在
         *  author:     myThis
         * */
        public bool IsStudTuitionExist(string termNo,string studNo)
        {
            var result = from studtui in db.StudTuition
                         where studtui.termNo == termNo && studtui.studNo == studNo
                         select studtui;
            if (result != null)
                return false;
            else
                return true;
        }

        /*  summary:    通过专业名字获取专业号
         *  param:      string specName(专业名)
         *  return:     string specNO
         *  author:     myThis
         * */
        public string GetSpecNoBySpecName(string specName)
        {
            Speciality speciality = (from s in db.Speciality
                                     where s.specName == specName
                                     select s).First();

            return speciality.specNo;
        }

        /*  summary:    获得当前已经创建的学院
         *  param:      
         *  return:     List<Department> 学院
         *  author:     myThis
         * */
        public List<Department> GetDepartMent()
        {
            //  查询数据库中查询学号 注:由于Linq的查询结果是集合，故需使用FirstOrDefault获得对应的值
            return  (from d in db.Department
                     select d).ToList();
        }

        /*  summary:    获得当前创建的学年
         *  param:      
         *  return:     List<Term> 学年
         *  author:     myThis
         * */
        public List<Term> GetTerm()
        {
            return (from t in db.Term
                    select t).ToList();
        }


        /*  summary:    获得某未创建标准的专业
         *  param:      string deptNo(学院号) string termNo(学期号)
         *  return:     List<Term> 学年
         *  author:     myThis
         * */
        public List<Speciality> GetNoTuitionSpeciality(string deptNo,string termNo)
        {
            return (from s in db.Speciality
                    from t in db.Tuition
                    where s.deptNo == deptNo && t.termNo == termNo  
                    select s).ToList();
        }

        /*  summary:    获得当前专业信息
         *  param:      string deptNo(学院号)
         *  return:     List<Speciality> 
         *  author:     myThis
         * */
        public List<Speciality> GetSpeciality( string deptNo)
        {
            return (from s in db.Speciality
                    where s.deptNo == deptNo
                    select s).ToList();
        }

        /*  summary:    获得班级信息
         *  param:      string specNo(专业号)
         *  return:     List<Class> 
         *  author:     myThis
         * */
        public List<Class> GetClass( string specNo)
        {
            return (from c in db.Class
                    where c.specNo == specNo
                    select c).ToList();
        }

        /*  summary:    获得学生信息
         *  param:      string clasNo(专业号)
         *  return:     List<Students> 
         *  author:     myThis
         * */
        public List<Students> GetStudents(string clasNo)
        {
            return (from s in db.Students
                    where s.clasNo == clasNo
                    select s).ToList();
        }

        /*  summary:    获得学费表存在的学期
         *  param:      string studNo(学号)
         *  return:     List<Students> 
         *  author:     myThis
         * */
        public List<StudTuition> GetStudTuition(string studNo)
        {
            return (from s in db.StudTuition
                    where s.studNo == studNo
                    select s).ToList();
        }

        /*  summary:    获得学费表不存在的学期
         *  param:      string specNo(学号) string term(学期)
         *  return:     List<Students> 
         *  author:     myThis
         * */
        public List<Term> GetNoExistTerm(string specNo)
        {
            return (from t in db.Tuition
                    from te in db.Term
                    where t.specNo == specNo && te.termNo != t.termNo
                    select te).ToList();
        }

        /*  summary:    创建的学费标准
         *  param:      term  specNo tuition  premiun accom  book other 
         *  return:     List<Term> 学年
         *  author:     myThis
         * */
        public void GreateTuition(string term,string specNo,int tuition, int premiun, int accom, int book, int other)
        {
            Tuition tui = new Tuition();
            tui.specNo = specNo;
            tui.termNo = term;
            tui.tuition1 = tuition;
            tui.premiun = premiun;
            tui.accom = accom;
            tui.book = book;
            tui.other = other;
            tui.total = tuition + premiun + accom + book + other;
            db.Tuition.InsertOnSubmit(tui);
            db.SubmitChanges();
        }

        /*  summary:    修改学费减免与贷款信息
         *  param:      string termNo(学期) string studNo(学号) int tuiWaiver(减免) int loans(贷款)
         *  return:     void
         *  author:     myThis
         * */
        public void SetStudTuition(string termNo,string studNo,int tuiWaiver,int loans)
        {

            StudTuition studTuition = (from st in db.StudTuition
                                       where st.termNo == termNo && st.studNo == studNo
                                       select st).First();
            studTuition.sTuiWaiver = tuiWaiver;
            studTuition.sLoans = loans;
            db.SubmitChanges();
        }

        /*  summary:    获得学院学费统计情况
         *  param:      string deptName(学院名)
         *  return:     object
         *  author:     myThis
         * */
         public object GetDepartmentStatistics(string deptName)
        {
            object result = from s in db.Department_statistics_view
                            where s.学院 == deptName
                            select s;
            return result;
        }

        /*  summary:    获得专业学费统计情况
         *  param:      string specName(专业名)
         *  return:     object
         *  author:     myThis
         * */
        public object GetSpecilityStatistics(string specName)
        {
            object result = from s in db.Speciality_statistics_view
                            where s.专业 == specName
                            select s;
            return result;
        }

        /*  summary:    获得班级学费统计情况
         *  param:      string clasName(专业名)
         *  return:     object
         *  author:     myThis
         * */
        public object GetClassStatistics(string clasName)
        {
            object result = from c in db.Class_statistics_view
                            where c.班级 == clasName
                            select c;
            return result;
        }

        /*  summary:    获得班级学费统计情况
         *  param:      string termNo(专业名)
         *  return:     object
         *  author:     myThis
         * */
        public object GetTermStatistics(string termNo)
        {
            object result = from t in db.Term_statistics_view
                            where t.学年 == termNo
                            select t;
            return result;
        }

        /*  summary:    获得学院未缴费学费统计情况
         *  param:      string deptName(学院名)
         *  return:     object
         *  author:     myThis
         * */
        public object GetDepartmentNoPay(string deptName)
        {
            object result = from d in db.Department_nopay_view
                            where d.学院 == deptName
                            select d;
            return result;
        }

        /*  summary:    获得专业未缴费学费统计情况
         *  param:      string specName(学院名)
         *  return:     object
         *  author:     myThis
         * */
        public object GetSpecilityNoPay(string specName)
        {
            object result = from s in db.Speciality_nopay_view
                            where s.专业 == specName
                            select s;
            return result;
        }

        /*  summary:    获得班级未缴费学费统计情况
         *  param:      string clasName(学院名)
         *  return:     object
         *  author:     myThis
         * */
        public object GetClassNoPay(string clasName)
        {
            object result = from c in db.Class_nopay_view
                            where c.班级 == clasName
                            select c;
            return result;
        }

        /*  summary:    获得所有的学费统计表
         *  param:      string clasName(学院名)
         *  return:     object
         *  author:     myThis
         * */
        public object GetAllTuition()
        {
            object result = from t in db.Tuition
                            from s in db.Speciality
                            where s.specNo == t.specNo
                            select new
                            {
                                TermNo = t.termNo,
                                SpecName = s.specName,
                                Tuition = t.tuition1,
                                Premiun = t.premiun,
                                Accom = t.accom,
                                Book = t.book,
                                Other = t.other,
                                Total = t.total
                            };
            return result;
        }

        /*  summary:    修改学费表的信息
         *  param:      string termNo(学期) string specName(专业名) int tuition(学费)
         *              int premiun(保险费) int accom(住宿费) int book(书本费)
         *              int other(其他费用) int total(总费) 
         *  return:     void
         *  author:     myThis
         * */
        public void SetTuition(string termNo, string specName, int tuition, int premiun
                                , int accom, int book, int other)
        {
            // 获得专业号
            string specNo = GetSpecNoBySpecName(specName);
            // 获得要修改的对象
            Tuition tui = (from t in db.Tuition
                           where t.termNo == termNo && t.specNo == specNo
                           select t).First();
            tui.tuition1 = tuition;
            tui.premiun = premiun;
            tui.accom = accom;
            tui.book = book;
            tui.other = other;
            tui.total = tuition + premiun+ accom+ book+ other;
            // 对数据库进行修改
            db.SubmitChanges();
        }

        /*  summary:    修改学费表的信息
         *  param:      string termNo(学期) string specName(专业名) int tuition(学费)
         *              int premiun(保险费) int accom(住宿费) int book(书本费)
         *              int other(其他费用) int total(总费) 
         *  return:     void
         *  author:     myThis
         * */
        public void DelTuition(string termNo, string specName)
        {
            // 获得专业号
            string specNo = GetSpecNoBySpecName(specName);
            var result = from r in db.Tuition
                         where r.specNo == specNo && r.termNo == termNo
                         select r;
            // 删除学费表
            db.Tuition.DeleteAllOnSubmit(result);
            // 对数据库进行修改
            db.SubmitChanges();
        }

        /*  summary:    获得学院缴费情况
         *  param:      string deptName
         *  return:     object
         *  author:     myThis
         * */
        public object GetDepartmentTuition(string deptName)
        {
            var result = from t in db.TuiTion_seacher_view_change
                         where t.学院 == deptName
                         select t;

            return result;
        }

        /*  summary:    获得专业缴费情况
         *  param:      string specName
         *  return:     object
         *  author:     myThis
         * */
        public object GetSpecilityTuition(string specName)
        {
            var result = from t in db.TuiTion_seacher_view_change
                         where t.专业 == specName
                         select t;
            return result;
        }

        /*  summary:    获得班级缴费情况
         *  param:      string clasName
         *  return:     object
         *  author:     myThis
         * */
        public object GetClassTuition(string clasName)
        {
            var result = from t in db.TuiTion_seacher_view_change
                         where t.班级 == clasName
                         select t;
            return result;
        }

        /*  summary:    获得学生缴费情况(通过姓名)
         *  param:      string studName
         *  return:     object
         *  author:     myThis
         * */
        public object GetStudentTuitionByName(string studName)
        {
            var result = from t in db.TuiTion_seacher_view_change
                         where t.姓名 == studName
                         select t;
            return result;
        }

        /*  summary:    获得学生缴费情况(通过学号)
         *  param:      string studNo
         *  return:     object
         *  author:     myThis
         * */
        public object GetStudentTuitionByStudNo(string studNo)
        {
            var result = from t in db.TuiTion_seacher_view_change
                         where t.学号 == studNo
                         select t;
            return result;
        }

        /*  summary:    获得某学年缴费情况(通过学号)
         *  param:      string termNo
         *  return:     object
         *  author:     myThis
         * */
        public object GetTermTuition(string termNo)
        {
            var result = from t in db.TuiTion_seacher_view_change
                         where t.学年号 == termNo
                         select t;
            return result;
        }

        /*  summary:    获得某学院学生的信息
         *  param:      string deptNo
         *  return:     List<Students>
         *  author:     myThis
         * */
        public List<Students> GetDeptStudent(string deptNo)
        {
            return (from s in db.Speciality
                    from c in db.Class
                    from stud in db.Students
                    where s.deptNo == deptNo && c.specNo == s.specNo && stud.clasNo == c.clasNo
                    select stud).ToList();
        }

        /*  summary:    获得某专业学生的信息
         *  param:      string specNo
         *  return:     List<Students>
         *  author:     myThis
         * */
        public List<Students> GetSpecStudent(string specNo)
        {
            return (from c in db.Class
                    from stud in db.Students
                    where c.specNo == specNo && stud.clasNo == c.clasNo
                    select stud).ToList();
        }

        /*  summary:    获得某班级学生的信息
         *  param:      string clasNo
         *  return:     List<Students>
         *  author:     myThis
         * */
        public List<Students> GetClasStudent(string clasNo)
        {
            return (from stud in db.Students
                    where stud.clasNo == clasNo
                    select stud).ToList();
        }

        /*  summary:    获得某学生的信息
         *  param:      string studNo
         *  return:     List<Students>
         *  author:     myThis
         * */
        public List<Students> GetStudent(string studNo)
        {
            return (from stud in db.Students
                    where stud.studNo == studNo
                    select stud).ToList();
        }

        /*  summary:    学生提交反馈建议
         *  param:      
         *  return:     void
         *  author:     myThis
         * */
        public void SubmitFbMassage(string propObject, string propContent, string propTime)
        {
            // 建立反馈建议实例
            Propose propose = new Propose();
            propose.propObject = propObject;
            propose.propContent = propContent;
            propose.propTime = DateTime.Parse(propTime);
            db.Propose.InsertOnSubmit(propose);
            db.SubmitChanges();
        }

        /*  summary:    获得学生的反馈信息
         *  param:      
         *  return:     object
         *  author:     myThis
         * */
        public object GetFbByStud()
        {
            return from p in db.Propose
                   where p.propObject == "finance"
                   select p;
        }

        /*  summary:    删除反馈信息
         *  param:      string propTime(时间)
         *  return:     void
         *  author:     myThis
         * */
        public void DelPropose(string propTime)
        {
            var result= from p in db.Propose
                   where p.propTime == DateTime.Parse(propTime)
                   select p;

            // 删除建议
            db.Propose.DeleteAllOnSubmit(result);
            // 对数据库进行修改
            db.SubmitChanges();
        }

    }
}
