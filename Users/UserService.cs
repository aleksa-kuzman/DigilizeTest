using System;
using DigilizeTest.Common.Database;
using DigilizeTest.Users.Dto;
using DigilizeTest.Users.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DigilizeTest.Users;

public class UserService
{
    private readonly AppDbContext _context;
    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserDto>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();

        var dtos = users.Adapt<List<UserDto>>();

        return dtos;
    }

    public async Task<UserDto> GetUserById(Guid id)
    {
        var users = await _context.Users.Where(m => m.Id == id).FirstOrDefaultAsync();

        var dto = users.Adapt<UserDto>();

        return dto;
    }

    public async Task<Guid> InsertUser(AddUserDto dto)
    {
        var newCompany = dto.Adapt<User>();

        var entity = await _context.Users.AddAsync(newCompany);

        await _context.SaveChangesAsync();

        return entity.Entity.Id;
    }



    public async Task<Guid?> UpdateUser(UpdateUserDto dto, Guid id)
    {
        var entityToUpdate = await _context.Users.Where(m => m.Id == id).FirstOrDefaultAsync();

        if (entityToUpdate == null)
        {

            return null;

        }
        entityToUpdate.Name = dto.Name;
        entityToUpdate.Surname = dto.Surname;

        await _context.SaveChangesAsync();

        return id;
    }

    public async Task<Guid?> DeleteUser(Guid id)
    {
        var toBeDeleted = await _context.Users.Where(m => m.Id == id).FirstOrDefaultAsync();

        if (toBeDeleted == null)
        {
            return null;
        }

        _context.Users.Remove(toBeDeleted);

        await _context.SaveChangesAsync();

        return id;
    }
}