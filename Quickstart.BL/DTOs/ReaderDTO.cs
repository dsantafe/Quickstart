using Quickstart.BL.Interfaces;

namespace Quickstart.BL.DTOs
{
    public class ReaderDTO : IPerson
    {
        public int ReaderId { get; set; }
        public string FirstMidName { get; set; }
        public string UserName { get; set; }
    }
}
