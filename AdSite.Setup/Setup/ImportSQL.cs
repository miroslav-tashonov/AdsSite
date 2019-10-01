using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;

namespace Setup
{
    public static class ImportSQL
    {
        public static void ImportSQLScripts(string conString, string cloneRepoLocation)
        {
            string[] fileScripts = new string[] { "1.Create_database.sql", "2.Import_script.sql", "3.Import_Roles.sql" };

            string connectionString = conString;
            string filepath = cloneRepoLocation + "\\AdSite.Data\\Scripts\\";
            try
            {
                foreach (string fileScript in fileScripts)
                {
                    if (File.Exists(filepath + fileScript))
                    {
                        FileInfo file = new FileInfo(filepath + fileScript);
                        string script = file.OpenText().ReadToEnd();
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            Server server = new Server(new ServerConnection(conn));

                            server.ConnectionContext.ExecuteNonQuery(script);
                        }
                        file.OpenText().Close();
                    }
                }
                System.Windows.MessageBox.Show("Import success ! ");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message + "" + ex.StackTrace);
            }
        }

        public static void ImportAdminCredentials(string conString, string username, string password)
        {
            try
            {
                Guid usernameGuid = Guid.NewGuid();

                string insertAdminQuery = "Use AdSite; INSERT [adsite].[AspNetUsers]([Id], [AccessFailedCount], [ConcurrencyStamp],[Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail],[NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed],[SecurityStamp], [TwoFactorEnabled], [UserName])";
                insertAdminQuery += " VALUES (@Id, @AccessFailedCount, @ConcurrencyStamp, @Email, @EmailConfirmed, @LockoutEnabled, @LockoutEnd, @NormalizedEmail, @NormalizedUserName, @PasswordHash, @PhoneNumber, @PhoneNumberConfirmed, @SecurityStamp, @TwoFactorEnabled, @UserName)";


                using (SqlConnection dataConnection = new SqlConnection(conString))
                {
                    using (SqlCommand dataCommand = new SqlCommand(insertAdminQuery, dataConnection))
                    {
                        dataCommand.Parameters.AddWithValue("Id", usernameGuid);
                        dataCommand.Parameters.AddWithValue("AccessFailedCount", 0);
                        dataCommand.Parameters.AddWithValue("ConcurrencyStamp", Guid.NewGuid());
                        dataCommand.Parameters.AddWithValue("Email", username);
                        dataCommand.Parameters.AddWithValue("EmailConfirmed", 1);
                        dataCommand.Parameters.AddWithValue("LockoutEnabled", 1);
                        dataCommand.Parameters.AddWithValue("LockoutEnd", DBNull.Value);
                        dataCommand.Parameters.AddWithValue("NormalizedEmail", username.ToUpper());
                        dataCommand.Parameters.AddWithValue("NormalizedUserName", username.ToUpper());
                        dataCommand.Parameters.AddWithValue("PasswordHash", HashPassword(password));
                        dataCommand.Parameters.AddWithValue("PhoneNumber", String.Empty);
                        dataCommand.Parameters.AddWithValue("PhoneNumberConfirmed", 1);
                        dataCommand.Parameters.AddWithValue("SecurityStamp", Guid.NewGuid());
                        dataCommand.Parameters.AddWithValue("TwoFactorEnabled", 0);
                        dataCommand.Parameters.AddWithValue("UserName", username);

                        dataConnection.Open();
                        dataCommand.ExecuteNonQuery();
                        dataConnection.Close();
                    }
                }


                string insertUserToAdminRoleQuery = "Use AdSite;  INSERT [adsite].[AspNetUserRoles] ([UserId], [RoleId])";
                insertUserToAdminRoleQuery += " VALUES (@UserId, @RoleId) ";


                using (SqlConnection dataConnection = new SqlConnection(conString))
                {
                    using (SqlCommand dataCommand = new SqlCommand(insertUserToAdminRoleQuery, dataConnection))
                    {
                        dataCommand.Parameters.AddWithValue("UserId", usernameGuid);
                        dataCommand.Parameters.AddWithValue("RoleId", new Guid("19151b2a-2a3e-4b55-a54b-835489bf5e42"));


                        dataConnection.Open();
                        dataCommand.ExecuteNonQuery();
                        dataConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message + "" + ex.StackTrace);
            }
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

    }
}
