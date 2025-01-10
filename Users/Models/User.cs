using DigilizeTest.Common.Models;

namespace DigilizeTest.Users.Models;

public class User : EntityBase
{
    public string Name { get; set; }
	public string Surname { get; set; }
}
