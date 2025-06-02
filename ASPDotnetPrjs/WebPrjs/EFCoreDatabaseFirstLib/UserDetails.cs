using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreDatabaseFirstLib
{
    //[Table("tbl_user")]
    public class UserDetails
    {
        public int Id { get; set; }
        public string UserName {  get; set; }
        public string Password { get; set; }
    }
}
