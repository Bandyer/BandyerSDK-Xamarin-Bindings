using System;
namespace BandyerDemo.Models
{
    public class User
    {
        public String Alias { get; set; }
        public String NickName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String ImageUri { get; set; }
        public bool Selected { get; set; } = false;
    }
}
