namespace Membership.UserManagerService.AspNetIdentity.DataContext;
internal class UserManagerServiceContext : IdentityDbContext<User>
{
    readonly AspNetIdentityOptions _options;

    public UserManagerServiceContext(IOptions<AspNetIdentityOptions> options)
    {
        _options = options.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_options.ConnectionString);
        base.OnConfiguring(optionsBuilder);
    }
}
