using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ServerNetworkMessage;


namespace Server
{
    static class Database
    {
        static SqlConnection conn = new SqlConnection("Server=mssql.fhict.local;"+
                                                      "Database=dbi359456;"+
                                                      "User Id=dbi359456;"+
                                                      "Password=supermario;");
        /*
        Connect to database
        */
        static bool Connect()
        {
            try
            {
                conn.Open();
            } catch
            {
                return false;
            }
            return true;
        }
        /*
        Generate random session string
        */
        static string GenerateRandomString(int length)
        {
            Random rand = new Random();
            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-={}[]:;<>,.?/|".ToCharArray();
            string randomString = "";
            for (int x = 0; x < length; x++)
            {
                randomString += chars[rand.Next(chars.Length - 1)];
            }
            return randomString;
        }
        /*
        Set session
        */
        static string Session(int id, NetworkMessage.Users user)
        {
            if (Connect())
            {
                string session = GenerateRandomString(50);
                string query;
                if (user == NetworkMessage.Users.Student)
                {
                    query = "INSERT INTO [StudentSession] (StudentId, SessionId) VALUES(@Id, (INSERT INTO [Session] (Session) OUTPUT inserted.Id VALUES(@Session)));";
                }
                else
                {
                    query = "INSERT INTO [TeacherSession] (TeacherId, SessionId) VALUES(@Id, (INSERT INTO [Session] (Session) OUTPUT inserted.Id VALUES(@Session)));";
                }
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Session", session);
                command.ExecuteNonQuery();
                conn.Close();
                return session;
            }
            return null;
        }
        /*
        Check session
        */
        static bool Session(int id, string session, NetworkMessage.Users user)
        {
            if(Connect())
            {
                string query;
                if(user == NetworkMessage.Users.Student)
                {
                    query = "SELECT COUNT(Student.Id) FROM [Student] LEFT JOIN [StudentSession] ON Student.Id = StudentSession.StudentId LEFT JOIN [Session] ON StudentSession.SessionId = Session.Id WHERE Student.Id = @Id AND Session.Session = @Session;";
                } else
                {
                    query = "SELECT COUNT(Teacher.Id) FROM [Teacher] LEFT JOIN [TeacherSession] ON Teacher.Id = TeacherSession.TeacherId LEFT JOIN [Session] ON TeacherSession.SessionId = Session.Id WHERE Teacher.Id = @Id AND Session.Session = @Session;";
                }
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Session", session);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read() && (int)reader[0] == 1)
                    {
                        conn.Close();
                        return true;
                    }
                }
                conn.Close();
            }
            return false;
        }
        static public string Login(NetworkMessage message)
        {
            if(message.Session != null && Session(message.UserId, message.Session, message.User))
            {
                return message.Session;
            }
            if (Connect())
            {
                string query;
                if (message.User == NetworkMessage.Users.Student)
                {
                    query = "SELECT COUNT(Id) FROM [Student] WHERE Id = @Id AND Password = @Password;";
                }
                else
                {
                    query = "SELECT COUNT(Id) FROM [Teacher] WHERE Id = @Id AND Password = @Password;";
                }
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Id", message.UserId);
                command.Parameters.AddWithValue("@Password", message.UserPassword);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read() && (int)reader[0] == 1)
                    {
                        conn.Close();
                        return Session(message.UserId, message.User);
                    }
                }
                conn.Close();
            }
            return null;
        }
        static public int Submit(NetworkMessage message)
        {
            if (Connect())
            {
                string query = "INSERT INTO [Submission] OUTPUT inserted.Id (StudentId) VALUES(@Id)";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Id", message.UserId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        conn.Close();
                        return (int)reader[0];
                    }
                }
                conn.Close();
            }
            return 0;
        }
    }
}
