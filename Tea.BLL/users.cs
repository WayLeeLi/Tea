using System;
using System.Data;
using System.Collections.Generic;
using Tea.Common;

namespace Tea.BLL
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public partial class users
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.users dal;
        public users()
        {
            dal = new DAL.users(siteConfig.sysdatabaseprefix);
        }

        #region 基本方法===================================
        public void JiSuan(int id)
        {
            //if (id > 0)
            //{
            //    Model.users model = GetModel(id);
            //    if (model != null)
            //    {
            //        try
            //        {
            //            //購物金
            //            int all =Utils.StrToInt(Tea.DBUtility.DbHelperSQL.Query("select sum(value) as c from shop_user_point_log where user_id=" + id + " and islock=1").Tables[0].Rows[0][0].ToString(),0);
            //            int yong = Utils.StrToInt(Tea.DBUtility.DbHelperSQL.Query("select sum(value) as c from shop_user_point_log where user_id=" + id + " and islock=4").Tables[0].Rows[0][0].ToString(), 0);
            //            int ti = Utils.StrToInt(Tea.DBUtility.DbHelperSQL.Query("select sum(value) as c from shop_user_point_log where user_id=" + id + " and islock=3").Tables[0].Rows[0][0].ToString(), 0);
            //            int qu = Utils.StrToInt(Tea.DBUtility.DbHelperSQL.Query("select sum(value) as c from shop_user_point_log where user_id=" + id + " and islock=2").Tables[0].Rows[0][0].ToString(), 0);

            //            model.point = all - yong - ti - qu;
            //        }
            //        catch (Exception eee) { }
            //        try
            //        {
 
            //            //累計已提現購物金
            //            model.amount = Utils.StrToInt(Tea.DBUtility.DbHelperSQL.Query("select sum(value) as c from shop_user_point_log where user_id=" + id + " and islock=3").Tables[0].Rows[0][0].ToString(), 0); ;
            //        }
            //        catch (Exception eee) { }
            //        try
            //        {
            //            //累計購物金額
            //            model.exp = Utils.StrToInt(Tea.DBUtility.DbHelperSQL.Query("select sum(value) as c from shop_user_point_log where user_id=" + id + " and islock=1").Tables[0].Rows[0][0].ToString(), 0);
            //        }
            //        catch (Exception eee) { }
            //        try
            //        {
                        
            //            //完成訂單數
            //            model.user_hei = Utils.StrToInt(Tea.DBUtility.DbHelperSQL.Query("select count(id) as c from shop_orders where user_id=" + id + " and status=3").Tables[0].Rows[0][0].ToString(), 0);
            //        }
            //        catch (Exception eee) { }

            //        try
            //        {
            //            //取消單數
            //            int quxiao = Utils.StrToInt(Tea.DBUtility.DbHelperSQL.Query("select count(id) as c from shop_orders where user_id=" + id + " and status=4").Tables[0].Rows[0][0].ToString(), 0);
            //            UpdateField(id, "quxiao=" + quxiao + "");
                       
            //        }
            //        catch (Exception eee) { }

            //        try
            //        {
            //            //退款單數

            //            int tui = Utils.StrToInt(Tea.DBUtility.DbHelperSQL.Query("select count(id) as c from shop_order_tui where user_id=" + id + " and status=3").Tables[0].Rows[0][0].ToString(), 0);
            //            UpdateField(id, "tui="+tui+"");
                       
            //        }
            //        catch (Exception eee) { }
                    
            //    }
            //    Update(model);
            //}
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public string getTitle(string id)
        {
            return dal.getTitle(id);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        public bool Exists(string user_name)
        {
            return dal.Exists(user_name);
        }

        /// <summary>
        /// 检查同一IP注册间隔(小时)内是否存在
        /// </summary>
        public bool Exists(string reg_ip, int regctrl)
        {
            return dal.Exists(reg_ip, regctrl);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.users model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.users model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.users GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        /// <param name="user_name">用户名(明文)</param>
        /// <param name="password">密码</param>
        /// <param name="emaillogin">是否允许邮箱做为登录</param>
        /// <param name="mobilelogin">是否允许手机做为登录</param>
        /// <param name="is_encrypt">是否需要加密密码</param>
        /// <returns></returns>
        public Model.users GetModel(string user_name, string password, int emaillogin, int mobilelogin, bool is_encrypt)
        {
            //检查一下是否需要加密
            if (is_encrypt)
            {
                //先取得该用户的随机密钥
                string salt = dal.GetSalt(user_name);
                if (string.IsNullOrEmpty(salt))
                {
                    return null;
                }
                //把明文进行加密重新赋值
                password = DESEncrypt.Encrypt(password, salt);
            }
            return dal.GetModel(user_name, password, emaillogin, mobilelogin);
        }

        /// <summary>
        /// 根据用户名返回一个实体
        /// </summary>
        public Model.users GetModel(string user_name)
        {
            return dal.GetModel(user_name);
        }
          /// <summary>
        /// 根据用户名返回一个实体
        /// </summary>
        public Model.users GetEModel(string user_name)
        {
            return dal.GetEModel(user_name);
        }
        
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        #endregion

        #region 扩展方法===================================
        /// <summary>
        /// 检查Email是否存在
        /// </summary>
        public bool ExistsEmail(string email)
        {
            return dal.ExistsEmail(email);
        }

        /// <summary>
        /// 检查手机号码是否存在
        /// </summary>
        public bool ExistsMobile(string mobile)
        {
            return dal.ExistsMobile(mobile);
        }

        /// <summary>
        /// 返回一个随机用户名
        /// </summary>
        public string GetRandomName(int length)
        {
            string temp = Utils.Number(length, true);
            if (Exists(temp))
            {
                return GetRandomName(length);
            }
            return temp;
        }

        /// <summary>
        /// 根据用户名取得Salt
        /// </summary>
        public string GetSalt(string user_name)
        {
            return dal.GetSalt(user_name);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public int UpdateField(int id, string strValue)
        {
            return dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 用户升级
        /// </summary>
        public bool Upgrade(int id)
        {
            if (!Exists(id))
            {
                return false;
            }
            Model.users model = GetModel(id);
            Model.user_groups groupModel = new user_groups().GetUpgrade(model.group_id, model.exp);
            if (groupModel == null)
            {
                return false;
            }
            int result = UpdateField(id, "group_id=" + groupModel.id);
            if (result > 0)
            {
                ////增加积分
                //if (groupModel.point > 0)
                //{
                //    new BLL.user_point_log().Add(model.id, model.user_name, groupModel.point, "升级获得积分", true,0,0);
                //}
                ////增加金额
                //if (groupModel.amount > 0)
                //{
                //    new BLL.user_amount_log().Add(model.id, model.user_name, groupModel.amount, "升级赠送金额");
                //}
            }
            return true;
        }
        #endregion
        
    }
}