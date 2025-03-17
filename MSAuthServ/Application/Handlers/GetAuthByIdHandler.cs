
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSAuthServ.Application.Interfaces;
using MSAuthServ.Domain;
using MSAuthServ.Application.Queries;

namespace MSAuthServ.Application.Handlers
{
    public class GetAuthByIdHandler : IRequestHandler<GetAuthByIdQuery, User>
    {
        private readonly IAuthRepository _repository;
        public GetAuthByIdHandler(IAuthRepository repository)
        {
            _repository = repository;
        }
        public async Task<User> Handle(GetAuthByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.UserId);
        }
    }
}