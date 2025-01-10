using System;
using DigilizeTest.Common.Database;
using DigilizeTest.Companies.Dto;
using DigilizeTest.Companies.Models;
using DigilizeTest.Users.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DigilizeTest.Companies;

public class CompanyService
{
    private readonly AppDbContext _context;
    public CompanyService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<CompanyDto>> GetCompanies()
    {
        var companies = await _context.Companies.ToListAsync();

        var dtos = companies.Adapt<List<CompanyDto>>();

        return dtos;
    }

    public async Task<CompanyDto> GetCompanyById(Guid id)
    {
        var companies = await _context.Companies.Where(m => m.Id == id).FirstOrDefaultAsync();

        var dto = companies.Adapt<CompanyDto>();

        return dto;
    }

    public async Task<Guid> InsertCompany(AddCompanyDto dto)
    {
        var newCompany = dto.Adapt<Company>();

        var entity = await _context.Companies.AddAsync(newCompany);

        await _context.SaveChangesAsync();

        return entity.Entity.Id;
    }

    public async Task<Guid?> UpdateCompany(UpdateCompanyAddressDto dto, Guid id)
    {
        var entityToUpdate = await _context.Companies.Where(m => m.Id == id).FirstOrDefaultAsync();

        if (entityToUpdate == null)
        {

            return null;


        }
        entityToUpdate.CompanyName = dto.CompanyName;

        await _context.SaveChangesAsync();

        return entityToUpdate.Id;

    }

    public async Task<Guid?> DeleteCompany(Guid id)
    {
        var toBeDeleted = await _context.Companies.Where(m => m.Id == id).FirstOrDefaultAsync();

        if (toBeDeleted == null)
        {
            return null;
        }

        _context.Companies.Remove(toBeDeleted);

        await _context.SaveChangesAsync();

        return id;
    }
}
