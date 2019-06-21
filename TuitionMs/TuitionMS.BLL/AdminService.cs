using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuitionMS.DAL;

namespace TuitionMS.BLL
{
    public class AdminService
    {
        /*
         *4.1拥有对管理员的创建、删除、修改。
	     *4.2查看系统中的所有的建议
	     *4.3对不同的用户给予不同的系统通知  
         */

        // 创建Tuition.DAL数据访问层中的类的实例
        TuitionMSDataContext db = new TuitionMSDataContext();

        /*  summary:    检查教务人员输入的和密码是否正确 根据不同的返回值，前台可跳转至不同的位置
         *  param:      username(职工号) password(密码)
         *  return:     若用户名与密码正确，则返回 "adminType adminNo" 
         *  author:     myThis
         * */
        public string CheckLogin(string adminID, string password)
        {
            //  查询数据库中查询学号 注:由于Linq的查询结果是集合，故需使用FirstOrDefault获得对应的值
            Administrator administrator = (from a in db.Administrator
                                           where a.adminNo == adminID && a.adminPwd == password
                                           select a).FirstOrDefault();
            if (administrator != null)
            {
                return administrator.adminNo + ' ' + administrator.adminType;
            }
            else
            {
                return "";
            }
        }
        /************************4.1拥有对管理员的创建、删除、修改。*****************************/

        /* 添加管理员
        * 输入：管理员信息
        * 返回：true or false
        */
        public bool addAdministrator(string manaNo, string manaPwd, string manaType, string manaTel)
        {
            Administrator ad = (from c in db.Administrator
                                where c.adminNo == manaNo
                                select c).FirstOrDefault();
            if (ad == null)
            {
                Administrator mana = new Administrator();
                mana.adminNo = manaNo;
                mana.adminPwd = manaPwd;
                mana.adminTel = manaTel;
                mana.adminType = manaType;
                db.Administrator.InsertOnSubmit(mana);
                db.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        /*
         * 删除管理员
         * 输入：管理员号
         * 返回：true or false
         */
        public bool delAdministrator(string manaNo)
        {
            Administrator ad = (from c in db.Administrator
                                where c.adminNo == manaNo
                                select c).FirstOrDefault();
            if (ad != null)
            {
                db.Administrator.DeleteOnSubmit(ad);
                db.SubmitChanges();
                return true;
            }
            else
                return false;
        }
        /*
         *修改管理员信息
         * 输入：管理员号
         * 返回：true or false
         */
        public bool updateAdministrator(string manaNo, string manaPwd, string manaType, string manaTel)
        {
            Administrator ad = (from c in db.Administrator
                                where c.adminNo == manaNo
                                select c).FirstOrDefault();
            if (ad != null)
            {
                ad.adminPwd = manaPwd;
                ad.adminType = manaType;
                ad.adminTel = manaTel;
                db.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /***********************4.2查看系统中的所有的建议***************************/
        //控件绑定显示数据即可
        /*
        public string showPorpose(int id)
        {
            Propose ad = (from c in db.Propose
                          where c.id == id
                          select c).FirstOrDefault();
            if (ad != null)
            {
                return ad.id + "," + ad.propObject + "," + ad.propContent + "," + ad.propTime.ToString();
            }
            else
            {
                return "";
            }
        }
        */

        /***************************4.3对不同的用户给予不同的系统通知*************************/
        /*  summary:    获得学生的反馈信息
         *  param:      
         *  return:     object
         *  author:     myThis
         * */
        public object GetFbByStud()
        {
            return from p in db.Propose
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
