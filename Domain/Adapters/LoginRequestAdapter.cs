using Domain.Commands.Account.Login;
using Mapster;

namespace Domain.Adapters
{
    public sealed class LoginRequestAdapter : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<LoginRequest, LoginRequestDto>()
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.Password);
        }
    }
}