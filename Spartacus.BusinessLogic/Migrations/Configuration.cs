namespace Spartacus.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Spartacus.BusinessLogic.DBModel.CatContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Spartacus.BusinessLogic.DBModel.CatContext";
        }

        protected override void Seed(Spartacus.BusinessLogic.DBModel.CatContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
