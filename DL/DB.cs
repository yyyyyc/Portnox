using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Models;
using System.Configuration;
namespace DL
{
    public class DB
    {
        public static List<Event> GetEvents()
        {
            List<Event> events = new List<Event>();
            
            string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();


            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetEvents";

                    //cmd.Parameters.Add(new SqlParameter("@s", SearchStr));

                    conn.Open();

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //res = res + reader["Event_ID"].ToString() + ","; // + reader["Name"].ToString() + " | ";
                            int id = (int)reader["ID"];
                            int event_id = (int)reader["Event_ID"];
                            DateTime event_dt = DateTime.Parse(reader["Event_DT"].ToString());
                            string switch_ip = reader["Switch_IP"].ToString();
                            int port_id = (int)reader["Port_Id"];
                            string device_mac = reader["Device_Mac"].ToString();
                            string remarks = reader["Remarks"].ToString();

                            Event ev = new Event(event_id, event_dt, switch_ip, port_id, device_mac, remarks);
                            events.Add(ev);
                        }
                    }

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }

            return events;
        }
    }
}
