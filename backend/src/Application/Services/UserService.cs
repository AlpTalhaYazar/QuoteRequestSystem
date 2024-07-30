using QuoteRequestSystem.Application.Helper;
using QuoteRequestSystem.Core.Services;
using QuoteRequestSystem.Core.UnitOfWork;
using QuoteRequestSystem.Domain.Entities;

namespace QuoteRequestSystem.Application.Services;

public class UserService : GenericService<User>, IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _unitOfWork.UserRepository.GetByEmailAsync(email);
    }

    public async Task<User> GetByEmailAndPasswordAsync(string email, string password)
    {
        return await _unitOfWork.UserRepository.GetByEmailAndPasswordAsync(email,
            (await PasswordHasher.CreatePasswordHash(password)).hash);
    }

    public async Task<(int Id, string Email, string Role)> AuthenticateAsync(string email, string password)
    {
        var user = _unitOfWork.UserRepository.GetByEmailAsync(email).Result;

        if (user == null || !(await PasswordHasher.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)))
            return (0, null, null);

        return (user.Id, user.Email, user.Role.Name);
    }

    public async Task<(int UserId, string FirstName, string LastName, string Email, string Role)> RegisterNewUserAsync(
        string email, string password, string firstName, string lastName)
    {
        User user = new User();
        user.Email = email;
        (user.PasswordHash, user.PasswordSalt) = await PasswordHasher.CreatePasswordHash(password);
        user.FirstName = firstName;
        user.LastName = lastName;
        user.RoleId = 2;
        user.Role = await _unitOfWork.UserRoleRepository.GetByIdAsync(user.RoleId);
        user.LastSignedInAt = DateTime.Now;
        user.CreatedAt = DateTime.Now;
        user.UpdatedAt = DateTime.Now;

        await _unitOfWork.UserRepository.AddAsync(user);

        await _unitOfWork.SaveChangesAsync();

        return (user.Id, user.FirstName, user.LastName, user.Email, user.Role.Name);
    }
}