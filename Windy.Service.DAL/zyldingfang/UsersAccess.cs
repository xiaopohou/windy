
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Windy.Common.Libraries;
 
 
using Windy.Service.DAL;

namespace Windy.Service.DAL.zyldingfang
{
    public class UsersAccess : DBAccessBase
    {
        private static UsersAccess m_Instance = null;
        /// <summary>
        /// 单实例
        /// </summary>
        public static UsersAccess Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new UsersAccess();
                return m_Instance;
            }
        }

        public short Add(Users users)
        {
            string szFiled = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18}"
                    , ServerData.UserTable.Name
                    , ServerData.UserTable.School
                    , ServerData.UserTable.ExamSchool
                    , ServerData.UserTable.Sequence
                    , ServerData.UserTable.Tel
                    , ServerData.UserTable.Baks
                    , ServerData.UserTable.PassWord
                    , ServerData.UserTable.PayMoney
                    , ServerData.UserTable.ExamPlace
                    , ServerData.UserTable.Room
                    , ServerData.UserTable.Hotel
                    , ServerData.UserTable.HotelExpense
                    , ServerData.UserTable.MoneyBack
                    , ServerData.UserTable.EmployeeID
                    , ServerData.UserTable.Gender
                    , ServerData.UserTable.Template
                    , ServerData.UserTable.PayPlace
                    , ServerData.UserTable.ExceptRoomie
                    , ServerData.UserTable.Status);
            string szValue = string.Format("'{0}','{1}','{2}',{3},'{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',{13},'{14}','{15}','{16}','{17}','{18}'"
                , users.Name
                , users.School
                , users.ExamSchool
                , users.Sequence
                , users.Tel
                , users.Bak
                , users.PassWord
                , users.PayMoney
                , users.ExamPlace
                , users.Room
                , users.Hotel
                , users.HotelExpense
                , users.MoneyBack
                , users.EmployeeID
                , users.Gender
                , users.Template
                , users.PayPlace
                , users.ExceptRoomie
                , users.Status);
            string szSql = string.Format("Insert Into {0}({1}) values({2})"
                , ServerData.DataTable.Users
                , szFiled
                , szValue);
            try
            {
                base.DataAccess.ExecuteNonQuery(szSql, CommandType.Text);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSql, "SQL语句执行失败!");
            }
            return ExecuteResult.OK;
        }

        /// <summary>
        /// 更新考点
        /// </summary>
        /// <param name="employee">考点对</param>
        /// <returns>ExecuteResult</returns>
        public short Update(Users item)
        {
            if (item == null)
                return ExecuteResult.PARAM_ERROR;

            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;

            string szFields = string.Format("{0}='{1}',{2}='{3}',{4}='{5}',{6}={7},{8}='{9}',{10}='{11}',{12}='{13}',{14}='{15}',{16}='{17}',{18}='{19}',{20}='{21}',{22}='{23}',{24}={25},{26}='{27}',{28}='{29}',{30}='{31}',{32}='{33}',{34}='{35}',{36}='{37}'"
                 , ServerData.UserTable.Name, item.Name
                 , ServerData.UserTable.School, item.School
                 , ServerData.UserTable.ExamSchool, item.ExamSchool
                 , ServerData.UserTable.Sequence, item.Sequence
                 , ServerData.UserTable.Tel, item.Tel
                 , ServerData.UserTable.Baks, item.Bak
                 , ServerData.UserTable.PayMoney, item.PayMoney
                 , ServerData.UserTable.ExamPlace, item.ExamPlace
                 , ServerData.UserTable.Room, item.Room
                 , ServerData.UserTable.Hotel, item.Hotel
                 , ServerData.UserTable.HotelExpense, item.HotelExpense
                 , ServerData.UserTable.MoneyBack, item.MoneyBack
                 , ServerData.UserTable.EmployeeID, item.EmployeeID
                 , ServerData.UserTable.Gender, item.Gender
                 , ServerData.UserTable.Template, item.Template
                 , ServerData.UserTable.PayPlace, item.PayPlace
                 , ServerData.UserTable.ExceptRoomie, item.ExceptRoomie
                 , ServerData.UserTable.Status, item.Status
                 , ServerData.UserTable.PassWord, item.PassWord
                 );
            string szCondition = string.Format("{0}={1}"
                , ServerData.UserTable.ID, item.ID
                );
            string szSQL = string.Format(ServerData.SQL.UPDATE, ServerData.DataTable.Users, szFields, szCondition);

            int nCount = 0;
            try
            {
                nCount = base.DataAccess.ExecuteNonQuery(szSQL, CommandType.Text);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "SQL语句执行失败!");
            }
            if (nCount <= 0)
            {
                LogManager.Instance.WriteLog("NurShiftAccess.UpdateShiftWardStatus", new string[] { "szSQL" }, new object[] { szSQL }, "SQL语句执行后返回0!");
                return ExecuteResult.ACCESS_ERROR;
            }
            return ExecuteResult.OK;
        }
        /// <summary>
        /// 更新考点
        /// </summary>
        /// <param name="employee">考点对</param>
        /// <returns>ExecuteResult</returns>
        public short UpdateByTel(Users item)
        {
            if (item == null)
                return ExecuteResult.PARAM_ERROR;

            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;

            string szFields = string.Format("{0}='{1}',{2}='{3}',{4}='{5}',{6}={7},{8}='{9}',{10}='{11}',{12}='{13}',{14}='{15}',{16}='{17}',{18}='{19}',{20}='{21}',{22}='{23}',{24}={25},{26}='{27}',{28}='{29}',{30}='{31}',{32}='{33}',{34}='{35}'"
                 , ServerData.UserTable.Name, item.Name
                 , ServerData.UserTable.School, item.School
                 , ServerData.UserTable.ExamSchool, item.ExamSchool
                 , ServerData.UserTable.Sequence, item.Sequence
                 , ServerData.UserTable.Tel, item.Tel
                 , ServerData.UserTable.Baks, item.Bak
                 , ServerData.UserTable.PayMoney, item.PayMoney
                 , ServerData.UserTable.ExamPlace, item.ExamPlace
                 , ServerData.UserTable.Room, item.Room
                 , ServerData.UserTable.Hotel, item.Hotel
                 , ServerData.UserTable.HotelExpense, item.HotelExpense
                 , ServerData.UserTable.MoneyBack, item.MoneyBack
                 , ServerData.UserTable.EmployeeID, item.EmployeeID
                 , ServerData.UserTable.Gender, item.Gender
                 , ServerData.UserTable.Template, item.Template
                 , ServerData.UserTable.PayPlace, item.PayPlace
                 , ServerData.UserTable.ExceptRoomie, item.ExceptRoomie
                 , ServerData.UserTable.Status, item.Status
                 );
            string szCondition = string.Format("{0}='{1}'"
                , ServerData.UserTable.Tel, item.Tel
                );
            string szSQL = string.Format(ServerData.SQL.UPDATE, ServerData.DataTable.Users, szFields, szCondition);

            int nCount = 0;
            try
            {
                nCount = base.DataAccess.ExecuteNonQuery(szSQL, CommandType.Text);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "SQL语句执行失败!");
            }
            if (nCount <= 0)
            {
                LogManager.Instance.WriteLog("NurShiftAccess.UpdateShiftWardStatus", new string[] { "szSQL" }, new object[] { szSQL }, "SQL语句执行后返回0!");
                return ExecuteResult.ACCESS_ERROR;
            }
            return ExecuteResult.OK;
        }
        /// <summary>
        /// 删除新闻资讯
        /// </summary>
        /// <param name="szGroupName">配置组</param>
        /// <param name="szConfigName">配置项</param>
        /// <returns>ExecuteResult</returns>
        public short Delete(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return ExecuteResult.PARAM_ERROR;

            string szCondition = string.Format("{0} in ({1})"
                , ServerData.UserTable.ID, ID);

            string szSQL = string.Format(ServerData.SQL.DELETE, ServerData.DataTable.Users, szCondition);

            int count = 0;
            try
            {
                count = base.DataAccess.ExecuteNonQuery(szSQL, CommandType.Text);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "SQL语句执行失败!");
            }
            return count > 0 ? ExecuteResult.OK : ExecuteResult.RES_NO_FOUND;
        }
        /// <summary>
        /// 根据主键查找行
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public short GetUsersByID(string ID, ref Users item)
        {
            if (string.IsNullOrEmpty(ID))
                return ExecuteResult.PARAM_ERROR;
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19}"
               , ServerData.UserTable.ID
               , ServerData.UserTable.Name
              
               , ServerData.UserTable.ExamSchool
               , ServerData.UserTable.Sequence
               , ServerData.UserTable.Tel
               , ServerData.UserTable.Baks
               , ServerData.UserTable.PassWord
               , ServerData.UserTable.PayMoney
               , ServerData.UserTable.ExamPlace
               , ServerData.UserTable.Room
               , ServerData.UserTable.Hotel
               , ServerData.UserTable.HotelExpense
               , ServerData.UserTable.MoneyBack
               , ServerData.UserTable.EmployeeID
               , ServerData.UserTable.Gender
               , ServerData.UserTable.Template
               , ServerData.UserTable.PayPlace
               , ServerData.UserTable.ExceptRoomie
               , ServerData.UserTable.Status
               , ServerData.UserTable.School);
            string szCondition = string.Format("1=1");
            szCondition = string.Format("{0} = {1}"
                    , ServerData.UserTable.ID, ID);
            string szTable = ServerData.DataTable.Users;
            string szSQL = string.Format(ServerData.SQL.SELECT_WHERE, szField, szTable, szCondition);

            IDataReader dataReader = null;
            try
            {
                dataReader = base.DataAccess.ExecuteReader(szSQL, CommandType.Text);
                if (dataReader == null || dataReader.IsClosed || !dataReader.Read())
                {
                    LogManager.Instance.WriteLog("DocumentAccess.GetDocInfoBySetID", new string[] { "szSQL" }, new object[] { szSQL }, "未查询到记录");
                    return ExecuteResult.RES_NO_FOUND;
                }

                if (item == null)
                    item = new Users();
                item.ID = int.Parse(dataReader.GetValue(0).ToString());
                item.Name = dataReader.IsDBNull(1) ? "" : dataReader.GetString(1);
                item.ExamSchool = dataReader.IsDBNull(2) ? "" : dataReader.GetString(2);
                item.Sequence = dataReader.IsDBNull(3) ? "" : dataReader.GetValue(3).ToString();
                item.Tel = dataReader.IsDBNull(4) ? "" : dataReader.GetValue(4).ToString();
                item.Bak = dataReader.IsDBNull(5) ? "" : dataReader.GetString(5);
                item.PassWord = dataReader.IsDBNull(6) ? "" : dataReader.GetString(6);
                item.PayMoney = dataReader.IsDBNull(7) ? "" : dataReader.GetString(7);
                item.ExamPlace = dataReader.IsDBNull(8) ? "" : dataReader.GetString(8);
                item.Room = dataReader.IsDBNull(9) ? "" : dataReader.GetString(9);
                item.Hotel = dataReader.IsDBNull(10) ? "" : dataReader.GetString(10);
                item.HotelExpense = dataReader.IsDBNull(11) ? "" : dataReader.GetString(11);
                item.MoneyBack = dataReader.IsDBNull(12) ? "" : dataReader.GetString(12);
                item.EmployeeID = dataReader.IsDBNull(13) ? "" : dataReader.GetValue(13).ToString();
                item.Gender = dataReader.IsDBNull(14) ? "" : dataReader.GetString(14);
                item.Template = dataReader.IsDBNull(15) ? "" : dataReader.GetString(15);
                item.PayPlace = dataReader.IsDBNull(16) ? "" : dataReader.GetString(16);
                item.ExceptRoomie = dataReader.IsDBNull(17) ? "" : dataReader.GetString(17);
                item.Status = dataReader.IsDBNull(18) ? "" : dataReader.GetString(18);
                item.School = dataReader.IsDBNull(19) ? "" : dataReader.GetString(19);
                return ExecuteResult.OK;
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "SQL语句执行失败!");
            }
            finally { base.DataAccess.CloseConnnection(false); }
        }
        /// <summary>
        /// 根据主键查找行
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public short GetUsersByTel(string Tel, ref Users item)
        {
            if (string.IsNullOrEmpty(Tel))
                return ExecuteResult.PARAM_ERROR;
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19}"
               , ServerData.UserTable.ID
               , ServerData.UserTable.Name

               , ServerData.UserTable.ExamSchool
               , ServerData.UserTable.Sequence
               , ServerData.UserTable.Tel
               , ServerData.UserTable.Baks
               , ServerData.UserTable.PassWord
               , ServerData.UserTable.PayMoney
               , ServerData.UserTable.ExamPlace
               , ServerData.UserTable.Room
               , ServerData.UserTable.Hotel
               , ServerData.UserTable.HotelExpense
               , ServerData.UserTable.MoneyBack
               , ServerData.UserTable.EmployeeID
               , ServerData.UserTable.Gender
               , ServerData.UserTable.Template
               , ServerData.UserTable.PayPlace
               , ServerData.UserTable.ExceptRoomie
               , ServerData.UserTable.Status
               , ServerData.UserTable.School);
            string szCondition = string.Format("1=1");
            szCondition = string.Format("{0} = '{1}'"
                    , ServerData.UserTable.Tel, Tel);
            string szTable = ServerData.DataTable.Users;
            string szSQL = string.Format(ServerData.SQL.SELECT_WHERE, szField, szTable, szCondition);

            IDataReader dataReader = null;
            try
            {
                dataReader = base.DataAccess.ExecuteReader(szSQL, CommandType.Text);
                if (dataReader == null || dataReader.IsClosed || !dataReader.Read())
                {
                    LogManager.Instance.WriteLog("DocumentAccess.GetDocInfoBySetID", new string[] { "szSQL" }, new object[] { szSQL }, "未查询到记录");
                    return ExecuteResult.RES_NO_FOUND;
                }

                if (item == null)
                    item = new Users();
                item.ID = int.Parse(dataReader.GetValue(0).ToString());
                item.Name = dataReader.IsDBNull(1) ? "" : dataReader.GetString(1);
                item.ExamSchool = dataReader.IsDBNull(2) ? "" : dataReader.GetString(2);
                item.Sequence = dataReader.IsDBNull(3) ? "" : dataReader.GetValue(3).ToString();
                item.Tel = dataReader.IsDBNull(4) ? "" : dataReader.GetValue(4).ToString();
                item.Bak = dataReader.IsDBNull(5) ? "" : dataReader.GetString(5);
                item.PassWord = dataReader.IsDBNull(6) ? "" : dataReader.GetString(6);
                item.PayMoney = dataReader.IsDBNull(7) ? "" : dataReader.GetString(7);
                item.ExamPlace = dataReader.IsDBNull(8) ? "" : dataReader.GetString(8);
                item.Room = dataReader.IsDBNull(9) ? "" : dataReader.GetString(9);
                item.Hotel = dataReader.IsDBNull(10) ? "" : dataReader.GetString(10);
                item.HotelExpense = dataReader.IsDBNull(11) ? "" : dataReader.GetString(11);
                item.MoneyBack = dataReader.IsDBNull(12) ? "" : dataReader.GetString(12);
                item.EmployeeID = dataReader.IsDBNull(13) ? "" : dataReader.GetValue(13).ToString();
                item.Gender = dataReader.IsDBNull(14) ? "" : dataReader.GetString(14);
                item.Template = dataReader.IsDBNull(15) ? "" : dataReader.GetString(15);
                item.PayPlace = dataReader.IsDBNull(16) ? "" : dataReader.GetString(16);
                item.ExceptRoomie = dataReader.IsDBNull(17) ? "" : dataReader.GetString(17);
                item.Status = dataReader.IsDBNull(18) ? "" : dataReader.GetString(18);
                item.School = dataReader.IsDBNull(19) ? "" : dataReader.GetString(19);
                return ExecuteResult.OK;
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "SQL语句执行失败!");
            }
            finally { base.DataAccess.CloseConnnection(false); }
        }
        /// <summary>
        /// 获取新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetUsersList(string keyWords,string EmployeeIDs, ref List<Users> lstUsers)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19}"
                 , ServerData.UserTable.ID
                 , ServerData.UserTable.Name
                 , ServerData.UserTable.School
                 , ServerData.UserTable.ExamSchool
                 , ServerData.UserTable.Sequence
                 , ServerData.UserTable.Tel
                 , ServerData.UserTable.Baks
                 , ServerData.UserTable.PassWord
                 , ServerData.UserTable.PayMoney
                 , ServerData.UserTable.ExamPlace
                 , ServerData.UserTable.Room
                 , ServerData.UserTable.Hotel
                 , ServerData.UserTable.HotelExpense
                 , ServerData.UserTable.MoneyBack
                 , ServerData.UserTable.EmployeeID
                 , ServerData.UserTable.Gender
                 , ServerData.UserTable.Template
                 , ServerData.UserTable.PayPlace
                 , ServerData.UserTable.ExceptRoomie
                 , ServerData.UserTable.Status);
            string szCondition = string.Format("1=1");
            if (!string.IsNullOrEmpty(keyWords))
                szCondition = string.Format("{0} and {1} like '%{2}%'"
                    , szCondition
                    , ServerData.UserTable.Name, keyWords);
            if (!string.IsNullOrEmpty(EmployeeIDs))
                szCondition = string.Format("{0} and {1} in ({2})"
                    , szCondition
                    , ServerData.UserTable.EmployeeID, EmployeeIDs);
            string szTable = ServerData.DataTable.Users;
            string szSQL = string.Format(ServerData.SQL.SELECT_WHERE, szField, szTable, szCondition);

            IDataReader dataReader = null;
            try
            {
                dataReader = base.DataAccess.ExecuteReader(szSQL, CommandType.Text);
                if (dataReader == null || dataReader.IsClosed || !dataReader.Read())
                {
                    LogManager.Instance.WriteLog("DocumentAccess.GetDocInfoBySetID", new string[] { "szSQL" }, new object[] { szSQL }, "未查询到记录");
                    return ExecuteResult.RES_NO_FOUND;
                }
                if (lstUsers == null)
                    lstUsers = new List<Users>();
                do
                {
                    Users item = new Users();
                    item.ID = int.Parse(dataReader.GetValue(0).ToString());
                    item.Name = dataReader.IsDBNull(1) ? "" : dataReader.GetString(1);
                    item.School = dataReader.IsDBNull(2) ? "" : dataReader.GetString(2);
                    item.ExamSchool = dataReader.IsDBNull(3) ? "" : dataReader.GetString(3);
                    item.Sequence = dataReader.IsDBNull(4) ? "" : dataReader.GetValue(4).ToString();
                    item.Tel = dataReader.IsDBNull(5) ? "" : dataReader.GetString(5).ToString();
                    item.Bak = dataReader.IsDBNull(6) ? "" : dataReader.GetString(6);
                    item.PassWord = dataReader.IsDBNull(7) ? "" : dataReader.GetString(7);
                    item.PayMoney = dataReader.IsDBNull(8) ? "" : dataReader.GetString(8);
                    item.ExamPlace = dataReader.IsDBNull(9) ? "" : dataReader.GetString(9);
                    item.Room = dataReader.IsDBNull(10) ? "" : dataReader.GetString(10);
                    item.Hotel = dataReader.IsDBNull(11) ? "" : dataReader.GetString(11);
                    item.HotelExpense = dataReader.IsDBNull(12) ? "" : dataReader.GetString(12);
                    item.MoneyBack = dataReader.IsDBNull(13) ? "" : dataReader.GetString(13);
                    item.EmployeeID = dataReader.IsDBNull(14) ? "" : dataReader.GetValue(14).ToString();
                    item.Gender = dataReader.IsDBNull(15) ? "" : dataReader.GetString(15).ToString();
                    item.Template = dataReader.IsDBNull(16) ? "" : dataReader.GetString(16);
                    item.PayPlace = dataReader.IsDBNull(17) ? "" : dataReader.GetString(17);
                    item.ExceptRoomie = dataReader.IsDBNull(18) ? "" : dataReader.GetString(18);
                    item.Status = dataReader.IsDBNull(19) ? "" : dataReader.GetString(19);
                    
                    lstUsers.Add(item);
                } while (dataReader.Read());
                return ExecuteResult.OK;
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "SQL语句执行失败!");
            }
            finally { base.DataAccess.CloseConnnection(false); }
        }
        /// <summary>
        /// 获取新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetUsersListByRoom(string Room,string Hotel,  ref List<Users> lstUsers)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19}"
                 , ServerData.UserTable.ID
                 , ServerData.UserTable.Name
                 , ServerData.UserTable.School
                 , ServerData.UserTable.ExamSchool
                 , ServerData.UserTable.Sequence
                 , ServerData.UserTable.Tel
                 , ServerData.UserTable.Baks
                 , ServerData.UserTable.PassWord
                 , ServerData.UserTable.PayMoney
                 , ServerData.UserTable.ExamPlace
                 , ServerData.UserTable.Room
                 , ServerData.UserTable.Hotel
                 , ServerData.UserTable.HotelExpense
                 , ServerData.UserTable.MoneyBack
                 , ServerData.UserTable.EmployeeID
                 , ServerData.UserTable.Gender
                 , ServerData.UserTable.Template
                 , ServerData.UserTable.PayPlace
                 , ServerData.UserTable.ExceptRoomie
                 , ServerData.UserTable.Status);
            string szCondition = string.Format("1=1");
           
                szCondition = string.Format("{0} and {1} = '{2}' and {3}='{4}'"
                    , szCondition
                    , ServerData.UserTable.Room, Room
                    , ServerData.UserTable.Hotel,Hotel);
            
            string szTable = ServerData.DataTable.Users;
            string szSQL = string.Format(ServerData.SQL.SELECT_WHERE, szField, szTable, szCondition);

            IDataReader dataReader = null;
            try
            {
                dataReader = base.DataAccess.ExecuteReader(szSQL, CommandType.Text);
                if (dataReader == null || dataReader.IsClosed || !dataReader.Read())
                {
                    LogManager.Instance.WriteLog("DocumentAccess.GetDocInfoBySetID", new string[] { "szSQL" }, new object[] { szSQL }, "未查询到记录");
                    return ExecuteResult.RES_NO_FOUND;
                }
                if (lstUsers == null)
                    lstUsers = new List<Users>();
                do
                {
                    Users item = new Users();
                    item.ID = int.Parse(dataReader.GetValue(0).ToString());
                    item.Name = dataReader.IsDBNull(1) ? "" : dataReader.GetString(1);
                    item.School = dataReader.IsDBNull(2) ? "" : dataReader.GetString(2);
                    item.ExamSchool = dataReader.IsDBNull(3) ? "" : dataReader.GetString(3);
                    item.Sequence = dataReader.IsDBNull(4) ? "" : dataReader.GetValue(4).ToString();
                    item.Tel = dataReader.IsDBNull(5) ? "" : dataReader.GetString(5).ToString();
                    item.Bak = dataReader.IsDBNull(6) ? "" : dataReader.GetString(6);
                    item.PassWord = dataReader.IsDBNull(7) ? "" : dataReader.GetString(7);
                    item.PayMoney = dataReader.IsDBNull(8) ? "" : dataReader.GetString(8);
                    item.ExamPlace = dataReader.IsDBNull(9) ? "" : dataReader.GetString(9);
                    item.Room = dataReader.IsDBNull(10) ? "" : dataReader.GetString(10);
                    item.Hotel = dataReader.IsDBNull(11) ? "" : dataReader.GetString(11);
                    item.HotelExpense = dataReader.IsDBNull(12) ? "" : dataReader.GetString(12);
                    item.MoneyBack = dataReader.IsDBNull(13) ? "" : dataReader.GetString(13);
                    item.EmployeeID = dataReader.IsDBNull(14) ? "" : dataReader.GetValue(14).ToString();
                    item.Gender = dataReader.IsDBNull(15) ? "" : dataReader.GetString(15).ToString();
                    item.Template = dataReader.IsDBNull(16) ? "" : dataReader.GetString(16);
                    item.PayPlace = dataReader.IsDBNull(17) ? "" : dataReader.GetString(17);
                    item.ExceptRoomie = dataReader.IsDBNull(18) ? "" : dataReader.GetString(18);
                    item.Status = dataReader.IsDBNull(19) ? "" : dataReader.GetString(19);

                    lstUsers.Add(item);
                } while (dataReader.Read());
                return ExecuteResult.OK;
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "SQL语句执行失败!");
            }
            finally { base.DataAccess.CloseConnnection(false); }
        }
        /// <summary>
        /// 获取组织级别
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetUsersPageList(int pageSize, int pageIndex, string Name, string EmployeeID, string Tel, ref List<Users> lstUsers)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            if (lstUsers == null)
                lstUsers = new List<Users>();
            string szTableName = ServerData.DataTable.Users;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19}"
                 , ServerData.UserTable.ID
                 , ServerData.UserTable.Name
                 , ServerData.UserTable.School
                 , ServerData.UserTable.ExamSchool
                 , ServerData.UserTable.Sequence
                 , ServerData.UserTable.Tel
                 , ServerData.UserTable.Baks
                 , ServerData.UserTable.PassWord
                 , ServerData.UserTable.PayMoney
                 , ServerData.UserTable.ExamPlace
                 , ServerData.UserTable.Room
                 , ServerData.UserTable.Hotel
                 , ServerData.UserTable.HotelExpense
                 , ServerData.UserTable.MoneyBack
                 , ServerData.UserTable.EmployeeID
                 , ServerData.UserTable.Gender
                 , ServerData.UserTable.Template
                 , ServerData.UserTable.PayPlace
                 , ServerData.UserTable.ExceptRoomie
                 , ServerData.UserTable.Status);
         
            string strWhere = "1=1";
            if (!String.IsNullOrEmpty(Name))
                strWhere = string.Format("{0} and {1} like '%{2}%'"
                    , strWhere
                    , ServerData.UserTable.Name, Name);
            if (!String.IsNullOrEmpty(EmployeeID))
                strWhere = string.Format("{0} and {1} in ({2})"
                    , strWhere
                    , ServerData.UserTable.EmployeeID, EmployeeID);
            if (!string.IsNullOrEmpty(Tel))
                strWhere = string.Format("{0} and {1} like '%{2}%'"
                    , strWhere
                    , ServerData.UserTable.Tel, Tel);
            string szSQL = string.Format("select {0} from {1} where {2} order by EmployeeID,Sequences"
               , szField
               , ServerData.DataTable.Users
               , strWhere);
            DataSet result = new DataSet();
            short shRet = CommonAccess.Instance.ExecuteQuery(szSQL,out result);
            if (shRet == ExecuteResult.OK)
            {
                try
                {
                    for (int index = pageSize*(pageIndex-1); index < result.Tables[0].Rows.Count; index++)
                    {
                        if (index >= (pageSize * pageIndex))
                            break;
                        Users item = new Users();
                        item.ID = int.Parse(result.Tables[0].Rows[index][0].ToString());
                        item.Name = result.Tables[0].Rows[index][1].ToString();
                        item.School = result.Tables[0].Rows[index][2].ToString();
                        item.ExamSchool = result.Tables[0].Rows[index][3].ToString();
                        item.Sequence = result.Tables[0].Rows[index][4].ToString();
                        item.Tel = result.Tables[0].Rows[index][5].ToString();
                        item.Bak = result.Tables[0].Rows[index][6].ToString();
                        item.PassWord = result.Tables[0].Rows[index][7].ToString();
                        item.PayMoney = result.Tables[0].Rows[index][8].ToString();
                        item.ExamPlace = result.Tables[0].Rows[index][9].ToString();
                        item.Room = result.Tables[0].Rows[index][10].ToString();
                        item.Hotel = result.Tables[0].Rows[index][11].ToString();
                        item.HotelExpense = result.Tables[0].Rows[index][12].ToString();
                        item.MoneyBack = result.Tables[0].Rows[index][13].ToString();
                        item.EmployeeID = result.Tables[0].Rows[index][14].ToString();
                        item.Gender = result.Tables[0].Rows[index][15].ToString();
                        item.Template = result.Tables[0].Rows[index][16].ToString();
                        item.PayPlace = result.Tables[0].Rows[index][17].ToString();
                        item.ExceptRoomie = result.Tables[0].Rows[index][18].ToString();

                        item.Status = result.Tables[0].Rows[index][19].ToString();
                        lstUsers.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "获取分页新闻资讯集合失败");
                }
            }
            return shRet;

        }
        /// <summary>
        /// 获取分页新闻资讯集合
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public short GetUsersTotalCount(string Name, string EmployeeID, string Tel, ref int TotalCount)
        {
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szTableName = ServerData.DataTable.Users;
            string strWhere = "1=1";
            if (!String.IsNullOrEmpty(Name))
                strWhere = string.Format("{0} and {1} like '%{2}%'"
                    , strWhere
                    , ServerData.UserTable.Name, Name);
            if (!String.IsNullOrEmpty(EmployeeID))
                strWhere = string.Format("{0} and {1}  in ({2})"
                    , strWhere
                    , ServerData.UserTable.EmployeeID, EmployeeID);
            if (!String.IsNullOrEmpty(Tel))
                strWhere = string.Format("{0} and {1} like '%{2}%'"
                    , strWhere
                    , ServerData.UserTable.Tel, Tel);
            DataSet result = new DataSet();
            short shRet = base.GetPageList(szTableName, null, null, 0, 0, true, null, strWhere, ref  result);
            if (shRet == ExecuteResult.OK)
            {
                TotalCount = int.Parse(result.Tables[0].Rows[0][0].ToString());
            }
            return shRet;

        }

        /// <summary>
        /// 执行指定的SQL语句查询
        /// </summary>
        /// <param name="sql">查询的SQL语句</param>
        /// <param name="result">查询返回的结果集</param>
        /// <returns>ExecuteResult</returns>
        public short ExecuteQuery(string sql, out DataSet result)
        {
            result = null;
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;

            try
            {
                result = base.DataAccess.ExecuteDataSet(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), sql, "SQL语句执行失败!");
            }
            return ExecuteResult.OK;
        }

        public short Exists(string szTel, string strPassWord,ref Users users)
        {
            if (string.IsNullOrEmpty(szTel) || string.IsNullOrEmpty(strPassWord))
                return ExecuteResult.PARAM_ERROR;
            if (base.DataAccess == null)
                return ExecuteResult.PARAM_ERROR;
            string szField = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19}"
                 , ServerData.UserTable.ID
                 , ServerData.UserTable.Name
                 , ServerData.UserTable.School
                 , ServerData.UserTable.ExamSchool
                 , ServerData.UserTable.Sequence
                 , ServerData.UserTable.Tel
                 , ServerData.UserTable.Baks
                 , ServerData.UserTable.PassWord
                 , ServerData.UserTable.PayMoney
                 , ServerData.UserTable.ExamPlace
                 , ServerData.UserTable.Room
                 , ServerData.UserTable.Hotel
                 , ServerData.UserTable.HotelExpense
                 , ServerData.UserTable.MoneyBack
                 , ServerData.UserTable.EmployeeID
                 , ServerData.UserTable.Gender
                 , ServerData.UserTable.Template
                 , ServerData.UserTable.PayPlace
                 , ServerData.UserTable.ExceptRoomie
                 , ServerData.UserTable.Status);
            string szCondition = string.Format("1=1");
            szCondition = string.Format("{0} = '{1}' and {2}='{3}'"
                    , ServerData.UserTable.Tel, szTel
                    , ServerData.UserTable.PassWord, strPassWord);
            string szTable = ServerData.DataTable.Users;
            string szSQL = string.Format(ServerData.SQL.SELECT_WHERE, szField, szTable, szCondition);

            IDataReader dataReader = null;
            try
            {
                dataReader = base.DataAccess.ExecuteReader(szSQL, CommandType.Text);
                if (dataReader == null || dataReader.IsClosed || !dataReader.Read())
                {
                    //LogManager.Instance.WriteLog("EmployeeAccess.Exists", new string[] { "szSQL" }, new object[] { szSQL }, "未查询到记录");
                    return ExecuteResult.RES_NO_FOUND;
                }

                if (users == null)
                    users = new Users();
                users.ID = int.Parse(dataReader.GetValue(0).ToString());
                users.Name = dataReader.IsDBNull(1) ? "" : dataReader.GetString(1);
                users.School = dataReader.IsDBNull(2) ? "" : dataReader.GetString(2);
                users.ExamSchool = dataReader.IsDBNull(3) ? "" : dataReader.GetString(3);
                users.Sequence = dataReader.IsDBNull(4) ? "" : dataReader.GetValue(4).ToString();
                users.Tel = dataReader.IsDBNull(5) ? "" : dataReader.GetString(5).ToString();
                users.Bak = dataReader.IsDBNull(6) ? "" : dataReader.GetString(6);
                users.PassWord = dataReader.IsDBNull(7) ? "" : dataReader.GetString(7);
                users.PayMoney = dataReader.IsDBNull(8) ? "" : dataReader.GetString(8);
                users.ExamPlace = dataReader.IsDBNull(9) ? "" : dataReader.GetString(9);
                users.Room = dataReader.IsDBNull(10) ? "" : dataReader.GetString(10);
                users.Hotel = dataReader.IsDBNull(11) ? "" : dataReader.GetString(11);
                users.HotelExpense = dataReader.IsDBNull(12) ? "" : dataReader.GetString(12);
                users.MoneyBack = dataReader.IsDBNull(13) ? "" : dataReader.GetString(13);
                users.EmployeeID = dataReader.IsDBNull(14) ? "" : dataReader.GetValue(14).ToString();
                users.Gender = dataReader.IsDBNull(15) ? "" : dataReader.GetString(15).ToString();
                users.Template = dataReader.IsDBNull(16) ? "" : dataReader.GetString(16);
                users.PayPlace = dataReader.IsDBNull(17) ? "" : dataReader.GetString(17);
                users.ExceptRoomie = dataReader.IsDBNull(18) ? "" : dataReader.GetString(18);
                users.Status = dataReader.IsDBNull(19) ? "" : dataReader.GetString(19);
                return ExecuteResult.OK;
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), szSQL, "SQL语句执行失败!");
            }
            finally { base.DataAccess.CloseConnnection(false); }
        }
    }
}
