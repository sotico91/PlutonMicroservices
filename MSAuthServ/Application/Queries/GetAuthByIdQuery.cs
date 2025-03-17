using MediatR;
using MSAuthServ.Domain;

namespace MSAuthServ.Application.Queries
{
	public class GetAuthByIdQuery : IRequest<User>
    {
        public int UserId { get; set; }
        public bool IncludeRoles { get; set; }

        public GetAuthByIdQuery(int userId, bool includeRoles = false)
        {
            UserId = userId;
            IncludeRoles = includeRoles;
        }
    }
}