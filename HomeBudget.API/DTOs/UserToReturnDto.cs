using System.Collections.Generic;

namespace HomeBudget.API.DTOs
{
    public class UserToReturnDto
    {
        public int Id { get; set; }
        public string Username { get; set;}
        public string Email { get; set; }
        public ICollection<OutgoingForReturnDto> Outgoings { get; set; }
    }
}
