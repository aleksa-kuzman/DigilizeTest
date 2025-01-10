using DigilizeTest.Common.Models;

namespace DigilizeTest.Companies.Models;

public class Company : EntityBase
{
    public string CompanyName { get; set; }
	public string Address { get; set; }
}
