using Quickstart.BL.Interfaces;

namespace Quickstart.BL.DTOs
{
    public class AuthorDTO : IPerson
    {
        public int AuthorId { get; set; }
        public string FirstMidName { get; set; }
        public string Biography { get; set; }
    }
}
