using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.SqlClient;
using System.Data;
using System.Text;
using EVO_ServerSide.Models;
using System.Xml.Linq;
using System.Reflection.PortableExecutable;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using String = System.String;


//namespace EVO_ServerSide.DAL
//{
    public class DBservices
    {

        //------------------------------------------------------------------------------------------------------------------------
        // METHODS FOR DATA BASE SERVICES:
        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {
            // read the connection string from the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString("myProjDB");
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
        //---------------------------------------------------------------------------------
        // This method creates the SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateCommandWithStoredProcedureGeneral(String spName, SqlConnection con, Dictionary<string, object> paramDic)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            if (paramDic != null)
                foreach (KeyValuePair<string, object> param in paramDic)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }

            return cmd;
        }
        //------------------------------------------------------------------------------------------------------------------------


        //// METHODS FOR -PRODUCER- DATA READ&WRITE:

        //--------------------------------------------------------------------------------------------------
        // This method insert a USER to the users table 
        //--------------------------------------------------------------------------------------------------
        public int InsertUser(Producer newProducer)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@firstName", newProducer.First_name);
            paramDic.Add("@lastName", newProducer.Last_name);
            paramDic.Add("@phoneNumber", newProducer.Phone_number);
            paramDic.Add("@email", newProducer.Email);
            paramDic.Add("@password", newProducer.Password);
            paramDic.Add("@photo", newProducer.Photo);
            paramDic.Add("@registrationDate", newProducer.Registration_date);
            paramDic.Add("@birthDate", newProducer.Birth_date);
            paramDic.Add("@city", newProducer.City);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_RegistraionProducer", con, paramDic);       // create the command

            try
            {
                int newProducerID = (int)cmd.ExecuteScalar();
                return newProducerID;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }//check

        //--------------------------------------------------------------------------------------------------
        // This method update a USER to the user table 
        //--------------------------------------------------------------------------------------------------
        //public User UpdateUser(User user)
        //{

        //    SqlConnection con;
        //    SqlCommand cmd;

        //    try
        //    {
        //        con = connect("myProjDB"); // create the connection
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    Dictionary<string, object> paramDic = new Dictionary<string, object>();
        //    paramDic.Add("@id", user.Id);
        //    paramDic.Add("@userName", user.Name);
        //    paramDic.Add("@email", user.Email);
        //    paramDic.Add("@password", user.Password);
        //    cmd = CreateCommandWithStoredProcedureGeneral("ChangeUsersDetails", con, paramDic);

        //    try
        //    {
        //        int numEffected = cmd.ExecuteNonQuery(); // execute the command
        //        return user;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    finally
        //    {
        //        if (con != null)
        //        {
        //            // close the db connection
        //            con.Close();
        //        }
        //    }

        //}

        //--------------------------------------------------------------------------------------------------
        // This method insert a USER to the users table 
        //--------------------------------------------------------------------------------------------------
        //public User LogInUserCheck(User user)
        //{

        //    SqlConnection con;
        //    SqlCommand cmd;

        //    try
        //    {
        //        con = connect("myProjDB"); // create the connection
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    Dictionary<string, object> paramDic = new Dictionary<string, object>();
        //    paramDic.Add("@email", user.Email);
        //    paramDic.Add("@password", user.Password);

        //    cmd = CreateCommandWithStoredProcedureGeneral("SP_DetailsCheckForLogIn", con, paramDic);       // create the command

        //    User u = new User();

        //    try
        //    {
        //        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        //        if (dataReader.Read()) //true - חזרו רשומות מהפרוצדורה
        //        {
        //            u.Id = Convert.ToInt32(dataReader["id"]);
        //            u.Name = dataReader["userName"].ToString();
        //            u.Email = dataReader["email"].ToString();
        //            u.Password = dataReader["user_password"].ToString();
        //            u.IsActive = Convert.ToBoolean(dataReader["isActive"]);
        //        }
        //        else // false - לא חזרו רשומות מהפרוצדורה
        //        {
        //            return null; // there no rows returned from the SP, means the details log in are NOT correct 
        //        }
        //        return u;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    finally
        //    {
        //        if (con != null)
        //        {
        //            // close the db connection
        //            con.Close();
        //        }
        //    }

        //}

        //--------------------------------------------------------------------------------------------------
        // This method add a GAME to the GamesUser table 
        //--------------------------------------------------------------------------------------------------
        //public int AddGameForUser(int gameID, int userID)
        //{

        //    SqlConnection con;
        //    SqlCommand cmd;

        //    try
        //    {
        //        con = connect("myProjDB"); // create the connection
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    Dictionary<string, object> paramDic = new Dictionary<string, object>();
        //    paramDic.Add("@GameID", gameID);
        //    paramDic.Add("@UserID", userID);

        //    cmd = CreateCommandWithStoredProcedureGeneral("PurchaseGame", con, paramDic);       // create the command

        //    try
        //    {
        //        int numEffected = cmd.ExecuteNonQuery(); // return num of rows effected
        //        return numEffected;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    finally
        //    {
        //        if (con != null)
        //        {
        //            // close the db connection
        //            con.Close();
        //        }
        //    }

        //}

        //--------------------------------------------------------------------------------------------------
        // This method delete a GAME from the GamesUser table 
        //--------------------------------------------------------------------------------------------------
        //public int DeleteGame(int gameID, int userID)
        //{

        //    SqlConnection con;
        //    SqlCommand cmd;

        //    try
        //    {
        //        con = connect("myProjDB"); // create the connection
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    Dictionary<string, object> paramDic = new Dictionary<string, object>();
        //    paramDic.Add("@GameID", gameID);
        //    paramDic.Add("@UserID", userID);

        //    cmd = CreateCommandWithStoredProcedureGeneral("DeleteGamePurchase", con, paramDic);       // create the command

        //    try
        //    {
        //        int numEffected = cmd.ExecuteNonQuery(); // return num of rows effected
        //        return numEffected;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    finally
        //    {
        //        if (con != null)
        //        {
        //            // close the db connection
        //            con.Close();
        //        }
        //    }

        //}

        //---------------------------------------------------------------------------------
        // This method read GAMES from the Games table 
        //---------------------------------------------------------------------------------
        //public List<Game> ReadAllGames()
        //{
        //    SqlConnection con;
        //    SqlCommand cmd;

        //    try
        //    {
        //        con = connect("myProjDB"); // create the connection
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    List<Game> gamesList = new List<Game>();

        //    cmd = CreateCommandWithStoredProcedureGeneral("SP_ReadGames", con, null);

        //    try
        //    {

        //        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        //        while (dataReader.Read())
        //        {
        //            Game g = new Game();

        //            g.AppID = Convert.ToInt32(dataReader["AppID"]);
        //            g.Name = dataReader["Name"].ToString();
        //            g.ReleaseDate = dataReader["Release_date"].ToString();
        //            g.Price = Convert.ToSingle(dataReader["Price"]);
        //            g.Description = dataReader["description"].ToString();
        //            g.HeaderImage = dataReader["Header_image"].ToString();
        //            g.Website = dataReader["Website"].ToString();
        //            g.Windows = Convert.ToBoolean(dataReader["Windows"]);
        //            g.Mac = Convert.ToBoolean(dataReader["Mac"]);
        //            g.Linux = Convert.ToBoolean(dataReader["Linux"]);
        //            g.ScoreRank = Convert.ToInt32(dataReader["Score_rank"]);
        //            g.Recommendation = dataReader["Recommendations"].ToString();
        //            g.Publisher = dataReader["Developers"].ToString();
        //            g.NumberOfPurchases = Convert.ToInt32(dataReader["NumberOfPurchases"]);

        //            gamesList.Add(g);
        //        }
        //        return gamesList;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            // close the db connection
        //            con.Close();
        //        }
        //    }
        //}

        //---------------------------------------------------------------------------------
        // This method read USERS GAMES (WISH LIST)
        //---------------------------------------------------------------------------------
        //public List<Game> ReadUserGames(int userId)
        //{
        //    SqlConnection con;
        //    SqlCommand cmd;

        //    try
        //    {
        //        con = connect("myProjDB"); // create the connection
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    List<Game> gamesList = new List<Game>();

        //    Dictionary<string, object> paramDic = new Dictionary<string, object>();
        //    paramDic.Add("@UserID", userId);

        //    cmd = CreateCommandWithStoredProcedureGeneral("SP_ReadUserGamesByUserID", con, paramDic);

        //    try
        //    {

        //        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        //        while (dataReader.Read())
        //        {
        //            Game g = new Game();

        //            g.AppID = Convert.ToInt32(dataReader["AppID"]); ;
        //            g.Name = dataReader["Name"].ToString();
        //            g.ReleaseDate = dataReader["Release_date"].ToString();
        //            g.Price = Convert.ToSingle(dataReader["Price"]);
        //            g.Description = dataReader["description"].ToString();
        //            g.HeaderImage = dataReader["Header_image"].ToString();
        //            g.Website = dataReader["Website"].ToString();
        //            g.Windows = Convert.ToBoolean(dataReader["Windows"]);
        //            g.Mac = Convert.ToBoolean(dataReader["Mac"]);
        //            g.Linux = Convert.ToBoolean(dataReader["Linux"]);
        //            g.ScoreRank = Convert.ToInt32(dataReader["Score_rank"]);
        //            g.Recommendation = dataReader["Recommendations"].ToString();
        //            g.Publisher = dataReader["Developers"].ToString();
        //            g.NumberOfPurchases = Convert.ToInt32(dataReader["NumberOfPurchases"]);

        //            gamesList.Add(g);
        //        }
        //        return gamesList;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            // close the db connection
        //            con.Close();
        //        }
        //    }
        //}

        //---------------------------------------------------------------------------------
        // This method read GAMES by min PRICE from the Games table 
        //---------------------------------------------------------------------------------
        //public List<Game> ReadGamesAbovePrice(float minPrice, int userId)
        //{

        //    SqlConnection con;
        //    SqlCommand cmd;

        //    try
        //    {
        //        con = connect("myProjDB"); // create the connection
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    List<Game> GamesListByPrice = new List<Game>();

        //    Dictionary<string, object> paramDic = new Dictionary<string, object>();
        //    paramDic.Add("@minPrice", minPrice);
        //    paramDic.Add("@UserID", userId);

        //    cmd = CreateCommandWithStoredProcedureGeneral("SP_ReadGamesAbovePrice", con, paramDic);

        //    try
        //    {
        //        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        //        while (dataReader.Read())
        //        {
        //            Game g = new Game();

        //            g.AppID = Convert.ToInt32(dataReader["AppID"]);
        //            g.Name = dataReader["Name"].ToString();
        //            g.ReleaseDate = dataReader["Release_date"].ToString();
        //            g.Price = Convert.ToSingle(dataReader["Price"]);
        //            g.Description = dataReader["description"].ToString();
        //            g.HeaderImage = dataReader["Header_image"].ToString();
        //            g.Website = dataReader["Website"].ToString();
        //            g.Windows = Convert.ToBoolean(dataReader["Windows"]);
        //            g.Mac = Convert.ToBoolean(dataReader["Mac"]);
        //            g.Linux = Convert.ToBoolean(dataReader["Linux"]);
        //            g.ScoreRank = Convert.ToInt32(dataReader["Score_rank"]);
        //            g.Recommendation = dataReader["Recommendations"].ToString();
        //            g.Publisher = dataReader["Developers"].ToString();
        //            g.NumberOfPurchases = Convert.ToInt32(dataReader["NumberOfPurchases"]);

        //            GamesListByPrice.Add(g);
        //        }
        //        return GamesListByPrice;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            // close the db connection
        //            con.Close();
        //        }
        //    }


        //}

        //---------------------------------------------------------------------------------
        // This method read GAMES by min SCORE from the Games table 
        //---------------------------------------------------------------------------------
        //public List<Game> ReadGamesAboveScore(int minScore, int userId)
        //{

        //    SqlConnection con;
        //    SqlCommand cmd;

        //    try
        //    {
        //        con = connect("myProjDB"); // create the connection
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    List<Game> GamesListByScore = new List<Game>();

        //    Dictionary<string, object> paramDic = new Dictionary<string, object>();
        //    paramDic.Add("@minScore", minScore);
        //    paramDic.Add("@UserID", userId);

        //    cmd = CreateCommandWithStoredProcedureGeneral("SP_ReadGamesAboveScore", con, paramDic);

        //    try
        //    {

        //        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        //        while (dataReader.Read())
        //        {
        //            Game g = new Game();

        //            g.AppID = Convert.ToInt32(dataReader["AppID"]);
        //            g.Name = dataReader["Name"].ToString();
        //            g.ReleaseDate = dataReader["Release_date"].ToString();
        //            g.Price = Convert.ToSingle(dataReader["Price"]);
        //            g.Description = dataReader["description"].ToString();
        //            g.HeaderImage = dataReader["Header_image"].ToString();
        //            g.Website = dataReader["Website"].ToString();
        //            g.Windows = Convert.ToBoolean(dataReader["Windows"]);
        //            g.Mac = Convert.ToBoolean(dataReader["Mac"]);
        //            g.Linux = Convert.ToBoolean(dataReader["Linux"]);
        //            g.ScoreRank = Convert.ToInt32(dataReader["Score_rank"]);
        //            g.Recommendation = dataReader["Recommendations"].ToString();
        //            g.Publisher = dataReader["Developers"].ToString();
        //            g.NumberOfPurchases = Convert.ToInt32(dataReader["NumberOfPurchases"]);

        //            GamesListByScore.Add(g);
        //        }
        //        return GamesListByScore;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            // close the db connection
        //            con.Close();
        //        }
        //    }


        //}

        //---------------------------------------------------------------------------------
        // This method read the USERS DATA for the admin page
        //---------------------------------------------------------------------------------
        //public List<Object> ReadUsersData()
        //{
        //    SqlConnection con;
        //    SqlCommand cmd;

        //    try
        //    {
        //        con = connect("myProjDB"); // create the connection
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    List<Object> usersList = new List<Object>();

        //    //Dictionary<string, object> paramDic = new Dictionary<string, object>();
        //    //paramDic.Add("@UserID", userId);

        //    cmd = CreateCommandWithStoredProcedureGeneral("SP_userInformation", con, null);

        //    try
        //    {

        //        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        //        while (dataReader.Read())
        //        {
        //            usersList.Add(new
        //            {
        //                id = Convert.ToInt32(dataReader["id"]),
        //                userName = dataReader["userName"].ToString(),
        //                gamesPurchased = Convert.ToInt32(dataReader["NumberOfGamesBought"]),
        //                moneySpent = Convert.ToDouble(dataReader["TotalAmountSpent"]),
        //                activeStatus = Convert.ToBoolean(dataReader["isActive"])
        //            });
        //        }
        //        return usersList;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            // close the db connection
        //            con.Close();
        //        }
        //    }
        //}


        //---------------------------------------------------------------------------------
        // This method read the GAMES DATA for the admin page
        //---------------------------------------------------------------------------------

        //public List<Object> ReadGamesData()
        //{
        //    SqlConnection con;
        //    SqlCommand cmd;

        //    try
        //    {
        //        con = connect("myProjDB"); // create the connection
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    List<Object> gamesList = new List<Object>();

        //    cmd = CreateCommandWithStoredProcedureGeneral("SP_gameInformation", con, null);

        //    try
        //    {

        //        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        //        while (dataReader.Read())
        //        {
        //            gamesList.Add(new
        //            {
        //                gameId = Convert.ToInt32(dataReader["GameID"]),
        //                title = dataReader["GameName"].ToString(),
        //                downloads = Convert.ToInt32(dataReader["NumberOfDownloads"]),
        //                totalRevenue = Convert.ToDouble(dataReader["TotalAmountPaid"]),
        //            });
        //        }
        //        return gamesList;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            // close the db connection
        //            con.Close();
        //        }
        //    }
        //}


    }
//}
