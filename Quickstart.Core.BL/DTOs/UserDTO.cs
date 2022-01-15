namespace Quickstart.Core.BL.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstMidName { get; set; }
        public string LastName { get; set; }

        public string FullName { get { return $"{FirstMidName} {LastName}"; } }
    }
}
