using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamGroupAdminPro.DAL
{
    internal class DataAccess
    {

        SqlConnection sqlconnection;
        //public DataAccess() => sqlconnection = new SqlConnection("Data Source=192.168.0.10; Database=CompanyDB; User ID=aroo; Password=12345;Integrated Security=False; Pooling=False;");
        public DataAccess() => sqlconnection = new SqlConnection("Data Source=.; Database=WarehouseDB;Integrated Security=True;Pooling=False");
        //open connection
        public void open()
        {
            if (sqlconnection.State != ConnectionState.Open)
            {
                sqlconnection.Open();
            }

        }

        //cloos
        public void close()
        {
            if (sqlconnection.State == ConnectionState.Open)
            {
                sqlconnection.Close();
            }
        }
        //method to red data from tabel
        public DataTable selectdata(string stored_procedur, SqlParameter[] param)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = stored_procedur;
            sqlcmd.Connection = sqlconnection;
            if (param != null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    sqlcmd.Parameters.Add(param[i]);

                }
            }

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        //methhot to updat ,delet, insetr
        public void execute(string stor_procedur, SqlParameter[] param)
        {


            SqlCommand sqlcmd = new SqlCommand();

            sqlcmd.CommandType = CommandType.StoredProcedure;

            sqlcmd.CommandText = stor_procedur;
            sqlcmd.Connection = sqlconnection;

            if (param != null)
            {
                sqlcmd.Parameters.AddRange(param);
            }
            open();
            try
            {
                sqlcmd.ExecuteNonQuery();
                close();
                open();
                close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
        }













    }
}
