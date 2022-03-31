using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;


namespace LoginRegister_mit_PHP
{
    public partial class Form1 : Form
    {

        private MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;
        public Form1()
        {
            server = "localhost";
            database = "users";
            uid = "root";
            password = "";

            string connString;
            connString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            conn = new MySqlConnection(connString);
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string userpassword = txtPassword.Text;
            string password = LoginEncrpyt.HashString(userpassword);

            if (Register(username, password))
            {
                MessageBox.Show($"User {username} has been created!");
            }
            else
            {
                MessageBox.Show($"User {username} has not been created!");
            }
        }

        public bool Register(string username, string password)
        {
            //string password = LoginEncrpyt.HashString(userpassword);
            string querry = $"INSERT INTO Accounts (id, username, password) VALUES ('', '{username}', '{password}');";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(querry, conn);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;

                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }

        }
        public bool IsLogin(string username, string password)

        {

            string querry = $"SELECT * FROM Accounts WHERE username= '{username}' AND password='{password}';";
            

            try
            {
                if (OpenConnection())

                {
                   
                        MySqlCommand cmd = new MySqlCommand(querry, conn);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            reader.Close();
                            conn.Close();
                            return true;
                        }
                        else
                        {
                            reader.Close();
                            conn.Close();
                            return false;

                        }
                    
                   
                }
                else
                {
                    conn.Close();
                    return false;
                }
            } catch (Exception ex)
            {
                conn.Close();
                return false;

            }


        }
        private bool OpenConnection()
        {
            try
            {
                conn.Open();
                return true;

            }
            catch (MySqlException ex) {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Connection Error");
                        break;
                    case 1045:
                        MessageBox.Show("Server username or password is incorrect!");
                        break;
                }
                return false;

            }
        }
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = LoginEncrpyt.HashString(txtPassword.Text);
          
           
           

            if (IsLogin(username, password))
            {
                MessageBox.Show($"Welcome {username} !");
            }
            else
            {
                MessageBox.Show($" {username} does not exist or password is wrong!");
            }

        }

    
    }
}
