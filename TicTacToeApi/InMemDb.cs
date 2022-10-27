using Microsoft.EntityFrameworkCore;

public class InMemDb : DbContext
{
	public InMemDb(DbContextOptions<InMemDb> options)
		: base(options)
	{
	}
    public string[]? BoardState { get; set; }
}