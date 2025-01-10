using System;
using DigilizeTest.Users.Dto;
using DigilizeTest.Users.Models;

namespace DigilizeTest.Users;

public static class Endpoints
{

     public static void MapUserEndpoints(this IEndpointRouteBuilder routeBuilder)
        {
            var groupEndpointRouteBuilder = routeBuilder.MapGroup("api/users");

            // GET: api/users
            groupEndpointRouteBuilder.MapGet("", async (UserService userService) =>
            {
                var users = await userService.GetUsers();
                return Results.Ok(users);
            });

            // GET: api/users/{id}
            groupEndpointRouteBuilder.MapGet("{id:guid}", async (Guid id, UserService userService) =>
            {
                var user = await userService.GetUserById(id);
                if (user == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(user);
            });

            // POST: api/users
            groupEndpointRouteBuilder.MapPost("", async (AddUserDto dto, UserService userService) =>
            {
                var userId = await userService.InsertUser(dto);
                return Results.Created($"/api/users/{userId}", userId);
            });

            // PUT: api/users/{id}
            groupEndpointRouteBuilder.MapPut("{id:guid}", async (Guid id, UpdateUserDto dto, UserService userService) =>
            {
                var updatedUserId = await userService.UpdateUser(dto, id);
                if (updatedUserId == null)
                {
                    return Results.NotFound();
                }
                return Results.NoContent(); // Success but no content to return
            });

            // DELETE: api/users/{id}
            groupEndpointRouteBuilder.MapDelete("{id:guid}", async (Guid id, UserService userService) =>
            {
                var deletedUserId = await userService.DeleteUser(id);
                if (deletedUserId == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(deletedUserId); // Return the deleted id
            });


            /// POST: api/users/valid-list
            /// Wanted to add some more stuff to show what else can be done
            groupEndpointRouteBuilder.MapPost("valid-list", async (List<User> users, UserService userService) =>
            {
                var userIds = await userService.InsertValidUserList(users);
                return Results.Created("api/users/valid-list", userIds);
            })
            .WithName("InsertValidUserList")
            .WithSummary("Inserts a list of valid users")
            .WithDescription("Filters the input list of users, validates them, and inserts only the valid users into the database.")
            .Produces<List<Guid>>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Accepts<List<User>>("application/json");
        }
}
