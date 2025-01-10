using System;
using DigilizeTest.Common.Database;
using DigilizeTest.Companies.Dto;
using DigilizeTest.Companies.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DigilizeTest.Companies;

public static class Endpoints
{
    public static void MapCompanyEndpoints(this IEndpointRouteBuilder routeBuilder)
        {
            var groupEndpointRouteBuilder = routeBuilder.MapGroup("api/companies");

            // GET: api/companies
            groupEndpointRouteBuilder.MapGet("", async (CompanyService companyService) =>
            {
                var companies = await companyService.GetCompanies();
                return Results.Ok(companies);
            });

            // GET: api/companies/{id}
            groupEndpointRouteBuilder.MapGet("{id:guid}", async (Guid id, CompanyService companyService) =>
            {
                var company = await companyService.GetCompanyById(id);
                if (company == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(company);
            });

            // POST: api/companies
            groupEndpointRouteBuilder.MapPost("", async ([FromBody]AddCompanyDto dto, CompanyService companyService) =>
            {
                var companyId = await companyService.InsertCompany(dto);
                return Results.Created($"/api/companies/{companyId}", companyId);
            });

            // PUT: api/companies/{id}
            groupEndpointRouteBuilder.MapPatch("{id:guid}", async (Guid id,[FromBody] UpdateCompanyAddressDto dto, CompanyService companyService) =>
            {
                var updatedCompanyId = await companyService.UpdateCompany(dto, id);
                if (updatedCompanyId == null)
                {
                    return Results.NotFound();
                }
                return Results.NoContent(); // Success but no content to return
            });

            // DELETE: api/companies/{id}
            groupEndpointRouteBuilder.MapDelete("{id:guid}", async (Guid id, CompanyService companyService) =>
            {
                var deletedCompanyId = await companyService.DeleteCompany(id);
                if (deletedCompanyId == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(deletedCompanyId); // Return the deleted id
            });
        }
}
