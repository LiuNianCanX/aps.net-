using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuitionMS.DAL;

namespace TuitionMS.BLL
{
    /*
     * 3．教务管理人员:
	 *  3.1可以按学号等学生的信息进行精确查询，或是按院系、班级信息对模糊查询。
	 *  3.2创建、删除、修改学生、班级、专业、学校等信息
	 *  3.3对个人的账号，密码等信息进行管理
	 *  3.4对系统进行建议
     */
    public class TeacherSerivce
    {
        // 创建Tuition.DAL数据访问层中的类的实例
        TuitionMSDataContext db = new TuitionMSDataContext();

        /*3.1可以按学号等学生的信息进行精确查询，或是按院系、班级信息对模糊查询。*/
        public string searchStuInfo(string stuNo)
        {
            Students stuInfo = (from s in db.Students
                                where s.studNo == stuNo
                                select s).First();
            Class clasInfo = (from s in db.Class
                              where s.clasNo == stuInfo.clasNo
                              select s).First();
            Speciality specInfo = (from s in db.Speciality
                                   where s.specNo == clasInfo.specNo
                                   select s).First();
            Department deptInfo = (from s in db.Department
                                   where s.deptNo == specInfo.deptNo
                                   select s).First();

            string info = stuInfo.studNo + "," + stuInfo.studName + "," + deptInfo.deptName + "," + specInfo.specName + ","
                + clasInfo.clasName + "," + stuInfo.studSex + "," + clasInfo.clasInstructor + "," + stuInfo.studTel
                + "," + stuInfo.studEmail;
            return info;
        }

        /*3.2创建、删除、修改学生、班级、专业、学校等信息*/

        /******************创建、删除、修改学生信息*******************/
        /*
         * 添加学生
         * 输入：学生信息
         * 返回：true or false
         */

        public bool InsertStudent(string StudNo, string StudPwd = "", string StudName = "",
                                 string ClasNo = "", string StudBirth = "", string StudTel = "",
                                 string StudMail = "")
        {
            Students student;
            //  设置默认密码为123456
            if (StudPwd == "")
                StudPwd = "123456";

            //  将字符串格式的出生日期转变DateTime格式的日期(在表示层时最好用Ajax控件控制输入)
            if (StudBirth != "")
            {
                // 日期存在，则将日期进行转换
                student = new Students
                {
                    studNo = StudNo,
                    studPwd = StudPwd,
                    studName = StudName,
                    clasNo = ClasNo,
                    studBirth = StudBirth,
                    studTel = StudTel,
                    studEmail = StudMail
                };
            }
            else
            {
                //  日期不存在，则不写入日期
                student = new Students
                {
                    studNo = StudNo,
                    studPwd = StudPwd,
                    studName = StudName,
                    clasNo = ClasNo,
                    studTel = StudTel,
                    studEmail = StudMail
                };
            }
            //  将其写入数据库中
            db.Students.InsertOnSubmit(student);
            //  提交数据
            db.SubmitChanges();
            return true;
        }
        /*
         * 删除学生信息
         * 输入：学生学号
         * 返回：true or false
         */
        public bool delStu(string stuNo)
        {
            Students stu = (from c in db.Students
                            where c.studNo == stuNo
                            select c).First();
            if (stu != null)
            {
                db.Students.DeleteOnSubmit(stu);
                db.SubmitChanges();
                return true;
            }
            else
                return false;
        }
        /*
         *修改学生信息
         * 输入：学生学号
         * 返回：true or false
         */
        public bool updateStu(string StudNo, string StudPwd = "", string StudName = "",
                                 string ClasNo = "", string StudBirth = "", string StudTel = "",
                                 string StudMail = "")
        {
            Students stu = (from c in db.Students
                            where c.studName == StudNo
                            select c).FirstOrDefault();
            if (stu != null)
            {
                if (StudBirth == "")
                {
                    stu.studNo = StudNo;
                    stu.clasNo = ClasNo;
                    stu.studName = StudName;
                    stu.studTel = StudTel;
                    stu.studPwd = StudPwd;
                    stu.studEmail = StudMail;
                }
                else
                {
                    stu.studNo = StudNo;
                    stu.studPwd = StudPwd;
                    stu.studName = StudName;
                    stu.clasNo = ClasNo;
                    stu.studTel = StudTel;
                    stu.studEmail = StudMail;
                }
                db.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /******************创建、删除、修改班级信息*******************/
        /*
     * 添加班级
     * 输入：班级信息
     * 返回：true or false
     */
        public bool addClass(string clsNo, string clsName, string clsInstructor, int clsNum, string specNo)
        {
            Class cls = (from c in db.Class
                         where c.clasNo == clsNo
                         select c).FirstOrDefault();
            if (cls != null)
            {
                Class stu = new Class
                {
                    clasNo = clsNo,
                    clasName = clsName,
                    clasInstructor = clsInstructor,
                    clasNum = clsNum,
                    specNo = specNo
                };
                db.Class.InsertOnSubmit(stu);
                db.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        /*
         * 删除班级
         * 输入：班级号
         * 返回：true or false
         */
        public bool delCls(string clsNo)
        {
            Class cls = (from c in db.Class
                         where c.clasNo == clsNo
                         select c).First();
            if (cls != null)
            {
                db.Class.DeleteOnSubmit(cls);
                db.SubmitChanges();
                return true;
            }
            else
                return false;
        }
        /*
         *修改班级信息
         * 输入：班级学号
         * 返回：true or false
         */
        public bool updateCls(string clsNo, string clsName, string clsInstructor, int clsNum, string specNo)
        {
            Class cls = (from c in db.Class
                         where c.clasNo == clsNo
                         select c).FirstOrDefault();
            if (cls != null)
            {
                cls.clasName = clsName;
                cls.clasInstructor = clsInstructor;
                cls.clasNum = clsNum;
                cls.specNo = specNo;
                db.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /******************创建、删除、修改专业信息*******************/
        /*
     * 专业班级
     * 输入：专业信息
     * 返回：true or false
     */
        public bool addSpeciality(string speNo, string speName, string speManager, string depNo)
        {
            Speciality spe = (from c in db.Speciality
                              where c.specNo == speNo
                              select c).FirstOrDefault();
            if (spe != null)
            {
                Speciality spe1 = new Speciality
                {
                    specNo = speNo,
                    specManager = speManager,
                    specName = speName,
                    deptNo = depNo
                };
                db.Speciality.InsertOnSubmit(spe1);
                db.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        /*
         * 删除专业
         * 输入：专业号
         * 返回：true or false
         */
        public bool delSpe(string speNo)
        {
            Speciality spe = (from c in db.Speciality
                              where c.specNo == speNo
                              select c).First();
            if (spe != null)
            {
                db.Speciality.DeleteOnSubmit(spe);
                db.SubmitChanges();
                return true;
            }
            else
                return false;
        }
        /*
         *修改专业信息
         * 输入：专业号
         * 返回：true or false
         */
        public bool updateSpe(string speNo, string speName, string speManager, string depNo)
        {
            Speciality spe = (from c in db.Speciality
                              where c.specNo == speNo
                              select c).FirstOrDefault();
            if (spe == null)
            {
                spe.specNo = speNo;
                spe.specManager = speManager;
                spe.specName = speName;
                spe.deptNo = depNo;
                db.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /******************创建、删除、修改院系信息*******************/
        /*
     * 添加院系
     * 输入：院系信息
     * 返回：true or false
     */
        public bool addDepartment(string depNo, string depName)
        {
            Department dep = (from c in db.Department
                              where c.deptNo == depNo
                              select c).FirstOrDefault();
            if (dep != null)
            {
                Department dep1 = new Department
                {
                    deptNo = depNo,
                    deptName = depName
                };
                db.Department.InsertOnSubmit(dep1);
                db.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        /*
         * 删除院系
         * 输入：院系号
         * 返回：true or false
         */
        public bool delDepartment(string depNo)
        {
            Department dep = (from c in db.Department
                              where c.deptNo == depNo
                              select c).FirstOrDefault();
            if (dep != null)
            {
                db.Department.DeleteOnSubmit(dep);
                db.SubmitChanges();
                return true;
            }
            else
                return false;
        }
        /*
         *修改院系信息
         * 输入：院系号
         * 返回：true or false
         */
        public bool updateDepartment(string depNo, string depName)
        {
            Department dep = (from c in db.Department
                              where c.deptNo == depNo
                              select c).FirstOrDefault();
            if (dep != null)
            {
                dep.deptName = depName;
                db.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /*3.3对个人的账号，密码等信息进行管理*/
        public bool changePwd(string tNo, string newPassword)
        {
            return false;
        }

        /*3.4对系统进行建议*/
        public bool propose(string propObj, string propCon, DateTime propT)
        {
            Propose propose = new Propose();
            propose.propObject = propObj;
            propose.propContent = propCon;
            propose.propTime = propT;
            db.Propose.InsertOnSubmit(propose);
            db.SubmitChanges();
            return true;
        }




        /*  添加学生
         *  输入：StudNo(学号),ClasNo(班级编号),StudName(姓名),StudPwd(密码)
         *              StudBirth(出生日期),StudTel(电话号码),StudSex(性别),StudMail(电子邮箱)
         *  返回：void
         * */
        /*
       public bool addStu(string stuNo, string ClassNo, string stuName, DateTime stuBirth, string Sex, string stuTel, string stupwd, string stuEmail)
        {
            Students stud = (from c in db.Students
                             where c.studName == stuNo
                             select c).FirstOrDefault();
            if (stud != null)
            {
                Students stu = new Students
                {
                    studNo = stuNo,
                    clasNo = ClassNo,
                    studName = stuName,
                    studBirth = stuBirth,
                    studSex = Sex,
                    studTel = stuTel,
                    studPwd = stupwd,
                    studEmail = stuEmail
                };
                db.Students.InsertOnSubmit(stu);
                db.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
       */

        /*  summary:    获得学生的反馈信息
         *  param:      
         *  return:     object
         *  author:     叶锦
         * */
        public object GetFbByStud()
        {
            return from p in db.Propose
                   where p.propObject == "teacher"
                   select p;
        }

        /*  summary:    删除反馈信息
         *  param:      string propTime(时间)
         *  return:     void
         *  author:     叶锦
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

        /*  summary:    获得当前学院信息
         *  param:      
         *  return:     List<Department> 学院
         *  author:     叶锦
         * */
        public List<Department> GetDepartMent()
        {
            //  查询数据库中查询学号 注:由于Linq的查询结果是集合，故需使用FirstOrDefault获得对应的值
            return (from d in db.Department
                    select d).ToList();
        }

        /*  summary:    获得当前专业信息
         *  param:      string deptNo(学院号)
         *  return:     List<Speciality> 
         *  author:     叶锦
         * */
        public List<Speciality> GetSpeciality(string deptNo)
        {
            return (from s in db.Speciality
                    where s.deptNo == deptNo
                    select s).ToList();
        }

        /*  summary:    获得班级信息
         *  param:      string specNo(专业号)
         *  return:     List<Class> 
         *  author:     叶锦
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
         *  author:     叶锦
         * */
        public List<Students> GetStudents(string clasNo)
        {
            return (from s in db.Students
                    where s.clasNo == clasNo
                    select s).ToList();
        }

        /*  summary:    获得某学院学生的信息
         *  param:      string deptNo
         *  return:     List<Students>
         *  author:     叶锦
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
         *  author:     叶锦
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
         *  author:     叶锦
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
         *  author:     叶锦
         * */
        public List<Students> GetStudent(string studNo)
        {
            return (from stud in db.Students
                    where stud.studNo == studNo
                    select stud).ToList();
        }

        /*  summary:    重置某学院学生的密码
         *  param:      string deptNo
         *  return:     void
         *  author:     叶锦
         * */
        public void ResetDeptStudentPwd(string deptNo , string ResetPwd = "123456")
        {
            // 获取某学院所有学生
            List<Students> students = GetDeptStudent(deptNo);
            // 获取学生
            foreach(Students s in students)
            {
                s.studPwd = ResetPwd;
            }
            // 修改信息
            db.SubmitChanges();
        }

        /*  summary:    重置某专业学生的密码
         *  param:      string specNo
         *  return:     void
         *  author:     叶锦
         * */
        public void ResetSpecStudentPwd(string specNo, string ResetPwd = "123456")
        {
            // 获取某学院所有学生
            List<Students> students = GetSpecStudent(specNo);
            // 获取学生
            foreach (Students s in students)
            {
                s.studPwd = ResetPwd;
            }
            // 修改信息
            db.SubmitChanges();
        }

        /*  summary:    重置某班级学生的密码
         *  param:      string clasNo
         *  return:     void
         *  author:     叶锦
         * */
        public void ResetClasStudentPwd(string clasNo, string ResetPwd = "123456")
        {
            // 获取某学院所有学生
            List<Students> students = GetClasStudent(clasNo);
            // 获取学生
            foreach (Students s in students)
            {
                s.studPwd = ResetPwd;
            }
            // 修改信息
            db.SubmitChanges();
        }

        /*  summary:    重置某学生的密码
         *  param:      string studNo
         *  return:     void
         *  author:     叶锦
         * */
        public void ResetStudentPwd(string studNo, string ResetPwd = "123456")
        {
            // 获取某学院所有学生
            List<Students> students = GetStudent(studNo);
            // 获取学生
            foreach (Students s in students)
            {
                s.studPwd = ResetPwd;
            }
            // 修改信息
            db.SubmitChanges();
        }
    }

}

