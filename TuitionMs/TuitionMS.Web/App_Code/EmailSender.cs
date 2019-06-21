using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using System.Net.Mail;

/// <summary>
/// EmailSender 的摘要说明
/// </summary>
public class EmailSender
{
    //从Web.config中的<appSettings>配置节获取相应的键值
    private string mailFromAddress = ConfigurationManager.AppSettings["MailFromAddress"];
    private bool useSsl = bool.Parse(ConfigurationManager.AppSettings["UseSsl"]);
    private string userName = ConfigurationManager.AppSettings["UserName"];
    private string password = ConfigurationManager.AppSettings["Password"];
    private string serverName = ConfigurationManager.AppSettings["ServerName"];
    private int serverPort = int.Parse(ConfigurationManager.AppSettings["ServerPort"]);
    private string mailToAddress = "";  //收件人邮箱

    /*  summary:    构造函数
     *  param:      address(收件人邮箱) pwd(重置后的密码)
     *  return:     
     *  author:     叶锦
     * */
    public EmailSender(string address)
    {
        mailToAddress = address;
    }

    public EmailSender()
    {
    }

    /*  summary:    构造函数
     *  param:      address(收件人邮箱) pwd(重置后的密码)
     *  return:     
     *  author:     叶锦
     * */
    public void SetEmailSender(string address)
    {
        mailToAddress = address;
    }

    /*  summary:    自定义方法，根据设置的SMTP服务器名、端口号、账户名、授权码等信息发送给定发件人邮箱、收件人邮箱、Email主题、Email内容等信息的邮件
     *  param:      
     *  return:     
     *  author:     叶锦
     * */
    public void Send(string subject,string content)
    {
        //新建SmtpClient类实例smtpClient对象，using语句块结束时释放smtpClient对象
        using (var smtpClient = new SmtpClient())
        {
            //设置是否使用SSL协议连接
            smtpClient.EnableSsl = useSsl;
            //设置SMTP服务器名
            smtpClient.Host = serverName;
            //设置SMTP服务器的端口号
            smtpClient.Port = serverPort;
            //设置SMTP服务器发送邮件的凭据（用户名和授权码)
            smtpClient.Credentials = new NetworkCredential(userName, password);
            MailMessage mailMessage = new MailMessage(
                                   mailFromAddress,   // 发件人邮箱
                                   mailToAddress,     // 收件人邮箱
                                   subject,  // Email主题
                                   content);  // Email内容
            //调用smtpClient对象的Send()方法发送邮件
            smtpClient.Send(mailMessage);
        }
    }
}