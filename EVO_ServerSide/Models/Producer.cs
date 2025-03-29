//using EVO_ServerSide.DAL;


namespace EVO_ServerSide.Models
{
    public class Producer
    {
        int account_ID;
        string first_name;
        string last_name;
        string phone_number;
        string email;
        string password;
        string photo;
        DateTime registration_date;
        DateOnly birth_date;
        string city;

        public Producer(int account_ID, 
                        string first_name, 
                        string last_name, 
                        string phone_number, 
                        string email, 
                        string password, 
                        string photo, 
                        DateTime registration_date, 
                        DateOnly birth_date, 
                        string city)
        {
            this.Account_ID = account_ID;
            this.First_name = first_name;
            this.Last_name = last_name;
            this.Phone_number = phone_number;
            this.Email = email;
            this.Password = password;
            this.Photo = photo;
            this.Registration_date = registration_date;
            this.Birth_date = birth_date;
            this.City = city;
        }

        public int Account_ID { get => account_ID; set => account_ID = value; }
        public string First_name { get => first_name; set => first_name = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public string Phone_number { get => phone_number; set => phone_number = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Photo { get => photo; set => photo = value; }
        public DateTime Registration_date { get => registration_date; set => registration_date = value; }
        public DateOnly Birth_date { get => birth_date; set => birth_date = value; }
        public string City { get => city; set => city = value; }

        public int InsertNewUser()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertUser(this);
        }
    }
}
