using Repository.Entities;

namespace Repository.Interfaces;

public interface ILoginService
{
    public  Task<DTO_Token> Login(DTO__Login form);
}
