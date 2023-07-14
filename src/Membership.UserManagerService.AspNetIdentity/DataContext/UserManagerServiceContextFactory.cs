namespace Membership.UserManagerService.AspNetIdentity.DataContext;
internal class UserManagerServiceContextFactory : IDesignTimeDbContextFactory<UserManagerServiceContext>
{
    public UserManagerServiceContext CreateDbContext(string[] args)
    {
        string connectionString = args.Length == 0 ? "" : args[0];

        if(args.Length == 0)
        {
            Console.Write("**********************************************");
            Console.WriteLine("Enter connection string when update database");
            Console.Write("Example: Update.Database ... -Args ");
            Console.Write("**********************************************");
        }

        return new(
            Microsoft.Extensions.Options.Options
            .Create(new AspNetIdentityOptions { ConnectionString = connectionString }));
        
    }
}
